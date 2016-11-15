using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.std_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class RealsenseCompressedImageSubscriber : ROSBridgeSubscriber
{
		public new static string GetMessageTopic()
		{
				return "/camera/depth/image_raw/compressedDepth";
		}  

		public new static string GetMessageType()
		{
				return "sensor_msgs/CompressedImage";
		}

		public new static ROSBridgeMsg ParseMessage(JSONNode msg)
		{
				return new  CompressedImageMsg (msg);
		}

		public new static void CallBack(ROSBridgeMsg msg)
		{
            //Debug.Log ("Render callback in /camera/depth/image_raw/compressedDepth" + msg);
		}
}
