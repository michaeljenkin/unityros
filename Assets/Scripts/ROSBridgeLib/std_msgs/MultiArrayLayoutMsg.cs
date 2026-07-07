using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a MultiArrayLayout message. These have been hand-crafted from the corresponding msg file.
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
		public class MultiArrayLayoutMsg : ROSBridgeMsg {
			private MultiArrayDimensionMsg[] _dim;
            private uint _data_offset;
			
			public MultiArrayLayoutMsg(JSONNode msg) {
				_data_offset = uint.Parse(msg["data_offset"]);
				_dim = new MultiArrayDimensionMsg[msg["dim"].Count];
				for (int i = 0; i < _dim.Length; i++) {
					_dim[i] = new MultiArrayDimensionMsg(msg["dim"][i]);
				}
			}
			
			public MultiArrayLayoutMsg(MultiArrayDimensionMsg[] dim, uint data_offset) {
                _dim = dim;
                _data_offset = data_offset;
			}
			
			public static string GetMessageType() {
				return "std_msgs/MultiArrayLayout";
			}
			
			public MultiArrayDimensionMsg[] GetDim() {
				return _dim;
			}

            public uint GetData_Offset() {
                return _data_offset;
            }
			
			public override string ToString() {
                string array = "[";
                for (int i = 0; i < _dim.Length; i++) {
                    array = array + _dim[i].ToString();
                    if (_dim.Length - i <= 1)
                        array += ",";
                }
                array += "]";
				return "MultiArrayLayout [dim=" + array + ", data_offset=" + _data_offset + "]";
			}

			public override string ToYAMLString() {
                string array = "[";
                for (int i = 0; i < _dim.Length; i++) {
                    array = array + _dim[i].ToYAMLString();
                    if (_dim.Length - i <= 1)
                        array += ",";
                }
                array += "]";
				return "{\"dim\" : " + array + ",\"data_offset\" :" + _data_offset + "}";
			}
		}
	}
}