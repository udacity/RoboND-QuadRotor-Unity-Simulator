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

namespace Messages.custom_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ptz : IRosMessage
    {

			public Single x; //woo
			public Single y; //woo
			public const int CAM_ABS = 0; //woo
			public const int CAM_REL = 1; //woo
			public const int CAM_VEL = 2; //woo
			public int CAM_MODE; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "1dbe794f54dc95f5130ef5e890abd627"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"float32 x
float32 y
int32 CAM_ABS=0
int32 CAM_REL=1
int32 CAM_VEL=2
int32 CAM_MODE"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.custom_msgs__ptz; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ptz()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ptz(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ptz(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //x
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            x = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //y
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            y = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //CAM_MODE
            piecesize = Marshal.SizeOf(typeof(int));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            CAM_MODE = (int)Marshal.PtrToStructure(h, typeof(int));
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
            
            //x
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(x, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //y
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(y, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //CAM_MODE
            scratch1 = new byte[Marshal.SizeOf(typeof(int))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(CAM_MODE, h.AddrOfPinnedObject(), false);
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
            
            //x
            x = (float)(rand.Next() + rand.NextDouble());
            //y
            y = (float)(rand.Next() + rand.NextDouble());
            //CAM_MODE
            CAM_MODE = rand.Next();
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            custom_msgs.ptz other = (Messages.custom_msgs.ptz)____other;

            ret &= x == other.x;
            ret &= y == other.y;
            ret &= CAM_MODE == other.CAM_MODE;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
