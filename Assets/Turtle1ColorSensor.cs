using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.turtlesim;
using System.Collections;
using SimpleJSON;
using UnityEngine;

/**
 * This is a toy example of the Unity-ROS interface talking to the TurtleSim 
 * tutorial (circa Groovy). Note that due to some changes since then this will have
 * to be slightly re-written, but as its a test ....
 * 
 * This defines the callback that links the color_sensor message and its callback
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class Turtle1ColorSensor : ROSBridgeSubscriber {
	
	public static string getMessageTopic() {
		return "/turtle1/color_sensor";
	}  
	
	public static string getMessageType() {
		return "turtlesim/Color";
	}
	
	public static ROSBridgeMsg parseMessage(JSONNode msg) {
		return new ColorMsg(msg);
	}
	
	public static void callBack(ROSBridgeMsg msg) {
		Debug.Log ("Render callback in /turtle1/color_sensor" + msg);
	}
}
