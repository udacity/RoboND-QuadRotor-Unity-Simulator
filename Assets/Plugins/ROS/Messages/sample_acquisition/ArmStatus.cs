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

namespace Messages.sample_acquisition
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ArmStatus : IRosMessage
    {

			public long pan_position; //woo
			public long tilt_position; //woo
			public long cable_position; //woo
			public bool engaged; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "c30065036257856887468e4606faa9f3"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"int64 pan_position
int64 tilt_position
int64 cable_position
bool engaged"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sample_acquisition__ArmStatus; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ArmStatus()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ArmStatus(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ArmStatus(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //pan_position
            piecesize = Marshal.SizeOf(typeof(long));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            pan_position = (long)Marshal.PtrToStructure(h, typeof(long));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //tilt_position
            piecesize = Marshal.SizeOf(typeof(long));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            tilt_position = (long)Marshal.PtrToStructure(h, typeof(long));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //cable_position
            piecesize = Marshal.SizeOf(typeof(long));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            cable_position = (long)Marshal.PtrToStructure(h, typeof(long));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //engaged
            engaged = SERIALIZEDSTUFF[currentIndex++]==1;
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
            
            //pan_position
            scratch1 = new byte[Marshal.SizeOf(typeof(long))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(pan_position, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //tilt_position
            scratch1 = new byte[Marshal.SizeOf(typeof(long))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(tilt_position, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //cable_position
            scratch1 = new byte[Marshal.SizeOf(typeof(long))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(cable_position, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //engaged
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)engaged ? 1 : 0 );
            pieces.Add(thischunk);
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
            
            //pan_position
            pan_position = (System.Int64)(rand.Next() << 32) | rand.Next();
            //tilt_position
            tilt_position = (System.Int64)(rand.Next() << 32) | rand.Next();
            //cable_position
            cable_position = (System.Int64)(rand.Next() << 32) | rand.Next();
            //engaged
            engaged = rand.Next(2) == 1;
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sample_acquisition.ArmStatus other = (Messages.sample_acquisition.ArmStatus)____other;

            ret &= pan_position == other.pan_position;
            ret &= tilt_position == other.tilt_position;
            ret &= cable_position == other.cable_position;
            ret &= engaged == other.engaged;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
