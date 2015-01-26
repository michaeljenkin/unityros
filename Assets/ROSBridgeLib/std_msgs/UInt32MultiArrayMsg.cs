using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace std_msgs {
        public class UInt32MultiArrayMsg : ROSBridgeMsg {
            private MultiArrayLayoutMsg _layout;
            private uint[] _data;

            public UInt32MultiArrayMsg(JSONNode msg) {
                _layout = new MultiArrayLayoutMsg(msg["layout"]);
                _data = new ulong[msg["data"].Length];
                for (int i = 0; i < msg["data"].Length; i++) {
                    _data[i] = msg["data"][i].AsInt;
                }
            }

            public Int32MultiArrayMsg(MultiArrayLayoutMsg layout, uint[] data) {
                _layout = layout;
                _data = data;
            }

            public static string getMessageType() {
                return "std_msgs/UInt32MultiArray";
            }

            public MultiArrayLayoutMsg getLayout() {
                return _layout;
            }

            public uint[] getData() {
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
                return "UInt32MultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
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