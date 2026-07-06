using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a Byte message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - added to std_msgs for consistency
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
		namespace std_msgs {
				public class ByteMsg : ROSBridgeMsg {
						private byte _data;

						public ByteMsg(JSONNode msg) {
								_data = byte.Parse(msg["data"]);
						}

						public ByteMsg(byte data) {
								_data = data;
						}

						public static string GetMessageType() {
								return "std_msgs/Byte";
						}

						public byte GetData() {
								return _data;
						}

						public override string ToString() {
								return "Byte [data=" + _data + "]";
						}

						public override string ToYAMLString() {
								return "{\"data\" : " + _data + "}";
						}
				}
		}
}
