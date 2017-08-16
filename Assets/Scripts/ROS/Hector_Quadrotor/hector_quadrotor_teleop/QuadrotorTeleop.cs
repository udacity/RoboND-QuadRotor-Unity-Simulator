//=================================================================================================
// Copyright (c) 2012-2016, Institute of Flight Systems and Automatic Control,
// Technische Universität Darmstadt.
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of hector_quadrotor nor the names of its contributors
//       may be used to endorse or promote products derived from this software
//       without specific prior written permission.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//=================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
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

public class QuadrotorTeleop : MonoBehaviour
{
	public struct SAxis
	{
		public int axis;
		public double factor;
		public double offset;
	};

	public struct SButton
	{
		public int button;
	};

	public struct SAxes
	{
		public SAxis x;
		public SAxis y;
		public SAxis z;
		public SAxis thrust;
		public SAxis yaw;
	}

	public struct SButtons
	{
		public SButton slow;
		public SButton go;
		public SButton stop;
		public SButton interrupt;
	}



	NodeHandle nh;
	Subscriber<Joy> joySubscriber;
	Publisher<TwistStamped> velocityPublisher;
	Publisher<AttitudeCommand> attitudePublisher;
	Publisher<YawRateCommand> yawRatePublisher;
	Publisher<ThrustCommand> thrustPublisher;
	ServiceClient<EnableMotors> motorEnableService;
	LandingClient landingClient;
	TakeoffClient takeoffClient;
	PoseClient poseClient;

	PoseStamped pose;
	double yaw;
	double slowFactor;
	string baseLinkFrame, baseStabilizedFrame, worldFrame;
	SButtons sButtons;
	SAxes sAxes;


	public void joyAttitudeCallback (Joy joy)
	{
		AttitudeCommand attitude = new AttitudeCommand ();
		ThrustCommand thrust = new ThrustCommand ();
		YawRateCommand yawrate = new YawRateCommand ();

		attitude.header.Stamp = thrust.header.Stamp = yawrate.header.Stamp = ROS.GetTime ();
		attitude.header.Frame_id = yawrate.header.Frame_id = baseStabilizedFrame;
		thrust.header.Frame_id = baseLinkFrame;

		attitude.roll = (float) ( -getAxis ( joy, sAxes.y ) * Math.PI / 180.0 );
		attitude.pitch = (float) ( getAxis ( joy, sAxes.x ) * Math.PI / 180.0 );
		if (getButton(joy, sButtons.slow))
		{
			attitude.roll *= (float) slowFactor;
			attitude.pitch *= (float) slowFactor;
		}
		attitudePublisher.publish(attitude);

		thrust.thrust = (float) getAxis(joy, sAxes.thrust);
		thrustPublisher.publish(thrust);

		yawrate.turnrate = (float) ( getAxis ( joy, sAxes.yaw ) * Math.PI / 180.0 );
		if (getButton(joy, sButtons.slow))
		{
			yawrate.turnrate *= (float) slowFactor;
		}

		yawRatePublisher.publish(yawrate);

		if (getButton(joy, sButtons.stop))
		{
			enableMotors(false);
		}
		else if (getButton(joy, sButtons.go))
		{
			enableMotors(true);
		}
	}

	void joyTwistCallback(Joy joy)
	{
		TwistStamped velocity = new TwistStamped ();
		velocity.header.Frame_id = baseStabilizedFrame;
		velocity.header.Stamp = ROS.GetTime ();

		velocity.twist.linear.x = getAxis(joy, sAxes.x);
		velocity.twist.linear.y = getAxis(joy, sAxes.y);
		velocity.twist.linear.z = getAxis(joy, sAxes.z);
		velocity.twist.angular.z = getAxis(joy, sAxes.yaw) * Math.PI/180.0;
		if (getButton(joy, sButtons.slow))
		{
			velocity.twist.linear.x *= slowFactor;
			velocity.twist.linear.y *= slowFactor;
			velocity.twist.linear.z *= slowFactor;
			velocity.twist.angular.z *= slowFactor;
		}
		velocityPublisher.publish(velocity);

		if (getButton(joy, sButtons.stop))
		{
			enableMotors(false);
		}
		else if (getButton(joy, sButtons.go))
		{
			enableMotors(true);
		}
	}

	void joyPoseCallback(Joy joy)
	{
		Messages.std_msgs.Time now = ROS.GetTime ();
		double dt = 0.0;
		if ( ( pose.header.Stamp.data.sec == 0 && pose.header.Stamp.data.nsec == 0 ) )
		{
			TimeData td = ( now - pose.header.Stamp ).data;
			double sec = td.toSec ();
			dt = Mathf.Max ( 0, Mathf.Min ( 1f, (float) sec ) );
		}

		if (getButton(joy, sButtons.go))
		{
			pose.header.Stamp = ROS.GetTime ();
			pose.header.Frame_id = worldFrame;
			pose.pose.position.x += (Math.Cos(yaw) * getAxis(joy, sAxes.x) - Math.Sin(yaw) * getAxis(joy, sAxes.y)) * dt;
			pose.pose.position.y += (Math.Cos(yaw) * getAxis(joy, sAxes.y) + Math.Sin(yaw) * getAxis(joy, sAxes.x)) * dt;
			pose.pose.position.z += getAxis(joy, sAxes.z) * dt;
			yaw += getAxis(joy, sAxes.yaw) * Math.PI/180.0 * dt;
			tf.net.emQuaternion q = tf.net.emQuaternion.FromRPY ( new tf.net.emVector3 ( 0, 0, yaw ) );

			pose.pose.orientation = q.ToMsg ();


			PoseGoal goal = new PoseGoal ();
			goal.target_pose = pose;
			poseClient.sendGoal(goal);
		}
		if (getButton(joy, sButtons.interrupt))
		{
			poseClient.cancelGoalsAtAndBeforeTime(ROS.GetTime ());
		}
		if (getButton(joy, sButtons.stop))
		{
			landingClient.sendGoalAndWait ( new LandingGoal (), new Messages.std_msgs.Duration ( new TimeData ( 10, 0 ) ), new Messages.std_msgs.Duration ( new TimeData ( 10, 0 ) ) );
		}
	}

	public double getAxis(Joy joy, SAxis axis)
	{
		if (axis.axis == 0 || Math.Abs(axis.axis) > joy.axes.Length)
		{
			ROS.Error("Axis " + axis.axis + " out of range, joy has " + joy.axes.Length + " axes");
			return 0;
		}

		double output = Math.Abs(axis.axis) / axis.axis * joy.axes[Math.Abs(axis.axis) - 1] * axis.factor + axis.offset;

		// TODO keep or remove deadzone? may not be needed
		// if (Math.Abs(output) < axis.max_ * 0.2)
		// {
		//   output = 0.0;
		// }

		return output;
	}

	public bool getButton(Joy joy, SButton button)
	{
		if (button.button <= 0 || button.button > joy.buttons.Length)
		{
			ROS.Error("Button " + button.button + " out of range, joy has " + joy.buttons.Length + " buttons");
			return false;
		}

		return joy.buttons[button.button - 1] > 0;
	}

	public bool enableMotors(bool enable)
	{
//		if ( !motorEnableService )
//		if (!motorEnableService.waitForExistence(Duration(5.0)))
//		{
//			ROS.Warn("Motor enable service not found");
//			return false;
//		}

		EnableMotors srv = new EnableMotors ();
		srv.req.enable = enable;
		return motorEnableService.call(srv);
	}

	public void stop()
	{
		if (velocityPublisher.getNumSubscribers() > 0)
		{
			velocityPublisher.publish(new TwistStamped());
		}
		if (attitudePublisher.getNumSubscribers() > 0)
		{
			attitudePublisher.publish(new AttitudeCommand());
		}
		if (thrustPublisher.getNumSubscribers() > 0)
		{
			thrustPublisher.publish(new ThrustCommand());
		}
		if (yawRatePublisher.getNumSubscribers() > 0)
		{
			yawRatePublisher.publish(new YawRateCommand());
		}
	}

	void Awake ()
	{
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

		nh.param<int>("x_axis", ref sAxes.x.axis, 5);
		nh.param<int>("y_axis", ref sAxes.y.axis, 4);
		nh.param<int>("z_axis", ref sAxes.z.axis, 2);
		nh.param<int>("thrust_axis", ref sAxes.thrust.axis, -3);
		nh.param<int>("yaw_axis", ref sAxes.yaw.axis, 1);

		nh.param<double>("yaw_velocity_max", ref sAxes.yaw.factor, 90.0);

		nh.param<int>("slow_button", ref sButtons.slow.button, 4);
		nh.param<int>("go_button", ref sButtons.go.button, 1);
		nh.param<int>("stop_button", ref sButtons.stop.button, 2);
		nh.param<int>("interrupt_button", ref sButtons.interrupt.button, 3);
		nh.param<double>("slow_factor", ref slowFactor, 0.2);

		// TODO dynamic reconfig
		string control_mode = "";
		nh.param<string>("control_mode", ref control_mode, "twist");

		NodeHandle robot_nh = ROS.GlobalNodeHandle;
//		NodeHandle robot_nh = new NodeHandle ();

		// TODO factor out
		robot_nh.param<string>("base_link_frame", ref baseLinkFrame, "base_link");
		robot_nh.param<string>("world_frame", ref worldFrame, "world");
		robot_nh.param<string>("base_stabilized_frame", ref baseStabilizedFrame, "base_stabilized");

		if (control_mode == "attitude")
		{
			nh.param<double>("pitch_max", ref sAxes.x.factor, 30.0);
			nh.param<double>("roll_max", ref sAxes.y.factor, 30.0);
			nh.param<double>("thrust_max", ref sAxes.thrust.factor, 10.0);
			nh.param<double>("thrust_offset", ref sAxes.thrust.offset, 10.0);
			joySubscriber = nh.subscribe<Joy> ( "joy", 1, joyAttitudeCallback );
			attitudePublisher = robot_nh.advertise<AttitudeCommand> ( "command/attitude", 10 );
			yawRatePublisher = robot_nh.advertise<YawRateCommand> ( "command/yawrate", 10 );
			thrustPublisher = robot_nh.advertise<ThrustCommand> ( "command/thrust", 10 );
		}
		else if (control_mode == "velocity")
		{
			nh.param<double>("x_velocity_max", ref sAxes.x.factor, 2.0);
			nh.param<double>("y_velocity_max", ref sAxes.y.factor, 2.0);
			nh.param<double>("z_velocity_max", ref sAxes.z.factor, 2.0);

			joySubscriber = nh.subscribe<Joy> ( "joy", 1, joyTwistCallback );
			velocityPublisher = robot_nh.advertise<TwistStamped> ( "command/twist", 10 );
		}
		else if (control_mode == "position")
		{
			nh.param<double>("x_velocity_max", ref sAxes.x.factor, 2.0);
			nh.param<double>("y_velocity_max", ref sAxes.y.factor, 2.0);
			nh.param<double>("z_velocity_max", ref sAxes.z.factor, 2.0);

			joySubscriber = nh.subscribe<Joy> ( "joy", 1, joyPoseCallback );

			pose.pose.position.x = 0;
			pose.pose.position.y = 0;
			pose.pose.position.z = 0;
			pose.pose.orientation.x = 0;
			pose.pose.orientation.y = 0;
			pose.pose.orientation.z = 0;
			pose.pose.orientation.w = 1;
		}
		else
		{
			ROS.Error("Unsupported control mode: " + control_mode);
		}

		motorEnableService = robot_nh.serviceClient<EnableMotors> ( "enable_motors" );
		takeoffClient = new TakeoffClient ( robot_nh, "action/takeoff" );
		landingClient = new LandingClient ( robot_nh, "action/landing" );
		poseClient = new PoseClient ( robot_nh, "action/pose" );
	}
}