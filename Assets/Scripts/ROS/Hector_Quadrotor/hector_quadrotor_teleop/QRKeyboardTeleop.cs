using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Ros_CSharp;
using Messages;
using Messages.geometry_msgs;
using Messages.tf2_msgs;
using Messages.sensor_msgs;
using hector_uav_msgs;
using LandingClient = actionlib.SimpleActionClient<hector_uav_msgs.LandingAction>;
using TakeoffClient = actionlib.SimpleActionClient<hector_uav_msgs.TakeoffAction>;
using PoseClient = actionlib.SimpleActionClient<hector_uav_msgs.PoseAction>;
using Empty = Messages.std_srvs.Empty;
using Trigger = Messages.std_srvs.Trigger;
using SetBool = Messages.std_srvs.SetBool;

/*
 * QRKeyboardTeleop: a basic teleop class for a quadrotor drone using Hector_Quadrotor commands
 */

public class QRKeyboardTeleop : MonoBehaviour
{
	public bool active;
	public UnityEngine.Vector3 Position { get; protected set; }
	public UnityEngine.Quaternion Rotation { get; protected set; }

	NodeHandle nh;
	Subscriber<Joy> joySubscriber;
	Publisher<Wrench> wrenchPub;
	Publisher<Twist> velocityPublisher;
	Publisher<AttitudeCommand> attitudePublisher;
	Publisher<YawRateCommand> yawRatePublisher;
	Publisher<ThrustCommand> thrustPublisher;
	ServiceClient<EnableMotors> motorEnableService;
	ServiceClient<SetBool.Request, SetBool.Response> resetService;
	ServiceClient<SetBool.Request, SetBool.Response> gravityService;
	Thread pubThread;
	Subscriber<PoseStamped> poseSub;
	Subscriber<Imu> imuSub;

	PoseStamped pose;
	double yaw;
	double slowFactor;
	string baseLinkFrame, baseStabilizedFrame, worldFrame;

	// attitude control
//	double maxPitch;
//	double maxRoll;
//	double maxThrust;
//	double thrustOffset;
	// twist (/position) control
	double xVelocityMax;
	double yVelocityMax;
	double zVelocityMax;
	bool init;
	bool _useGravity;

	void Awake ()
	{
		if ( !active )
		{
			enabled = false;
			return;
		}
		init = false;
		ROSController.StartROS ( OnRosInit );
	}

	void OnDestroy ()
	{
		stop ();
	}

	void OnRosInit ()
	{
		nh = ROS.GlobalNodeHandle;
//		NodeHandle privateNH = new NodeHandle("~");
		// TODO dynamic reconfig
		string control_mode = "";
		nh.param<string>("control_mode", ref control_mode, "wrench");

//		NodeHandle robot_nh = new NodeHandle ("~");

		// TODO factor out
//		nh.param<string>("base_link_frame", ref baseLinkFrame, "base_link");
//		nh.param<string>("world_frame", ref worldFrame, "world");
//		nh.param<string>("base_stabilized_frame", ref baseStabilizedFrame, "base_stabilized");

		if ( control_mode == "wrench" )
		{
			wrenchPub = nh.advertise<Wrench> ( "quad_rotor/cmd_force", 10 );
			poseSub = nh.subscribe<PoseStamped> ( "quad_rotor/pose", 10, PoseCallback );
			imuSub = nh.subscribe<Imu> ( "quad_rotor/imu", 10, ImuCallback );
			velocityPublisher = nh.advertise<Twist> ( "quad_rotor/cmd_vel", 10 );
		}
/*		else if (control_mode == "attitude")
		{
//			nh.param<double>("pitch_max", ref sAxes.x.factor, 30.0);
//			nh.param<double>("roll_max", ref sAxes.y.factor, 30.0);
//			nh.param<double>("thrust_max", ref sAxes.thrust.factor, 10.0);
//			nh.param<double>("thrust_offset", ref sAxes.thrust.offset, 10.0);
			attitudePublisher = nh.advertise<AttitudeCommand> ( "command/attitude", 10 );
			yawRatePublisher = nh.advertise<YawRateCommand> ( "command/yawrate", 10 );
			thrustPublisher = nh.advertise<ThrustCommand> ( "command/thrust", 10 );
		}
		else if (control_mode == "velocity" || control_mode == "twist")
		{
			// Gazebo uses Y=forward and Z=up
			nh.param<double>("x_velocity_max", ref xVelocityMax, 2.0);
			nh.param<double>("y_velocity_max", ref zVelocityMax, 2.0);
			nh.param<double>("z_velocity_max", ref yVelocityMax, 2.0);

			velocityPublisher = nh.advertise<Twist> ( "command/twist", 10 );
		}
		else if (control_mode == "position")
		{
			// Gazebo uses Y=forward and Z=up
			nh.param<double>("x_velocity_max", ref xVelocityMax, 2.0);
			nh.param<double>("y_velocity_max", ref zVelocityMax, 2.0);
			nh.param<double>("z_velocity_max", ref yVelocityMax, 2.0);

			pose.pose.position.x = 0;
			pose.pose.position.y = 0;
			pose.pose.position.z = 0;
			pose.pose.orientation.x = 0;
			pose.pose.orientation.y = 0;
			pose.pose.orientation.z = 0;
			pose.pose.orientation.w = 1;
		}*/
		else
		{
			ROS.Error("Unsupported control mode: " + control_mode);
		}

		motorEnableService = nh.serviceClient<EnableMotors> ( "enable_motors" );
		resetService = nh.serviceClient<SetBool.Request, SetBool.Response> ( "quad_rotor/reset_orientation" );
		gravityService = nh.serviceClient<SetBool.Request, SetBool.Response> ( "quad_rotor/gravity" );
//		takeoffClient = new TakeoffClient ( nh, "action/takeoff" );
//		landingClient = new LandingClient ( nh, "action/landing" );
//		poseClient = new PoseClient ( nh, "action/pose" );

		init = true;
	}

	public bool enableMotors (bool enable)
	{
		if ( !init )
			return false;
		EnableMotors srv = new EnableMotors ();
		srv.req.enable = enable;
		return motorEnableService.call ( srv );
	}

	public void stop ()
	{
		if ( !init )
			return;
		
		if ( velocityPublisher != null && velocityPublisher.getNumSubscribers () > 0 )
		{
			velocityPublisher.publish ( new Twist () );
		}
		if ( attitudePublisher != null && attitudePublisher.getNumSubscribers () > 0 )
		{
			attitudePublisher.publish ( new AttitudeCommand () );
		}
		if ( thrustPublisher != null && thrustPublisher.getNumSubscribers () > 0 )
		{
			thrustPublisher.publish ( new ThrustCommand () );
		}
		if ( yawRatePublisher != null && yawRatePublisher.getNumSubscribers () > 0 )
		{
			yawRatePublisher.publish ( new YawRateCommand () );
		}
	}

	public void SendTwist (UnityEngine.Vector3 linear, UnityEngine.Vector3 angular)
	{
		if ( !init )
			return;
		
		Twist twist = new Twist ();
		twist.linear = new Messages.geometry_msgs.Vector3 ( linear.ToRos () );
		twist.angular = new Messages.geometry_msgs.Vector3 ( angular.ToRos () );

		velocityPublisher.publish ( twist );
	}

	public void SendWrench (UnityEngine.Vector3 force, UnityEngine.Vector3 torque)
	{
		if ( !init )
			return;
		
		Wrench wrench = new Wrench ();
		wrench.force = new Messages.geometry_msgs.Vector3 ( force.ToRos () );
		wrench.torque = new Messages.geometry_msgs.Vector3 ( torque.ToRos () );

		wrenchPub.publish ( wrench );
	}

	public void TriggerReset ()
	{
		if ( !init )
			return;
		
		new Thread ( CallReset ).Start ();
	}

	void CallReset ()
	{
		if ( !init )
			return;
		
		SetBool.Request req = new SetBool.Request ();
		SetBool.Response resp = new SetBool.Response ();
		if ( resetService.call ( req, ref resp ) )
		{
			Debug.Log ( "reset called!" );
		}
	}

	public void SetGravity (bool gravity)
	{
		if ( !init )
			return;

		_useGravity = gravity;
		new Thread ( _SetGravity ).Start ();
	}

	void _SetGravity ()
	{
		SetBool.Request req = new SetBool.Request ();
		SetBool.Response resp = new SetBool.Response ();
		req.data = _useGravity;
		if ( gravityService.call ( req, ref resp ) )
		{
			Debug.Log ( "gravity service called: " + _useGravity );
		}
	}

	void PoseCallback (PoseStamped poseInfo)
	{
		Position = poseInfo.pose.position.ToUnity ();
		Rotation = poseInfo.pose.orientation.ToUnity ();
//		Debug.Log ( "Pose " + poseInfo.header.Seq );
//		Debug.Log ( "Pose " + poseInfo.header.Frame_id + " " + poseInfo.header.Stamp.data.toSec () );
//		Debug.Log ( "Pose " + poseInfo.header.Stamp.data.toSec () );
	}

	void ImuCallback (Imu imu)
	{
//		Debug.Log ( "Imu " + imu.header.Seq );
	}
}