using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define an Empty message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - added to std_msgs for consistency
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
		namespace std_msgs {
				public class EmptyMsg : ROSBridgeMsg {

						public EmptyMsg(JSONNode msg) { }

						public EmptyMsg() { }

						public static string GetMessageType() {
								return "std_msgs/Empty";
						}
								

						public override string ToString() {
								return "Empty []";
						}

						public override string ToYAMLString() {
								return "{ }";
						}
				}
		}
}
