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
    public class RecognizedObject : IRosMessage
    {

			public Header header; //woo
			public Messages.object_recognition_msgs.ObjectType type; //woo
			public Single confidence; //woo
			public Messages.sensor_msgs.PointCloud2[] point_clouds;
			public Messages.shape_msgs.Mesh bounding_mesh; //woo
			public Point[] bounding_contours;
			public PoseWithCovarianceStamped pose; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "f92c4cb29ba11f26c5f7219de97e900d"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
object_recognition_msgs/ObjectType type
float32 confidence
sensor_msgs/PointCloud2[] point_clouds
shape_msgs/Mesh bounding_mesh
geometry_msgs/Point[] bounding_contours
geometry_msgs/PoseWithCovarianceStamped pose"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.object_recognition_msgs__RecognizedObject; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public RecognizedObject()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RecognizedObject(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RecognizedObject(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //type
            type = new Messages.object_recognition_msgs.ObjectType(SERIALIZEDSTUFF, ref currentIndex);
            //confidence
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            confidence = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //point_clouds
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (point_clouds == null)
                point_clouds = new Messages.sensor_msgs.PointCloud2[arraylength];
            else
                Array.Resize(ref point_clouds, arraylength);
            for (int i=0;i<point_clouds.Length; i++) {
                //point_clouds[i]
                point_clouds[i] = new Messages.sensor_msgs.PointCloud2(SERIALIZEDSTUFF, ref currentIndex);
            }
            //bounding_mesh
            bounding_mesh = new Messages.shape_msgs.Mesh(SERIALIZEDSTUFF, ref currentIndex);
            //bounding_contours
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (bounding_contours == null)
                bounding_contours = new Point[arraylength];
            else
                Array.Resize(ref bounding_contours, arraylength);
            for (int i=0;i<bounding_contours.Length; i++) {
                //bounding_contours[i]
                bounding_contours[i] = new Point(SERIALIZEDSTUFF, ref currentIndex);
            }
            //pose
            pose = new PoseWithCovarianceStamped(SERIALIZEDSTUFF, ref currentIndex);
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
            //type
            if (type == null)
                type = new Messages.object_recognition_msgs.ObjectType();
            pieces.Add(type.Serialize(true));
            //confidence
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(confidence, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //point_clouds
            hasmetacomponents |= true;
            if (point_clouds == null)
                point_clouds = new Messages.sensor_msgs.PointCloud2[0];
            pieces.Add(BitConverter.GetBytes(point_clouds.Length));
            for (int i=0;i<point_clouds.Length; i++) {
                //point_clouds[i]
                if (point_clouds[i] == null)
                    point_clouds[i] = new Messages.sensor_msgs.PointCloud2();
                pieces.Add(point_clouds[i].Serialize(true));
            }
            //bounding_mesh
            if (bounding_mesh == null)
                bounding_mesh = new Messages.shape_msgs.Mesh();
            pieces.Add(bounding_mesh.Serialize(true));
            //bounding_contours
            hasmetacomponents |= true;
            if (bounding_contours == null)
                bounding_contours = new Point[0];
            pieces.Add(BitConverter.GetBytes(bounding_contours.Length));
            for (int i=0;i<bounding_contours.Length; i++) {
                //bounding_contours[i]
                if (bounding_contours[i] == null)
                    bounding_contours[i] = new Point();
                pieces.Add(bounding_contours[i].Serialize(true));
            }
            //pose
            if (pose == null)
                pose = new PoseWithCovarianceStamped();
            pieces.Add(pose.Serialize(true));
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
            //type
            type = new Messages.object_recognition_msgs.ObjectType();
            type.Randomize();
            //confidence
            confidence = (float)(rand.Next() + rand.NextDouble());
            //point_clouds
            arraylength = rand.Next(10);
            if (point_clouds == null)
                point_clouds = new Messages.sensor_msgs.PointCloud2[arraylength];
            else
                Array.Resize(ref point_clouds, arraylength);
            for (int i=0;i<point_clouds.Length; i++) {
                //point_clouds[i]
                point_clouds[i] = new Messages.sensor_msgs.PointCloud2();
                point_clouds[i].Randomize();
            }
            //bounding_mesh
            bounding_mesh = new Messages.shape_msgs.Mesh();
            bounding_mesh.Randomize();
            //bounding_contours
            arraylength = rand.Next(10);
            if (bounding_contours == null)
                bounding_contours = new Point[arraylength];
            else
                Array.Resize(ref bounding_contours, arraylength);
            for (int i=0;i<bounding_contours.Length; i++) {
                //bounding_contours[i]
                bounding_contours[i] = new Point();
                bounding_contours[i].Randomize();
            }
            //pose
            pose = new PoseWithCovarianceStamped();
            pose.Randomize();
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            object_recognition_msgs.RecognizedObject other = (Messages.object_recognition_msgs.RecognizedObject)____other;

            ret &= header.Equals(other.header);
            ret &= type.Equals(other.type);
            ret &= confidence == other.confidence;
            if (point_clouds.Length != other.point_clouds.Length)
                return false;
            for (int __i__=0; __i__ < point_clouds.Length; __i__++)
            {
                ret &= point_clouds[__i__].Equals(other.point_clouds[__i__]);
            }
            ret &= bounding_mesh.Equals(other.bounding_mesh);
            if (bounding_contours.Length != other.bounding_contours.Length)
                return false;
            for (int __i__=0; __i__ < bounding_contours.Length; __i__++)
            {
                ret &= bounding_contours[__i__].Equals(other.bounding_contours[__i__]);
            }
            ret &= pose.Equals(other.pose);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
