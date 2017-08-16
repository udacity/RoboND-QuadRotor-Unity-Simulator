using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using uint8 = System.Byte;
using Messages.geometry_msgs;
using Messages.sensor_msgs;
using Messages.actionlib_msgs;

using Messages.std_msgs;
using String=System.String;

namespace Messages.std_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Time : IRosMessage
    {

			public TimeData data; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "cd7166c74c552c311fbcc2fe5a7bc289"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"time data"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.std_msgs__Time; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Time()
        {
            
        }

		public Time (Time other)
		{
			data = new TimeData ( other.data.sec, other.data.nsec );
		}

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Time(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Time(byte[] SERIALIZEDSTUFF, ref int currentIndex)
        {
            Deserialize(SERIALIZEDSTUFF, ref currentIndex);
        }


		public Time(TimeData d)
		{
			data = d;
		}


        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override void Deserialize(byte[] SERIALIZEDSTUFF, ref int currentIndex)
        {
            int arraylength=-1;
            bool hasmetacomponents = false;
            object __thing;
            int piecesize=0;
            byte[] thischunk, scratch1, scratch2;
            IntPtr h;
            
            //data
            data.sec = BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            data.nsec  = BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override byte[] Serialize(bool partofsomethingelse)
        {
            int currentIndex=0, length=0;
            bool hasmetacomponents = false;
            byte[] thischunk, scratch1, scratch2;
            List<byte[]> pieces = new List<byte[]>();
            GCHandle h;
            
            //data
            pieces.Add(BitConverter.GetBytes(data.sec));
            pieces.Add(BitConverter.GetBytes(data.nsec));
            //combine every array in pieces into one array and return it
            int __a_b__f = pieces.Sum((__a_b__c)=>__a_b__c.Length);
            int __a_b__e=0;
            byte[] __a_b__d = new byte[__a_b__f];
            foreach(var __p__ in pieces)
            {
                Array.Copy(__p__,0,__a_b__d,__a_b__e,__p__.Length);
                __a_b__e += __p__.Length;
            }
            return __a_b__d;
        }

        public override void Randomize()
        {
            int arraylength=-1;
            Random rand = new Random();
            int strlength;
            byte[] strbuf, myByte;
            
            //data
            data.sec = Convert.ToUInt32(rand.Next());
            data.nsec  = Convert.ToUInt32(rand.Next());
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            std_msgs.Time other = (Messages.std_msgs.Time)____other;

            ret &= data.Equals(other.data);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }

		public static Time operator - (Time lhs, Time rhs)
		{
			double nsecLeft = (double) lhs.data.nsec + (double) lhs.data.sec * 1e9;
			double nsecRight = (double) rhs.data.nsec + (double) rhs.data.sec * 1e9;
			double nsecTotal = nsecLeft - nsecRight;
			uint sec = (uint) ( nsecTotal - (ulong) nsecTotal / 1e9 );
			uint nsec = (uint) ( nsecTotal - sec );
			return new Time ( new TimeData ( sec, nsec ) );
		}

		public static implicit operator Duration (Time t)
		{
			return new Duration ( t.data );
		}

		public static Time operator + (Time lhs, Duration rhs)
		{
			return new Time ( lhs.data + rhs.data );
		}

		public static bool operator >= (Time lhs, Time rhs)
		{
			if ( ReferenceEquals ( lhs, rhs ) )
				return true;

			return lhs.data.toSec () >= rhs.data.toSec ();
		}

		public static bool operator <= (Time lhs, Time rhs)
		{
			if ( ReferenceEquals ( lhs, rhs ) )
				return true;

			return lhs.data.toSec () <= rhs.data.toSec ();
		}

		public static bool operator > (Time lhs, Time rhs)
		{
			return !( lhs <= rhs );
		}

		public static bool operator < (Time lhs, Time rhs)
		{
			return !( lhs >= rhs );
		}
    }
}
