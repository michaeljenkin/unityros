using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
        public class Int8MultiArrayMsg : ROSBridgeMsg {
            private MultiArrayLayoutMsg _layout;
            private sbyte[] _data;

            public Int8MultiArrayMsg(JSONNode msg) {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new ulong[msg["data"].Length];
                for (int i = 0; i < msg["data"].Length; i++) {
                    _data[i] = msg["data"][i].AsInt;
                }
            }

            public Int8MultiArrayMsg(MultiArrayLayoutMsg layout, sbyte[] data) {
                _layout = layout;
                _data = data;
            }

            public static string getMessageType() {
                return "std_msgs/Int8MultiArray";
            }

            public MultiArrayLayoutMsg getLayout() {
                return _layout;
            }

            public sbyte[] getData() {
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
                return "Int8MultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
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