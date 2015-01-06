using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace turtlesim {
		public class PoseMsg : ROSBridgeMsg {
			private float _x, _y, _theta, _linear_velocity, _angular_velocity;

			public PoseMsg(JSONNode msg) {
				_x = float.Parse(msg["x"]);
				_y = float.Parse (msg["y"]);
				_theta = float.Parse (msg["theta"]);
				_linear_velocity = float.Parse(msg["linear_velocity"]);
				_angular_velocity = float.Parse (msg["angular_velocity"]);
			}

			public PoseMsg(float x, float y, float theta, float linear_velocity, float angular_velocity) {
				_x = x;
				_y = y;
				_theta = theta;
				_linear_velocity = linear_velocity;
				_angular_velocity = angular_velocity;
			}
			
			public static string getMessageType() {
				return "turtlesim/Pose";
			}
			
			public float getX() {
				return _x;
			}
			
			public float getY() {
				return _y;
			}
			
			public float getTheta() {
				return _theta;
			}

			public float getLinear_Velocity() {
				return _linear_velocity;
			}
			
			public float getAngular_Velocity() {
				return _angular_velocity;
			}
			
			public override string ToString() {
				return "turtlesim/Pose [x=" + _x + ",  y=" + _y + ", theta=" + _theta +  
					", linear_velocity=" + _linear_velocity + ", angular_velocity=" + _angular_velocity + "]";
			}
			

			
			public override string ToYAMLString() {
				return "{\"x\": " + _x + ", \"y\": " + _y + ", \"theta\": " + _theta + 
					", \"linear_velocity\": " + _linear_velocity + ", \"angular_velocity\": " + _angular_velocity +"}";
			}
		}
	}
}
