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
using Messages.geometry_msgs;

namespace hector_quadrotor_model
{
	public class Helpers
	{
		public static bool isnan<T> ( T val )
		{
			return !val.Equals ( val );
		}

		public static bool isnan<T> (T[] array)
		{
			return array.TrueForAll ( (x ) =>
			{
				return !isnan ( x );
			} );
		}

		public static bool isnan<T> (T[] array, int start, int end)
		{
			T[] array2 = new T[end - start + 1];
			for ( int i = start; i < end; i++ )
				array2 [ i - start ] = array [ i ];

			return array2.TrueForAll ( (x ) =>
			{
				return !isnan ( x );
			} );
		}

		public static bool isinf<T> (T value)
		{
			float f = (float) ( (object) value );
			double d = (double) ( (object) value );
			if ( typeof (T) == typeof (float) )
				return f == float.PositiveInfinity || f == float.NegativeInfinity;
			else
			if ( typeof (T) == typeof (double) )
				return d == double.PositiveInfinity || d == double.NegativeInfinity;
			else
				return false;
		}

		public static bool isinf<T> (T[] array)
		{
			return array.TrueForAll ( (x ) =>
			{
				return !isinf ( x );
			} );
		}

		public static bool isinf<T> (T[] array, int start, int end)
		{
			T[] array2 = new T[end - start + 1];
			for ( int i = start; i < end; i++ )
				array2 [ i - start ] = array [ i ];

			return array2.TrueForAll ( (x ) =>
			{
				return !isinf ( x );
			} );
		}

		public static T limit<T> (T value, T min, T max)
		{
			double d = (double) ( (object) value );
			double dmin = (double) ( (object) min );
			double dmax = (double) ( (object) max );
			if (!isnan(dmin) && d < dmin) value = min;
			if (!isnan(dmax) && d > dmax) value = max;
			return value;
		}

		public static void limit<T> (ref T[] array, T min, T max) where T : struct
		{
			array.ForEach ( x => x = limit ( x, min, max ) );
//			array.ForEach ( (x) =>
//			{
//				limit ( ref x, min, max );
//			} );
		}

		public static void limit<T> (ref T[] array, int start, int end, T min, T max)
		{
			for ( int i = start; i < end; i++ )
			{
				array [ i ] = limit ( array [ i ], min, max );
			}
		}

		public static void checknan<T> (ref T value, string errorText = "")
		{
			if ( isnan ( value ) )
			{
				#if DEBUG
				if ( errorText != "" )
					UnityEngine.Debug.LogError ( errorText + " contains **!?* Nan values!" );
				#endif
				value = default (T);
				return;
			}
			if ( isinf ( value ) )
			{
				#if DEBUG
				if ( errorText != "" )
					UnityEngine.Debug.LogError ( errorText + " is +-Inf!" );
				#endif
				value = default (T);
			}
		}

		// ??? fucking C++, man. how you gonna pass a generic type and just assume it has an xyz in it...
		/*
	template <typename Message, typename Vector> static inline void toVector(const Message& msg, Vector& vector)
	{
	  vector.x = msg.x;
	  vector.y = msg.y;
	  vector.z = msg.z;
	}

	template <typename Message, typename Vector> static inline void fromVector(const Vector& vector, Message& msg)
	{
	  msg.x = vector.x;
	  msg.y = vector.y;
	  msg.z = vector.z;
	}

	template <typename Message, typename Quaternion> static inline void toQuaternion(const Message& msg, Quaternion& vector)
	{
	  vector.w = msg.w;
	  vector.x = msg.x;
	  vector.y = msg.y;
	  vector.z = msg.z;
	}

	template <typename Message, typename Quaternion> static inline void fromQuaternion(const Quaternion& vector, Message& msg)
	{
	  msg.w = vector.w;
	  msg.x = vector.x;
	  msg.y = vector.y;
	  msg.z = vector.z;
	}

	static inline geometry_msgs::Vector3 operator+(const geometry_msgs::Vector3& a, const geometry_msgs::Vector3& b)
	{
	  geometry_msgs::Vector3 result;
	  result.x = a.x + b.x;
	  result.y = a.y + b.y;
	  result.z = a.z + b.z;
	  return result;
	}*/

		// moved this over to Wrench.cs, so commenting out
/*		public static Wrench operator+ (Wrench lhs, Wrench rhs)
		{
			Wrench result = new Wrench ();
			result.force = lhs.force + rhs.force;
			result.torque = lhs.torque + rhs.torque;
			return result;
		}*/

/*		public class PrintVector<T>
		{
		public PrintVector (const_iterator begin, const_iterator end, const std::string &delimiter = "[ ]") : begin_(begin), end_(end), delimiter_(delimiter) {}
		const_iterator begin() const { return begin_; }
		const_iterator end() const { return end_; }
		std::size_t size() const { return end_ - begin_; }

		std::ostream& operator>>(std::ostream& os) const {
		if (!delimiter_.empty()) os << delimiter_.substr(0, delimiter_.size() - 1);
		for(const_iterator it = begin(); it != end(); ++it) {
		if (it != begin()) os << " ";
		os << *it;
		}
		if (!delimiter_.empty()) os << delimiter_.substr(1, delimiter_.size() - 1);
		return os;
		}

		private:
		const_iterator begin_, end_;
		std::string delimiter_;
		};
		template <typename T> std::ostream &operator<<(std::ostream& os, const PrintVector<T>& vector) { return vector >> os; }*/
		
	}
}