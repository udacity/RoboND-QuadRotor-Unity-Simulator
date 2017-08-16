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
    public class simpleintarray : IRosMessage
    {

			public short[] knownlengtharray = new short[3];
			public short[] unknownlengtharray;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "5788d544e629aca889424556ab4e5260"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"int16[3] knownlengtharray
int16[] unknownlengtharray"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.custom_msgs__simpleintarray; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public simpleintarray()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public simpleintarray(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public simpleintarray(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //knownlengtharray
            hasmetacomponents |= false;
            if (knownlengtharray == null)
                knownlengtharray = new short[3];
            else
                Array.Resize(ref knownlengtharray, 3);
            for (int i=0;i<knownlengtharray.Length; i++) {
                //knownlengtharray[i]
                piecesize = Marshal.SizeOf(typeof(short));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                knownlengtharray[i] = (short)Marshal.PtrToStructure(h, typeof(short));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //unknownlengtharray
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (unknownlengtharray == null)
                unknownlengtharray = new short[arraylength];
            else
                Array.Resize(ref unknownlengtharray, arraylength);
            for (int i=0;i<unknownlengtharray.Length; i++) {
                //unknownlengtharray[i]
                piecesize = Marshal.SizeOf(typeof(short));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                unknownlengtharray[i] = (short)Marshal.PtrToStructure(h, typeof(short));
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
            
            //knownlengtharray
            hasmetacomponents |= false;
            if (knownlengtharray == null)
                knownlengtharray = new short[0];
            for (int i=0;i<knownlengtharray.Length; i++) {
                //knownlengtharray[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(short))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(knownlengtharray[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //unknownlengtharray
            hasmetacomponents |= false;
            if (unknownlengtharray == null)
                unknownlengtharray = new short[0];
            pieces.Add(BitConverter.GetBytes(unknownlengtharray.Length));
            for (int i=0;i<unknownlengtharray.Length; i++) {
                //unknownlengtharray[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(short))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(unknownlengtharray[i], h.AddrOfPinnedObject(), false);
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
            
            //knownlengtharray
            if (knownlengtharray == null)
                knownlengtharray = new short[3];
            else
                Array.Resize(ref knownlengtharray, 3);
            for (int i=0;i<knownlengtharray.Length; i++) {
                //knownlengtharray[i]
                knownlengtharray[i] = (System.Int16)rand.Next(System.Int16.MaxValue + 1);
            }
            //unknownlengtharray
            arraylength = rand.Next(10);
            if (unknownlengtharray == null)
                unknownlengtharray = new short[arraylength];
            else
                Array.Resize(ref unknownlengtharray, arraylength);
            for (int i=0;i<unknownlengtharray.Length; i++) {
                //unknownlengtharray[i]
                unknownlengtharray[i] = (System.Int16)rand.Next(System.Int16.MaxValue + 1);
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            custom_msgs.simpleintarray other = (Messages.custom_msgs.simpleintarray)____other;

            if (knownlengtharray.Length != other.knownlengtharray.Length)
                return false;
            for (int __i__=0; __i__ < knownlengtharray.Length; __i__++)
            {
                ret &= knownlengtharray[__i__] == other.knownlengtharray[__i__];
            }
            if (unknownlengtharray.Length != other.unknownlengtharray.Length)
                return false;
            for (int __i__=0; __i__ < unknownlengtharray.Length; __i__++)
            {
                ret &= unknownlengtharray[__i__] == other.unknownlengtharray[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
