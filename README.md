# RoboND-UnitySim
Prototype project for Robotics NanoDegree  
Provides a simulation of a quad drone for controls and deep learning exercises

The project features two main environments ('scenes') to experiment with:
1. quad_indoor: a giant box sized 300m³, with a 1m tiling grid covering all sides.
2. proto4: an outdoor city environment
The city scene starts with a menu to choose between controlling the quad for the Controls and Deep Learning projects, and setting up to train the neural network for the Deep Learning project. In the training mode, a human character will spawn in a random place with a random appearance, and a camera follows the person around to record.

**Firing up the executable**  
Find your OS's executable [here](https://github.com/udacity/RoboND-Controls-Lab/releases)  
Either run from a terminal or edit the app properties, and append either the word `indoor` or the word `outdoor` (or `city`) to the path. If nothing is specified, the indoor scene will be loaded.  
For issues connecting the sim to your VM or host OS, see the troubleshooting section at the bottom.

# ROS Services and Topics
**Quad**
```
quad_rotor/cmd_force
```
Publish force and torque using a [Wrench](http://docs.ros.org/jade/api/geometry_msgs/html/msg/Wrench.html) message.
```
quad_rotor/cmd_vel
```
Publish linear and angular velocity using a [Twist](http://docs.ros.org/jade/api/geometry_msgs/html/msg/Twist.html) message
```
quad_rotor/pose
```
The Quad publishes its pose to this topic using [PoseStamped](http://docs.ros.org/jade/api/geometry_msgs/html/msg/PoseStamped.html)
```
quad_rotor/imu
```
The Quad publishes its velocity/acceleration using an [Imu](http://docs.ros.org/api/sensor_msgs/html/msg/Imu.html)
```
quad_rotor/gravity
```
Use this service to toggle gravity on/off using [SetBool](http://docs.ros.org/jade/api/std_srvs/html/srv/SetBool.html)
```
quad_rotor/x_force_constrained
quad_rotor/y_force_constrained
quad_rotor/z_force_constrained
```
Services to constrain movement on an axis using [SetBool](http://docs.ros.org/jade/api/std_srvs/html/srv/SetBool.html)
```
quad_rotor/x_torque_constrained
quad_rotor/y_torque_constrained
quad_rotor/z_torque_constrained
```
Services to constrain rotation on an axis using [SetBool](http://docs.ros.org/jade/api/std_srvs/html/srv/SetBool.html)
```
quad_rotor/reset_orientation
```
Service to reset the Quad's orientation, of type [SetBool](http://docs.ros.org/jade/api/std_srvs/html/srv/SetBool.html) (data ignored)
```
quad_rotor/set_pose
```
Service to set the Quad's position and orientation directly, using _SetPose_ (see Assets/Scripts/Ros/SetPose.srv)
```
quad_rotor/clear_path
```
Service to reset the path planner's current path, of type [SetBool](http://docs.ros.org/jade/api/std_srvs/html/srv/SetBool.html) (data ignored)
```
quad_rotor/set_path
```
Service to set a path to follow, of type _SetPath_ (see Project/Assets/Scripts/Ros/SetPath.srv). Path must contain 2+ waypoints

**Camera**
```
quad_rotor/camera_pose_type
```
Service to set the camera's pose, of type _SetInt_ (see Project/Assets/Scripts/Ros/SetInt.srv). Poses are:  
0 - Forward / 1 - Side / 2 - Top / 3 - Iso / 4 - Free
```
quad_rotor/camera_distance
```
Set the distance of the camera to the Quad, of type _SetFloat_ (see Project/Assets/Scripts/Ros/SetFloat.srv)

### Examples ###

To publish an upward thrust of 0.1, use this format:
```
$ rostopic pub /quad_rotor/cmd_force geometry_msgs/Wrench "force:
  x: 0.0
  y: 0.0
  z: 0.1
torque:
  x: 0.0
  y: 0.0
  z: 0.0"
```
_Note: if gravity is on, a force that small won't lift the quad off the ground_  

To turn on gravity, use the following:  
```
$ rosservice call /quad_rotor/gravity "data: true"
```


# Controlling the Quad and Camera
1. `F12`: toggle local control on/off
2. `WSAD (arrow keys)`: Move around
3. `Space/C`: Thrust up/down
4. `Q/E`: Turn around
5. `Scroll wheel`: zoom in/out
6. `RMB (hold & drag)`: Rotate camera
7. `RMB (click)`: Reset camera
8. `G`: Gravity on/off
9. `R`: Reset quad orientation
10. `1-4`: Switch camera views (Front/side/top/Iso)
11. `P`: Plot waypoint
12. `O`: Begin following current path
13. `I`: Clear all waypoints
14. `L`: Toggle this info on/off
15. `Esc`: Quit

# Capturing images for deep learning (city only): #
1. Fire up the executable with the city environment. Select `DL Training` from the menu
2. To begin recording, press `R` to bring up the dialog and choose where to save the recording. Select or create a convenient folder, such as in your Desktop or Documents, and confirm, and recording begins.
3. Images are captured from two cameras - one that sees the environment as you do, and one that sees in black&white as shown at the bottom right. The images are captured once every 3 seconds or so.
4. To stop recording, press `R` again, or simply close the executable (`Esc`)

# Troubleshooting #
**ros_settings.txt**  
Included in the project and the executables is a config file named __ros_settings.txt__. This file can be used to control which IP the sim tries to connect to. Depending on your host OS and whether you're running Ros in a VM or locally, you may or may not need to modify this file for the sim to successfully talk to ros.
The file begins in a a format like this:
```
{
	"vm-ip" : "192.168.30.111",
	"vm-port" : 11311,
	"vm-override" : true,
	"host-ip": "0.0.0.0",
	"host-override" : false
}
```
The first line:  
```
"vm-ip" : "192.168.30.111",
```
Sets the IP where Ros is running.
```
"vm-port" : 11311,
```
This one is the port where Ros is running. You'll almost never need to modify this.  
```
"vm-override" : true,
```
This controls whether the above info is used or not. If you run Ros in a VM, keep this to _true_. If you're running Ros locally, set it to _false_ and sim will use _127.0.0.1_.  
```
"host-ip": "0.0.0.0",
```
This value can override the _sim's_ IP to respond to requests if you're having trouble publishing or subscribing to topics, or calling services. This mostly happens when running the sim itself in Ubuntu or Debian, which have an entry in /etc/hosts linking your host's _hostname_ to the address _127.0.1.1_, which Ros (in the sim) usually has issues with. You could instead modify your hosts file, at your own risk.
```
"host-override" : false
```
If you need to change `host-ip`, you'll want to set this to _true_.

If you modify any settings in the file, you'll need to stop and play (in the Editor) or relaunch the sim afterward.


**Ros is connected but topics or services are not working**  
If the sim appears to be connected but you're not able to publish to topics or call services, and you're _not_ running the sim from a Linux host, your VM may be passing its hostname instead of its IP with these requests. To fix it, you'll need to set the environment variable `ROS_IP`. It should look something like this:
```
ROS_IP=192.168.30.111
```
With the IP being your VM's IP.  
To have it set automatically, add a line like the following to your `.bashrc`:  
```
export ROS_IP=`echo $(hostname -I)`
```
And then just run the following to reload .bashrc:  
```
source ~/.bashrc
```
And relaunch the sim (or Unity). You should now be able to properly publish topics and call services.
