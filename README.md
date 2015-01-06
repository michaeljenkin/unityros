Its self documenting (ha).

So its a unity project, with two basic libraries
	* SimpleJSON - to do the basic JSON work
	* ROSBridgeLib - to do the ROSBridge-Unity heavy lifting

There is a sample application built at the top of the library. It requires a ROS world somewhere running the turtlesim package along with rosbridge. Turtlesim made some changes after groovy, and this has only been tested with groovy, so I would start there.

There is one hard coded constant in TurtleSimViewer - the ip address of the host running the rosbridge package.

Fire up the turtle simulator under ros along with rosbridge web socket server. Then fire up the unity program. with luck (?) you should see a checkerboard with a robot on it. The robot is listening to the location of the turtle and updating its location and orientation as appropriate. So if you teleoperate the turtle around its motion should be tracked by the unity robot. The unity camera is slaved to the robot.

The cursor keys are used to generate tele operational instructions for the turtle simulator in ros, which in turn moves the turtle, which in turn moves the robot in unity.

As a final example, the T key is tied to a ros service, that turns the pen on and off on the turtle simulator. 
