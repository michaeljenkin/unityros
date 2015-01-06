using System.Collections;
using SimpleJSON;

/**
 * This defines a subscriber. Subscribers listen to publishers in ROS. Now if we could have inheritance
 * on static classes then we could do this differently. But basically, you have to make up one of these
 * for every subscriber you need.
 * 
 * Subscribers require a ROSBridgePacket to subscribe to (its type). They need the name of
 * the message, and they need something to draw it. 
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 */

namespace ROSBridgeLib {
	public class ROSBridgeSubscriber {

		public static string getMessageTopic() {
			return null;
		}  

		public static string getMessageType() {
			return null;
		}

		public static ROSBridgeMsg parseMessage(JSONNode msg) {
			return null;
		}

		public static void callBack(ROSBridgeMsg msg) {
		}
	}
}

