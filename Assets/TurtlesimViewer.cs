using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;
using ROSBridgeLib.turtlesim;

/**
 * This is a toy example of the Unity-ROS interface talking to the TurtleSim 
 * tutorial (circa Groovy). Note that due to some changes since then this will have
 * to be slightly re-written, but as its a test ....
 * 
 * THis does all the ROS work.
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class TurtlesimViewer : MonoBehaviour  {
	private ROSBridgeWebSocketConnection ros = null;	
	private Boolean _useJoysticks;
	private Boolean lineOn;

	// the critical thing here is to define our subscribers, publishers and service response handlers
	void Start () {
		FloorTile.Floor (0, 0, 12, 12);
		_useJoysticks = Input.GetJoystickNames ().Length > 0;
		ros = new ROSBridgeWebSocketConnection ("ws://10.0.1.95", 9090);
		ros.addSubscriber (typeof(Turtle1ColorSensor));
		ros.addSubscriber (typeof(Turtle1Pose));
		ros.addPublisher (typeof(Turtle1Teleop));
		ros.addServiceResponse (typeof(Turtle1ServiceResponse));
		ros.Connect ();
		ros.callService ("/turtle1/set_pen", "{\"off\": 0}");
		lineOn = true;
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
		float _dx, _dy;
		int _button = 0;
		
		if(_useJoysticks) {
			_dx = Input.GetAxis ("Joy0X");
			_dy = Input.GetAxis ("Joy0Y");
		} else {
			_dx = Input.GetAxis("Horizontal");
			_dy = Input.GetAxis ("Vertical");
		}
		float linear = _dy * 0.5f;
		float angular = -_dx * 0.2f;

		VelocityMsg msg = new VelocityMsg (linear, angular);

		ros.publish (Turtle1Teleop.getMessageTopic (), msg);

		if (Input.GetKeyDown (KeyCode.T)) {
			if (lineOn)
				ros.callService ("/turtle1/set_pen", "{\"off\": 1}");
			else
				ros.callService ("/turtle1/set_pen", "{\"off\": 0}");
			lineOn = !lineOn;
		}
		ros.render ();

	}
}
