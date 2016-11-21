using System.Collections;
using System.Text;
using System.IO;
using SimpleJSON;
using ROSBridgeLib.std_msgs;
using UnityEngine;

using PointCloud.PointTypes;
using PointCloud;

/**
 * Define a PointCloud2 message.
 *  
 * @author Miquel Massot Campos
 */

namespace ROSBridgeLib {
	namespace sensor_msgs {
		public class PointCloud2Msg : ROSBridgeMsg {
			private HeaderMsg _header;
			private uint _height;
			private uint _width;
			private PointFieldMsg _fields;
			private bool _is_bigendian;
			private bool _is_dense;
			private uint _point_step;
			private uint _row_step;
			private PointCloud<PointXYZRGB> _cloud;


			public PointCloud2Msg(JSONNode msg) {
				_header = new HeaderMsg (msg ["header"]);
				_height = uint.Parse(msg ["height"]);
				_width = uint.Parse(msg ["width"]);
				_fields = new PointFieldMsg (msg ["fields"]);
				_is_bigendian = msg["is_bigendian"].AsBool;
				_is_dense = msg["is_dense"].AsBool;
				_point_step = uint.Parse(msg ["point_step"]);
				_row_step = uint.Parse(msg ["row_step"]);
				_cloud = ReadData(System.Convert.FromBase64String(msg ["data"]));
			}

			public PointCloud2Msg(HeaderMsg header, uint height, uint width, PointFieldMsg fields, bool is_bigendian, uint point_step, uint row_step, byte[] data, bool is_dense) {
				_header = header;
				_height = height;
				_width = width;
				_fields = fields;
				_is_dense = is_dense;
				_is_bigendian = is_bigendian;
				_point_step = point_step;
				_row_step = row_step;
				_cloud = ReadData(data);
			}

			private PointCloud<PointXYZRGB> ReadData(byte[] byteArray) {
				Stream stream = new MemoryStream(byteArray);
				BinaryReader b = new BinaryReader(stream);

				PointCloud<PointXYZRGB> cloud = new PointCloud<PointXYZRGB> ();

				switch (_fields.GetDatatype()) {
				case PointFieldMsg.FLOAT32:
					for (int i = 0; i < _height * _width; i++) {
						float x = b.ReadSingle ();
						float y = b.ReadSingle ();
						float z = b.ReadSingle ();
						float rgb = b.ReadSingle ();
						PointXYZRGB p = new PointXYZRGB (x,y,z,rgb);
						cloud.Add (p);
					}
					break;
				default:
					Debug.LogError ("PointCloud datatype not supported!");	
					break;
				}
				return cloud;
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

			public PointCloud<PointXYZRGB> GetCloud() {
				return _cloud;
			}

			public static string GetMessageType() {
				return "sensor_msgs/PointCloud2";
			}

			public override string ToString() {
				return "PointCloud2 [header=" + _header.ToString() +
						"height=" + _height +
						"width=" + _width +
						"fields=" + _fields.ToString() +
						"is_bigendian=" + _is_bigendian +
						"is_dense=" + _is_dense +
						"point_step=" + _point_step +
						"row_step=" + _row_step + "]";
			}

			public override string ToYAMLString() {
				return "{\"header\" :" + _header.ToYAMLString() +
						"\"height\" :" + _height +
						"\"width\" :" + _width +
						"\"fields\" :" + _fields.ToYAMLString() +
						"\"is_bigendian\" :" + _is_bigendian +
						"\"is_dense\" :" + _is_dense +
						"\"point_step\" :" + _point_step +
						"\"row_step\" :" + _row_step + "}";
			}
		}
	}
}
