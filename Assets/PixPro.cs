using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using System.IO;

/**
 * A callback to define a Pixpro image
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class Pixpro : ROSBridgeSubscriber {
	
	public new static string GetMessageTopic() {
		return "/pixpro/compressed";
	}  
	
	public new static string GetMessageType() {
		return "sensor_msgs/CompressedImage";
	}
	
	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		Debug.Log ("ParseMessage in Pixpro");
		return new CompressedImageMsg(msg);
	}
	
	public new static void CallBack(ROSBridgeMsg msg) {
		Debug.Log (GetMessageTopic () + " received");
		
		CompressedImageMsg imageMsg = (CompressedImageMsg) msg;

		Texture2D tex = new Texture2D (2, 2);
		byte[] image = imageMsg.GetImage ();
		tex.LoadImage (image);

		GameObject sphere = GameObject.Find ("/SkySphere");
		if(sphere == null)
			Debug.Log ("No /SkySpheree");
		else {
			Texture2D.DestroyImmediate (sphere.GetComponent<Renderer>().material.mainTexture, true);
			sphere.GetComponent<Renderer>().material.mainTexture = tex;

			Debug.Log ("Texture updated");
		}

		GameObject cube = GameObject.Find ("/Cube");
		if(sphere == null)
			Debug.Log ("No /Cube");
		else {
			Texture2D.DestroyImmediate (cube.GetComponent<Renderer>().material.mainTexture, true);
			cube.GetComponent<Renderer>().material.mainTexture = tex;
			cube.transform.Rotate(0, Time.deltaTime * 50, 0, Space.World);

			Debug.Log ("Cube Texture updated");
			Debug.Log ("delta was " + Time.deltaTime);
		}

		
		
		
	}
}

