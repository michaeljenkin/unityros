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

public class Turtle1String : ROSBridgeSubscriber {
	
	public new static string GetMessageTopic() {
		return "/turtle1/text";
	}  
	
	public new static string GetMessageType() {
		return "std_msgs/String";
	}
	
	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new StringMsg(msg);
	}
	
	public new static void CallBack(ROSBridgeMsg msg) {
        StringMsg str = (StringMsg) msg;
        string s = str.GetData();
        Debug.Log($"Got a text message length {s.Length}");
        Debug.Log(s);
    }
}
