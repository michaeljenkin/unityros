using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace turtlesim {
		public class ColorMsg : ROSBridgeMsg {
			private int _r, _g, _b; //uint

			public ColorMsg(JSONNode msg) {
				_r = int.Parse(msg["r"]);
				_g = int.Parse(msg["g"]);
				_b = int.Parse(msg["b"]);
			}
			
			public ColorMsg(int r, int g, int b) {
				_r = r;
				_g = g;
				_b = b;
			}
			
			public static string getMessageType() {
				return "turtlesim/Color";
			}
			
			public int getR() {
				return _r;
			}
			
			public int getG() {
				return _g;
			}

			public int getB() {
				return _b;
			}
			
			public override string ToString() {
				return "turtlesim/Color [r=" + _r + ",  g=" + _g + ", b=" + _b + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"r\" : " + _r + ", \"g\": " + _g + ", \"b\": " + _b + "}";
			}
		}
	}
}
