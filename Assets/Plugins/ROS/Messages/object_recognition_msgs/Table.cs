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
    public class Table : IRosMessage
    {

			public Header header; //woo
			public Pose pose; //woo
			public Point[] convex_hull;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "39efebc7d51e44bd2d72f2df6c7823a2"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
geometry_msgs/Pose pose
geometry_msgs/Point[] convex_hull"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.object_recognition_msgs__Table; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Table()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Table(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Table(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //pose
            pose = new Pose(SERIALIZEDSTUFF, ref currentIndex);
            //convex_hull
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (convex_hull == null)
                convex_hull = new Point[arraylength];
            else
                Array.Resize(ref convex_hull, arraylength);
            for (int i=0;i<convex_hull.Length; i++) {
                //convex_hull[i]
                convex_hull[i] = new Point(SERIALIZEDSTUFF, ref currentIndex);
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
            //pose
            if (pose == null)
                pose = new Pose();
            pieces.Add(pose.Serialize(true));
            //convex_hull
            hasmetacomponents |= true;
            if (convex_hull == null)
                convex_hull = new Point[0];
            pieces.Add(BitConverter.GetBytes(convex_hull.Length));
            for (int i=0;i<convex_hull.Length; i++) {
                //convex_hull[i]
                if (convex_hull[i] == null)
                    convex_hull[i] = new Point();
                pieces.Add(convex_hull[i].Serialize(true));
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
            //pose
            pose = new Pose();
            pose.Randomize();
            //convex_hull
            arraylength = rand.Next(10);
            if (convex_hull == null)
                convex_hull = new Point[arraylength];
            else
                Array.Resize(ref convex_hull, arraylength);
            for (int i=0;i<convex_hull.Length; i++) {
                //convex_hull[i]
                convex_hull[i] = new Point();
                convex_hull[i].Randomize();
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            object_recognition_msgs.Table other = (Messages.object_recognition_msgs.Table)____other;

            ret &= header.Equals(other.header);
            ret &= pose.Equals(other.pose);
            if (convex_hull.Length != other.convex_hull.Length)
                return false;
            for (int __i__=0; __i__ < convex_hull.Length; __i__++)
            {
                ret &= convex_hull[__i__].Equals(other.convex_hull[__i__]);
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
