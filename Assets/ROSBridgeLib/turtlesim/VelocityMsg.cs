using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a turtle velocity message. This has been hand-crafted from the corresponding
 * turtle message file. Note: the Groovy version of turtlesim uses this message. Later
 * versions of turtle sim do not. This will have to be fixed in the future.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 */

namespace ROSBridgeLib {
	namespace turtlesim {
		public class VelocityMsg : ROSBridgeMsg {
			private float _linear, _angular;

			public VelocityMsg(JSONNode msg) {
				_linear = float.Parse(msg["linear"]);
				_angular = float.Parse (msg["angular"]);
			}
			
			public VelocityMsg(float linear, float angular) {
				_linear = linear;
				_angular = angular;
			}
			
			public static string GetMessageType() {
				return "turtlesim/Velocity";
			}
			
			public float GetLinear() {
				return _linear;
			}
			
			public float GetAngular() {
				return _angular;
			}

			public override string ToString() {
				return "turtlesim/Velocity [linear=" + _linear + ",  angular=" + _angular + "]";
			}

			public override string ToYAMLString() {
				return "{\"linear\" : " + _linear + ", \"angular\" : " + _angular +"}";
			}
		}
	}
}

