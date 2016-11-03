cmake_minimum_required(VERSION 3.5)
project(Assets)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES
    Plugins/websocket-sharp.dll
    ROSBridgeLib/geometry_msgs/TwistMsg.cs
    ROSBridgeLib/geometry_msgs/Vector3Msg.cs
    ROSBridgeLib/sensor_msgs/CompressedImageMsg.cs
    ROSBridgeLib/std_msgs/HeaderMsg.cs
    ROSBridgeLib/std_msgs/Int32Msg.cs
    ROSBridgeLib/std_msgs/Int32MultiArrayMsg.cs
    ROSBridgeLib/std_msgs/Int64Msg.cs
    ROSBridgeLib/std_msgs/Int64MultiArrayMsg.cs
    ROSBridgeLib/std_msgs/Int8Msg.cs
    ROSBridgeLib/std_msgs/Int8MultiArrayMsg.cs
    ROSBridgeLib/std_msgs/MultiArrayDimensionMsg.cs
    ROSBridgeLib/std_msgs/MultiArrayLayoutMsg.cs
    ROSBridgeLib/std_msgs/StringMsg.cs
    ROSBridgeLib/std_msgs/TimeMsg.cs
    ROSBridgeLib/std_msgs/UInt16Msg.cs
    ROSBridgeLib/std_msgs/UInt16MultiArrayMsg.cs
    ROSBridgeLib/std_msgs/UInt32Msg.cs
    ROSBridgeLib/std_msgs/UInt32MultiArrayMsg.cs
    ROSBridgeLib/std_msgs/UInt64Msg.cs
    ROSBridgeLib/std_msgs/UInt64MultiArrayMsg.cs
    ROSBridgeLib/std_msgs/UInt8Msg.cs
    ROSBridgeLib/std_msgs/UInt8MultiArrayMsg.cs
    ROSBridgeLib/turtlesim/ColorMsg.cs
    ROSBridgeLib/turtlesim/PoseMsg.cs
    ROSBridgeLib/turtlesim/VelocityMsg.cs
    ROSBridgeLib/ROSBridgeMsg.cs
    ROSBridgeLib/ROSBridgePublisher.cs
    ROSBridgeLib/ROSBridgeSubscriber.cs
    ROSBridgeLib/ROSBridgeWebSocketConnection.cs
    ROSBridgeLib/websocket-sharp.dll
    FloorTile.cs
    FollowCamera.cs
    PixPro.cs
    SimpleJSON.cs
    SkyMaterial.mat
    Turtle1ColorSensor.cs
    Turtle1Pose.cs
    Turtle1ServiceResponse.cs
    Turtle1Teleop.cs
    TurtlesimViewer.cs)

add_executable(Assets ${SOURCE_FILES})