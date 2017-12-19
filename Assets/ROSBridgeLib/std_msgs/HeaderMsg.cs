using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

/**
 * Define a Header message. These have been hand-crafted from the corresponding msg file.
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
		public class HeaderMsg : ROSBridgeMsg {
			private int _seq;
			private TimeMsg _stamp;
			private string _frame_id;
			
			public HeaderMsg(JSONNode msg) {
				_seq = int.Parse (msg["seq"]);
				_stamp = new TimeMsg (msg ["stamp"]);
				_frame_id = msg["frame_id"];
			}
			
			public HeaderMsg(int seq, TimeMsg stamp, string frame_id) {
				_seq = seq;
				_stamp = stamp;
				_frame_id = frame_id;
			}
			
			public static string GetMessageType() {
				return "std_msgs/Header";
			}
			
			public int GetSeq() {
				return _seq;
			}
			
			public TimeMsg GetTimeMsg() {
				return _stamp;
			}

			public string GetFrameId() {
				return _frame_id;
			}
			
			public override string ToString() {
				return "Header [seq=" + _seq + ",  stamp=" + _stamp + ", frame_id=" + _frame_id + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"seq\" : " + _seq + ", \"stamp\" : " + _stamp.ToYAMLString () + ", frame_id=" + _frame_id + "}";
			}
		}
	}
}
