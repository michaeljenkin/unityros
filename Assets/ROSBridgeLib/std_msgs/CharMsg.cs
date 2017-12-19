using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a time message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - updated to most recent version
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.1
 */

namespace ROSBridgeLib {
		namespace std_msgs {
				public class CharMsg : ROSBridgeMsg {
						private char _data;

						public CharMsg(JSONNode msg) {
								_data = char.Parse(msg["data"]);
						}

						public CharMsg(char data) {
								_data = data;
						}

						public static string GetMessageType() {
								return "std_msgs/Char";
						}

						public char GetData() {
								return _data;
						}

						public override string ToString() {
								return "Char [data=" + _data + "]";
						}

						public override string ToYAMLString() {
								return "{\"data\" : " + _data + "}";
						}
				}
		}
}
