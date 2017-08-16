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
    public class NavSatStatus : IRosMessage
    {

			public const sbyte STATUS_NO_FIX =  -1; //woo
			public const sbyte STATUS_FIX =      0; //woo
			public const sbyte STATUS_SBAS_FIX = 1; //woo
			public const sbyte STATUS_GBAS_FIX = 2; //woo
			public sbyte status; //woo
			public const ushort SERVICE_GPS =     1; //woo
			public const ushort SERVICE_GLONASS = 2; //woo
			public const ushort SERVICE_COMPASS = 4; //woo
			public const ushort SERVICE_GALILEO = 8; //woo
			public ushort service; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "331cdbddfa4bc96ffc3b9ad98900a54c"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"int8 STATUS_NO_FIX=-1
int8 STATUS_FIX=0
int8 STATUS_SBAS_FIX=1
int8 STATUS_GBAS_FIX=2
int8 status
uint16 SERVICE_GPS=1
uint16 SERVICE_GLONASS=2
uint16 SERVICE_COMPASS=4
uint16 SERVICE_GALILEO=8
uint16 service"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__NavSatStatus; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public NavSatStatus()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public NavSatStatus(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public NavSatStatus(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //status
            piecesize = Marshal.SizeOf(typeof(sbyte));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            status = (sbyte)Marshal.PtrToStructure(h, typeof(sbyte));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //service
            piecesize = Marshal.SizeOf(typeof(ushort));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            service = (ushort)Marshal.PtrToStructure(h, typeof(ushort));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
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
            
            //status
            scratch1 = new byte[Marshal.SizeOf(typeof(sbyte))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(status, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //service
            scratch1 = new byte[Marshal.SizeOf(typeof(ushort))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(service, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
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
            
            //status
            status = (SByte)(rand.Next(255) - 127);
            //service
            service = (System.UInt16)rand.Next(System.UInt16.MaxValue + 1);
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.NavSatStatus other = (Messages.sensor_msgs.NavSatStatus)____other;

            ret &= status == other.status;
            ret &= service == other.service;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
