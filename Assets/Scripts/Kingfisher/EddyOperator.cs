using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;
using ROSBridgeLib.geometry_msgs;


/**
 * Talk to Eddy
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class EddyOperator : MonoBehaviour  {
	private ROSBridgeWebSocketConnection ros = null;	
	private Boolean _useJoysticks;

	
	// the critical thing here is to define our subscribers, publishers and service response handlers
	void Start () {
		//FloorTile.Floor (0, 0, 12, 12);
		_useJoysticks = Input.GetJoystickNames ().Length > 0;
		ros = new ROSBridgeWebSocketConnection ("ws://130.63.93.66", 9090);
		ros.AddSubscriber (typeof(Pixpro));
		ros.Connect ();
	}
	
	// extremely important to disconnect from ROS. OTherwise packets continue to flow
	void OnApplicationQuit() {
		if(ros!=null)
			ros.Disconnect ();
	}
	
	// Update is called once per frame in Unity. The Unity camera follows the robot (which is driven by
	// the ROS environment. We also use the joystick or cursor keys to generate teleoperational commands
	// that are sent to the ROS world, which drives the robot which ...
	void Update () {
		ros.Render ();
	}

}