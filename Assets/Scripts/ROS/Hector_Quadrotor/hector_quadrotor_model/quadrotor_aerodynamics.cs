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
using Messages.geometry_msgs;
using System;
using System.Threading;
using hector_quadrotor_model;

namespace hector_quadrotor_model
{
	public struct DragParameters
	{
		public double C_wxy;
		public double C_wz;
		public double C_mxy;
		public double C_mz;
	}

	public struct DragModel
	{
		public DragParameters parameters_;
		public double[] u;
		public double[] y;

		public void Init ()
		{
			u = new double[6];
			y = new double[6];
		}
	}


	class QuadrotorAerodynamics
	{
		Quaternion orientation_;
		Twist twist_;
		Vector3 wind_;
		Wrench wrench_;

		object mutex_;

		DragModel drag_model_;

		public QuadrotorAerodynamics ()
		{
			// initialize drag model
			// quadrotorDrag_initialize();
			drag_model_ = new DragModel ();
			drag_model_.Init ();
		}

		public bool configure (NodeHandle param)
		{
			// get model parameters
			if ( !param.getParam ( "C_wxy", ref drag_model_.parameters_.C_wxy ) )
				return false;
			if ( !param.getParam ( "C_wz", ref drag_model_.parameters_.C_wz ) )
				return false;
			if ( !param.getParam ( "C_mxy", ref drag_model_.parameters_.C_mxy ) )
				return false;
			if ( !param.getParam ( "C_mz", ref drag_model_.parameters_.C_mz ) )
				return false;

//		#ifndef NDEBUG
//		  std::cout << "Loaded the following quadrotor drag model parameters from namespace " << param.getNamespace() << ":" << std::endl;
//		  std::cout << "C_wxy = " << drag_model_.parameters_.C_wxy << std::endl;
//		  std::cout << "C_wz = "  << drag_model_.parameters_.C_wz << std::endl;
//		  std::cout << "C_mxy = " << drag_model_.parameters_.C_mxy << std::endl;
//		  std::cout << "C_mz = "  << drag_model_.parameters_.C_mz << std::endl;
//		#endif

			reset ();
			return true;
		}

		public void reset ()
		{
			lock ( mutex_ )
			{
				drag_model_.u.Initialize ();
				drag_model_.y.Initialize ();
				
				twist_ = new Twist ();
				wind_ = new Vector3 ();
				wrench_ = new Wrench ();
			}
		}


		public void update (double dt)
		{
			if ( dt <= 0.0 )
				return;

			lock ( mutex_ )
			{
				// fill input vector u for drag model
				drag_model_.u [ 0 ] = ( twist_.linear.x - wind_.x );
				drag_model_.u [ 1 ] = -( twist_.linear.y - wind_.y );
				drag_model_.u [ 2 ] = -( twist_.linear.z - wind_.z );
				drag_model_.u [ 3 ] = twist_.angular.x;
				drag_model_.u [ 4 ] = -twist_.angular.y;
				drag_model_.u [ 5 ] = -twist_.angular.z;
				
				// We limit the input velocities to +-100. Required for numeric stability in case of collisions,
				// where velocities returned by Gazebo can be very high.
				Helpers.limit(ref drag_model_.u, -100.0, 100.0);
				
				// convert input to body coordinates
				UnityEngine.Quaternion orientation = new UnityEngine.Quaternion ( (float) orientation_.x, (float) orientation_.y, (float) orientation_.z, (float) orientation_.w );
				UnityEngine.Vector3 linear = new UnityEngine.Vector3 ( (float) drag_model_.u [ 0 ], (float) drag_model_.u [ 1 ], (float) drag_model_.u [ 2 ] );
				UnityEngine.Vector3 angular = new UnityEngine.Vector3 ( (float) drag_model_.u [ 3 ], (float) drag_model_.u [ 4 ], (float) drag_model_.u [ 5 ] );
				linear = orientation * linear;
				angular = orientation * angular;
//				Eigen::Quaterniond orientation(orientation_.w, orientation_.x, orientation_.y, orientation_.z);
//				Eigen::Matrix<double,3,3> rotation_matrix(orientation.toRotationMatrix());
//				Eigen::Map<Eigen::Vector3d> linear(&(drag_model_.u[0]));
//				Eigen::Map<Eigen::Vector3d> angular(&(drag_model_.u[3]));
//				linear  = rotation_matrix * linear;
//				angular = rotation_matrix * angular;
				
//				ROS.Debug ("quadrotor_aerodynamics", "aerodynamics.twist:  " + PrintVector<double>(drag_model_.u.begin(), drag_model_.u.begin() + 6));
				for ( int i = 0; i < drag_model_.u.Length; i++ )
					Helpers.checknan ( ref drag_model_.u [ i ], "drag model input" );
				
				// update drag model
				f (drag_model_.u, dt, ref drag_model_.y);
				
//				ROS_DEBUG_STREAM_NAMED("quadrotor_aerodynamics", "aerodynamics.force:  " << PrintVector<double>(drag_model_.y.begin() + 0, drag_model_.y.begin() + 3));
//				ROS_DEBUG_STREAM_NAMED("quadrotor_aerodynamics", "aerodynamics.torque: " << PrintVector<double>(drag_model_.y.begin() + 3, drag_model_.y.begin() + 6));
				for ( int i = 0; i < drag_model_.y.Length; i++ )
					Helpers.checknan ( ref drag_model_.y [ i ], "drag model output" );
				
				// drag_model_ gives us inverted vectors!
				wrench_.force.x  = -( drag_model_.y[0]);
				wrench_.force.y  = -(-drag_model_.y[1]);
				wrench_.force.z  = -(-drag_model_.y[2]);
				wrench_.torque.x = -( drag_model_.y[3]);
				wrench_.torque.y = -(-drag_model_.y[4]);
				wrench_.torque.z = -(-drag_model_.y[5]);
			}
		}

		public void setOrientation (Quaternion orientation)
		{
			lock ( mutex_ )
			{
				orientation_ = orientation;
			}
		}

		public void setTwist (Twist twist)
		{
			lock ( mutex_ )
			{
				twist_ = twist;
			}
		}

		public void setBodyTwist (Twist body_twist)
		{
			lock ( mutex_ )
			{
				UnityEngine.Quaternion orientation = new UnityEngine.Quaternion ( (float) orientation_.x, (float) orientation_.y, (float) orientation_.z, (float) orientation_.w );
				orientation = UnityEngine.Quaternion.Inverse ( orientation );
				UnityEngine.Vector3 body_linear = new UnityEngine.Vector3 ( (float) body_twist.linear.x, (float) body_twist.linear.y, (float) body_twist.linear.z );
				UnityEngine.Vector3 world_linear = orientation * body_linear;
//				Eigen::Quaterniond orientation(orientation_.w, orientation_.x, orientation_.y, orientation_.z);
//				Eigen::Matrix<double,3,3> inverse_rotation_matrix(orientation.inverse().toRotationMatrix());
//				
//				Eigen::Vector3d body_linear(body_twist.linear.x, body_twist.linear.y, body_twist.linear.z);
//				Eigen::Vector3d world_linear(inverse_rotation_matrix * body_linear);
				twist_.linear.x = world_linear.x;
				twist_.linear.y = world_linear.y;
				twist_.linear.z = world_linear.z;

				UnityEngine.Vector3 body_angular = new UnityEngine.Vector3 ( (float) body_twist.angular.x, (float) body_twist.angular.y, (float) body_twist.angular.z );
				UnityEngine.Vector3 world_angular = orientation * body_angular;
//				Eigen::Vector3d body_angular(body_twist.angular.x, body_twist.angular.y, body_twist.angular.z);
//				Eigen::Vector3d world_angular(inverse_rotation_matrix * body_angular);
				twist_.angular.x = world_angular.x;
				twist_.angular.y = world_angular.y;
				twist_.angular.z = world_angular.z;
			}
		}

		public void setWind (Vector3 wind)
		{
			lock ( mutex_ )
			{
				wind_ = wind;
			}
		}

		public Wrench getWrench () { return wrench_; }

		public void f (double[] uin, double dt, ref double[] y)
		{
			quadrotorDrag ( uin, drag_model_.parameters_, dt, ref y );
		}

		void quadrotorDrag (double[] uin, DragParameters parameter, double dt, ref double[] y)
		{
			int i;
			double absoluteVelocity;
			double absoluteAngularVelocity;

			/*  initialize vectors */
			for (i = 0; i < 6; i++) {
				y[i] = 0.0;
			}

			/*  Input variables */
			/*  Constants */
			/*  temporarily used vector */
			absoluteVelocity = Math.Sqrt((MatlabHelpers.rt_powd_snf (uin[0], 2.0) + MatlabHelpers.rt_powd_snf (uin[1], 2.0))
				+ MatlabHelpers.rt_powd_snf (uin[2], 2.0));
			absoluteAngularVelocity = Math.Sqrt((MatlabHelpers.rt_powd_snf (uin[3], 2.0) + MatlabHelpers.rt_powd_snf (uin[4],
				2.0)) + MatlabHelpers.rt_powd_snf (uin[5], 2.0));

			/*  system outputs */
			/*  calculate drag force */
			y[0] = parameter.C_wxy * absoluteVelocity * uin[0];
			y[1] = parameter.C_wxy * absoluteVelocity * uin[1];
			y[2] = parameter.C_wz * absoluteVelocity * uin[2];

			/*  calculate draq torque */
			y[3] = parameter.C_mxy * absoluteAngularVelocity * uin[3];
			y[4] = parameter.C_mxy * absoluteAngularVelocity * uin[4];
			y[5] = parameter.C_mz * absoluteAngularVelocity * uin[5];
		}

		/* End of code generation (quadrotorDrag.c) */


	  
	}
}