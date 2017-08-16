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
//#include <hardware_interface/internal/hardware_resource_manager.h>
using System.Collections.Generic;
using Ros_CSharp;
using Messages;
using Messages.geometry_msgs;
using Messages.sensor_msgs;
using hardware_interface;
using hector_quadrotor_interface;
using hector_uav_msgs;

namespace hector_quadrotor_interface
{
	public class QuadrotorInterface : HardwareInterface // hardware_interface::HardwareInterface
	{
		PoseHandle pose_;
		TwistHandle twist_;
		AccelerationHandle accel_;
		ImuHandle imu_;
		MotorStatusHandle motor_status_;

		Dictionary<string, CommandHandle> inputs_;
		Dictionary<string, CommandHandle> outputs_;
		Dictionary<string, CommandHandle> enabled_;

		public QuadrotorInterface () {}
		~QuadrotorInterface() {}

		void registerPose (Pose pose)
		{
			pose_ = new PoseHandle ( this, pose );
		}

		void registerTwist (Twist twist)
		{
			twist_ = new TwistHandle ( this, twist );
		}

		void registerAccel (Accel accel)
		{
			accel_ = new AccelerationHandle ( this, accel );
		}

		void registerSensorImu (Imu imu)
		{
			imu_ = new ImuHandle ( this, imu );
		}

		void registerMotorStatus (MotorStatus motor_status)
		{
			motor_status_ = new MotorStatusHandle ( this, motor_status );
		}

		PoseHandle getPose ()
		{
			return pose_;
		}

		TwistHandle getTwist ()
		{
			return twist_;
		}

		AccelerationHandle getAccel ()
		{
			return accel_;
		}

		ImuHandle getSensorImu ()
		{
			return imu_;
		}

		MotorStatusHandle getMotorStatus ()
		{
			return motor_status_;
		}

		// private

//		HandleType getHandle<HandleType> () where HandleType : CommandHandle, new()
//		{
//			return ( new HandleType ().CopyFrom ( this, this.nam ) );
//		}

		HandleType addInput<HandleType> (string name) where HandleType : CommandHandle, new()
		{
			HandleType input = getInput<HandleType> ( name );
			if ( input != null )
				return input;

			// create new input handle
			input = new HandleType ();
			input.CopyFrom ( this, name, "" );
			inputs_ [ name ] = input;

			// connect to outputs of same name
			if ( outputs_.ContainsKey ( name ) )
			{
				HandleType output = (HandleType) outputs_ [ name ];
				if ( output != null )
					output.connectTo ( input );
			}

			return input;
		}

		HandleType addOutput<HandleType> (string name) where HandleType : CommandHandle, new()
		{
			HandleType output = getOutput<HandleType> ( name );
			if ( output != null )
				return output;

			// create new output handle
			output = new HandleType ();
			output.CopyFrom ( this, name, "" );
			outputs_.Add ( name, output );
			output.newData ();
//			output = output.ownData (new T ());
//			output = output.ownData (new typename HandleType::ValueType());

			//claim resource
			claim ( name );

			// connect to inputs of same name
			if ( inputs_.ContainsKey ( name ) )
			{
				HandleType input = (HandleType) inputs_[ name ];
				if ( input != null )
					output.connectTo ( input );
			}

			return output;
		}

		HandleType getOutput<HandleType> (string name) where HandleType : CommandHandle, new()
		{
			if ( outputs_.ContainsKey ( name ) )
				return (HandleType) outputs_ [ name ];

			return new HandleType ();
		}

		HandleType getInput<HandleType> (string name) where HandleType : CommandHandle, new()
		{
			if ( inputs_.ContainsKey ( name ) )
				return (HandleType) inputs_ [ name ];

			return new HandleType ();
		}

		// ??
//		template<typename HandleType>
//		typename HandleType::ValueType 
		ValueType getCommand<HandleType, ValueType> (string name) where HandleType : CommandHandle, new()
		{
			HandleType output = getOutput<HandleType> ( name );
			if ( output == null || !output.connected () )
				return default (ValueType);
			
			return (ValueType) output.command ();
		}

		public Pose getPoseCommand ()          { return getCommand<PoseCommandHandle, Pose>("pose"); }
		public Twist getTwistCommand ()        { return getCommand<TwistCommandHandle, Twist>("twist"); }
		public Wrench getWrenchCommand ()      { return getCommand<WrenchCommandHandle, Wrench>("wrench"); }
		public MotorCommand getMotorCommand () { return getCommand<MotorCommandHandle, MotorCommand>("motor"); }

		public bool enabled (CommandHandle handle)
		{
			if ( handle == null || !handle.connected () )
				return false;
			string resource = handle.getName ();
			return enabled_.ContainsKey ( resource );
		}

		public bool start (CommandHandle handle)
		{
			if ( handle == null || !handle.connected () )
				return false;
			string resource = handle.getName ();

			if ( enabled_.ContainsKey ( resource ) )
			{
				if ( enabled_ [ resource ] == handle )
					return true;
				
			} else
			{
				enabled_.Add ( resource, handle );
				ROS.Info ( "quadrotor_interface", "Enabled %s output", resource );
				return true;
			}

			return false;
		}

		public bool stop (CommandHandle handle)
		{
			if ( handle == null )
				return false;
			string resource = handle.getName ();

			if ( enabled_.ContainsKey ( resource ) )
			{
				if ( enabled_ [ resource ] == handle )
				{
					enabled_.Remove ( resource );
					ROS.Info ( "quadrotor_interface", "Disabled %s control", resource );
					return true;
				}
			}

			return false;
		}

		public void disconnect (CommandHandle handle)
		{
			if ( handle == null )
				return;
			string resource = handle.getName ();
			if ( inputs_.ContainsKey ( resource ) )
			{
				CommandHandle input = inputs_ [ resource ];
				if ( (CommandHandle) input.get () != handle )
					input.reset ();
			}
			if ( outputs_.ContainsKey ( resource ) )
			{
				CommandHandle output = outputs_ [ resource ];
				if ( (CommandHandle) output.get () != handle )
					output.reset ();
			}
		}

		public bool preempt (CommandHandle handle)
		{
			string resource = handle.getName ();
			if ( outputs_.ContainsKey ( resource ) )
			{
				CommandHandle output = outputs_ [ resource ];
				if ( output != null )
				{
					output.setPreempted ();
					return true;
				}
			}
			return false;
		}
	}
} // namespace hector_quadrotor_interface