using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System;
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
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
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

		private object _queueLock = new object ();

		private static string getMessageType(Type t) {
			return (string) t.GetMethod ("getMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, null);
		}

		private static string getMessageTopic(Type t) {
			return (string) t.GetMethod ("getMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, null);
		}

		private static ROSBridgeMsg parseMessage(Type t, JSONNode node) {
			return (ROSBridgeMsg) t.GetMethod ("parseMessage", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {node});
		}

		private static void update(Type t, ROSBridgeMsg msg) {
			t.GetMethod ("callBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {msg});
		}

		private static void serviceResponse(Type t, string service, string yaml) {
			t.GetMethod ("serviceCallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke (null, new object[] {service, yaml});
		}

		private static void isValidServiceResponse(Type t) {
			if (t.GetMethod ("serviceCallBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("invalid service response handler");
		}

		private static void isValidSubscriber(Type t) {
			if(t.GetMethod ("callBack", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing callback method");
			if (t.GetMethod ("getMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing getMessageType method");
			if(t.GetMethod ("getMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing getMessageTopic method");
			if(t.GetMethod ("parseMessage", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing parseMessage method");
		}

		private static void isValidPublisher(Type t) {
			if (t.GetMethod ("getMessageType", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing getMessageType method");
			if(t.GetMethod ("getMessageTopic", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) == null)
				throw new Exception ("missing getMessageTopic method");
		}
		
		public ROSBridgeWebSocketConnection(string host, int port) {
			_host = host;
			_port = port;
			_myThread = null;
			_subscribers = new List<Type> ();
			_publishers = new List<Type> ();
		}

		public void addServiceResponse(Type serviceResponse) {
			isValidServiceResponse (serviceResponse);
			_serviceResponse = serviceResponse;
		}

		public void addSubscriber(Type subscriber) {
			isValidSubscriber(subscriber);
			_subscribers.Add (subscriber);
		}

		public void addPublisher(Type publisher) {
			isValidPublisher(publisher);
			_publishers.Add (publisher);
		}

		public void Connect() {
			_myThread = new System.Threading.Thread (Run);
			_myThread.Start ();
		}

		public void Disconnect() {
			_myThread.Abort ();
			foreach(Type p in _subscribers) {
				_ws.Send(ROSBridgeMsg.unSubscribe(getMessageTopic(p)));
				//Debug.Log ("Sending " + ROSBridgePacket.unSubscribe(getMessageTopic(p)));
			}
			foreach(Type p in _publishers) {
				_ws.Send(ROSBridgeMsg.unAdvertise (getMessageTopic(p)));
				//Debug.Log ("Sending " + ROSBridgePacket.unAdvertise (getMessageTopic(p)));
			}
			_ws.Close ();
		}

		private void Run() {
			_ws = new WebSocket(_host + ":" + _port);
			_ws.OnMessage += (sender, e) => this.OnMessage(e.Data);
			_ws.Connect();

			foreach(Type p in _subscribers) {
				_ws.Send(ROSBridgeMsg.subscribe (getMessageTopic(p), getMessageType (p)));
				//Debug.Log ("Sending " + ROSBridgePacket.subscribe (getMessageTopic(p), getMessageType (p)));
			}
			foreach(Type p in _publishers) {
				_ws.Send(ROSBridgeMsg.advertise (getMessageTopic(p), getMessageType(p)));
				//Debug.Log ("Sending " + ROSBridgePacket.advertise (getMessageTopic(p), getMessageType(p)));
			}
			while(true) {
				Thread.Sleep (10000);
			}
		}

		private void OnMessage(string s) {
			//Debug.Log ("Got message " + s);
			if((s!= null) && !s.Equals ("")) {
				JSONNode node = JSONNode.Parse(s);
				string op = node["op"];
				if("publish".Equals (op)) {
					string topic = node["topic"];
					foreach(Type p in _subscribers) {
						if(topic.Equals (getMessageTopic (p))) {
							ROSBridgeMsg msg = parseMessage(p, node["msg"]);
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
								if(!found)
									_taskQ.Add (newTask);
							}

						}
					}
				} else if("service_response".Equals (op)) {
					Debug.Log ("Got service response " + node.ToString ());
					_serviceName = node["service"];
					_serviceValues = (node["values"] == null) ? "" : node["values"].ToString ();
				} else
					Debug.Log ("Must write code here for other messages");
			}
		}

		public void render() {
			RenderTask newTask = null;
			lock (_queueLock) {
				if(_taskQ.Count > 0) {
					newTask = _taskQ[0];
					_taskQ.RemoveAt (0);
				}
			}
			if(newTask != null)
				update(newTask.getSubscriber (), newTask.getMsg ());

			if (_serviceName != null) {
				serviceResponse (_serviceResponse, _serviceName, _serviceValues);
				_serviceName = null;
			}
		}

		public void publish(String topic, ROSBridgeMsg msg) {
			if(_ws != null) {
				string s = ROSBridgeMsg.publish (topic, msg.ToYAMLString ());
				//Debug.Log ("Sending " + s);
				_ws.Send (s);
			}
		}

		public void callService(string service, string args) {
			if (_ws != null) {
				string s = ROSBridgeMsg.callService (service, args);
				Debug.Log ("Sending " + s);
				_ws.Send (s);
			}
		}
	}
}
