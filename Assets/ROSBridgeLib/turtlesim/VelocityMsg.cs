using System.Collections;
using System.Text;
using SimpleJSON;

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
			
			public static string getMessageType() {
				return "turtlesim/Velocity";
			}
			
			public float getLinear() {
				return _linear;
			}
			
			public float getAngular() {
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

