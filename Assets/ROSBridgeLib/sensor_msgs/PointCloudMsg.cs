using System.Collections;
using System.Text;
using SimpleJSON;
using ROSBridgeLib.std_msgs;
using UnityEngine;

namespace ROSBridgeLib {
		namespace sensor_msgs {
				public class PointCloudMsg : ROSBridgeMsg {
						//private HeaderMsg _header; // Don't need this atm
						private uint _width, _height;
						private uint _point_step, _row_step;
						private uint _num_points;

						private byte[] _data;

						public PointCloudMsg (JSONNode msg) {
								//_header = new HeaderMsg (msg ["header"]);

								_width = uint.Parse(msg["width"]);
								_height = uint.Parse(msg["height"]);
								_point_step = uint.Parse(msg["point_step"]);
								_row_step = uint.Parse(msg["row_step"]);
								_num_points = _width * _height;

								_data = System.Convert.FromBase64String(msg ["data"]);
								Debug.Log ("data[0]: " + _data[0] );
						}

						public static string GetMessageType() {
								return "sensor:msgs/PointCloud2";
						}

						public byte[] GetData()	{
								return _data;
						}

						public uint GetWidth() {
								return _width;
						}

						public uint GetHeight() {
								return _height;
						}

						public uint GetPointStep() {
								return _point_step;
						}

						public uint GetRowStep() {
								return _row_step;
						}

						public uint GetNumPoints() {
								return _num_points;
						}

						public override string ToString() {
								return "Point cloud. dim=" + _width + "x" + _height;
						}
				}
		}
}