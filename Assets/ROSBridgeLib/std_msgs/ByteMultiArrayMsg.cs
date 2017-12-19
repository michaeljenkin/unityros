using System.Collections;
using System.Text;
using SimpleJSON;

/**
 * Define a ByteMultiArray message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 * 3.3 - added to std_msgs for consistency
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
		namespace std_msgs {
				public class ByteMultiArrayMsg : ROSBridgeMsg {
						private MultiArrayLayoutMsg _layout;
						private byte[] _data;

						public ByteMultiArrayMsg(JSONNode msg) {
								_layout = new MultiArrayLayoutMsg(msg["layout"]);
								_data = new byte[msg["data"].Count];
								for (int i = 0; i < _data.Length; i++) {
										_data[i] = byte.Parse(msg["data"][i]);
								}
						}

						public ByteMultiArrayMsg(MultiArrayLayoutMsg layout, byte[] data) {
								_layout = layout;
								_data = data;
						}

						public static string GetMessageType() {
								return "std_msgs/ByteMultiArray";
						}

						public byte[] GetData() {
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
								return "ByteMultiArray [layout=" + _layout.ToString() + ", data=" + _data + "]";
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
