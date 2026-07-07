using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define an Int8 message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - added to std_msgs for consistency
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
	namespace std_msgs {
		public class Int8Msg : ROSBridgeMsg {
			private sbyte _data;
			
			public Int8Msg(JSONNode msg) {
				_data = sbyte.Parse(msg["data"]);
			}
			
			public Int8Msg(sbyte data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/Int8";
			}
			
			public sbyte GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "Int8 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}