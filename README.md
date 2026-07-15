![Test scene: the turtle in TurtleSim (right, running under ROS) and its
corresponding avatar in Unity (left, on the checkerboard) are shown side
by side. The avatar's position and orientation are updated in real time
to match the turtle's pose, demonstrating the ROS-to-Unity communication
bridge.](images/ros-unity-testscene.png)

Welcome to UnityROS. This is a tool that provides a mapping between ROS 2 
and the Unity ecosystems to enable solutions to take advantage of the best 
of both environments. UnityROS relies on rosbridge to do the heavy lifting
between the two systems. Basically, UnityROS defines a 1:1 mapping between
ROS message types and C# classes, and then does the mapping. On the Unity
side the receipt of a message will invoke a method in the rendering thread
to deal with the receipt of the message.

A demonstration of the tool is included here. It relies on the turtlesim
package on the ROS side. In some ROS environment (for which you know the ipaddress)
execute

ros2 run turtlesim turtlesim_node &
ros2 run rosbridge_server rosbridge_websocket

Then fire up the Unity side. The DalekWorld scene provides a very simple 
demonstration of the system. You must provide the Turtlesim Viewer script attached to 
the Main Camera the IP address of the machine running the rosbridge_server (ROS Bridge WS label).
The status of the connection can be monitored from both the ROS and Unity sides. 

One can control the motion of the turtlesim either through the ROS side (using some teleop node)
or through the Unity side. From the Unity side the keyboard arrow keys will translate or rotate
the robot and the T key will toggle the line drawn on the turtlesim_node as the turtle is moved.

Version History

Version 4.0

Validated the library end-to-end with a new test scene combining turtlesim
under ROS and a Unity checkerboard scene with a robot avatar. The test confirms
bidirectional synchronization: the Unity avatar mirrors turtlesim's pose
in real time, and cursor-key input captured in Unity is converted into
Twist commands that drive the turtle in ROS, exactly as in the original
TurtleSimViewer demo.



Reworked the communication layer to make it more robust.

Version 3.3

Updated everything to work with ROS Jazzy.  All of the std_msgs are now there, although some of the more less often
used ones are not fully debugged. 

Cleaned up the repository somewhat, removing files not directly source code related.

Updated the TurtleSimViewer application so that the parameters are visible in the editor.

Removed unnecessary files from the repository

Version 3.2

Updated turtlesim example to work with ROS Hydro and added some geometry message files

Version 3.1

Major change here is to make the c# method names all upper case and to add better comments

Version  3.0
Its self documenting (ha).

So its a unity project, with two basic libraries
	* SimpleJSON - to do the basic JSON work
	* ROSBridgeLib - to do the ROSBridge-Unity heavy lifting

There is a sample application built at the top of the library. It requires a ROS world somewhere running the turtlesim package along with rosbridge. Turtlesim made some changes after groovy, and this has only been tested with groovy, so I would start there.

There is one hard coded constant in TurtleSimViewer - the ip address of the host running the rosbridge package.

Fire up the turtle simulator under ros along with rosbridge web socket server. Then fire up the unity program. with luck (?) you should see a checkerboard with a robot on it. The robot is listening to the location of the turtle and updating its location and orientation as appropriate. So if you teleoperate the turtle around its motion should be tracked by the unity robot. The unity camera is slaved to the robot.

The cursor keys are used to generate tele operational instructions for the turtle simulator in ros, which in turn moves the turtle, which in turn moves the robot in unity.

As a final example, the T key is tied to a ros service, that turns the pen on and off on the turtle simulator. 

Note: SimpleJSON is included here as a convenience. It has its own licensing requirements. See source code
and unity store for details.

Version 2.0

A hacked up version for the CSA demo
