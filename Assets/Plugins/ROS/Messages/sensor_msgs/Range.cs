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
    public class Range : IRosMessage
    {

			public Header header; //woo
			public const byte ULTRASOUND = 0; //woo
			public const byte INFRARED = 1; //woo
			public byte radiation_type; //woo
			public Single field_of_view; //woo
			public Single min_range; //woo
			public Single max_range; //woo
			public Single range; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "c005c34273dc426c67a020a87bc24148"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
uint8 ULTRASOUND=0
uint8 INFRARED=1
uint8 radiation_type
float32 field_of_view
float32 min_range
float32 max_range
float32 range"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__Range; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Range()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Range(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Range(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //radiation_type
            radiation_type=SERIALIZEDSTUFF[currentIndex++];
            //field_of_view
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            field_of_view = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //min_range
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            min_range = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //max_range
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            max_range = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //range
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            range = (Single)Marshal.PtrToStructure(h, typeof(Single));
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
            
            //header
            if (header == null)
                header = new Header();
            pieces.Add(header.Serialize(true));
            //radiation_type
            pieces.Add(new[] { (byte)radiation_type });
            //field_of_view
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(field_of_view, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //min_range
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(min_range, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //max_range
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(max_range, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //range
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(range, h.AddrOfPinnedObject(), false);
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
            
            //header
            header = new Header();
            header.Randomize();
            //radiation_type
            myByte = new byte[1];
            rand.NextBytes(myByte);
            radiation_type= myByte[0];
            //field_of_view
            field_of_view = (float)(rand.Next() + rand.NextDouble());
            //min_range
            min_range = (float)(rand.Next() + rand.NextDouble());
            //max_range
            max_range = (float)(rand.Next() + rand.NextDouble());
            //range
            range = (float)(rand.Next() + rand.NextDouble());
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.Range other = (Messages.sensor_msgs.Range)____other;

            ret &= header.Equals(other.header);
            ret &= radiation_type == other.radiation_type;
            ret &= field_of_view == other.field_of_view;
            ret &= min_range == other.min_range;
            ret &= max_range == other.max_range;
            ret &= range == other.range;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
