# ROSBridgeLib
A Unity library for communicattion with ROS through [RosBridge](http://wiki.ros.org/rosbridge_suite)

The first version of this I believe origins from [Michael Jenkin](https://github.com/michaeljenkin), in the repo [unityros](https://github.com/michaeljenkin/unityros). He has made a sample unity project showing turtlesim, with good instructions on how to use this project. All honor goes to him. I created this project because there was no repository containing the barebone library.

This repository is intended to be imported as a git [submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules).

## Included messages
This repository does not contain every ROS message. If you need to add one, please fork this repository, add the file and make a pull request.

## Documentation
Documentation is in the code. I have added some more in addition to what Michael Jenkin (original
author). The main file is ROSBridgeWebSocketConnection.cs, which sets up everything.

## Example usage
A Unity project which uses this repository [UnityROSSensorVisualizer](https://github.com/MathiasCiarlo/UnityROSSensorVisualizer):

``` cs
public class RealsenseViewer : MonoBehaviour  {
  private ROSBridgeWebSocketConnection ros = null;
    
  void Start() {
    ros = new ROSBridgeWebSocketConnection ("ws://localhost", 9090);
    ros.AddSubscriber (typeof(RealsenseCompressedImageSubscriber));
    ros.AddServiceResponse (typeof(RealsenseServiceResponse));
    ros.Connect ();
  }
  
  // Extremely important to disconnect from ROS. OTherwise packets continue to flow
  void OnApplicationQuit() {
    if(ros!=null) {
      ros.Disconnect ();
    }
  }
  
  // Update is called once per frame in Unity
  void Update () {
    ros.Render ();
  }
}
```

## License
Note: SimpleJSON is included here as a convenience. It has its own licensing requirements. See source code and unity store for details.
