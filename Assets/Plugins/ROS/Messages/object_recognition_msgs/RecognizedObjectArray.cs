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

namespace Messages.object_recognition_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class RecognizedObjectArray : IRosMessage
    {

			public Header header; //woo
			public Messages.object_recognition_msgs.RecognizedObject[] objects;
			public Single[] cooccurrence;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "bad6b1546b9ebcabb49fb3b858d78964"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
object_recognition_msgs/RecognizedObject[] objects
float32[] cooccurrence"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.object_recognition_msgs__RecognizedObjectArray; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public RecognizedObjectArray()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RecognizedObjectArray(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RecognizedObjectArray(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //objects
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (objects == null)
                objects = new Messages.object_recognition_msgs.RecognizedObject[arraylength];
            else
                Array.Resize(ref objects, arraylength);
            for (int i=0;i<objects.Length; i++) {
                //objects[i]
                objects[i] = new Messages.object_recognition_msgs.RecognizedObject(SERIALIZEDSTUFF, ref currentIndex);
            }
            //cooccurrence
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (cooccurrence == null)
                cooccurrence = new Single[arraylength];
            else
                Array.Resize(ref cooccurrence, arraylength);
            for (int i=0;i<cooccurrence.Length; i++) {
                //cooccurrence[i]
                piecesize = Marshal.SizeOf(typeof(Single));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                cooccurrence[i] = (Single)Marshal.PtrToStructure(h, typeof(Single));
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
            //objects
            hasmetacomponents |= true;
            if (objects == null)
                objects = new Messages.object_recognition_msgs.RecognizedObject[0];
            pieces.Add(BitConverter.GetBytes(objects.Length));
            for (int i=0;i<objects.Length; i++) {
                //objects[i]
                if (objects[i] == null)
                    objects[i] = new Messages.object_recognition_msgs.RecognizedObject();
                pieces.Add(objects[i].Serialize(true));
            }
            //cooccurrence
            hasmetacomponents |= false;
            if (cooccurrence == null)
                cooccurrence = new Single[0];
            pieces.Add(BitConverter.GetBytes(cooccurrence.Length));
            for (int i=0;i<cooccurrence.Length; i++) {
                //cooccurrence[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(cooccurrence[i], h.AddrOfPinnedObject(), false);
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
            //objects
            arraylength = rand.Next(10);
            if (objects == null)
                objects = new Messages.object_recognition_msgs.RecognizedObject[arraylength];
            else
                Array.Resize(ref objects, arraylength);
            for (int i=0;i<objects.Length; i++) {
                //objects[i]
                objects[i] = new Messages.object_recognition_msgs.RecognizedObject();
                objects[i].Randomize();
            }
            //cooccurrence
            arraylength = rand.Next(10);
            if (cooccurrence == null)
                cooccurrence = new Single[arraylength];
            else
                Array.Resize(ref cooccurrence, arraylength);
            for (int i=0;i<cooccurrence.Length; i++) {
                //cooccurrence[i]
                cooccurrence[i] = (float)(rand.Next() + rand.NextDouble());
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            object_recognition_msgs.RecognizedObjectArray other = (Messages.object_recognition_msgs.RecognizedObjectArray)____other;

            ret &= header.Equals(other.header);
            if (objects.Length != other.objects.Length)
                return false;
            for (int __i__=0; __i__ < objects.Length; __i__++)
            {
                ret &= objects[__i__].Equals(other.objects[__i__]);
            }
            if (cooccurrence.Length != other.cooccurrence.Length)
                return false;
            for (int __i__=0; __i__ < cooccurrence.Length; __i__++)
            {
                ret &= cooccurrence[__i__] == other.cooccurrence[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
