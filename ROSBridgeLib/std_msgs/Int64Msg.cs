using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define an Int64MultiArray message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - updated to most recetn version
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
	namespace std_msgs {
		public class Int64Msg : ROSBridgeMsg {
			private long _data;
			
			public Int64Msg(JSONNode msg) {
				_data = long.Parse(msg["data"]);
			}
			
			public Int64Msg(long data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/Int64";
			}
			
			public long GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "Int64 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}