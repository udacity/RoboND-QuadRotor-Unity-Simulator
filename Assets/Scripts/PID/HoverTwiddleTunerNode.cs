using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Ros_CSharp;
using Empty = Messages.std_srvs.Empty;
using SetBool = Messages.std_srvs.SetBool;
using PoseStamped = Messages.geometry_msgs.PoseStamped;
using Pose = Messages.geometry_msgs.Pose;
using Wrench = Messages.geometry_msgs.Wrench;
using SetPose = Messages.quad_controller.SetPose;
using SetInt = Messages.quad_controller.SetInt;

public class HoverTestRun
{
	bool isStarted;
	List<double[]> testData;
	int oscillationCount;
	double maxDuration;
	double startTime;
	double duration;

	public HoverTestRun ()
	{
		Reset ();
	}

	public void AddPose (PoseStamped ps)
	{
		if ( !isStarted )
			return;

		if ( startTime == 0 )
		{
			startTime = ps.header.Stamp.data.toSec ();
			return;
		}

		testData.Add ( new double[] {
			ps.header.Stamp.data.toSec (),
			ps.pose.position.z
		} );
	}

	public double ComputeTotalError (double setPoint)
	{
		double totalError = 0;
		foreach ( var item in testData )
			totalError += Math.Abs ( setPoint - item [ 1 ] );
		return totalError;
	}

	public bool IsFinished ()
	{
		if ( !isStarted || testData.Count == 0 )
			return false;

		// test is finished if we've reached max duration
		double duration = testData [ testData.Count - 1 ] [ 0 ] - startTime;
		if ( duration > maxDuration )
		{
			isStarted = false;
			return true;
		}

		return false;
	}

	public void Reset ()
	{
		isStarted = false;
		testData = new List<double[]> ();
		oscillationCount = 0;
		maxDuration = 0;
		startTime = 0;
		duration = 0;
	}

	public void Start ()
	{
		isStarted = true;
	}
}