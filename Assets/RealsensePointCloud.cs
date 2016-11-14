using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.std_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class RealsensePointCloud : ROSBridgeSubscriber
{
		public new static string GetMessageTopic()
		{
				return "/camera/depth/points";
		}  

		public new static string GetMessageType()
		{
				return "sensor_msgs/PointCloud2";
		}

		public new static ROSBridgeMsg ParseMessage(JSONNode msg)
		{
				return new PointCloudMsg (msg);
		}

		public new static void CallBack(ROSBridgeMsg msg)
		{
				//Debug.Log ("Render callback in /camera/depth/points" + msg);
		}
}
