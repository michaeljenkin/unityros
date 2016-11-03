using ROSBridgeLib;
using UnityEngine;

public class RealsenseServiceResponse
{

		public static void ServiceCallBack(string service, string response)
		{
				if(response == null)
						Debug.Log ("ServiceCallback for service " + service);
				else
						Debug.Log ("ServiceCallback for service " + service + " response " + response);
		}
}