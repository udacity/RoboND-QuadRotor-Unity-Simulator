using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Ros_CSharp;
using Wrench = Messages.geometry_msgs.Wrench;
using PoseStamped = Messages.geometry_msgs.PoseStamped;
using Point = Messages.geometry_msgs.Point;
using Vec3 = Messages.geometry_msgs.Vector3;
using Float64 = Messages.std_msgs.Float64;

//from dynamic_reconfigure.server import Server
//from quad_controller.cfg import position_controller_paramsConfig

[System.Serializable]
public class PositionControllerNode
{
	bool firstPoseReceived;
	public PIDController xController;
	public PIDController yController;
	public PIDController zController;

	public Action<Vec3> torqueCallback;
	public Action<double> thrustCallback;

	public PositionControllerNode ()
	{
		firstPoseReceived = false;

		// x config
		double xKp = 0;
		double xKi = 0;
		double xKIMax = 0;
		double xKd = 0;
		xController = new PIDController ( xKp, xKi, xKd, xKIMax, 0 );

		// y config
		double yKp = 0;
		double yKi = 0;
		double yKIMax = 0;
		double yKd = 0;
		yController = new PIDController ( xKp, xKi, xKd, xKIMax, 0 );

		// z config
		double zKp = 0;
		double zKi = 0;
		double zKIMax = 0;
		double zKd = 0;
		zController = new PIDController ( xKp, xKi, xKd, xKIMax, 0 );
	}

	double Clamp (double value, double min, double max)
	{
		if ( value > max )
			value = max;
		else
		if ( value < min )
			value = min;
		return value;
	}

	public void SetStartTime (double startTime)
	{
		xController.SetStartTime ( startTime );
		yController.SetStartTime ( startTime );
		zController.SetStartTime ( startTime );
	}

	public void SetGoal (Point goal)
	{
		xController.SetTarget ( goal.x );
		yController.SetTarget ( goal.y );
		zController.SetTarget ( goal.z );
	}

	public void UpdatePose (PoseStamped ps)
	{
		if ( !firstPoseReceived )
		{
			firstPoseReceived = true;
			SetGoal ( ps.pose.position );
		}

		Vec3 rpyCmd = new Vec3 ();

		double radian = System.Math.PI / 180;
		double t = ps.header.Stamp.data.toSec ();

		// Control Roll to to move along Y
		double rollCmd = xController.Update ( ps.pose.position.x, t );
		rollCmd = Clamp ( rollCmd, -10.0 * radian, 10.0 * radian );

		// Control Pitch to move along X
		double pitchCmd = yController.Update ( ps.pose.position.y, t );
		pitchCmd = Clamp ( pitchCmd, -10.0 * radian, 10.0 * radian );

		// Control Thrust to move along Z
		double thrust = zController.Update ( ps.pose.position.z, t );

		rpyCmd.x = rollCmd;
		rpyCmd.y = pitchCmd;

		string s = string.Format ( "r: {0}, p: {1}, thrust: {2}", rollCmd, pitchCmd, thrust );
		Debug.Log ( s );

		// publish
		torqueCallback ( rpyCmd );
		thrustCallback ( thrust );
	}
}