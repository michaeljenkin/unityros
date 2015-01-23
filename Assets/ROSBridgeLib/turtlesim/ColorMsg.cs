using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a turtle colour message. This has been hand-crafted from the corresponding
 * turtle message file.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 */

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
			
			public static string GetMessageType() {
				return "turtlesim/Color";
			}
			
			public int GetR() {
				return _r;
			}
			
			public int GetG() {
				return _g;
			}

			public int GetB() {
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
