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
using Ros_CSharp;
using Messages.std_msgs;
using Messages.geometry_msgs;
using Messages.sensor_msgs;
using Messages.nav_msgs;
using Messages.tf2_msgs;
using hector_uav_msgs;
using System.Threading;
using Header = Messages.std_msgs.Header; // clarify ambiguity between this and Ros_CSharp.Header
// find and port this...
//#include <urdf_parser/urdf_parser.h>

namespace hector_quadrotor_interface
{
	public class ImuSubscriberHelper
	{
		Imu imu_;
		Subscriber<Imu> imu_sub_;

		public ImuSubscriberHelper (NodeHandle nh, string topic, Imu imu)
		{
			imu_ = imu;
			imu_sub_ = nh.subscribe<Imu> ( topic, 1, imuCallback );
		}

		~ImuSubscriberHelper ()
		{
			imu_sub_.shutdown ();
		}

		public void imuCallback (Imu imu)
		{
			imu_ = imu;
		}
	}

	public class OdomSubscriberHelper
	{
		Subscriber<Odometry> odom_sub_;

		Pose pose_;
		Twist twist_;
		Accel acceleration_;
		Header header_;

		public OdomSubscriberHelper (NodeHandle nh, string topic, Pose pose, Twist twist, Accel acceleration, Header header)
		{
			pose_ = pose;
			twist_ = twist;
			acceleration_ = acceleration;
			header_ = header;
			odom_sub_ = nh.subscribe<Odometry> ( topic, 1, odomCallback );
		}

		~OdomSubscriberHelper ()
		{
			odom_sub_.shutdown ();
		}

		void odomCallback (Odometry odom)
		{
			// calculate acceleration
			if ( header_.Stamp.data.toSec () != 0.0 && odom.header.Stamp.data.toSec () != 0.0 )
			{
				double acceleration_time_constant = 0.1;
				double dt = ( ( odom.header.Stamp - header_.Stamp ).data.toSec () );
				if ( dt > 0.0 )
				{
					acceleration_.linear.x = ( ( odom.twist.twist.linear.x - twist_.linear.x ) + acceleration_time_constant * acceleration_.linear.x ) / ( dt + acceleration_time_constant );
					acceleration_.linear.y = ( ( odom.twist.twist.linear.y - twist_.linear.y ) + acceleration_time_constant * acceleration_.linear.y ) / ( dt + acceleration_time_constant );
					acceleration_.linear.z = ( ( odom.twist.twist.linear.z - twist_.linear.z ) + acceleration_time_constant * acceleration_.linear.z ) / ( dt + acceleration_time_constant );
					acceleration_.angular.x = ( ( odom.twist.twist.angular.x - twist_.angular.x ) + acceleration_time_constant * acceleration_.angular.x ) / ( dt + acceleration_time_constant );
					acceleration_.angular.y = ( ( odom.twist.twist.angular.y - twist_.angular.y ) + acceleration_time_constant * acceleration_.angular.y ) / ( dt + acceleration_time_constant );
					acceleration_.angular.z = ( ( odom.twist.twist.angular.z - twist_.angular.z ) + acceleration_time_constant * acceleration_.angular.z ) / ( dt + acceleration_time_constant );
				}
			}

			header_ = odom.header;
			pose_ = odom.pose.pose;
			twist_ = odom.twist.twist;
		}
	}

	public class PoseSubscriberHelper
	{
		Subscriber<PoseStamped> pose_sub_;
		PoseStamped pose_;

		public PoseSubscriberHelper (NodeHandle nh, string topic)
		{
			pose_sub_ = nh.subscribe<PoseStamped> ( topic, 1, poseCallback );
		}

		~PoseSubscriberHelper ()
		{
			pose_sub_.shutdown ();
		}

	  	public PoseStamped get () { return pose_; }

		void poseCallback (PoseStamped pose)
		{
			pose_ = pose;
		}
	}

	public class PoseDifferentiatorHelper
	{
		Pose last_pose_;
		Twist last_twist_;
		Time last_time_;

		public void updateAndEstimate (Time time, Pose pose, Twist twist, Accel accel)
		{
			if ( last_pose_ != null )
			{

				double dt = ( time - last_time_ ).data.toSec ();
				double roll, pitch, yaw, last_roll, last_pitch, last_yaw;
				tf.net.emQuaternion q = new tf.net.emQuaternion ( pose.orientation );
				tf.net.emVector3 v = q.getRPY ();
				roll = v.x;
				pitch = v.y;
				yaw = v.z;
				q = new tf.net.emQuaternion ( last_pose_.orientation );
				v = q.getRPY ();
				last_roll = v.x;
				last_pitch = v.y;
				last_yaw = v.z;

//				fromMsg ( pose.orientation, ref q );
//				Matrix3x3 ( q ).getRPY ( roll, pitch, yaw );
//
//				fromMsg ( last_pose_.orientation, q );
//				Matrix3x3 ( q ).getRPY ( last_roll, last_pitch, last_yaw );

				twist.linear.x = ( pose.position.x - last_pose_.position.x ) / dt;
				twist.linear.y = ( pose.position.y - last_pose_.position.y ) / dt;
				twist.linear.z = ( pose.position.z - last_pose_.position.z ) / dt;
				twist.angular.x = differenceWithWraparound ( roll, last_roll ) / dt;
				twist.angular.y = differenceWithWraparound ( pitch, last_pitch ) / dt;
				twist.angular.z = differenceWithWraparound ( yaw, last_yaw ) / dt;

				if ( last_twist_ != null )
				{
					accel.linear.x = ( twist.linear.x - last_twist_.linear.x ) / dt;
					accel.linear.y = ( twist.linear.y - last_twist_.linear.y ) / dt;
					accel.linear.z = ( twist.linear.z - last_twist_.linear.z ) / dt;

					accel.angular.x = ( twist.angular.x - last_twist_.angular.x ) / dt;
					accel.angular.y = ( twist.angular.y - last_twist_.angular.y ) / dt;
					accel.angular.z = ( twist.angular.z - last_twist_.angular.z ) / dt;
					last_twist_ = twist;
				} else
				{
					last_twist_ = twist;
				}
				last_pose_ = pose;

			} else
			{
				last_pose_ = pose;
			}
			last_time_ = time;
		}

		double differenceWithWraparound (double angle, double last_angle)
		{

			double diff = angle - last_angle;
			if ( diff > System.Math.PI )
			{
				return diff - 2 * System.Math.PI;
			} else
			if ( diff < -System.Math.PI )
			{
				return diff + 2 * System.Math.PI;
			} else
			{
				return diff;
			}
		}
	}

	public class StateSubscriberHelper
	{
		Subscriber<TransformStamped> tf_sub_;
		bool available_;
		Pose pose_;
		Twist twist_;
		Accel accel_;
		Header header_;
		PoseDifferentiatorHelper diff_;

		public StateSubscriberHelper (NodeHandle nh, string topic, Pose pose, Twist twist, Accel accel, Header header)
		{
			pose_ = pose;
			twist_ = twist;
			accel_ = accel;
			header_ = header;
	    	available_ = false;
			tf_sub_ = nh.subscribe<TransformStamped> ( topic, 1, tfCb );
	  }

		~StateSubscriberHelper ()
		{
			tf_sub_.shutdown ();
		}

		public bool isAvailable () { return available_; }

		void tfCb (TransformStamped transform)
		{
			header_ = transform.header;
			pose_.position.x = transform.transform.translation.x;
			pose_.position.y = transform.transform.translation.y;
			pose_.position.z = transform.transform.translation.z;
			pose_.orientation = transform.transform.rotation;

			diff_.updateAndEstimate ( header_.Stamp, pose_, twist_, accel_ );
			available_ = true;
		}

	  // TODO shapeshifter to replace PoseSubscriber and OdomSubscriberHelper
	//    void stateCb(topic_tools::ShapeShifter const &input) {
	//      if (input.getDataType() == "nav_msgs/Odometry") {
	//        Odometry:: odom = input.instantiate<Odometry>();
	//        odomCallback(*odom);
	//        return;
	//      }

	//      if (input.getDataType() == "geometry_msgs/PoseStamped") {
	//        PoseStamped:: pose = input.instantiate<PoseStamped>();
	//        poseCallback(*pose);
	//        return;
	//      }

	//      if (input.getDataType() == "sensor_msgs/Imu") {
	//        Imu:: imu = input.instantiate<Imu>();
	//        imuCallback(*imu);
	//        return;
	//      }

	//      if (input.getDataType() == "geometry_msgs/TransformStamped") {
	//        TransformStamped:: tf = input.instantiate<TransformStamped>();
	//        tfCallback(*tf);
	//        return;
	//      }

	//      ROS_ERROR_THROTTLE(1.0, "message_to_tf received a %s message. Supported message types: nav_msgs/Odometry geometry_msgs/PoseStamped sensor_msgs/Imu", input.getDataType().c_str());
	//    }
	}

	public class AttitudeSubscriberHelper
	{
		Subscriber<AttitudeCommand> attitude_subscriber_;
		Subscriber<YawRateCommand> yawrate_subscriber_;
		Subscriber<ThrustCommand> thrust_subscriber_;
		object command_mutex_;
		AttitudeCommand attitude_command_;
		YawRateCommand yawrate_command_;
		ThrustCommand thrust_command_;

		public AttitudeSubscriberHelper (NodeHandle nh, object command_mutex, AttitudeCommand attitude_command, YawRateCommand yawrate_command, ThrustCommand thrust_command)
		{
			command_mutex_ = command_mutex;
			attitude_command_ = attitude_command;
			yawrate_command_ = yawrate_command;
			thrust_command_ = thrust_command;
		
			attitude_subscriber_ = nh.subscribe<AttitudeCommand> ( "attitude", 1, attitudeCommandCb );
			yawrate_subscriber_ = nh.subscribe<YawRateCommand> ( "yawrate", 1, yawrateCommandCb );
			thrust_subscriber_ = nh.subscribe<ThrustCommand> ( "thrust", 1, thrustCommandCb );
		}

		~AttitudeSubscriberHelper ()
		{
			attitude_subscriber_.shutdown ();
			yawrate_subscriber_.shutdown ();
			thrust_subscriber_.shutdown ();
		}

	  	void attitudeCommandCb (AttitudeCommand command)
		{
			lock ( command_mutex_ )
			{
				attitude_command_ = command;
				if ( attitude_command_.header.Stamp.data.toSec () == 0.0 )
				{
					attitude_command_.header.Stamp = ROS.GetTime ();
				}
			}
		}

		void yawrateCommandCb (YawRateCommand command)
		{
			lock ( command_mutex_ )
			{
				yawrate_command_ = command;
				if ( yawrate_command_.header.Stamp.data.toSec () == 0.0 )
				{
					yawrate_command_.header.Stamp = ROS.GetTime ();
				}
			}
		}

		void thrustCommandCb (ThrustCommand command)
		{
			lock ( command_mutex_ )
			{
				thrust_command_ = command;
				if ( thrust_command_.header.Stamp.data.toSec () == 0.0 )
				{
					attitude_command_.header.Stamp = ROS.GetTime ();
				}
			}
		}
	}


	public static class HelpersStatic
	{
		public static bool getMassAndInertia (NodeHandle nh, ref double mass, ref double[] inertia)
		{
			throw new System.NotImplementedException ( "Need to port over the urdf parser and other things commented out here" );
			return false;
		}

/*		public static bool getMassAndInertia (NodeHandle nh, ref double mass, ref double[] inertia)
		{
			string robot_description = "";
			if ( !nh.getParam ( "robot_description", ref robot_description ) )
			{
//				ROS.Error ( "getMassAndInertia() couldn't find URDF at " + nh.getNamespace () + "/robot_description" );
				return false;
			}

			// add a using urdf, after finding and porting urdf and urdfParser and all that
			ModelInterface model;
			try
			{
				model = urdf.parseURDF ( robot_description );
			}
			catch (System.Exception ex)
			{
//				ROS.Error ( "getMassAndInertia() couldn't parse URDF at " + nh.getNamespace () + "/robot_description: " + ex.Message );
				return false;
			}

			Inertial inertial = model.getRoot ().inertial;
			if ( inertial = null || inertial.mass == null || inertial.ixx == null || inertial.iyy == null || inertial.izz == null )
			{
//				ROS.Error ( "getMassAndInertia() requires inertial information stored on the root link " + nh.getNamespace () + "/robot_description" );
				return false;
			}

			mass = inertial.mass;
			inertia [ 0 ] = inertial.ixx;
			inertia [ 1 ] = inertial.iyy;
			inertia [ 2 ] = inertial.izz;
			return true;
		}*/

		public static bool poseWithinTolerance (Pose pose_current, Pose pose_target, double dist_tolerance, double yaw_tolerance)
		{
			if ( yaw_tolerance > 0.0 )
			{
				double yaw_current, yaw_target;
				tf.net.emQuaternion q = new tf.net.emQuaternion ( pose_current.orientation );
				double temp;

				yaw_current = q.getRPY ().z;
				q = new tf.net.emQuaternion ( pose_target.orientation );
				yaw_target = q.getRPY ().z;
//				tf2::fromMsg ( pose_current.orientation, q );
//				tf2::Matrix3x3 ( q ).getRPY ( temp, temp, yaw_current );
//				tf2::fromMsg ( pose_target.orientation, q );
//				tf2::Matrix3x3 ( q ).getRPY ( temp, temp, yaw_target );

				double yaw_error = yaw_current - yaw_target;
				// detect wrap around pi and compensate
				if (yaw_error > System.Math.PI)
				{
					yaw_error -= 2 * System.Math.PI;
				}
				else if (yaw_error < -System.Math.PI)
				{
					yaw_error += 2 * System.Math.PI;
				}
				if ( System.Math.Abs ( yaw_error ) > yaw_tolerance )
				{
					ROS.Debug ( "Waiting for yaw " + System.Math.Abs ( yaw_current - yaw_target ) );
					return false;
				}

			}

			if ( dist_tolerance > 0.0 )
			{
				UnityEngine.Vector3 v_current = new UnityEngine.Vector3 ( (float) pose_current.position.x, (float) pose_current.position.y, (float) pose_current.position.z );
				UnityEngine.Vector3 v_target = new UnityEngine.Vector3 ( (float) pose_target.position.x, (float) pose_target.position.y, (float) pose_target.position.z );
				if ( ( v_current - v_target ).sqrMagnitude > dist_tolerance * dist_tolerance )
				{
					ROS.Debug ( "Waiting for distance " + ( v_current - v_target ).sqrMagnitude );
					return false;
				}
			}

			return true;
		}
	}
} // namespace hector_quadrotor_interface