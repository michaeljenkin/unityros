using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
		public class TimeMsg : ROSBridgeMsg {
			private int _secs, _nsecs;

			public TimeMsg(JSONNode msg) {
				_secs = int.Parse(msg["data"]["secs"]);
				_nsecs = int.Parse (msg["data"]["nsecs"]);
			}

			public TimeMsg(int secs, int nsecs) {
				_secs = secs;
				_nsecs = nsecs;
			}

			public static string getMessageType() {
				return "std_msgs/Time";
			}

			public int getSecs() {
				return _secs;
			}

			public int getNsecs() {
				return _nsecs;
			}

			public override string ToString() {
				return "Time [secs=" + _secs + ",  nsecs=" + _nsecs + "]";
			}

			public override string ToYAMLString() {
				return "{\"data\" : {\"secs\" : " + _secs + ", \"nsecs\" : " + _nsecs + "}}";
			}
		}
	}
}
