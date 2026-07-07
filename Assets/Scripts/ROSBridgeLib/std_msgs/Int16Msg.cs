using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define an Int16 message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - updated to most recent version
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
		namespace std_msgs {
				public class Int16Msg : ROSBridgeMsg {
						private short _data;

						public Int16Msg(JSONNode msg) {
								_data = short.Parse(msg["data"]);
						}

						public Int16Msg(short data) {
								_data = data;
						}

						public static string GetMessageType() {
								return "std_msgs/Int16";
						}

						public short GetData() {
								return _data;
						}

						public override string ToString() {
								return "Int16 [data=" + _data + "]";
						}

						public override string ToYAMLString() {
								return "{\"data\" : " + _data + "}";
						}
				}
		}
}