using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a String message. These have been hand-crafted from the corresponding msg file.
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
		public class StringMsg : ROSBridgeMsg {
			private string _data;
			
			public StringMsg(JSONNode msg) {
				_data = msg["data"];
			}
			
			public StringMsg(string data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/String";
			}
			
			public string GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "String [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}