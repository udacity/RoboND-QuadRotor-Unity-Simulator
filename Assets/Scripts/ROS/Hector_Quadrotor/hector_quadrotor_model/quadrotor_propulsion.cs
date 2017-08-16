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
using hector_uav_msgs;
using Messages;
using Messages.geometry_msgs;
using Messages.std_msgs;
using System;
using System.Collections.Generic;
using System.Threading;

namespace hector_quadrotor_model
{
	public struct PropulsionParameters
	{
		public double k_m;
		public double k_t;
		public double CT2s;
		public double CT1s;
		public double CT0s;
		public double Psi;
		public double J_M; // = double.PositiveInfinity;
		public double R_A; // = double.PositiveInfinity;
		public double alpha_m;
		public double beta_m;
		public double l_m;

		public void Init ()
		{
			J_M = double.PositiveInfinity;
			R_A = double.PositiveInfinity;
		}
/*		public PropulsionParameters ()
		{
			k_m = 0;
			k_t = 0;
			CT2s = 0;
			CT1s = 0;
			CT0s = 0;
			Psi = 0;
			J_M = double.PositiveInfinity;
			R_A = double.PositiveInfinity;
			alpha_m = 0;
			beta_m = 0;
			l_m = 0;
		}*/
	}

	public class QuadrotorPropulsion
	{
		public struct PropulsionModel
		{
			public PropulsionParameters parameters_;
			public double[] x;
			public double[] x_pred;
			public double[] u;
			public double[] y;

			public void Init ()
			{
				parameters_.Init (); // do the infinity thing, since structs can't have explicit parameterless constructors nor field initializers
				x = new double[4];
				x_pred = new double[4];
				u = new double[10];
				y = new double[14];
			}
		}

		Wrench wrench_;
		Supply supply_;
		MotorStatus motor_status_;
		Time last_command_time_;

		double initial_voltage_;

		Queue<MotorPWM> command_queue_;
		object command_queue_mutex_;
		AutoResetEvent command_condition_;
		object mutex_;
		PropulsionModel propulsion_model_;

		public QuadrotorPropulsion ()
		{
			// initialize propulsion model
			// quadrotorPropulsion_initialize();
			propulsion_model_ = new PropulsionModel ();
			propulsion_model_.Init (); 
		}

		bool configure (NodeHandle param)
		{
//			get model parameters
			if ( !param.getParam ( "k_m", ref propulsion_model_.parameters_.k_m ) )
				return false;
			if ( !param.getParam ( "k_t", ref propulsion_model_.parameters_.k_t ) )
				return false;
			if ( !param.getParam ( "CT0s", ref propulsion_model_.parameters_.CT0s ) )
				return false;
			if ( !param.getParam ( "CT1s", ref propulsion_model_.parameters_.CT1s ) )
				return false;
			if ( !param.getParam ( "CT2s", ref propulsion_model_.parameters_.CT2s ) )
				return false;
			if ( !param.getParam ( "J_M", ref propulsion_model_.parameters_.J_M ) )
				return false;
			if ( !param.getParam ( "l_m", ref propulsion_model_.parameters_.l_m ) )
				return false;
			if ( !param.getParam ( "Psi", ref propulsion_model_.parameters_.Psi ) )
				return false;
			if ( !param.getParam ( "R_A", ref propulsion_model_.parameters_.R_A ) )
				return false;
			if ( !param.getParam ( "alpha_m", ref propulsion_model_.parameters_.alpha_m ) )
				return false;
			if ( !param.getParam ( "beta_m", ref propulsion_model_.parameters_.beta_m ) )
				return false;

//		#ifndef NDEBUG
//		  std::cout << "Loaded the following quadrotor propulsion model parameters from namespace " << param.getNamespace() << ":" << std::endl;
//		  std::cout << "k_m     = " << propulsion_model_.parameters_.k_m << std::endl;
//		  std::cout << "k_t     = " << propulsion_model_.parameters_.k_t << std::endl;
//		  std::cout << "CT2s    = " << propulsion_model_.parameters_.CT2s << std::endl;
//		  std::cout << "CT1s    = " << propulsion_model_.parameters_.CT1s << std::endl;
//		  std::cout << "CT0s    = " << propulsion_model_.parameters_.CT0s << std::endl;
//		  std::cout << "Psi     = " << propulsion_model_.parameters_.Psi << std::endl;
//		  std::cout << "J_M     = " << propulsion_model_.parameters_.J_M << std::endl;
//		  std::cout << "R_A     = " << propulsion_model_.parameters_.R_A << std::endl;
//		  std::cout << "l_m     = " << propulsion_model_.parameters_.l_m << std::endl;
//		  std::cout << "alpha_m = " << propulsion_model_.parameters_.alpha_m << std::endl;
//		  std::cout << "beta_m  = " << propulsion_model_.parameters_.beta_m << std::endl;
//		#endif

			initial_voltage_ = 14.8;
			param.getParam ( "supply_voltage", ref initial_voltage_ );
			reset ();

			return true;
		}

		public void reset ()
		{
			lock ( mutex_ )
			{
				propulsion_model_.x.Initialize ();
				propulsion_model_.x_pred.Initialize ();
				propulsion_model_.u.Initialize ();
				propulsion_model_.y.Initialize ();
				
				wrench_ = new Wrench ();
				
				motor_status_ = new MotorStatus ();
				motor_status_.voltage = new float[4];
				motor_status_.frequency = new float[4];
				motor_status_.current = new float[4];
				
				supply_ = new Supply ();
				supply_.voltage = new float[1];
				supply_.voltage[0] = (float) initial_voltage_;
				
				last_command_time_ = ROS.GetTime ();

				command_queue_ = new Queue<MotorPWM> ();
			}
		}

		public void update (double dt)
		{
			if ( dt <= 0.0 )
				return;

//			ROS_DEBUG_STREAM_NAMED("quadrotor_propulsion", "propulsion.twist:   " << PrintVector<double>(propulsion_model_.u.begin(), propulsion_model_.u.begin() + 6));
//			ROS_DEBUG_STREAM_NAMED("quadrotor_propulsion", "propulsion.voltage: " << PrintVector<double>(propulsion_model_.u.begin() + 6, propulsion_model_.u.begin() + 10));
//			ROS_DEBUG_STREAM_NAMED("quadrotor_propulsion", "propulsion.x_prior: " << PrintVector<double>(propulsion_model_.x.begin(), propulsion_model_.x.end()));

			Helpers.checknan ( ref propulsion_model_.u, "propulsion model input" );
			Helpers.checknan ( ref propulsion_model_.x, "propulsion model state" );

			// update propulsion model
			f(propulsion_model_.x, propulsion_model_.u, dt, propulsion_model_.y, propulsion_model_.x_pred);
			propulsion_model_.x = propulsion_model_.x_pred;

//			ROS_DEBUG_STREAM_NAMED("quadrotor_propulsion", "propulsion.x_post:  " << PrintVector<double>(propulsion_model_.x.begin(), propulsion_model_.x.end()));
//			ROS_DEBUG_STREAM_NAMED("quadrotor_propulsion", "propulsion.force:   " << PrintVector<double>(propulsion_model_.y.begin() + 0, propulsion_model_.y.begin() + 3) << " " <<
//				"propulsion.torque:  " << PrintVector<double>(propulsion_model_.y.begin() + 3, propulsion_model_.y.begin() + 6));

			Helpers.checknan ( ref propulsion_model_.y, "propulsion model output" );

			wrench_.force.x = propulsion_model_.y [ 0 ];
			wrench_.force.y = -propulsion_model_.y [ 1 ];
			wrench_.force.z = propulsion_model_.y [ 2 ];
			wrench_.torque.x = propulsion_model_.y [ 3 ];
			wrench_.torque.y = -propulsion_model_.y [ 4 ];
			wrench_.torque.z = -propulsion_model_.y [ 5 ];

			motor_status_.voltage [ 0 ] = (float) propulsion_model_.u [ 6 ];
			motor_status_.voltage [ 1 ] = (float) propulsion_model_.u [ 7 ];
			motor_status_.voltage [ 2 ] = (float) propulsion_model_.u [ 8 ];
			motor_status_.voltage [ 3 ] = (float) propulsion_model_.u [ 9 ];

			motor_status_.frequency [ 0 ] = (float) propulsion_model_.y [ 6 ];
			motor_status_.frequency [ 1 ] = (float) propulsion_model_.y [ 7 ];
			motor_status_.frequency [ 2 ] = (float) propulsion_model_.y [ 8 ];
			motor_status_.frequency [ 3 ] = (float) propulsion_model_.y [ 9 ];
			motor_status_.running = motor_status_.frequency [ 0 ] > 1.0 && motor_status_.frequency [ 1 ] > 1.0 && motor_status_.frequency [ 2 ] > 1.0 && motor_status_.frequency [ 3 ] > 1.0;

			motor_status_.current [ 0 ] = (float) propulsion_model_.y [ 10 ];
			motor_status_.current [ 1 ] = (float) propulsion_model_.y [ 11 ];
			motor_status_.current [ 2 ] = (float) propulsion_model_.y [ 12 ];
			motor_status_.current [ 3 ] = (float) propulsion_model_.y [ 13 ];
		}

		public void engage ()
		{
			motor_status_.on = true;
		}

		public void shutdown ()
		{
			motor_status_.on = false;
		}

		public void setTwist (Twist twist)
		{
			lock ( mutex_ )
			{
				propulsion_model_.u[0] = twist.linear.x;
				propulsion_model_.u[1] = -twist.linear.y;
				propulsion_model_.u[2] = -twist.linear.z;
				propulsion_model_.u[3] = twist.angular.x;
				propulsion_model_.u[4] = -twist.angular.y;
				propulsion_model_.u[5] = -twist.angular.z;
			}

			// We limit the input velocities to +-100. Required for numeric stability in case of collisions,
			// where velocities returned by Gazebo can be very high.
			propulsion_model_.u.ForEach ( x => x = Math.Min ( 100.0, Math.Max ( -100.0, x ) ) );
		}

		public void setVoltage (MotorPWM voltage)
		{
			lock ( mutex_ )
			{
				last_command_time_ = voltage.header.Stamp;
				
				if ( motor_status_.on && voltage.pwm.Length >= 4 )
				{
					propulsion_model_.u [ 6 ] = voltage.pwm [ 0 ] * supply_.voltage [ 0 ] / 255.0;
					propulsion_model_.u [ 7 ] = voltage.pwm [ 1 ] * supply_.voltage [ 0 ] / 255.0;
					propulsion_model_.u [ 8 ] = voltage.pwm [ 2 ] * supply_.voltage [ 0 ] / 255.0;
					propulsion_model_.u [ 9 ] = voltage.pwm [ 3 ] * supply_.voltage [ 0 ] / 255.0;
				} else
				{
					propulsion_model_.u [ 6 ] = 0.0;
					propulsion_model_.u [ 7 ] = 0.0;
					propulsion_model_.u [ 8 ] = 0.0;
					propulsion_model_.u [ 9 ] = 0.0;
				}
			}
		}

		public Wrench getWrench () { return wrench_; }
		public Supply getSupply () { return supply_; }
		public MotorStatus getMotorStatus () { return motor_status_; }

		public void addCommandToQueue (MotorCommand command)
		{
			MotorPWM pwm = new MotorPWM ();
			pwm.header = command.header;
			pwm.pwm = new byte[ command.voltage.Length ];
			for ( int i = 0; i < command.voltage.Length; ++i )
			{
				byte temp = (byte) Math.Round ( command.voltage [ i ] / supply_.voltage [ 0 ] * 255.0 );
				if ( temp < 0 )
					pwm.pwm [ i ] = 0;
				else
				if ( temp > 255 )
					pwm.pwm [ i ] = 255;
				else
					pwm.pwm [ i ] = temp;
			}
			addPWMToQueue ( pwm );
		}

		public void addPWMToQueue (MotorPWM pwm)
		{
			lock ( command_queue_mutex_ )
			{
				if ( !motor_status_.on )
				{
					ROS.Warn ( "quadrotor_propulsion", "Received new motor command. Enabled motors." );
					engage ();
				}
				
				ROS.Debug ( "quadrotor_propulsion", "Received motor command valid at " + pwm.header.Stamp.ToString () );
				command_queue_.Enqueue ( pwm );
				command_condition_.Set ();
			}
		}

		public bool processQueue (Time timestamp, Duration tolerance, Duration delay, Duration wait, CallbackQueue callback_queue)
		{
			bool new_command = false;
			lock ( command_queue_mutex_ )
			{
				
				Time min = new Time ( timestamp.data );
				Time max = new Time ( timestamp.data );
				try {
					min = timestamp - delay - tolerance /* - ros::Duration(dt) */;
				} catch (Exception e) {}
				
				try {
					max = timestamp - delay + tolerance;
				} catch (Exception e) {}
				
				do {
					
					while ( command_queue_.Count > 0 )
					{
						MotorPWM new_motor_voltage = command_queue_.Peek ();
						Time new_time = new_motor_voltage.header.Stamp;
						
						if (new_time.data.toSec () == 0.0 || (new_time >= min && new_time <= max))
						{
							setVoltage (new_motor_voltage);
							command_queue_.Dequeue ();
							new_command = true;
							
							// new motor command is too old:
						} else if (new_time < min)
						{
							ROS.Debug ("quadrotor_propulsion", "Command received was %fs too old. Discarding.", (new_time - timestamp).data.toSec());
							command_queue_.Dequeue ();
							
							// new motor command is too new:
						} else {
							break;
						}
					}

					if (command_queue_.Count == 0 && !new_command)
					{
						if (!motor_status_.on || wait.data.toSec () == 0.0) break;
						
						ROS.Debug ("quadrotor_propulsion", "Waiting for command at simulation step t = %fs... last update was %fs ago", timestamp.data.toSec(), (timestamp - last_command_time_).data.toSec());
						if ( callback_queue == null )
						{
							if ( command_condition_.WaitOne ((int)(wait.data.toSec()*1000)) ) continue;
//							if (command_condition_.timed_wait(lock, wait.toBoost())) continue;
						} else {
							command_condition_.Set ();
							callback_queue.callAvailable ( (int) (wait.toSec ()*1000) );
							command_condition_.Reset ();
							if (command_queue_.Count > 0) continue;
						}
						
						ROS.Error ("quadrotor_propulsion", "Command timed out. Disabled motors.");
						shutdown();
					}
					
				} while ( false );
				
				if (new_command)
				{
//					ROS.Debug ("quadrotor_propulsion", "Using motor command valid at t = " << last_command_time_.toSec() << "s for simulation step at t = " << timestamp.toSec() << "s (dt = " << (timestamp - last_command_time_).toSec() << "s)");
				
				}
			}

			return new_command;
		}

		public void setInitialSupplyVoltage (double voltage) { initial_voltage_ = voltage; }


		void quadrotorPropulsion (double[] xin, double[] uin, PropulsionParameters parameter, double dt, double[] y, double[] xpred)
		{
			double[] v_1 = new double[4];
			int i;
			double[] F_m = new double[4];
			double[] U = new double[4];
			double[] M_e = new double[4];
			double[] I = new double[4];
			double[] F = new double[3];
			double b_F_m;
			double temp;
			double d0;

			/*  initialize vectors */
			for (i = 0; i < 4; i++) {
				xpred[i] = xin[i];

				/*  motorspeed */
				v_1[i] = 0.0;
			}

			y = new double[14];
			y.Initialize ();
//			memset(&y[0], 0, 14U * sizeof(double));
			for (i = 0; i < 4; i++) {
				F_m[i] = 0.0;
				U[i] = 0.0;
				M_e[i] = 0.0;
				I[i] = 0.0;
			}

			for (i = 0; i < 3; i++) {
				F[i] = 0.0;
			}

			/*  Input variables */
			U[0] = uin[6];
			U[1] = uin[7];
			U[2] = uin[8];
			U[3] = uin[9];

			/*  Constants */
			v_1[0] = -uin[2] + parameter.l_m * uin[4];
			v_1[1] = -uin[2] - parameter.l_m * uin[3];
			v_1[2] = -uin[2] - parameter.l_m * uin[4];
			v_1[3] = -uin[2] + parameter.l_m * uin[3];

			/*  calculate thrust for all 4 rotors */
			for ( i = 0; i < 4; i++ )
			{
				/*  if the flow speed at infinity is negative */
				if ( v_1 [ i ] < 0.0 )
				{
					b_F_m = ( parameter.CT2s * MatlabHelpers.rt_powd_snf ( v_1 [ i ], 2.0 ) + parameter.CT1s *
					v_1 [ i ] * xin [ i ] ) + parameter.CT0s * MatlabHelpers.rt_powd_snf ( xin [ i ], 2.0 );

					/*  if the flow speed at infinity is positive */
				} else
				{
					b_F_m = ( -parameter.CT2s * MatlabHelpers.rt_powd_snf ( v_1 [ i ], 2.0 ) + parameter.CT1s *
					v_1 [ i ] * xin [ i ] ) + parameter.CT0s * MatlabHelpers.rt_powd_snf ( xin [ i ], 2.0 );
				}

				/*  sum up all rotor forces */
				/*  Identification of Roxxy2827-34 motor with 10x4.5 propeller */
				/*  temporarily used Expressions */
				temp = ( U [ i ] * parameter.beta_m - parameter.Psi * xin [ i ] ) / ( 2.0 *
				parameter.R_A );
				temp += Math.Sqrt ( MatlabHelpers.rt_powd_snf ( temp, 2.0 ) + U [ i ] * parameter.alpha_m /
				parameter.R_A );
				d0 = parameter.Psi * temp;

				/*  electrical torque motor 1-4 */
				/*  new version */
				/*  old version */
				/*  fx   = (Psi/R_A*(U-Psi*omega_m) - M_m)/J_M; */
				/*  A    = -(Psi^2/R_A)/J_M; */
				/*  B(1) =  Psi/(J_M*R_A); */
				/*  B(2) = -1/J_M; */
				/*  system outputs. Use euler solver to predict next time step */
				/*  predicted motor speed */
				/*  electric torque */
				/* y = [M_e I]; */
				/*  system jacobian */
				/*  A       = 1 + dt*A; */
				/*  input jacobian */
				/*  B       = A*B*dt; */
				M_e [ i ] = d0;
				I [ i ] = temp;
				xpred [ i ] = xin [ i ] + dt * ( 1.0 / parameter.J_M * ( d0 - ( parameter.k_t * b_F_m
				+ parameter.k_m * xin [ i ] ) ) );
				F_m [ i ] = b_F_m;
				F [ 2 ] += b_F_m;
			}

			/*  System output, i.e. force and torque of quadrotor */
			y[0] = 0.0;
			y[1] = 0.0;
			y[2] = F[2];

			/*  torque for rotating quadrocopter around x-axis is the mechanical torque */
			y[3] = (F_m[3] - F_m[1]) * parameter.l_m;

			/*  torque for rotating quadrocopter around y-axis is the mechanical torque */
			y[4] = (F_m[0] - F_m[2]) * parameter.l_m;

			/*  torque for rotating quadrocopter around z-axis is the electrical torque */
			y[5] = ((-M_e[0] - M_e[2]) + M_e[1]) + M_e[3];

			/*  motor speeds (rad/s) */
			for (i = 0; i < 4; i++) {
				y[i + 6] = xpred[i];
			}

			/*  motor current (A) */
			for (i = 0; i < 4; i++) {
				y[i + 10] = I[i];
			}

			/*  M_e(1:4) / Psi; */
		}

		/* End of code generation (quadrotorPropulsion.c) */

		void f (double[] xin, double[] uin, double dt, double[] y, double[] xpred)
		{
			quadrotorPropulsion ( xin, uin, propulsion_model_.parameters_, dt, y, xpred );
		}
	}
}