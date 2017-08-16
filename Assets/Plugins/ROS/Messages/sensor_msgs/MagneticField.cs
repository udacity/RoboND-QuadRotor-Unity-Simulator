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
    public class MagneticField : IRosMessage
    {

			public Header header; //woo
			public Vector3 magnetic_field; //woo
			public double[] magnetic_field_covariance = new double[9];


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "2f3b0b43eed0c9501de0fa3ff89a45aa"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
geometry_msgs/Vector3 magnetic_field
float64[9] magnetic_field_covariance"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__MagneticField; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public MagneticField()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public MagneticField(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public MagneticField(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //magnetic_field
            magnetic_field = new Vector3(SERIALIZEDSTUFF, ref currentIndex);
            //magnetic_field_covariance
            hasmetacomponents |= false;
            if (magnetic_field_covariance == null)
                magnetic_field_covariance = new double[9];
            else
                Array.Resize(ref magnetic_field_covariance, 9);
            for (int i=0;i<magnetic_field_covariance.Length; i++) {
                //magnetic_field_covariance[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                magnetic_field_covariance[i] = (double)Marshal.PtrToStructure(h, typeof(double));
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
            //magnetic_field
            if (magnetic_field == null)
                magnetic_field = new Vector3();
            pieces.Add(magnetic_field.Serialize(true));
            //magnetic_field_covariance
            hasmetacomponents |= false;
            if (magnetic_field_covariance == null)
                magnetic_field_covariance = new double[0];
            for (int i=0;i<magnetic_field_covariance.Length; i++) {
                //magnetic_field_covariance[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(magnetic_field_covariance[i], h.AddrOfPinnedObject(), false);
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
            //magnetic_field
            magnetic_field = new Vector3();
            magnetic_field.Randomize();
            //magnetic_field_covariance
            if (magnetic_field_covariance == null)
                magnetic_field_covariance = new double[9];
            else
                Array.Resize(ref magnetic_field_covariance, 9);
            for (int i=0;i<magnetic_field_covariance.Length; i++) {
                //magnetic_field_covariance[i]
                magnetic_field_covariance[i] = (rand.Next() + rand.NextDouble());
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.MagneticField other = (Messages.sensor_msgs.MagneticField)____other;

            ret &= header.Equals(other.header);
            ret &= magnetic_field.Equals(other.magnetic_field);
            if (magnetic_field_covariance.Length != other.magnetic_field_covariance.Length)
                return false;
            for (int __i__=0; __i__ < magnetic_field_covariance.Length; __i__++)
            {
                ret &= magnetic_field_covariance[__i__] == other.magnetic_field_covariance[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
