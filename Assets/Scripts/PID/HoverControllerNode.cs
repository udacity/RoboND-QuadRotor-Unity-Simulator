using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Ros_CSharp;
using Wrench = Messages.geometry_msgs.Wrench;
using PoseStamped = Messages.geometry_msgs.PoseStamped;
using RosTime = Messages.std_msgs.Time;

[System.Serializable]
public class HoverControllerNode
{
	public PIDController controller;

	public Action<double> thrustCallback;

	public HoverControllerNode ()
	{
		// PID params
		double kiMax = 20;
		double kp = 0;
		double ki = 0;
		double kd = 0;

		controller = new PIDController ( kp, ki, kd, kiMax, 0 );
	}

	public void SetStartTime (double startTime)
	{
		controller.SetStartTime ( startTime );
	}

	public void SetGoal (double target)
	{
		controller.SetTarget ( target );
	}

	public void UpdatePose (PoseStamped pose)
	{
		double zCmd = controller.Update ( pose.pose.position.z, pose.header.Stamp.data.toSec () );

		thrustCallback ( zCmd );
	}
}