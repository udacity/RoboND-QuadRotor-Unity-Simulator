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

namespace Messages.nav_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class MapMetaData : IRosMessage
    {

			public Time map_load_time; //woo
			public Single resolution; //woo
			public uint width; //woo
			public uint height; //woo
			public Pose origin; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "10cfc8a2818024d3248802c00c95f11b"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"time map_load_time
float32 resolution
uint32 width
uint32 height
geometry_msgs/Pose origin"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.nav_msgs__MapMetaData; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public MapMetaData()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public MapMetaData(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public MapMetaData(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //map_load_time
            map_load_time = new Time(new TimeData(
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex),
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex+Marshal.SizeOf(typeof(System.Int32)))));
            currentIndex += 2*Marshal.SizeOf(typeof(System.Int32));
            //resolution
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            resolution = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //width
            piecesize = Marshal.SizeOf(typeof(uint));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            width = (uint)Marshal.PtrToStructure(h, typeof(uint));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //height
            piecesize = Marshal.SizeOf(typeof(uint));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            height = (uint)Marshal.PtrToStructure(h, typeof(uint));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //origin
            origin = new Pose(SERIALIZEDSTUFF, ref currentIndex);
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
            
            //map_load_time
            pieces.Add(BitConverter.GetBytes(map_load_time.data.sec));
            pieces.Add(BitConverter.GetBytes(map_load_time.data.nsec));
            //resolution
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(resolution, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //width
            scratch1 = new byte[Marshal.SizeOf(typeof(uint))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(width, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //height
            scratch1 = new byte[Marshal.SizeOf(typeof(uint))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(height, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //origin
            if (origin == null)
                origin = new Pose();
            pieces.Add(origin.Serialize(true));
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
            
            //map_load_time
            map_load_time = new Time(new TimeData(
                    Convert.ToUInt32(rand.Next()),
                    Convert.ToUInt32(rand.Next())));
            //resolution
            resolution = (float)(rand.Next() + rand.NextDouble());
            //width
            width = (uint)rand.Next();
            //height
            height = (uint)rand.Next();
            //origin
            origin = new Pose();
            origin.Randomize();
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            nav_msgs.MapMetaData other = (Messages.nav_msgs.MapMetaData)____other;

            ret &= map_load_time.data.Equals(other.map_load_time.data);
            ret &= resolution == other.resolution;
            ret &= width == other.width;
            ret &= height == other.height;
            ret &= origin.Equals(other.origin);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
