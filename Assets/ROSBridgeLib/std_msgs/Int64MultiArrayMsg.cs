using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
        public class Int64MultiArrayMsg : ROSBridgeMsg {
            private MultiArrayLayoutMsg _layout;
            private long[] _data;

            public Int64MultiArrayMsg(JSONNode msg) {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new long[msg["data"].Length];
                for (int i = 0; i < msg["data"].Length; i++) {
                    _data[i] = msg["data"][i].AsDouble;
                }
            }

            public UInt64MultiArrayMsg(MultiArrayLayoutMsg layout, long[] data) {
                _layout = layout;
                _data = data;
            }

            public static string getMessageType() {
                return "std_msgs/Int64MultiArray";
            }

            public MultiArrayLayoutMsg getLayout() {
                return _layout;
            }

            public long[] getData() {
                return _data;
            }

            public MultiArrayLayoutMsg getLayout() {
                return _layout;
            }

            public override string ToString() {
                string array = "[";
                for (int i = 0; i < _data.Length; i++) {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }
                array += "]";
                return "Int64MultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
            }

            public override string ToYAMLString() {
                string array = "[";
                for (int i = 0; i < _data.Length; i++) {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }
                array += "]";
                return "{\"layout\" : " + _layout.ToYAMLString() + ", \"data\" : " + array + "}";
            }
        }
	}
}