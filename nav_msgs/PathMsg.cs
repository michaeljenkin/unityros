using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.geometry_msgs;
// Need geometry_msgs/PoseStamped[]

namespace ROSBridgeLib {
	namespace nav_msgs {
		public class PathMsg : ROSBridgeMsg {
			public HeaderMsg _header;
			public List<PoseStampedMsg> _poses;
			
			public PathMsg(JSONNode msg) {
				_header = new HeaderMsg(msg["header"]);
                // Treat poses
                for (int i = 0; i < msg["poses"].Count; i++ ) {
					_poses.Add(new PoseStampedMsg(msg["poses"][i]));
				}
			}
			
/* 			public CompressedImageMsg(JSONNode msg) {
				_format = msg ["format"];
				_header = new HeaderMsg (msg ["header"]);
				//_poses = 
			} */
			
/* 			public static string GetMessageType() {
				return "geometry_msgs/Twist";
			} */
			
/* 			public HeaderMsg GetHeader() {
				return _header;
			} */

/* 			public PoseStamped[] GetPoses() {
				return _poses;
			} */
/* 			
			public override string ToString() {
				return "Twist [linear=" + _linear.ToString() + ",  angular=" + _angular.ToString() + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"linear\" : " + _linear.ToYAMLString() + ", \"angular\" : " + _angular.ToYAMLString() + "}";
			} */
		}
	}
}