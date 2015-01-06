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
 * This defines the callback that links the pose message. It moves the Dalek with
 * the turtlesim
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class Turtle1Pose : ROSBridgeSubscriber {
	
	public static string getMessageTopic() {
		return "/turtle1/pose";
	}  
	
	public static string getMessageType() {
		return "turtlesim/Pose";
	}
	
	public static ROSBridgeMsg parseMessage(JSONNode msg) {
		return new PoseMsg(msg);
	}
	
	public static void callBack(ROSBridgeMsg msg) {
		GameObject robot = GameObject.Find ("Dalek");
		if (robot == null)
			Debug.Log ("Can't find the robot???");
		else {
			PoseMsg pose = (PoseMsg) msg;
			robot.transform.position = new Vector3(pose.getX (), 0.2f, pose.getY());
			robot.transform.rotation = Quaternion.AngleAxis (-pose.getTheta() * 180.0f / 3.1415f, Vector3.up);
		}
	}
}
