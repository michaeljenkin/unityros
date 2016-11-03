using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib
{
		namespace realsense
		{
				public class PointCloudMsg : ROSBridgeMsg
				{
						private uint _width, _height;

						private byte[] _data;

						public PointCloudMsg (JSONNode msg)
						{
								//_data = msg ["data"];
								_width = uint.Parse(msg["width"]);
								_height = uint.Parse(msg["height"]);
						}

						public static string GetMessageType()
						{
								return "sensor:msgs/PointCloud2";
						}

						public byte[] GetData()
						{
								return _data;
						}

						public uint GetWidth()
						{
								return _width;
						}

						public uint GetHeight()
						{
								return _height;
						}

						public override string ToString()
						{
								return "Point cloud. dim=" + _width + "x" + _height + ". Probably not a good idea to print the cloud...";
						}
				}
		}
}