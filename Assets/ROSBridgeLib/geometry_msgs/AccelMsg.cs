using System.Collections;
using System.Text;
using SimpleJSON;
using ROSBridgeLib.geometry_msgs;

/**
 * Define an Accel message. These have been hand-crafted from the corresponding msg file.
 * 
 * Version History
 *  3.3 - added to std_msgs for consistency
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.3
 */

namespace ROSBridgeLib {
		namespace geometry_msgs {
				public class AccelMsg : ROSBridgeMsg {
						private double _linear;
						private double _angular;

						public AccelMsg(JSONNode msg) {
								_linear = double.Parse(msg["linear"]);
								_angular = double.Parse(msg["angular"]);
						}

						public AccelMsg(double linear, double angular) {
								_linear = linear;
								_angular = angular;
						}

						public static string GetMessageType() {
								return "geometry_msgs/Accel";
						}

						public double GetLinear() {
								return _linear;
						}

						public double GetAngular() {
								return _angular;
						}

						public override string ToString() {
								return "Accel [linear=" + _linear + ",  angular="+ _angular + "]";
						}

						public override string ToYAMLString() {
								return "{\"linear\" : " + _linear + ", \"angular\" : " + _angular + "}";
						}
				}
		}
}