using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.realsense;

public class RealsenseViewer : MonoBehaviour  {
		private ROSBridgeWebSocketConnection ros = null;	

		// The critical thing here is to define our subscribers, publishers and service response handlers
		void Start ()
		{
				FloorTile.Floor (0, 0, 12, 12);

				ros = new ROSBridgeWebSocketConnection ("ws://localhost", 9090);

				ros.AddSubscriber (typeof(RealsensePointCloud));
				ros.AddServiceResponse (typeof(RealsenseServiceResponse));
				ros.Connect ();
		}

		// Extremely important to disconnect from ROS. OTherwise packets continue to flow
		void OnApplicationQuit() {
				if(ros!=null)
						ros.Disconnect ();
		}

		// Update is called once per frame in Unity. The Unity camera follows the robot (which is driven by
		// the ROS environment. We also use the joystick or cursor keys to generate teleoperational commands
		// that are sent to the ROS world, which drives the robot which ...
		void Update () {

				// This might tell the websocket it finished rendering, not sure though
				ros.Render ();

		}
}
