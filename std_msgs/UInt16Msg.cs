using System.Collections;
using System.Text;
using SimpleJSON;

/* 
 * @brief ROSBridgeLib
 * @author Michael Jenkin, Robert Codd-Downey, Andrew Speers and Miquel Massot Campos
 */

namespace ROSBridgeLib {
	namespace std_msgs {
		public class UInt16Msg : ROSBridgeMsg {
			private ushort _data;
			
			public UInt16Msg(JSONNode msg) {
				_data = ushort.Parse(msg);
			}
			
			public UInt16Msg(ushort data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/UInt16";
			}
			
			public ushort GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "UInt16 [data=" + _data + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data + "}";
			}
		}
	}
}