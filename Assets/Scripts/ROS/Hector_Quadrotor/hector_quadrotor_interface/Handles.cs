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
using Ros_CSharp;
using Messages;
using Messages.geometry_msgs;
using Messages.sensor_msgs;
using hector_uav_msgs;
//using Pose = geometry_msgs.Pose;
//using Point = geometry_msgs.Point;
//using Quaternion = geometry_msgs.Quaternion;
//using Accel = Messages.geometry_msgs.Accel;
//using Twist = geometry_msgs.Twist;
//using Vector3 = geometry_msgs.Vector3;
//using Wrench = geometry_msgs.Wrench;
//using Imu = sensor_msgs.Imu;
//using MotorStatus = hector_uav_msgs.MotorStatus;
//using MotorCommand = hector_uav_msgs.MotorCommand;
//using AttitudeCommand = hector_uav_msgs.AttitudeCommand;
//using YawrateCommand = hector_uav_msgs.YawrateCommand;
//using ThrustCommand = hector_uav_msgs.ThrustCommand;
using hector_quadrotor_interface;

namespace hector_quadrotor_interface
{
	public class Handle<Derived, T>
	{
		protected QuadrotorInterface interface_;
		protected string name_;
		protected string field_;
		protected T value_;

		public Handle (string name, string field = "")
		{
			interface_ = null;
			name_ = name;
			field_ = field;
			value_ = default(T);
		}
		public Handle (QuadrotorInterface qInterface, string name, string field = "")
		{
			interface_ = qInterface;
			name_ = name;
			field_ = field;
			value_ = default(T);
		}

		public Handle (QuadrotorInterface qInterface, T source, string name, string field = "")
		{
			interface_ = qInterface;
			name_ = name;
			field_ = field;
			value_ = source;
		}

		~Handle () {}

		public virtual string getName () { return name_; }
		public virtual string getField () { return field_; }

		public virtual bool connected ()
		{
			return !value_.Equals ( default(T) );
		}
		public virtual void reset () { value_ = default(T); }

//		public Derived operator=(const T *source) { value_ = source; return static_cast<Derived &>(*this); }
		public T get () { return value_; }
//		public const T &operator*() const { return *value_; }
	}

	public class PoseHandle : Handle<PoseHandle, Pose>
	{
		public PoseHandle () : base ("pose") {}
		public PoseHandle (QuadrotorInterface qInterface) : base (qInterface, "pose") {}
		public PoseHandle (QuadrotorInterface qInterface, Pose pose) : base (qInterface, pose, "pose") {}
		~PoseHandle() {}

		public Pose pose () { return get (); }

		public void getEulerRPY (ref double roll, ref double pitch, ref double yaw)
		{
			Quaternion q = pose ().orientation;
			double x = q.x;
			double y = q.y;
			double z = q.z;
			double w = q.w;
			roll = Math.Atan2 ( 2.0 * y * z + 2.0 * w * x, z * z - y * y - x * x + w * w );
			pitch = -Math.Asin ( 2.0 * x * z - 2.0 * w * y );
			yaw = Math.Atan2 ( 2.0 * x * y + 2.0 * w * z, x * x + w * w - z * z - y * y );
		}

		public double getYaw ()
		{
			Quaternion q = pose ().orientation;
			return Math.Atan2 ( 2.0 * q.x * q.y + 2.0 * q.w * q.z, q.x * q.x + q.w * q.w - q.z * q.z - q.y * q.y );
		}

		public Vector3 toBody (Vector3 nav)
		{
			Quaternion q = pose ().orientation;
			double x = q.x;
			double y = q.y;
			double z = q.z;
			double w = q.w;
			Vector3 body = new Vector3 ();
			body.x = ( w * w + x * x - y * y - z * z ) * nav.x + ( 2.0 * x * y + 2.0 * w * z ) * nav.y + ( 2.0 * x * z - 2.0 * w * y ) * nav.z;
			body.y = ( 2.0 * x * y - 2.0 * w * z ) * nav.x + ( w * w - x * x + y * y - z * z ) * nav.y + ( 2.0 * y * z + 2.0 * w * x ) * nav.z;
			body.z = ( 2.0 * x * z + 2.0 * w * y ) * nav.x + ( 2.0 * y * z - 2.0 * w * x ) * nav.y + ( w * w - x * x - y * y + z * z ) * nav.z;
			
			return body;
		}

		public Vector3 fromBody (Vector3 body)
		{
			Quaternion q = pose ().orientation;
			double x = q.x;
			double y = q.y;
			double z = q.z;
			double w = q.w;
			Vector3 nav = new Vector3 ();
			nav.x = ( w * w + x * x - y * y - z * z ) * body.x + ( 2.0 * x * y - 2.0 * w * z ) * body.y + ( 2.0 * x * z + 2.0 * w * y ) * body.z;
			nav.y = ( 2.0 * x * y + 2.0 * w * z ) * body.x + ( w * w - x * x + y * y - z * z ) * body.y + ( 2.0 * y * z - 2.0 * w * x ) * body.z;
			nav.z = ( 2.0 * x * z - 2.0 * w * y ) * body.x + ( 2.0 * y * z + 2.0 * w * x ) * body.y + ( w * w - x * x - y * y + z * z ) * body.z;
			
			return nav;
		}
	}
//	typedef boost::shared_ptr<PoseHandle> PoseHandlePtr;

	public class TwistHandle : Handle<TwistHandle, Twist>
	{
		public TwistHandle () : base ("twist") {}
		public TwistHandle (QuadrotorInterface qInterface) : base (qInterface, "twist") {}
		public TwistHandle (QuadrotorInterface qInterface, Twist twist) : base (qInterface, twist, "twist") {}
		~TwistHandle() {}

		public Twist twist () { return get (); }
	};
//	typedef boost::shared_ptr<TwistHandle> TwistHandlePtr;

	public class AccelerationHandle : Handle<AccelerationHandle, Accel> // need to port Accel, apparently
	{
		public AccelerationHandle () : base ("acceleration") {}
		public AccelerationHandle (QuadrotorInterface qInterface) : base (qInterface, "acceleration") {}
		public AccelerationHandle (QuadrotorInterface qInterface, Accel acceleration) : base (qInterface, acceleration, "acceleration") {}
		~AccelerationHandle() {}

		public Accel acceleration () { return get (); }
	}
//	typedef boost::shared_ptr<AccelerationHandle> AccelerationHandlePtr;

	public class StateHandle
//	class StateHandle : public PoseHandle, public TwistHandle
	{
		public StateHandle () {}
		public StateHandle (QuadrotorInterface qInterface, Pose pose, Twist twist)
		{
			poseHandle = new PoseHandle ( qInterface, pose );
			twistHandle = new TwistHandle ( qInterface, twist );
		}
		~StateHandle () {}

		public virtual bool connected () { return poseHandle.connected () && twistHandle.connected (); }
		protected PoseHandle poseHandle;
		protected TwistHandle twistHandle;
	}
//	typedef boost::shared_ptr<StateHandle> StateHandlePtr;

	class ImuHandle : Handle<ImuHandle, Imu>
	{
		public ImuHandle () : base ("imu") {}
		public ImuHandle (QuadrotorInterface qInterface, Imu imu) : base (qInterface, imu, "imu") {}
		~ImuHandle() {}

		public Imu imu () { return get (); }
	}
//	typedef boost::shared_ptr<ImuHandle> ImuHandlePtr;

	class MotorStatusHandle : Handle<MotorStatusHandle, MotorStatus>
	{
		public MotorStatusHandle () : base ("motor_status") {}
		public MotorStatusHandle (QuadrotorInterface qInterface, MotorStatus motor_status) : base (qInterface, motor_status, "motor_status") {}
		~MotorStatusHandle () {}

		public MotorStatus motorStatus () { return get (); }
	}
//	typedef boost::shared_ptr<MotorStatusHandle> MotorStatusHandlePtr;

	public class CommandHandle
	{
		protected bool wasNew () { bool old = new_value_; new_value_ = false; return old; }
		protected bool new_value_;

		QuadrotorInterface interface_;
		string name_;
		string field_;
		object my_;
		bool preempted_;

		public CommandHandle ()
		{
			interface_ = null;
			name_ = "";
			preempted_ = false;
			new_value_ = false;
		}

		public CommandHandle (CommandHandle other)
		{
			interface_ = other.interface_;
			name_ = other.getName ();
			field_ = other.getField ();
			new_value_ = other.new_value_;
		}

		public CommandHandle (QuadrotorInterface qInterface, string name, string field)
		{
			interface_ = qInterface;
			name_ = name;
			field_ = field;
			new_value_ = false;
		}

		public void CopyFrom (CommandHandle other)
		{
			if ( other == null )
				return;
			interface_ = other.interface_;
			name_ = other.getName ();
			field_ = other.getField ();
			new_value_ = other.new_value_;
		}

		public void CopyFrom (QuadrotorInterface qInterface, string name, string field)
		{
			interface_ = qInterface;
			name_ = name;
			field_ = field;
			new_value_ = false;
		}

		~CommandHandle () {}

		public virtual string getName () { return name_; }
		public virtual string getField () { return field_; }
		public virtual bool connected () { return false; }
		public virtual void reset () {}

		public object get () { return null; }

		public bool enabled () { return interface_.enabled ( this ); }
		public bool start () { preempted_ = false; return interface_.start ( this ); }
		public void stop () { preempted_ = false; interface_.stop ( this ); }
		public void disconnect () { interface_.disconnect ( this ); }

		public bool preempt () { return interface_.preempt ( this ); }
		public void setPreempted () { preempted_ = true; }
		public bool preempted () { return preempted_; }

		public virtual System.Type GetValueType () { return typeof (object); }

		public T ownData<T> (T data)
		{
			my_ = data;
			return data;
		}

		public virtual void newData () { my_ = null; }

		public virtual object command () { return null; }

		public bool connectFrom<T> (T output) where T : CommandHandle
		{
			T me = (T) this;
			if ( me == null )
				return false;
			ROS.Debug ( "Connected output port '%s (%p)' to input port '%s (%p)'", output.getName (), output, me.getName (), me );
			return ( me = (T) output.get () ).connected ();
		}

		public bool connectTo<T> (T input) where T : CommandHandle
		{
			T me = (T) this;
			if ( me == null )
				return false;
			ROS.Debug ( "Connected output port '%s (%p)' to input port '%s (%p)'", me.getName (), me, input.getName (), input );
			return ( input = (T) me.get () ).connected ();
		}
	}
//	typedef boost::shared_ptr<CommandHandle> CommandHandlePtr;

//	namespace quadrotor_internal
//	{
//		internal struct FieldAccessor<Derived>
//		{
//			public delegate object get (object o);
////			static typename Derived::T *get(void *) { return 0; }
//		}
//	}

//	public abstract class MidHandle : CommandHandle
//	{
//		public MidHandle () : base () {}
//		public MidHandle (MidHandle other) : base (other) {}
//	}

	public class CommandHandle_<Derived, T, Parent> : CommandHandle
	{
		public CommandHandle_ ()
		{
			command_ = default (T);
		}

		public CommandHandle_ (CommandHandle other) : base (other)
		{
			command_ = default (T);
		}

		public CommandHandle_ (QuadrotorInterface qInterface, string name, string field = "") : base (qInterface, name, field)
		{
			command_ = default (T);
		}

		~CommandHandle_ () {}

		public virtual bool connected ()
		{
			return ( !get ().Equals (default (T)) );
		}
		public virtual void reset () { command_ = default (T); base.reset (); }

//		public Derived& operator=(T *source) { command_ = source; return static_cast<Derived &>(*this); }
		public T get () { return !command_.Equals (default (T)) ? command_ : (T) base.get (); }
//		public T operator*() const { return *command_; }

//		public T command () { return get (); }
		public T getCommand () { new_value_ = false; return get (); }
		public void setCommand (T command) { new_value_ = true; command_ = command; }
		public bool getCommand (T command) { command = getCommand (); return wasNew (); }

		public override Type GetValueType () { return typeof (T); }

		public override void newData ()
		{
			ownData ( default (T) );
		}

		public override object command ()
		{
			return get ();
		}

		public bool update (T command)
		{
			if ( !connected () )
				return false;
			command = getCommand ();
			return true;
		}

		protected T command_;
	}

	public class PoseCommandHandle : CommandHandle_<PoseCommandHandle, Pose, CommandHandle>
	{
		public PoseCommandHandle () {}
		public PoseCommandHandle (QuadrotorInterface qInterface, string name, string field = "") : base (qInterface, name, field) {}
		public PoseCommandHandle (Pose command) { command_ = command; }
		~PoseCommandHandle () {}
	}
//	typedef boost::shared_ptr<PoseCommandHandle> PoseCommandHandlePtr;

	public class HorizontalPositionCommandHandle : CommandHandle_<HorizontalPositionCommandHandle, Point, PoseCommandHandle>
	{
		public HorizontalPositionCommandHandle () {}
		public HorizontalPositionCommandHandle (PoseCommandHandle other) : base (other) {}
		public HorizontalPositionCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name, "position.xy") {}
		public HorizontalPositionCommandHandle (Point command) { command_ = command; }
		~HorizontalPositionCommandHandle () {}

		public virtual bool getCommand (ref double x, ref double y)
		{
			x = get ().x;
			y = get ().y;
			return wasNew ();
		}

		public virtual void setCommand (double x, double y)
		{
			new_value_ = true;
			get ().x = x;
			get ().y = y;
		}

		public bool update (Pose command)
		{
			if ( !connected () )
				return false;
			getCommand ( ref command.position.x, ref command.position.y );
			return true;
		}

		public void getError (PoseHandle pose, ref double x, ref double y)
		{
			getCommand ( ref x, ref y );
			x -= pose.get ().position.x;
			y -= pose.get ().position.y;
		}
	}
//	typedef boost::shared_ptr<HorizontalPositionCommandHandle> HorizontalPositionCommandHandlePtr;

//	namespace internal {
//	  template <> struct FieldAccessor<HorizontalPositionCommandHandle> {
//	    static Point *get(Pose *pose) { return &(pose.position); }
//	  };
//	}

	public class HeightCommandHandle : CommandHandle_<HeightCommandHandle, double, PoseCommandHandle>
	{
		public HeightCommandHandle () {}
		public HeightCommandHandle (PoseCommandHandle other) : base (other) {}
		public HeightCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name, "position.z") {}
		public HeightCommandHandle (double command) { command_ = command; }
		~HeightCommandHandle () {}

		public bool update (Pose command)
		{
			if ( !connected () )
				return false;
			command.position.z = getCommand ();
			return true;
		}

		public double getError (PoseHandle pose)
		{
			return getCommand () - pose.get ().position.z;
		}
	}
//	typedef boost::shared_ptr<HeightCommandHandle> HeightCommandHandlePtr;

//	namespace internal {
//	  template <> struct FieldAccessor<HeightCommandHandle> {
//	    static double *get(Pose *pose) { return &(pose.position.z); }
//	  };
//	}

	public class HeadingCommandHandle : CommandHandle_<HeadingCommandHandle, Quaternion, PoseCommandHandle>
	{
		protected double scalar_;

		public HeadingCommandHandle () {}
		public HeadingCommandHandle (PoseCommandHandle other) : base (other)
		{
			scalar_ = 0;
		}
		public HeadingCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name, "orientation.yaw")
		{
			scalar_ = 0;
		}
		public HeadingCommandHandle (Quaternion command) { command_ = command; }
		~HeadingCommandHandle () {}

		public double getCommand ()
		{
//			if (scalar_) return *scalar_; // um... shit? supposed to be a double* here...
			Quaternion q = get ();

			return Math.Atan2 ( 2.0 * q.x * q.y + 2.0 * q.w * q.z, q.x * q.x + q.w * q.w - q.z * q.z - q.y * q.y );
		}

		public void setCommand (double command)
		{
			if ( get () != null )
			{
				command_.x = 0.0;
				command_.y = 0.0;
				command_.z = Math.Sin ( command / 2.0 );
				command_.w = Math.Cos ( command / 2.0 );
			}

			scalar_ = command;
		}

		public bool update (Pose command)
		{
			if ( get () != null )
			{
				command.orientation = get ();
				return true;
			}
//			if (scalar_) { // again, shit...
			command.orientation.x = 0.0;
			command.orientation.y = 0.0;
			command.orientation.z = Math.Sin ( scalar_ / 2.0 );
			command.orientation.x = Math.Cos ( scalar_ / 2.0 );
			return true;
//			}
//			return false;
		}

		public double getError (PoseHandle pose)
		{
			double pi2 = 2.0 * Math.PI;
			double error = getCommand () - pose.getYaw () + pi2;
			error -= Math.Floor ( error / pi2 ) * pi2;
			return error - pi2;
		}
	}
//	typedef boost::shared_ptr<HeadingCommandHandle> HeadingCommandHandlePtr;

//	namespace internal {
//	  template <> struct FieldAccessor<HeadingCommandHandle> {
//	    static Quaternion *get(Pose *pose) { return &(pose.orientation); }
//	  };
//	}

	public class TwistCommandHandle : CommandHandle_<TwistCommandHandle, Twist, CommandHandle>
	{
		public TwistCommandHandle () {}
		public TwistCommandHandle (QuadrotorInterface qInterface, string name, string field = "") : base (qInterface, name, field) {}
		public TwistCommandHandle (Twist command) { command_ = command; }
		~TwistCommandHandle () {}
	}
//	typedef boost::shared_ptr<TwistCommandHandle> TwistCommandHandlePtr;

	public class AccelCommandHandle : CommandHandle_<AccelCommandHandle, Accel, CommandHandle>
	{
		public AccelCommandHandle () {}
		public AccelCommandHandle (QuadrotorInterface qInterface, string name, string field = "") : base (qInterface, name, field) {}
		public AccelCommandHandle (Accel command) { command_ = command; }
		~AccelCommandHandle () {}
	}
//	typedef boost::shared_ptr<AccelCommandHandle> AccelCommandHandlePtr;

	public class HorizontalVelocityCommandHandle : CommandHandle_<HorizontalVelocityCommandHandle, Vector3, TwistCommandHandle>
	{
		public HorizontalVelocityCommandHandle () {}
		public HorizontalVelocityCommandHandle (TwistCommandHandle other) : base (other) {}
		public HorizontalVelocityCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name, "linear.xy") {}
		public HorizontalVelocityCommandHandle (Vector3 command) { command_ = command; }
		~HorizontalVelocityCommandHandle () {}

		public bool getCommand (ref double x, ref double y)
		{
			x = get ().x;
			y = get ().y;
			return true;
		}

		public void setCommand (double x, double y)
		{
			get ().x = x;
			get ().y = y;
		}

		public bool update (Twist command)
		{
			if ( !connected () )
				return false;
			getCommand ( ref command.linear.x, ref command.linear.y );
			return true;
		}
	}
//	typedef boost::shared_ptr<HorizontalVelocityCommandHandle> HorizontalVelocityCommandHandlePtr;

//	namespace internal {
//	  template <> struct FieldAccessor<HorizontalVelocityCommandHandle> {
//	    static Vector3 *get(Twist *twist) { return &(twist.linear); }
//	  };
//	}

	public class VerticalVelocityCommandHandle : CommandHandle_<VerticalVelocityCommandHandle, double, TwistCommandHandle>
	{
		public VerticalVelocityCommandHandle () {}
		public VerticalVelocityCommandHandle (TwistCommandHandle other) : base (other) {}
		public VerticalVelocityCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name, "linear.z") {}
		public VerticalVelocityCommandHandle (double command) { command_ = command; }
		~VerticalVelocityCommandHandle () {}

		public bool update (Twist command)
		{
			if ( !connected () )
				return false;
			command.linear.z = get ();
			return true;
		}
	}
//	typedef boost::shared_ptr<VerticalVelocityCommandHandle> VerticalVelocityCommandHandlePtr;

//	namespace internal {
//	  template <> struct FieldAccessor<VerticalVelocityCommandHandle> {
//	    static double *get(Twist *twist) { return &(twist.linear.z); }
//	  };
//	}

	public class AngularVelocityCommandHandle : CommandHandle_<AngularVelocityCommandHandle, double, TwistCommandHandle>
	{
		public AngularVelocityCommandHandle () {}
		public AngularVelocityCommandHandle (TwistCommandHandle other) : base (other) {}
		public AngularVelocityCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name, "angular.z") {}
		public AngularVelocityCommandHandle (double command) { command_ = command; }
		~AngularVelocityCommandHandle () {}

		public bool update (Twist command)
		{
			if ( !connected () )
				return false;
			command.linear.z = get ();
			return true;
		}
	}
//	typedef boost::shared_ptr<AngularVelocityCommandHandle> AngularVelocityCommandHandlePtr;

//	namespace internal {
//	  template <> struct FieldAccessor<AngularVelocityCommandHandle> {
//	    static double *get(Twist *twist) { return &(twist.angular.z); }
//	  };
//	}

	public class WrenchCommandHandle : CommandHandle_<WrenchCommandHandle, Wrench, CommandHandle>
	{
		public WrenchCommandHandle () {}
		public WrenchCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name) {}
		~WrenchCommandHandle () {}
	}
//	typedef boost::shared_ptr<WrenchCommandHandle> WrenchCommandHandlePtr;

	public class MotorCommandHandle : CommandHandle_<MotorCommandHandle, MotorCommand, CommandHandle>
	{
		public MotorCommandHandle () {}
		public MotorCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name) {}
		~MotorCommandHandle () {}
	}
//	typedef boost::shared_ptr<MotorCommandHandle> MotorCommandHandlePtr;

	public class AttitudeCommandHandle : CommandHandle_<AttitudeCommandHandle, AttitudeCommand, CommandHandle>
	{
		public AttitudeCommandHandle () {}
		public AttitudeCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name) {}
		~AttitudeCommandHandle () {}
	}
//	typedef boost::shared_ptr<AttitudeCommandHandle> AttitudeCommandHandlePtr;

	public class YawrateCommandHandle : CommandHandle_<YawrateCommandHandle, YawRateCommand, CommandHandle>
	{
		public YawrateCommandHandle () {}
		public YawrateCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name) {}
		~YawrateCommandHandle () {}
	}
//	typedef boost::shared_ptr<YawrateCommandHandle> YawrateCommandHandlePtr;

	public class ThrustCommandHandle : CommandHandle_<ThrustCommandHandle, ThrustCommand, CommandHandle>
	{
		public ThrustCommandHandle () {}
		public ThrustCommandHandle (QuadrotorInterface qInterface, string name) : base (qInterface, name) {}
		~ThrustCommandHandle () {}
	}
//	typedef boost::shared_ptr<ThrustCommandHandle> ThrustCommandHandlePtr;

} // namespace hector_quadrotor_interface