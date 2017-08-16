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
    public class PointCloud : IRosMessage
    {

			public Header header; //woo
			public Point32[] points;
			public Messages.sensor_msgs.ChannelFloat32[] channels;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "d8e9c3f5afbdd8a130fd1d2763945fca"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
geometry_msgs/Point32[] points
ChannelFloat32[] channels"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__PointCloud; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public PointCloud()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public PointCloud(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public PointCloud(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //points
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (points == null)
                points = new Point32[arraylength];
            else
                Array.Resize(ref points, arraylength);
            for (int i=0;i<points.Length; i++) {
                //points[i]
                points[i] = new Point32(SERIALIZEDSTUFF, ref currentIndex);
            }
            //channels
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (channels == null)
                channels = new Messages.sensor_msgs.ChannelFloat32[arraylength];
            else
                Array.Resize(ref channels, arraylength);
            for (int i=0;i<channels.Length; i++) {
                //channels[i]
                channels[i] = new Messages.sensor_msgs.ChannelFloat32(SERIALIZEDSTUFF, ref currentIndex);
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
            //points
            hasmetacomponents |= true;
            if (points == null)
                points = new Point32[0];
            pieces.Add(BitConverter.GetBytes(points.Length));
            for (int i=0;i<points.Length; i++) {
                //points[i]
                if (points[i] == null)
                    points[i] = new Point32();
                pieces.Add(points[i].Serialize(true));
            }
            //channels
            hasmetacomponents |= true;
            if (channels == null)
                channels = new Messages.sensor_msgs.ChannelFloat32[0];
            pieces.Add(BitConverter.GetBytes(channels.Length));
            for (int i=0;i<channels.Length; i++) {
                //channels[i]
                if (channels[i] == null)
                    channels[i] = new Messages.sensor_msgs.ChannelFloat32();
                pieces.Add(channels[i].Serialize(true));
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
            //points
            arraylength = rand.Next(10);
            if (points == null)
                points = new Point32[arraylength];
            else
                Array.Resize(ref points, arraylength);
            for (int i=0;i<points.Length; i++) {
                //points[i]
                points[i] = new Point32();
                points[i].Randomize();
            }
            //channels
            arraylength = rand.Next(10);
            if (channels == null)
                channels = new Messages.sensor_msgs.ChannelFloat32[arraylength];
            else
                Array.Resize(ref channels, arraylength);
            for (int i=0;i<channels.Length; i++) {
                //channels[i]
                channels[i] = new Messages.sensor_msgs.ChannelFloat32();
                channels[i].Randomize();
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.PointCloud other = (Messages.sensor_msgs.PointCloud)____other;

            ret &= header.Equals(other.header);
            if (points.Length != other.points.Length)
                return false;
            for (int __i__=0; __i__ < points.Length; __i__++)
            {
                ret &= points[__i__].Equals(other.points[__i__]);
            }
            if (channels.Length != other.channels.Length)
                return false;
            for (int __i__=0; __i__ < channels.Length; __i__++)
            {
                ret &= channels[__i__].Equals(other.channels[__i__]);
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
