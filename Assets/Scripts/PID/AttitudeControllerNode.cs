using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Ros_CSharp;
using Imu = Messages.sensor_msgs.Imu;
using Wrench = Messages.geometry_msgs.Wrench;
using Pose = Messages.geometry_msgs.Pose;
using PoseStamped = Messages.geometry_msgs.PoseStamped;
using Vec3 = Messages.geometry_msgs.Vector3;
using Float64 = Messages.std_msgs.Float64;
using RosTime = Messages.std_msgs.Time;

[System.Serializable]
public class AttitudeControllerNode
{
	public Action<Vec3> torqueCallback;
	public Action<double> thrustCallback;

	public PIDController rollController;
	public PIDController pitchController;
	public PIDController yawController;
	RosTime prevTime;
	Imu lastImu;
	double zThrust;

	public AttitudeControllerNode ()
	{
		prevTime = new RosTime ( new Messages.TimeData ( 0, 0 ) );

		// Roll controller
		double rollKp = 0;
		double rollKi = 0;
		double rollKIMax = 0;
		double rollKd = 0;
		rollController = new PIDController ( rollKp, rollKi, rollKd, rollKIMax, 0 );

		// Pitch controller
		double pitchKp = 0;
		double pitchKi = 0;
		double pitchKIMax = 0;
		double pitchKd = 0;
		pitchController = new PIDController ( pitchKp, pitchKi, pitchKd, pitchKIMax, 0 );

		// Yaw controller
		double yawKp = 0;
		double yawKi = 0;
		double yawKIMax = 0;
		double yawKd = 0;
		yawController = new PIDController ( yawKp, yawKi, yawKd, yawKIMax, 0 );

		lastImu = new Imu ();
		zThrust = 0;
	}

	public void Init ()
	{
		prevTime = ROS.GetTime ();
		double t = prevTime.data.toSec ();
		rollController.SetStartTime ( t );
		pitchController.SetStartTime ( t );
		yawController.SetStartTime ( t );
	}

	public void SetStartTime (double startTime)
	{
		pitchController.SetStartTime ( startTime );
		rollController.SetStartTime ( startTime );
		yawController.SetStartTime ( startTime );
	}

	void UpdateAttitude (Vec3 attVector)
	{
		rollController.SetTarget ( attVector.x );
		pitchController.SetTarget ( attVector.y );
		yawController.SetTarget ( attVector.z );
	}

	void UpdateThrust (double thrust)
	{
		zThrust = thrust;
	}

	public void UpdateImu (Imu imu)
	{
		lastImu = imu;
		Update ();
	}

	void Update ()
	{
		Quaternion qTemp = lastImu.orientation.ToUnity ();
		Vector3 euler = qTemp.eulerAngles;
		double rollCmd = rollController.Update ( euler.x, ROS.GetTime ().data.toSec () );
		double pitchCmd = pitchController.Update ( euler.y, ROS.GetTime ().data.toSec () );
		double yawCmd = yawController.Update ( euler.z, ROS.GetTime ().data.toSec () );

		string s = string.Format ( "pry: {0},{1},{2} orientation: {3},{4},{5},{6}", euler.x, euler.y, euler.z, qTemp.x, qTemp.y, qTemp.z, qTemp.w );
		Debug.Log ( s );
		s = string.Format ( "pry in degrees: {0},{1},{2}", euler.x * Mathf.Rad2Deg, euler.y * Mathf.Rad2Deg, euler.z * Mathf.Rad2Deg );
		Debug.Log ( s );

		Vec3 v = new Vec3 ();
		v.x = rollCmd;
		v.y = pitchCmd;
		v.z = yawCmd;

		torqueCallback ( v );
		thrustCallback ( zThrust );
	}
}