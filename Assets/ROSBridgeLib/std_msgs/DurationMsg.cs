using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

/**
 * Define a Duration message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - added to std_msgs for consistency
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
		namespace std_msgs {
				public class DurationMsg : ROSBridgeMsg {
						private int _secs, _nsecs;

						public DurationMsg(JSONNode msg) {
								_secs = int.Parse (msg["secs"]);
								_secs = int.Parse (msg["nsecs"]);

						}

						public DurationMsg(int secs, int nsecs) {
								_secs = secs;
								_nsecs = nsecs;
						}

						public static string GetMessageType() {
								return "std_msgs/Duration";
						}

						public int GetSecs() {
								return _secs;
						}

						public int GetNsecs() {
								return _nsecs;
						}

						public override string ToString() {
								return "Duration [secs=" + _secs + ",  nsecs=" + _nsecs + "]";
						}

						public override string ToYAMLString() {
								return "{\"secs\" : " + _secs + ", \"nsecs\" : " +  _nsecs + "}";
						}
				}
		}
}

