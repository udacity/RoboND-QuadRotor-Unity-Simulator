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

namespace Messages.pcl_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class PolygonMesh : IRosMessage
    {

			public Header header; //woo
			public Messages.sensor_msgs.PointCloud2 cloud; //woo
			public Messages.pcl_msgs.Vertices[] polygons;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "45a5fc6ad2cde8489600a790acc9a38a"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
sensor_msgs/PointCloud2 cloud
Vertices[] polygons"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.pcl_msgs__PolygonMesh; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public PolygonMesh()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public PolygonMesh(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public PolygonMesh(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //cloud
            cloud = new Messages.sensor_msgs.PointCloud2(SERIALIZEDSTUFF, ref currentIndex);
            //polygons
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (polygons == null)
                polygons = new Messages.pcl_msgs.Vertices[arraylength];
            else
                Array.Resize(ref polygons, arraylength);
            for (int i=0;i<polygons.Length; i++) {
                //polygons[i]
                polygons[i] = new Messages.pcl_msgs.Vertices(SERIALIZEDSTUFF, ref currentIndex);
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
            //cloud
            if (cloud == null)
                cloud = new Messages.sensor_msgs.PointCloud2();
            pieces.Add(cloud.Serialize(true));
            //polygons
            hasmetacomponents |= true;
            if (polygons == null)
                polygons = new Messages.pcl_msgs.Vertices[0];
            pieces.Add(BitConverter.GetBytes(polygons.Length));
            for (int i=0;i<polygons.Length; i++) {
                //polygons[i]
                if (polygons[i] == null)
                    polygons[i] = new Messages.pcl_msgs.Vertices();
                pieces.Add(polygons[i].Serialize(true));
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
            //cloud
            cloud = new Messages.sensor_msgs.PointCloud2();
            cloud.Randomize();
            //polygons
            arraylength = rand.Next(10);
            if (polygons == null)
                polygons = new Messages.pcl_msgs.Vertices[arraylength];
            else
                Array.Resize(ref polygons, arraylength);
            for (int i=0;i<polygons.Length; i++) {
                //polygons[i]
                polygons[i] = new Messages.pcl_msgs.Vertices();
                polygons[i].Randomize();
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            pcl_msgs.PolygonMesh other = (Messages.pcl_msgs.PolygonMesh)____other;

            ret &= header.Equals(other.header);
            ret &= cloud.Equals(other.cloud);
            if (polygons.Length != other.polygons.Length)
                return false;
            for (int __i__=0; __i__ < polygons.Length; __i__++)
            {
                ret &= polygons[__i__].Equals(other.polygons[__i__]);
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
