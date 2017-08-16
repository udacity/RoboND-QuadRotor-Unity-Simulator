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
    public class NavSatFix : IRosMessage
    {

			public Header header; //woo
			public Messages.sensor_msgs.NavSatStatus status; //woo
			public double latitude; //woo
			public double longitude; //woo
			public double altitude; //woo
			public double[] position_covariance = new double[9];
			public const byte COVARIANCE_TYPE_UNKNOWN = 0; //woo
			public const byte COVARIANCE_TYPE_APPROXIMATED = 1; //woo
			public const byte COVARIANCE_TYPE_DIAGONAL_KNOWN = 2; //woo
			public const byte COVARIANCE_TYPE_KNOWN = 3; //woo
			public byte position_covariance_type; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "2d3a8cd499b9b4a0249fb98fd05cfa48"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
NavSatStatus status
float64 latitude
float64 longitude
float64 altitude
float64[9] position_covariance
uint8 COVARIANCE_TYPE_UNKNOWN=0
uint8 COVARIANCE_TYPE_APPROXIMATED=1
uint8 COVARIANCE_TYPE_DIAGONAL_KNOWN=2
uint8 COVARIANCE_TYPE_KNOWN=3
uint8 position_covariance_type"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__NavSatFix; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public NavSatFix()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public NavSatFix(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public NavSatFix(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //status
            status = new Messages.sensor_msgs.NavSatStatus(SERIALIZEDSTUFF, ref currentIndex);
            //latitude
            piecesize = Marshal.SizeOf(typeof(double));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            latitude = (double)Marshal.PtrToStructure(h, typeof(double));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //longitude
            piecesize = Marshal.SizeOf(typeof(double));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            longitude = (double)Marshal.PtrToStructure(h, typeof(double));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //altitude
            piecesize = Marshal.SizeOf(typeof(double));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            altitude = (double)Marshal.PtrToStructure(h, typeof(double));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //position_covariance
            hasmetacomponents |= false;
            if (position_covariance == null)
                position_covariance = new double[9];
            else
                Array.Resize(ref position_covariance, 9);
            for (int i=0;i<position_covariance.Length; i++) {
                //position_covariance[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                position_covariance[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //position_covariance_type
            position_covariance_type=SERIALIZEDSTUFF[currentIndex++];
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
            //status
            if (status == null)
                status = new Messages.sensor_msgs.NavSatStatus();
            pieces.Add(status.Serialize(true));
            //latitude
            scratch1 = new byte[Marshal.SizeOf(typeof(double))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(latitude, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //longitude
            scratch1 = new byte[Marshal.SizeOf(typeof(double))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(longitude, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //altitude
            scratch1 = new byte[Marshal.SizeOf(typeof(double))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(altitude, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //position_covariance
            hasmetacomponents |= false;
            if (position_covariance == null)
                position_covariance = new double[0];
            for (int i=0;i<position_covariance.Length; i++) {
                //position_covariance[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(position_covariance[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //position_covariance_type
            pieces.Add(new[] { (byte)position_covariance_type });
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
            //status
            status = new Messages.sensor_msgs.NavSatStatus();
            status.Randomize();
            //latitude
            latitude = (rand.Next() + rand.NextDouble());
            //longitude
            longitude = (rand.Next() + rand.NextDouble());
            //altitude
            altitude = (rand.Next() + rand.NextDouble());
            //position_covariance
            if (position_covariance == null)
                position_covariance = new double[9];
            else
                Array.Resize(ref position_covariance, 9);
            for (int i=0;i<position_covariance.Length; i++) {
                //position_covariance[i]
                position_covariance[i] = (rand.Next() + rand.NextDouble());
            }
            //position_covariance_type
            myByte = new byte[1];
            rand.NextBytes(myByte);
            position_covariance_type= myByte[0];
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.NavSatFix other = (Messages.sensor_msgs.NavSatFix)____other;

            ret &= header.Equals(other.header);
            ret &= status.Equals(other.status);
            ret &= latitude == other.latitude;
            ret &= longitude == other.longitude;
            ret &= altitude == other.altitude;
            if (position_covariance.Length != other.position_covariance.Length)
                return false;
            for (int __i__=0; __i__ < position_covariance.Length; __i__++)
            {
                ret &= position_covariance[__i__] == other.position_covariance[__i__];
            }
            ret &= position_covariance_type == other.position_covariance_type;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
