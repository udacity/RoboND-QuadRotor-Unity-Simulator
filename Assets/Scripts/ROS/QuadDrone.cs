// comment this out to not collect time samples for timestamp test
// #define TIMETEST


using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Ros_CSharp;
using hector_uav_msgs;
using Messages;
using Twist = Messages.geometry_msgs.Twist;
using GVector3 = Messages.geometry_msgs.Vector3;
using PoseStamped = Messages.geometry_msgs.PoseStamped;
using Wrench = Messages.geometry_msgs.Wrench;
using Imu = Messages.sensor_msgs.Imu;
using Image = Messages.sensor_msgs.Image;
using Path = Messages.nav_msgs.Path;
using SetBool = Messages.std_srvs.SetBool;
using Empty = Messages.std_srvs.Empty;
using Trigger = Messages.std_srvs.Trigger;
using SetPose = Messages.quad_controller.SetPose;
using GetPath = Messages.quad_controller.GetPath;
using SetPath = Messages.quad_controller.SetPath;



/*
 * QuadDrone: receives messages from a QRKeyboardTeleop, and applies force/torque to a QuadController
 */

public class QuadDrone : MonoBehaviour
{
	public QuadController droneController;
	public SimpleQuadController inputCtrl;
	public PathFollower pather;
	public bool active;

	// node to link everything up to ros
	NodeHandle nh;
	// service to set drone orientation. still working on getting it to work
//	ServiceServer resetOrientSrv;
	// service to turn drone motor on/off. still working on it..
	ServiceServer enableMotorSrv;
	// publishers for drone and camera info
	Publisher<PoseStamped> posePub;
	Publisher<Imu> imuPub;
	Publisher<Image> imgPub;
	// service for the drone's current path
	ServiceServer pathSrv;
	// subscribers for drone movement
	Subscriber<Twist> twistSub;
	Subscriber<Wrench> wrenchSub;
	// thread to run the publishers on
	Thread pubThread;

	// services to enable/disable gravity and constrain movement
	ServiceServer gravitySrv;
	ServiceServer constrainForceXSrv;
	ServiceServer constrainForceYSrv;
	ServiceServer constrainForceZSrv;
	ServiceServer constrainTorqueXSrv;
	ServiceServer constrainTorqueYSrv;
	ServiceServer constrainTorqueZSrv;
	ServiceServer triggerResetSrv;
	ServiceServer setPoseSrv;
	ServiceServer clearPathSrv;
	ServiceServer setPathSrv;

	uint frameSeq = 0;
	bool waitStartPath;

	void Awake ()
	{
		if ( !active )
		{
			enabled = false;
			return;
		}

		inputCtrl = GetComponent<SimpleQuadController> ();
		pather = GetComponent<PathFollower> ();
	}

	void Start ()
	{
		ROSController.StartROS ( OnRosInit );
	}

	void LateUpdate ()
	{
		if ( waitStartPath )
		{
			DelayedStartPath ();
		}
	}

	void OnRosInit ()
	{
		nh = ROS.GlobalNodeHandle;
//		nh = new NodeHandle ( "~" );
		pathSrv = nh.advertiseService<GetPath.Request, GetPath.Response> ( "quad_rotor/get_path", GetPathService );
//		setOrientSrv = nh.advertiseService<Messages.std_srvs.Empty.Request> ("quad_rotor/reset_orientation", TriggerReset)
//		enableMotorSrv = nh.advertiseService<EnableMotors.Request, EnableMotors.Response> ( "enable_motors", OnEnableMotors );
		nh.setParam ( "control_mode", "wrench" ); // for now force twist mode
		twistSub = nh.subscribe<Twist> ( "quad_rotor/cmd_vel", 10, TwistCallback );
		wrenchSub = nh.subscribe<Wrench> ( "quad_rotor/cmd_force", 10, WrenchCallback );
		posePub = nh.advertise<PoseStamped> ( "quad_rotor/pose", 10, false );
		imuPub = nh.advertise<Imu> ( "quad_rotor/imu", 10, false );
//		imgPub = nh.advertise<Image> ( "quad_rotor/image", 10, false );
		pubThread = new Thread ( PublishAll );
		pubThread.Start ();

		gravitySrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/gravity", GravityService );
		constrainForceXSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/x_force_constrained", ConstrainForceX );
		constrainForceYSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/y_force_constrained", ConstrainForceY );
		constrainForceZSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/z_force_constrained", ConstrainForceZ );
		constrainTorqueXSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/x_torque_constrained", ConstrainTorqueX );
		constrainTorqueYSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/y_torque_constrained", ConstrainTorqueY );
		constrainTorqueZSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/z_torque_constrained", ConstrainTorqueZ );
		triggerResetSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/reset_orientation", TriggerReset );
//		triggerResetSrv = nh.advertiseService<Empty.Request, Empty.Response> ( "quad_rotor/reset_orientation", TriggerReset );
		setPoseSrv = nh.advertiseService<SetPose.Request, SetPose.Response> ( "quad_rotor/set_pose", SetPoseService );
		clearPathSrv = nh.advertiseService<SetBool.Request, SetBool.Response> ( "quad_rotor/clear_path", ClearPathService );
		setPathSrv = nh.advertiseService<SetPath.Request, SetPath.Response> ( "quad_rotor/set_path", SetPathService );
	}

	bool OnEnableMotors (EnableMotors.Request req, ref EnableMotors.Response resp)
	{
		if ( droneController != null )
		{
			droneController.MotorsEnabled = req.enable;
			resp.success = true;
			return true;
		}

		resp.success = false;
		return false;
	}

	void TwistCallback (Twist msg)
	{
		if ( inputCtrl.active )
			return;

		Vector3 linear = msg.linear.ToUnityVector ();
		Vector3 angular = msg.angular.ToUnityVector ();
		if ( droneController != null )
		{
			if ( !droneController.MotorsEnabled )
				droneController.MotorsEnabled = true;
			droneController.SetLinearVelocity ( linear, true );
			droneController.SetAngularVelocity ( angular, true );
		}
	}

	void WrenchCallback (Wrench msg)
	{
		if ( inputCtrl.active )
			return;

		Vector3 force = msg.force.ToUnityVector ();
		Vector3 torque = msg.torque.ToUnityVector ();
		if ( droneController != null )
		{
			if ( !droneController.MotorsEnabled )
				droneController.MotorsEnabled = true;
			droneController.ApplyMotorForce ( force, true );
			droneController.ApplyMotorTorque ( torque, true );
		}
	}

	void PublishAll ()
	{
		int sleep = 1000 / 60;
		while ( ROS.ok && !ROS.shutting_down )
		{
			PoseStamped ps = new PoseStamped ();
			ps.pose = new Messages.geometry_msgs.Pose ();
			ps.pose.position = new Messages.geometry_msgs.Point ( droneController.Position.ToRos () );
			ps.pose.orientation = new Messages.geometry_msgs.Quaternion ( droneController.Rotation.ToRos () );
			posePub.publish ( ps );

			Imu imu = new Imu ();
			imu.angular_velocity_covariance = new double[9] { -1, 0, 0, 0, 0, 0, 0, 0, 0 };
			imu.linear_acceleration_covariance = new double[9] { -1, 0, 0, 0, 0, 0, 0, 0, 0 };
			imu.orientation_covariance = new double[9] { -1, 0, 0, 0, 0, 0, 0, 0, 0 };
			imu.angular_velocity = new GVector3 ( droneController.AngularVelocity.ToRos () );
			imu.linear_acceleration = new GVector3 ( droneController.LinearAcceleration.ToRos () );
			imu.orientation = new Messages.geometry_msgs.Quaternion ( droneController.Rotation.ToRos () );
			imuPub.publish ( imu );

			Thread.Sleep ( sleep );
		}
	}

	void OldPublishAll ()
	{
		// pose info
		PoseStamped ps = new PoseStamped ();
//		ps.header = new Messages.std_msgs.Header ();
//		ps.header.Stamp = ROS.GetTime ();
//		ps.header.Frame_id = "";
		ps.pose = new Messages.geometry_msgs.Pose ();
		Imu imu = new Imu ();
		// imu info
//		imu.header = new Messages.std_msgs.Header ( ps.header );
		imu.angular_velocity_covariance = new double[9] { -1, 0, 0, 0, 0, 0, 0, 0, 0 };
		imu.linear_acceleration_covariance = new double[9] { -1, 0, 0, 0, 0, 0, 0, 0, 0 };
		imu.orientation_covariance = new double[9] { -1, 0, 0, 0, 0, 0, 0, 0, 0 };
		// image info
		Image img = new Image ();
//		img.header = new Messages.std_msgs.Header ( ps.header );
		img.width = (uint) QuadController.ImageWidth;
		img.height = (uint) QuadController.ImageHeight;
		img.encoding = "mono16"; // "rgba8";
		img.step = img.width * 2; // * 4
		img.is_bigendian = 1;


//		int sleep = 1000 / 30;
//		int sleep = 1000 / 2;
		int sleep = 1000 / 60;
//		int sleep = 1000 / 120;
//		int sleep = 1000 / 250;
		#if TIMETEST
		Queue<TimeData> tdq = new Queue<TimeData> ();
		Queue<long> tq = new Queue<long> ();
		#endif
		while ( ROS.ok && !ROS.shutting_down )
		{
			#if TIMETEST
			tdq.Enqueue ( ROS.GetTime ().data );
			tq.Enqueue ( System.DateTime.Now.Ticks );
			#endif
			// publish pose
///*
//			ps.header = new Messages.std_msgs.Header ();
//			ps.header.Stamp = ROS.GetTime ();
//			ps.header.Frame_id = ps.header.Stamp.data.toSec ().ToString ();
//			ps.header.Frame_id = "";
//			ps.header.Seq = frameSeq;
			ps.pose.position = new Messages.geometry_msgs.Point ( droneController.Position.ToRos () );
			ps.pose.orientation = new Messages.geometry_msgs.Quaternion ( droneController.Rotation.ToRos () );
			posePub.publish ( ps );
			if ( ps.header != null )
				Debug.Log ( "Send Time " + ps.header.Stamp.data.toSec () );
			else
				Debug.Log ( "Send time null header" );
//			*/

			// publish imu
///*
//			imu.header = new Messages.std_msgs.Header ();
//			imu.header.Frame_id = "";
//			imu.header.Seq = frameSeq++;
//			imu.header.Stamp = ROS.GetTime ();
//			imu.header.Stamp = ps.header.Stamp;
			imu.angular_velocity = new GVector3 ( droneController.AngularVelocity.ToRos () );
			imu.linear_acceleration = new GVector3 ( droneController.LinearAcceleration.ToRos () );
			imu.orientation = new Messages.geometry_msgs.Quaternion ( droneController.Rotation.ToRos () );
			imuPub.publish ( imu );
//			Debug.Log ( "Send Imu " + imu.header.Seq );
			
			// publish image
//			img.data = droneController.GetImageData ();
//			imgPub.publish ( img );
//			*/

			Thread.Sleep ( sleep );
		}

		#if TIMETEST
		while ( tdq.Count > 0 )
		{
			TimeData td = tdq.Dequeue ();
			Debug.Log ( "t: " + td.sec + " " + td.nsec );
		}
		while ( tq.Count > 0 )
			Debug.Log ( "ticks: " + tq.Dequeue () );
		#endif
	}

	bool GetPathService (GetPath.Request req, ref GetPath.Response resp)
	{
		Debug.Log ( "getpath service called!" );
		Path path = new Path ();
		path.header = new Messages.std_msgs.Header ();
		path.header.Frame_id = "global";
		path.header.Stamp = ROS.GetTime ();
		path.header.Seq = 0;
		Pathing.PathSample[] samples = PathPlanner.GetPath ();
		int count = samples.Length;
		path.poses = new PoseStamped[ count ];
		Debug.Log ( "sending " + count + " samples" );
		for ( int i = 0; i < count; i++ )
		{
			PoseStamped pst = new PoseStamped ();
			pst.header = new Messages.std_msgs.Header ();
			pst.header.Frame_id = "local";
			pst.header.Stamp = ROS.GetTime ();
			pst.header.Seq = (uint) i;
			pst.pose = new Messages.geometry_msgs.Pose ();
			pst.pose.position = new Messages.geometry_msgs.Point ( samples [ i ].position.ToRos () );
			pst.pose.orientation = new Messages.geometry_msgs.Quaternion ( samples [ i ].orientation.ToRos () );
			path.poses [ i ] = pst;
		}
		resp.path = path;

		return true;
	}

	bool GravityService (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "gravity service!" );
		droneController.UseGravity = req.data;
		resp.message = droneController.UseGravity.ToString ();
		resp.success = true;

//		droneController.TriggerReset ();
		droneController.ApplyMotorForce ( Vector3.zero );
		droneController.ApplyMotorTorque ( Vector3.zero );

		return true;
	}

	bool ConstrainForceX (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "constrain_force_x service!" );
		droneController.ConstrainForceX = req.data;
		resp.message = droneController.ConstrainForceX.ToString ();
		resp.success = true;

//		droneController.TriggerReset ();
		droneController.ApplyMotorForce ( Vector3.zero );
		droneController.ApplyMotorTorque ( Vector3.zero );

		return true;
	}

	bool ConstrainForceY (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "constrain_force_y service!" );
		droneController.ConstrainForceY = req.data;
		resp.message = droneController.ConstrainForceY.ToString ();
		resp.success = true;

//		droneController.TriggerReset ();
		droneController.ApplyMotorForce ( Vector3.zero );
		droneController.ApplyMotorTorque ( Vector3.zero );

		return true;
	}

	bool ConstrainForceZ (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "constrain_force_z service!" );
		droneController.ConstrainForceZ = req.data;
		resp.message = droneController.ConstrainForceZ.ToString ();
		resp.success = true;

//		droneController.TriggerReset ();
		droneController.ApplyMotorForce ( Vector3.zero );
		droneController.ApplyMotorTorque ( Vector3.zero );

		return true;
	}

	bool ConstrainTorqueX (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "constrain_torque_x service!" );
		droneController.ConstrainTorqueX = req.data;
		resp.message = droneController.ConstrainTorqueX.ToString ();
		resp.success = true;
		return true;
	}

	bool ConstrainTorqueY (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "constrain_torque_y service!" );
		droneController.ConstrainTorqueY = req.data;
		resp.message = droneController.ConstrainTorqueY.ToString ();
		resp.success = true;

//		droneController.TriggerReset ();
		droneController.ApplyMotorForce ( Vector3.zero );
		droneController.ApplyMotorTorque ( Vector3.zero );

		return true;
	}

	bool ConstrainTorqueZ (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "constrain_torque_z service!" );
		droneController.ConstrainTorqueZ = req.data;
		resp.message = droneController.ConstrainTorqueZ.ToString ();
		resp.success = true;

//		droneController.TriggerReset ();
		droneController.ApplyMotorForce ( Vector3.zero );
		droneController.ApplyMotorTorque ( Vector3.zero );

		return true;
	}

	bool TriggerReset (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "reset orientation service!" );
		droneController.TriggerReset ();
		droneController.ApplyMotorForce ( Vector3.zero );
		droneController.ApplyMotorTorque ( Vector3.zero );
		return true;
	}

	bool SetPoseService (SetPose.Request req, ref SetPose.Response resp)
	{
		Debug.Log ( "setpose service!" );
		// first ToUnity goes from PointQuaternion to Vector3/Quaternion, and 2nd goes from Ros' coord system to Unity's
		droneController.SetPositionAndOrientation ( req.pose.position.ToUnity ().ToUnity (), req.pose.orientation.ToUnity ().ToUnity () );
		resp.message = "";
		resp.success = true;
		return true;
	}

	bool ClearPathService (SetBool.Request req, ref SetBool.Response resp)
	{
		Debug.Log ( "clear path service!" );
		PathPlanner.Clear ();
		resp.message = "";
		resp.success = true;
		return true;
	}

	bool SetPathService (SetPath.Request req, ref SetPath.Response resp)
	{
		Debug.Log ( "Set path service!" );
		PathPlanner.Clear ( true );

		if ( req.path.poses.Length > 1 )
		{
			string info = "";
			int idx = 0;

			PathPlanner.Clear ( true );
			List<Pathing.PathSample> nodes = new List<Pathing.PathSample> ();
			foreach ( PoseStamped ps in req.path.poses )
			{
				info += "pose " + ( idx++ ) + "position: " + ps.pose.position + " orientation: " + ps.pose.orientation + "\n";
				nodes.Add ( new Pathing.PathSample ( ps.pose.position.ToUnity ().ToUnity (), ps.pose.orientation.ToUnity ().ToUnity (), -1 ) );
//				PathPlanner.AddNode ( ps.pose.position.ToUnity ().ToUnity (), ps.pose.orientation.ToUnity ().ToUnity () );
			}
			Debug.Log ( info );
			PathPlanner.SetPath ( nodes );
			
			resp.success = true;
			resp.message = "";

			waitStartPath = true;
			
		} else
		{
			resp.success = false;
			resp.message = "Need to include at least 2 poses for a path";
		}

		return true;
	}

	void DelayedStartPath ()
	{
		droneController.ResetOrientation ();
		pather.SetPath ( new Pathing.Path ( PathPlanner.GetPath () ) );
		PathPlanner.Clear ( false ); // clear the path but keep the visualization
		waitStartPath = false;
	}
}