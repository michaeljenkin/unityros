using System.Collections;
using System.Text;
using SimpleJSON;

/* 
 * @brief ROSBridgeLib
 * @author Michael Jenkin, Robert Codd-Downey, Andrew Speers and Miquel Massot Campos
 */

namespace ROSBridgeLib {
	namespace std_msgs {
		public class Uint64Msg : ROSBridgeMsg {
			private ulong _data;
			
			public Uint64Msg(JSONNode msg) {
				_data = ulong.Parse(msg);
			}
			
			public Uint64Msg(ulong data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/UInt64";
			}
			
			public ulong GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "Bool [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}