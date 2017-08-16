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
using hector_uav_msgs;
//#include <limits> // ?
using hector_quadrotor_model;

namespace hector_quadrotor_interface
{
	public class FieldLimiter<T>
	{
		T min_, max_;

		public FieldLimiter ()
		{
//	    : min_(-numeric_limits<T>::infinity())
//	    , max_( numeric_limits<T>::infinity())
		}

//		public FieldLimiter (T value)
//		{
//			return limit ( value );
//		}

		public void init (NodeHandle nh, string field = "")
		{
			float fmin = float.NegativeInfinity;
			float fmax = float.PositiveInfinity;
			double dmin = double.NegativeInfinity;
			double dmax = double.PositiveInfinity;

			bool isDouble = typeof (T) == typeof (double);

			string prefix = !string.IsNullOrEmpty ( field ) ? field + "/" : "";
			if ( nh.hasParam ( prefix + "max" ) || nh.hasParam ( prefix + "min" ) )
			{
				object posinf = isDouble ? dmax : fmax;
				object neginf = isDouble ? dmin : fmin;
				nh.param<T> ( prefix + "max", ref max_, (T) posinf );
				nh.param<T> ( prefix + "min", ref min_, (T) neginf );
//				nh.param<T>(prefix + "max", ref max_, numeric_limits<T>::infinity());
//				nh.param<T>(prefix + "min", ref min_, -max_);
				ROS.Info ("Limits " + nh.getNamespace () + "/" + field + " initialized " + field + " with min " + min_ + " and max " + max_);
			}
		}

		public T limit (T value)
		{
			double d = (double) ( (object) value );
			double dmin = (double) ( (object) min_ );
			double dmax = (double) ( (object) max_ );
			float f = (float) ( (object) value );
			float fmin = (float) ( (object) min_ );
			float fmax = (float) ( (object) max_ );

			if ( typeof (T) == typeof (double) )
			{
				if ( !Helpers.isnan ( dmin ) && d < dmin )
					value = min_;
				if ( !Helpers.isnan ( dmax ) && d > dmax )
					value = max_;
				
			} else
			{
				if ( !Helpers.isnan ( fmin ) && f < fmin )
					value = min_;
				if ( !Helpers.isnan ( fmax ) && f > fmax )
					value = max_;
			}
			return value;
		}

//		public T limit (T value)
//		{
//			return max ( min_, min ( max_, value ) );
//		}

		// can't overload () here..
//		public T operator () (T value)
//		{
//	    	return limit ( value );
//		} 
	}


	class PointLimiter
	{
		FieldLimiter<double> x, y, z;

		public PointLimiter ()
		{
			x = new FieldLimiter<double> ();
			y = new FieldLimiter<double> ();
			z = new FieldLimiter<double> ();
		}

		public void init (NodeHandle nh, string field = "")
		{
			NodeHandle field_nh = new NodeHandle ( nh, field );
			x.init ( field_nh, "x" );
			y.init ( field_nh, "y" );
			z.init ( field_nh, "z" );
		}

		public Point limit (Point input)
		{
			Point output = new Point ();
			output.x = x.limit ( input.x );
			output.y = y.limit ( input.y );
			output.z = z.limit ( input.z );
			return output;
		}

		// can't overload () here..
//		public Point operator() (Point input)
//		{
//	    	return limit ( input );
//		}
	}

	public class Vector3Limiter
	{
		FieldLimiter<double> x, y, z;
		double absolute_maximum, absolute_maximum_xy;

		public Vector3Limiter ()
		{
			x = new FieldLimiter<double> ();
			y = new FieldLimiter<double> ();
			z = new FieldLimiter<double> ();
		}

		public void init (NodeHandle nh, string field = "")
		{
			NodeHandle field_nh = new NodeHandle ( nh, field );
			x.init ( field_nh, "x" );
			y.init ( field_nh, "y" );
			z.init ( field_nh, "z" );
			field_nh.param ( "max", ref absolute_maximum, double.PositiveInfinity );
			field_nh.param ( "max_xy", ref absolute_maximum_xy, double.PositiveInfinity );
		}

		public Vector3 limit (Vector3 input)
		{
			Vector3 output = new Vector3 ();
			output.x = x.limit ( input.x );
			output.y = y.limit ( input.y );
			output.z = z.limit ( input.z );

			double absolute_value_xy = System.Math.Sqrt ( output.x * output.x + output.y * output.y );
			if ( absolute_value_xy > absolute_maximum_xy )
			{
				output.x *= absolute_maximum_xy / absolute_value_xy;
				output.y *= absolute_maximum_xy / absolute_value_xy;
				output.z *= absolute_maximum_xy / absolute_value_xy;
			}

			double absolute_value = System.Math.Sqrt ( output.x * output.x + output.y * output.y + output.z * output.z );
			if ( absolute_value > absolute_maximum )
			{
				output.x *= absolute_maximum / absolute_value;
				output.y *= absolute_maximum / absolute_value;
				output.z *= absolute_maximum / absolute_value;
			}

			return output;
		}

		// can't overload () here..
//		public Vector3 operator () (Vector3 input)
//		{
//			return limit ( input );
//		}
	}


	public class TwistLimiter
	{
		Vector3Limiter linear, angular;

		public void init (NodeHandle nh, string field = "")
		{
			NodeHandle field_nh = new NodeHandle ( nh, field );
			linear.init ( field_nh, "linear" );
			angular.init ( field_nh, "angular" );
		}

		public Twist limit (Twist input)
		{
			Twist output = new Twist ();
			output.linear = linear.limit ( input.linear );
			output.angular = angular.limit ( input.angular );
			return output;
		}

		// can't overload () here..
//		public Twist operator () (Twist input)
//		{
//			return limit ( input );
//		}
	}

	public class WrenchLimiter
	{
		Vector3Limiter force, torque;

		public void init (NodeHandle nh, string field = "")
		{
			NodeHandle field_nh = new NodeHandle ( nh, field );
			force.init ( field_nh, "force" );
			torque.init ( field_nh, "torque" );
		}

		public Wrench limit (Wrench input)
		{
			Wrench output = new Wrench ();
			output.force = force.limit ( input.force );
			output.torque = torque.limit ( input.torque );
			return output;
		}

		// can't overload () here..
//		public Wrench operator () (Wrench input)
//		{
//			return limit ( input );
//		}
	}

	public class AttitudeCommandLimiter
	{
		FieldLimiter<double> roll, pitch;
		double absolute_max;

		public void init (NodeHandle nh, string field = "")
		{
			NodeHandle field_nh = new NodeHandle ( nh, field );
			roll.init ( field_nh, "roll" );
			pitch.init ( field_nh, "pitch" );
			field_nh.param<double> ( "max_roll_pitch", ref absolute_max, double.PositiveInfinity );
		}

		public AttitudeCommand limit (AttitudeCommand input)
		{
			AttitudeCommand output = new AttitudeCommand ();
			output.header = input.header;
			output.roll = (float) roll.limit ( input.roll );
			output.pitch = (float) pitch.limit ( input.pitch );

			double absolute_value = System.Math.Sqrt ( output.roll * output.roll + output.pitch * output.pitch );
			if ( absolute_value > absolute_max )
			{
				output.roll *= (float) ( absolute_max / absolute_value );
				output.pitch *= (float) ( absolute_max / absolute_value );
			}

			return output;
		}

		// can't overload () here..
//		public AttitudeCommand operator () (AttitudeCommand input)
//		{
//			return limit ( input );
//		}
	}

	public class YawRateCommandLimiter
	{
		FieldLimiter<double> turnrate;

		public void init (NodeHandle nh, string field = "")
		{
			turnrate.init ( nh, field );
		}

		public YawRateCommand limit (YawRateCommand input)
		{
			YawRateCommand output = new YawRateCommand ();
			output.header = input.header;
			output.turnrate = (float) turnrate.limit ( input.turnrate );
			return output;
		}

		// can't overload () here..
//		public YawRateCommand operator () (YawRateCommand input)
//		{
//			return limit ( input );
//		}
	}

	public class ThrustCommandLimiter
	{
		FieldLimiter<double> thrust;

		public void init (NodeHandle nh, string field = "")
		{
			thrust.init ( nh, field );
		}

		public ThrustCommand limit (ThrustCommand input)
		{
			ThrustCommand output = new ThrustCommand ();
			output.header = input.header;
			output.thrust = (float) thrust.limit ( input.thrust );
			return output;
		}

		// can't overload () here..
//		public ThrustCommand operator () (ThrustCommand input)
//		{
//			return limit ( input );
//		}
	}
} // namespace hector_quadrotor_interface