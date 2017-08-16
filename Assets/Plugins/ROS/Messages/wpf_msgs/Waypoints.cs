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

namespace Messages.wpf_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Waypoints : IRosMessage
    {

			public int[] robots;
			public Messages.wpf_msgs.Point2[] path;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "ecd896609fd827470735b1eee1359fa2"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"int32[] robots
Point2[] path"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.wpf_msgs__Waypoints; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Waypoints()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Waypoints(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Waypoints(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //robots
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (robots == null)
                robots = new int[arraylength];
            else
                Array.Resize(ref robots, arraylength);
            for (int i=0;i<robots.Length; i++) {
                //robots[i]
                piecesize = Marshal.SizeOf(typeof(int));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                robots[i] = (int)Marshal.PtrToStructure(h, typeof(int));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //path
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (path == null)
                path = new Messages.wpf_msgs.Point2[arraylength];
            else
                Array.Resize(ref path, arraylength);
            for (int i=0;i<path.Length; i++) {
                //path[i]
                path[i] = new Messages.wpf_msgs.Point2(SERIALIZEDSTUFF, ref currentIndex);
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
            
            //robots
            hasmetacomponents |= false;
            if (robots == null)
                robots = new int[0];
            pieces.Add(BitConverter.GetBytes(robots.Length));
            for (int i=0;i<robots.Length; i++) {
                //robots[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(int))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(robots[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //path
            hasmetacomponents |= true;
            if (path == null)
                path = new Messages.wpf_msgs.Point2[0];
            pieces.Add(BitConverter.GetBytes(path.Length));
            for (int i=0;i<path.Length; i++) {
                //path[i]
                if (path[i] == null)
                    path[i] = new Messages.wpf_msgs.Point2();
                pieces.Add(path[i].Serialize(true));
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
            
            //robots
            arraylength = rand.Next(10);
            if (robots == null)
                robots = new int[arraylength];
            else
                Array.Resize(ref robots, arraylength);
            for (int i=0;i<robots.Length; i++) {
                //robots[i]
                robots[i] = rand.Next();
            }
            //path
            arraylength = rand.Next(10);
            if (path == null)
                path = new Messages.wpf_msgs.Point2[arraylength];
            else
                Array.Resize(ref path, arraylength);
            for (int i=0;i<path.Length; i++) {
                //path[i]
                path[i] = new Messages.wpf_msgs.Point2();
                path[i].Randomize();
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            wpf_msgs.Waypoints other = (Messages.wpf_msgs.Waypoints)____other;

            if (robots.Length != other.robots.Length)
                return false;
            for (int __i__=0; __i__ < robots.Length; __i__++)
            {
                ret &= robots[__i__] == other.robots[__i__];
            }
            if (path.Length != other.path.Length)
                return false;
            for (int __i__=0; __i__ < path.Length; __i__++)
            {
                ret &= path[__i__].Equals(other.path[__i__]);
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
