using System.Collections;
using System.Text;
using SimpleJSON;

/* 
 * @brief ROSBridgeLib
 * @author Michael Jenkin, Robert Codd-Downey, Andrew Speers and Miquel Massot Campos
 */

namespace ROSBridgeLib {
	namespace std_msgs {
		public class UInt32Msg : ROSBridgeMsg {
			private uint _data;
			
			public UInt32Msg(JSONNode msg) {
				_data = uint.Parse(msg);
			}
			
			public UInt32Msg(uint data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/UInt32";
			}
			
			public uint GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "UInt32 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}