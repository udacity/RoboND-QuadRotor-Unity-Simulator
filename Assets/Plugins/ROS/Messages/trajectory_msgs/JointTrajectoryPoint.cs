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

namespace Messages.trajectory_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class JointTrajectoryPoint : IRosMessage
    {

			public double[] positions;
			public double[] velocities;
			public double[] accelerations;
			public double[] effort;
			public Duration time_from_start; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "f3cd1e1c4d320c79d6985c904ae5dcd3"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"float64[] positions
float64[] velocities
float64[] accelerations
float64[] effort
duration time_from_start"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.trajectory_msgs__JointTrajectoryPoint; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public JointTrajectoryPoint()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public JointTrajectoryPoint(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public JointTrajectoryPoint(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //positions
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (positions == null)
                positions = new double[arraylength];
            else
                Array.Resize(ref positions, arraylength);
            for (int i=0;i<positions.Length; i++) {
                //positions[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                positions[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //velocities
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (velocities == null)
                velocities = new double[arraylength];
            else
                Array.Resize(ref velocities, arraylength);
            for (int i=0;i<velocities.Length; i++) {
                //velocities[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                velocities[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //accelerations
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (accelerations == null)
                accelerations = new double[arraylength];
            else
                Array.Resize(ref accelerations, arraylength);
            for (int i=0;i<accelerations.Length; i++) {
                //accelerations[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                accelerations[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //effort
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (effort == null)
                effort = new double[arraylength];
            else
                Array.Resize(ref effort, arraylength);
            for (int i=0;i<effort.Length; i++) {
                //effort[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                effort[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //time_from_start
            time_from_start = new Duration(new TimeData(
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex),
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex+Marshal.SizeOf(typeof(System.Int32)))));
            currentIndex += 2*Marshal.SizeOf(typeof(System.Int32));
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
            
            //positions
            hasmetacomponents |= false;
            if (positions == null)
                positions = new double[0];
            pieces.Add(BitConverter.GetBytes(positions.Length));
            for (int i=0;i<positions.Length; i++) {
                //positions[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(positions[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //velocities
            hasmetacomponents |= false;
            if (velocities == null)
                velocities = new double[0];
            pieces.Add(BitConverter.GetBytes(velocities.Length));
            for (int i=0;i<velocities.Length; i++) {
                //velocities[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(velocities[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //accelerations
            hasmetacomponents |= false;
            if (accelerations == null)
                accelerations = new double[0];
            pieces.Add(BitConverter.GetBytes(accelerations.Length));
            for (int i=0;i<accelerations.Length; i++) {
                //accelerations[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(accelerations[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //effort
            hasmetacomponents |= false;
            if (effort == null)
                effort = new double[0];
            pieces.Add(BitConverter.GetBytes(effort.Length));
            for (int i=0;i<effort.Length; i++) {
                //effort[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(effort[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //time_from_start
            pieces.Add(BitConverter.GetBytes(time_from_start.data.sec));
            pieces.Add(BitConverter.GetBytes(time_from_start.data.nsec));
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
            
            //positions
            arraylength = rand.Next(10);
            if (positions == null)
                positions = new double[arraylength];
            else
                Array.Resize(ref positions, arraylength);
            for (int i=0;i<positions.Length; i++) {
                //positions[i]
                positions[i] = (rand.Next() + rand.NextDouble());
            }
            //velocities
            arraylength = rand.Next(10);
            if (velocities == null)
                velocities = new double[arraylength];
            else
                Array.Resize(ref velocities, arraylength);
            for (int i=0;i<velocities.Length; i++) {
                //velocities[i]
                velocities[i] = (rand.Next() + rand.NextDouble());
            }
            //accelerations
            arraylength = rand.Next(10);
            if (accelerations == null)
                accelerations = new double[arraylength];
            else
                Array.Resize(ref accelerations, arraylength);
            for (int i=0;i<accelerations.Length; i++) {
                //accelerations[i]
                accelerations[i] = (rand.Next() + rand.NextDouble());
            }
            //effort
            arraylength = rand.Next(10);
            if (effort == null)
                effort = new double[arraylength];
            else
                Array.Resize(ref effort, arraylength);
            for (int i=0;i<effort.Length; i++) {
                //effort[i]
                effort[i] = (rand.Next() + rand.NextDouble());
            }
            //time_from_start
            time_from_start = new Duration(new TimeData(
                    Convert.ToUInt32(rand.Next()),
                    Convert.ToUInt32(rand.Next())));
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            trajectory_msgs.JointTrajectoryPoint other = (Messages.trajectory_msgs.JointTrajectoryPoint)____other;

            if (positions.Length != other.positions.Length)
                return false;
            for (int __i__=0; __i__ < positions.Length; __i__++)
            {
                ret &= positions[__i__] == other.positions[__i__];
            }
            if (velocities.Length != other.velocities.Length)
                return false;
            for (int __i__=0; __i__ < velocities.Length; __i__++)
            {
                ret &= velocities[__i__] == other.velocities[__i__];
            }
            if (accelerations.Length != other.accelerations.Length)
                return false;
            for (int __i__=0; __i__ < accelerations.Length; __i__++)
            {
                ret &= accelerations[__i__] == other.accelerations[__i__];
            }
            if (effort.Length != other.effort.Length)
                return false;
            for (int __i__=0; __i__ < effort.Length; __i__++)
            {
                ret &= effort[__i__] == other.effort[__i__];
            }
            ret &= time_from_start.data.Equals(other.time_from_start.data);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
