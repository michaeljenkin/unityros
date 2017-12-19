using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define an Int32MultiArray message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - updated to most recent version
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
		namespace std_msgs {
				public class Int16MultiArrayMsg : ROSBridgeMsg {
						private MultiArrayLayoutMsg _layout;
						private short[] _data;

						public Int16MultiArrayMsg(JSONNode msg) {
								_layout = new MultiArrayLayoutMsg(msg["layout"]);
								_data = new short[msg["data"].Count];
								for (int i = 0; i < _data.Length; i++) {
										_data[i] = short.Parse(msg["data"][i]);
								}
						}

						public Int16MultiArrayMsg(MultiArrayLayoutMsg layout, short[] data) {
								_layout = layout;
								_data = data;
						}

						public static string getMessageType() {
								return "std_msgs/Int16MultiArray";
						}

						public short[] GetData() {
								return _data;
						}

						public MultiArrayLayoutMsg GetLayout() {
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
								return "Int16MultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
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