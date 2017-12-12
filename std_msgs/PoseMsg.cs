using System.Collections;
using System.Text;
using SimpleJSON;
using ROSBridgeLib.std_msgs;

/**
 * Define a turtle pose message. This has been hand-crafted from the corresponding
 * turtle message file.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 */

namespace ROSBridgeLib {
	namespace std_msgs {
		public class PoseMsg : ROSBridgeMsg {
			public float _x, _y, _z;

			public PoseMsg(JSONNode msg) {
				_x = float.Parse(msg["x"]);
				_y = float.Parse(msg["y"]);
				_z = float.Parse(msg["z"]);
			}

			public PoseMsg(float x, float y, float z) {
				_x = x;
				_y = y;
				_z = z;
			}
			
/* 			public static string getMessageType() {
				return "turtlesim/Pose";
			} */
			
/* 			public float GetX() {
				return _x;
			}
			
			public float GetY() {
				return _y;
			}
			
			public float GetZ() {
				return _z;
			} */
		}
	}
}
