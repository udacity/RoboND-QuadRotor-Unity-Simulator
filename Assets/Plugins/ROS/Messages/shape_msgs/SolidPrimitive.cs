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

namespace Messages.shape_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class SolidPrimitive : IRosMessage
    {

			public const byte BOX = 1; //woo
			public const byte SPHERE = 2; //woo
			public const byte CYLINDER = 3; //woo
			public const byte CONE = 4; //woo
			public byte type; //woo
			public double[] dimensions;
			public const byte BOX_X = 0; //woo
			public const byte BOX_Y = 1; //woo
			public const byte BOX_Z = 2; //woo
			public const byte SPHERE_RADIUS = 0; //woo
			public const byte CYLINDER_HEIGHT = 0; //woo
			public const byte CYLINDER_RADIUS = 1; //woo
			public const byte CONE_HEIGHT = 0; //woo
			public const byte CONE_RADIUS = 1; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "d8f8cbc74c5ff283fca29569ccefb45d"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"uint8 BOX=1
uint8 SPHERE=2
uint8 CYLINDER=3
uint8 CONE=4
uint8 type
float64[] dimensions
uint8 BOX_X=0
uint8 BOX_Y=1
uint8 BOX_Z=2
uint8 SPHERE_RADIUS=0
uint8 CYLINDER_HEIGHT=0
uint8 CYLINDER_RADIUS=1
uint8 CONE_HEIGHT=0
uint8 CONE_RADIUS=1"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.shape_msgs__SolidPrimitive; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public SolidPrimitive()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public SolidPrimitive(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public SolidPrimitive(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //type
            type=SERIALIZEDSTUFF[currentIndex++];
            //dimensions
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (dimensions == null)
                dimensions = new double[arraylength];
            else
                Array.Resize(ref dimensions, arraylength);
            for (int i=0;i<dimensions.Length; i++) {
                //dimensions[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                dimensions[i] = (double)Marshal.PtrToStructure(h, typeof(double));
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
            
            //type
            pieces.Add(new[] { (byte)type });
            //dimensions
            hasmetacomponents |= false;
            if (dimensions == null)
                dimensions = new double[0];
            pieces.Add(BitConverter.GetBytes(dimensions.Length));
            for (int i=0;i<dimensions.Length; i++) {
                //dimensions[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(dimensions[i], h.AddrOfPinnedObject(), false);
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
            
            //type
            myByte = new byte[1];
            rand.NextBytes(myByte);
            type= myByte[0];
            //dimensions
            arraylength = rand.Next(10);
            if (dimensions == null)
                dimensions = new double[arraylength];
            else
                Array.Resize(ref dimensions, arraylength);
            for (int i=0;i<dimensions.Length; i++) {
                //dimensions[i]
                dimensions[i] = (rand.Next() + rand.NextDouble());
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            shape_msgs.SolidPrimitive other = (Messages.shape_msgs.SolidPrimitive)____other;

            ret &= type == other.type;
            if (dimensions.Length != other.dimensions.Length)
                return false;
            for (int __i__=0; __i__ < dimensions.Length; __i__++)
            {
                ret &= dimensions[__i__] == other.dimensions[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
