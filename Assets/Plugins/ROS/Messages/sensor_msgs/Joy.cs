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
    public class Joy : IRosMessage
    {

			public Header header; //woo
			public Single[] axes;
			public int[] buttons;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "5a9ea5f83505693b71e785041e67a8bb"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
float32[] axes
int32[] buttons"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__Joy; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Joy()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Joy(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Joy(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //axes
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (axes == null)
                axes = new Single[arraylength];
            else
                Array.Resize(ref axes, arraylength);
            for (int i=0;i<axes.Length; i++) {
                //axes[i]
                piecesize = Marshal.SizeOf(typeof(Single));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                axes[i] = (Single)Marshal.PtrToStructure(h, typeof(Single));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //buttons
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (buttons == null)
                buttons = new int[arraylength];
            else
                Array.Resize(ref buttons, arraylength);
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                piecesize = Marshal.SizeOf(typeof(int));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                buttons[i] = (int)Marshal.PtrToStructure(h, typeof(int));
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
            //axes
            hasmetacomponents |= false;
            if (axes == null)
                axes = new Single[0];
            pieces.Add(BitConverter.GetBytes(axes.Length));
            for (int i=0;i<axes.Length; i++) {
                //axes[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(axes[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //buttons
            hasmetacomponents |= false;
            if (buttons == null)
                buttons = new int[0];
            pieces.Add(BitConverter.GetBytes(buttons.Length));
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(int))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(buttons[i], h.AddrOfPinnedObject(), false);
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
            //axes
            arraylength = rand.Next(10);
            if (axes == null)
                axes = new Single[arraylength];
            else
                Array.Resize(ref axes, arraylength);
            for (int i=0;i<axes.Length; i++) {
                //axes[i]
                axes[i] = (float)(rand.Next() + rand.NextDouble());
            }
            //buttons
            arraylength = rand.Next(10);
            if (buttons == null)
                buttons = new int[arraylength];
            else
                Array.Resize(ref buttons, arraylength);
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                buttons[i] = rand.Next();
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.Joy other = (Messages.sensor_msgs.Joy)____other;

            ret &= header.Equals(other.header);
            if (axes.Length != other.axes.Length)
                return false;
            for (int __i__=0; __i__ < axes.Length; __i__++)
            {
                ret &= axes[__i__] == other.axes[__i__];
            }
            if (buttons.Length != other.buttons.Length)
                return false;
            for (int __i__=0; __i__ < buttons.Length; __i__++)
            {
                ret &= buttons[__i__] == other.buttons[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
