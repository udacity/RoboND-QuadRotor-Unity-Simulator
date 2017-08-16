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

namespace Messages.sensor_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Imu : IRosMessage
    {

			public Header header; //woo
			public Quaternion orientation; //woo
			public double[] orientation_covariance = new double[9];
			public Vector3 angular_velocity; //woo
			public double[] angular_velocity_covariance = new double[9];
			public Vector3 linear_acceleration; //woo
			public double[] linear_acceleration_covariance = new double[9];


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "6a62c6daae103f4ff57a132d6f95cec2"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
geometry_msgs/Quaternion orientation
float64[9] orientation_covariance
geometry_msgs/Vector3 angular_velocity
float64[9] angular_velocity_covariance
geometry_msgs/Vector3 linear_acceleration
float64[9] linear_acceleration_covariance"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__Imu; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Imu()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Imu(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Imu(byte[] SERIALIZEDSTUFF, ref int currentIndex)
        {
            Deserialize(SERIALIZEDSTUFF, ref currentIndex);
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
            
            //header
            header = new Header(SERIALIZEDSTUFF, ref currentIndex);
            //orientation
            orientation = new Quaternion(SERIALIZEDSTUFF, ref currentIndex);
            //orientation_covariance
            hasmetacomponents |= false;
            if (orientation_covariance == null)
                orientation_covariance = new double[9];
            else
                Array.Resize(ref orientation_covariance, 9);
            for (int i=0;i<orientation_covariance.Length; i++) {
                //orientation_covariance[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                orientation_covariance[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //angular_velocity
            angular_velocity = new Vector3(SERIALIZEDSTUFF, ref currentIndex);
            //angular_velocity_covariance
            hasmetacomponents |= false;
            if (angular_velocity_covariance == null)
                angular_velocity_covariance = new double[9];
            else
                Array.Resize(ref angular_velocity_covariance, 9);
            for (int i=0;i<angular_velocity_covariance.Length; i++) {
                //angular_velocity_covariance[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                angular_velocity_covariance[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //linear_acceleration
            linear_acceleration = new Vector3(SERIALIZEDSTUFF, ref currentIndex);
            //linear_acceleration_covariance
            hasmetacomponents |= false;
            if (linear_acceleration_covariance == null)
                linear_acceleration_covariance = new double[9];
            else
                Array.Resize(ref linear_acceleration_covariance, 9);
            for (int i=0;i<linear_acceleration_covariance.Length; i++) {
                //linear_acceleration_covariance[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                linear_acceleration_covariance[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
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
            
            //header
            if (header == null)
                header = new Header();
            pieces.Add(header.Serialize(true));
            //orientation
            if (orientation == null)
                orientation = new Quaternion();
            pieces.Add(orientation.Serialize(true));
            //orientation_covariance
            hasmetacomponents |= false;
            if (orientation_covariance == null)
                orientation_covariance = new double[0];
            for (int i=0;i<orientation_covariance.Length; i++) {
                //orientation_covariance[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(orientation_covariance[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //angular_velocity
            if (angular_velocity == null)
                angular_velocity = new Vector3();
            pieces.Add(angular_velocity.Serialize(true));
            //angular_velocity_covariance
            hasmetacomponents |= false;
            if (angular_velocity_covariance == null)
                angular_velocity_covariance = new double[0];
            for (int i=0;i<angular_velocity_covariance.Length; i++) {
                //angular_velocity_covariance[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(angular_velocity_covariance[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //linear_acceleration
            if (linear_acceleration == null)
                linear_acceleration = new Vector3();
            pieces.Add(linear_acceleration.Serialize(true));
            //linear_acceleration_covariance
            hasmetacomponents |= false;
            if (linear_acceleration_covariance == null)
                linear_acceleration_covariance = new double[0];
            for (int i=0;i<linear_acceleration_covariance.Length; i++) {
                //linear_acceleration_covariance[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(linear_acceleration_covariance[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
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
            
            //header
            header = new Header();
            header.Randomize();
            //orientation
            orientation = new Quaternion();
            orientation.Randomize();
            //orientation_covariance
            if (orientation_covariance == null)
                orientation_covariance = new double[9];
            else
                Array.Resize(ref orientation_covariance, 9);
            for (int i=0;i<orientation_covariance.Length; i++) {
                //orientation_covariance[i]
                orientation_covariance[i] = (rand.Next() + rand.NextDouble());
            }
            //angular_velocity
            angular_velocity = new Vector3();
            angular_velocity.Randomize();
            //angular_velocity_covariance
            if (angular_velocity_covariance == null)
                angular_velocity_covariance = new double[9];
            else
                Array.Resize(ref angular_velocity_covariance, 9);
            for (int i=0;i<angular_velocity_covariance.Length; i++) {
                //angular_velocity_covariance[i]
                angular_velocity_covariance[i] = (rand.Next() + rand.NextDouble());
            }
            //linear_acceleration
            linear_acceleration = new Vector3();
            linear_acceleration.Randomize();
            //linear_acceleration_covariance
            if (linear_acceleration_covariance == null)
                linear_acceleration_covariance = new double[9];
            else
                Array.Resize(ref linear_acceleration_covariance, 9);
            for (int i=0;i<linear_acceleration_covariance.Length; i++) {
                //linear_acceleration_covariance[i]
                linear_acceleration_covariance[i] = (rand.Next() + rand.NextDouble());
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.Imu other = (Messages.sensor_msgs.Imu)____other;

            ret &= header.Equals(other.header);
            ret &= orientation.Equals(other.orientation);
            if (orientation_covariance.Length != other.orientation_covariance.Length)
                return false;
            for (int __i__=0; __i__ < orientation_covariance.Length; __i__++)
            {
                ret &= orientation_covariance[__i__] == other.orientation_covariance[__i__];
            }
            ret &= angular_velocity.Equals(other.angular_velocity);
            if (angular_velocity_covariance.Length != other.angular_velocity_covariance.Length)
                return false;
            for (int __i__=0; __i__ < angular_velocity_covariance.Length; __i__++)
            {
                ret &= angular_velocity_covariance[__i__] == other.angular_velocity_covariance[__i__];
            }
            ret &= linear_acceleration.Equals(other.linear_acceleration);
            if (linear_acceleration_covariance.Length != other.linear_acceleration_covariance.Length)
                return false;
            for (int __i__=0; __i__ < linear_acceleration_covariance.Length; __i__++)
            {
                ret &= linear_acceleration_covariance[__i__] == other.linear_acceleration_covariance[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
