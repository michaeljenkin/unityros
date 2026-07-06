using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using SimpleJSON;
using UnityEngine;

/**
 * This class handles the connection with the external ROS world, deserializing
 * json messages into appropriate instances of packets and messages.
 * 
 * This class also provides a mechanism for having the callback's exectued on the rendering thread.
 * (Remember, Unity has a single rendering thread, so we want to do all of the communications stuff away
 * from that. 
 * 
 * The one other clever thing that is done here is that we only keep 1 (the most recent!) copy of each message type
 * that comes along.
 * 
 * Version History
 * 4.0 - 
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Marylou DUBOIS
 * @version 4.0
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.1
 */

namespace ROSBridgeLib {
	public class ROSBridgeWebSocketConnection {
		private class RenderTask {
			private Type _subscriber;
			private string _topic;
			private ROSBridgeMsg _msg;

			public RenderTask(Type subscriber, string topic, ROSBridgeMsg msg) {
				_subscriber = subscriber;
				_topic = topic;
				_msg = msg;
			}

			public Type getSubscriber() {
				return _subscriber;
			}

			public ROSBridgeMsg getMsg() {
				return _msg;
			}

			public string getTopic() {
				return _topic;
			}
		};
		private string _host;
		private int _port;
		private WebSocket _ws;
		private System.Threading.Thread _myThread;
		private List<Type> _subscribers; // our subscribers
		private List<Type> _publishers; //our publishers
		private Type _serviceResponse; // to deal with service responses
		private string _serviceName = null;
		private string _serviceValues = null;
		private List<RenderTask> _taskQ = new List<RenderTask>();
		private List<JSONNode> _fragments = new List<JSONNode>();

		// 4.0 : Added volatile flag for clean thread shutdown instead of Thread.Abort (which is dangerous and can leave resources in bad state)
		private volatile bool _run = false;

		private object _queueLock = new object ();

		private static string GetMessageType(Type t) {
			return (string) t.GetMethod ("GetMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, null);
		}

		private static string GetMessageTopic(Type t) {
			return (string) t.GetMethod ("GetMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, null);
		}

		private static ROSBridgeMsg ParseMessage(Type t, JSONNode node) {
			return (ROSBridgeMsg) t.GetMethod ("ParseMessage", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {node});
		}

		private static void Update(Type t, ROSBridgeMsg msg) {
			t.GetMethod ("CallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {msg});
		}

		private static void ServiceResponse(Type t, string service, string yaml) {
			t.GetMethod ("ServiceCallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {service, yaml});
		}

		private static void IsValidServiceResponse(Type t) {
			if (t.GetMethod ("ServiceCallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("invalid service response handler");
		}

		private static void IsValidSubscriber(Type t) {
			if(t.GetMethod ("CallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing Callback method");
			if (t.GetMethod ("GetMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageType method");
			if(t.GetMethod ("GetMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageTopic method");
			if(t.GetMethod ("ParseMessage", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing ParseMessage method");
		}

		private static void IsValidPublisher(Type t) {
			if (t.GetMethod ("GetMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageType method");
			if(t.GetMethod ("GetMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing GetMessageTopic method");
		}

		/**
		 * Make a connection to a host/port. 
		 * This does not actually start the connection, use Connect to do that.
		 */
		public ROSBridgeWebSocketConnection(string host, int port) {
			_host = host;
			_port = port;
			_myThread = null;
			_subscribers = new List<Type> ();
			_publishers = new List<Type> ();
		}

		/**
		 * Add a service response callback to this connection.
		 */
		public void AddServiceResponse(Type serviceResponse) {
			IsValidServiceResponse (serviceResponse);
			_serviceResponse = serviceResponse;
		}

		/**
		 * Add a subscriber callback to this connection. There can be many subscribers.
		 */
		public void AddSubscriber(Type subscriber) {
			IsValidSubscriber(subscriber);
			_subscribers.Add (subscriber);
		}

		/**
		 * Add a publisher to this connection. There can be many publishers.
		 */
		public void AddPublisher(Type publisher) {
			IsValidPublisher(publisher);
			_publishers.Add (publisher);
		}

		/**
		 * Connect to the remote ros environment.
		 */
		public void Connect() {
			// 4.0 : Set _run flag BEFORE starting thread to ensure clean loop condition
			_run = true;
			_myThread = new System.Threading.Thread (Run);
			_myThread.Start ();
		}

		/**
		 * Disconnect from the remote ros environment.
		 */
		public void Disconnect() {
			// 4.0 : REPLACED Thread.Abort() with clean shutdown using _run flag and Join()
			// Thread.Abort() is dangerous, can leave connections/threads in bad state. This is safer.
			_run = false;
			if(_myThread != null && _myThread.IsAlive) {
				_myThread.Join(2000);  // Wait up to 2 seconds for thread to exit gracefully
			}
			
			// 4.0 : Added null-check for _ws to prevent NullReferenceException if Disconnect called before connection established
			if(_ws != null) {
				foreach(Type p in _subscribers) {
					_ws.Send(ROSBridgeMsg.UnSubscribe(GetMessageTopic(p)));
				}
				foreach(Type p in _publishers) {
					_ws.Send(ROSBridgeMsg.UnAdvertise (GetMessageTopic(p)));
				}
				_ws.Close ();
			}
		}

		private void Run() {
			// 4.0 : Added "ws://" protocol to WebSocket URL. Without it, URL may be invalid depending on WebSocketSharp behavior
			_ws = new WebSocket("ws://" + _host + ":" + _port);
			_ws.OnMessage += (sender, e) => this.OnMessage(e.Data);
			_ws.Connect();

			foreach(Type p in _subscribers) {
				_ws.Send(ROSBridgeMsg.Subscribe (GetMessageTopic(p), GetMessageType (p)));
				Debug.Log ("Sending " + ROSBridgeMsg.Subscribe (GetMessageTopic(p), GetMessageType (p)));
			}
			foreach(Type p in _publishers) {
				_ws.Send(ROSBridgeMsg.Advertise (GetMessageTopic(p), GetMessageType(p)));
				Debug.Log ("Sending " + ROSBridgeMsg.Advertise (GetMessageTopic(p), GetMessageType(p)));
			}
			
			// 4.0 : REPLACED while(true) with while(_run). Now the loop can be interrupted cleanly via Disconnect()
			while(_run) {
				Thread.Sleep (10000);
			}
		}

		private JSONNode ProcessFragment(JSONNode node) {
			Debug.Log("Processing a fragment");
			string id;
			int num, total;
			try {
				id = node["id"];
				num = int.Parse(node["num"]);
				total = int.Parse(node["total"]);
			} catch(Exception e) {
				Debug.LogError($"4.0 : Fragment parsing failed: {e.Message}");
				return (JSONNode) null;
			}

			_fragments.Add(node); 
			int count = 0;
			Debug.Log($"Fragments are {_fragments.Count}");
			for(int i=0;i<_fragments.Count;i++) {
				string z = _fragments[i]["id"];
				if(z == id) {
					count++;
				}
			}

			if(total == count) {
				StringBuilder sb = new StringBuilder();
				// 4.0 : FIXED fragment reassembly: use separate list for processing to avoid modifying _fragments during iteration
				List<int> indicesToRemove = new List<int>();
				for(int i=0; i<total; i++) {
					for(int k=0;k<_fragments.Count;k++) {
						string kid = _fragments[k]["id"];
						string knum = _fragments[k]["num"];
						if((kid == id) && (int.Parse(knum) == i)) {
							sb.Append(_fragments[k]["data"]);
							indicesToRemove.Add(k);
							break;
						}
					}
				}
				// 4.0 : Remove fragments in reverse order to avoid index shifting issues
				for(int i = indicesToRemove.Count - 1; i >= 0; i--) {
					_fragments.RemoveAt(indicesToRemove[i]);
				}
				
				string msg = sb.ToString();
				Debug.Log($"Got message {msg}");
				msg = msg.Replace("\\/", "/");
				Debug.Log($"After hack {msg}");
				// 4.0 : Protected fragment JSON parse with try/catch. On error, log and return null instead of reparsing
				try {
					JSONNode nodex = JSONNode.Parse(msg);
					Debug.Log("Parsed ok");
					return(nodex);
				} catch(Exception e) {
					Debug.LogError($"4.0 : Failed to parse reassembled fragment: {e.Message}\nMessage: {msg}");
					return null;
				}
			}
			return((JSONNode) null);
		}

		private void OnMessage(string s) {
			Debug.Log ("Got a message " + s);
			if((s!= null) && !s.Equals ("")) {
				// 4.0 : Protected initial JSON.Parse with try/catch. Malformed JSON crashes the thread
				// 4.0 : Protected initial JSON.Parse with try/catch. Malformed JSON crashes the thread without this.
				JSONNode node = null;
				try {
					node = JSONNode.Parse(s);
				} catch(Exception e) {
					Debug.LogError($"4.0 : Failed to parse incoming message: {e.Message}\nMessage: {s}");
					return;
				}
				
				if(node == null) {
					Debug.LogError("4.0 : JSONNode.Parse returned null for message: " + s);
					return;
				}
				
				string op = node["op"];
				Debug.Log ("Operation is " + op);
				if("fragment".Equals(op)) {
					node = ProcessFragment(node);
					if(node == null) {
						Debug.Log("Fragment not yet complete");
						return;  // fragment not complete
					}
					op = node["op"]; // process the completed fragment
				}

				if("publish".Equals (op)) {
					// 4.0 : Added null-check for topic field. Missing topic in message would cause exception.
					string topic = node["topic"];
					if(topic == null) {
						Debug.LogError($"4.0 : Received publish message without topic field: {node.ToString()}");
						return;
					}
					
					Debug.Log ("Got a message on " + topic);
					Debug.Log(node["msg"]);
					foreach(Type p in _subscribers) {
						Debug.Log($"Looking through subscribers {GetMessageTopic(p)}");
						if(topic.Equals (GetMessageTopic (p))) {
							Debug.Log ("And will parse it " + GetMessageTopic (p));
							Debug.Log(node);
							
							// 4.0 : Protected ParseMessage with try/catch. If any message constructor fails, log error and skip this message.
							// 4.0 : Also added null-check for msg field.
							if(node["msg"] == null) {
								Debug.LogError($"4.0 : Received publish message without 'msg' field for topic {topic}: {node.ToString()}");
								continue;
							}
							
							ROSBridgeMsg msg = null;
							try {
								msg = ParseMessage(p, node["msg"]);
								Debug.Log($"4.0 : Message parsed successfully for topic {topic}");
							} catch(Exception e) {
								Debug.LogError($"4.0 : Failed to parse message for topic {topic}\nSubscriber: {GetMessageTopic(p)}\nMessage content: {node["msg"].ToString()}\nError: {e.Message}\nStack: {e.StackTrace}");
								continue;
							}
							
							RenderTask newTask = new RenderTask(p, topic, msg);
							lock(_queueLock) {
								bool found = false;
								for(int i=0;i<_taskQ.Count;i++) {
									if(_taskQ[i].getTopic().Equals (topic)) {
										_taskQ.RemoveAt (i);
										_taskQ.Insert (i, newTask);
										found = true;
										break;
									}
								}
								if(!found) {
									_taskQ.Add (newTask);
								}
							}

						}
					}
				} else if("service_response".Equals (op)) {
					Debug.Log ("Got service response " + node.ToString ());
					_serviceName = node["service"];
					_serviceValues = (node["values"] == null) ? "" : node["values"].ToString ();
				} else
					Debug.Log ("Must write code here for other messages");
			} else
				Debug.Log ("Got an empty message from the web socket");
		}

		public void Render() {
			RenderTask newTask = null;
			lock (_queueLock) {
				if(_taskQ.Count > 0) {
					newTask = _taskQ[0];
					_taskQ.RemoveAt (0);
				}
			}
			if(newTask != null)
				Update(newTask.getSubscriber (), newTask.getMsg ());

			if (_serviceName != null) {
				ServiceResponse (_serviceResponse, _serviceName, _serviceValues);
				_serviceName = null;
			}
		}

		public void Publish(String topic, ROSBridgeMsg msg) {
			if(_ws != null) {
				string s = ROSBridgeMsg.Publish (topic, msg.ToYAMLString ());
				//Debug.Log ("Sending " + s);
				_ws.Send (s);
			}
		}

		public void CallService(string service, string args) {
			if (_ws != null) {
				string s = ROSBridgeMsg.CallService (service, args);
				//Debug.Log ("Sending " + s);
				_ws.Send (s);
			}
		}
	}
}
