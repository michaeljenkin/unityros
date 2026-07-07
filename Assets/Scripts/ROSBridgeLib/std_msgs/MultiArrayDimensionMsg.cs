using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a MultiArrayDimension message. These have been hand-crafted from the corresponding msg file.
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
		public class MultiArrayDimensionMsg : ROSBridgeMsg {
			private string _label;
            private uint _size, _stride;
			
			public MultiArrayDimensionMsg(JSONNode msg) {
				_label = msg["label"];
				_size = uint.Parse(msg["size"]);
				_stride = uint.Parse(msg["stride"]);
			}
			
            public MultiArrayDimensionMsg(string label, uint size, uint stride) {
				_label = label;
                _size = size;
                _stride = stride;
			}
			
			public static string GetMessageType() {
				return "std_msgs/MultiArrayDimension";
			}

			public string GetLabel() {
				return _label;
			}

            public uint GetSize() {
                return _size;
            }

            public uint GetStride() {
                return _stride;
            }

			public override string ToString() {
				return "MultiArrayDimension [label=" + _label + ", size=" + _size + ", stride = " + _stride + "]";
			}
			
			public override string ToYAMLString() {
                return "{\"label\" : " + _label + ",\"size\" :" + _size + ",\"stride\" :" + _stride + "}";
			}
		}
	}
}