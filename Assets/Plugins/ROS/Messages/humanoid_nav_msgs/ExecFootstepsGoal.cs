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

namespace Messages.humanoid_nav_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ExecFootstepsGoal : IRosMessage
    {

			public Messages.humanoid_nav_msgs.StepTarget[] footsteps;
			public double feedback_frequency; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "40a3f8092ef3bb49c3253baa6eb94932"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"humanoid_nav_msgs/StepTarget[] footsteps
float64 feedback_frequency"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.humanoid_nav_msgs__ExecFootstepsGoal; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ExecFootstepsGoal()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ExecFootstepsGoal(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ExecFootstepsGoal(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //footsteps
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (footsteps == null)
                footsteps = new Messages.humanoid_nav_msgs.StepTarget[arraylength];
            else
                Array.Resize(ref footsteps, arraylength);
            for (int i=0;i<footsteps.Length; i++) {
                //footsteps[i]
                footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget(SERIALIZEDSTUFF, ref currentIndex);
            }
            //feedback_frequency
            piecesize = Marshal.SizeOf(typeof(double));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            feedback_frequency = (double)Marshal.PtrToStructure(h, typeof(double));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
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
            
            //footsteps
            hasmetacomponents |= true;
            if (footsteps == null)
                footsteps = new Messages.humanoid_nav_msgs.StepTarget[0];
            pieces.Add(BitConverter.GetBytes(footsteps.Length));
            for (int i=0;i<footsteps.Length; i++) {
                //footsteps[i]
                if (footsteps[i] == null)
                    footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget();
                pieces.Add(footsteps[i].Serialize(true));
            }
            //feedback_frequency
            scratch1 = new byte[Marshal.SizeOf(typeof(double))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(feedback_frequency, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
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
            
            //footsteps
            arraylength = rand.Next(10);
            if (footsteps == null)
                footsteps = new Messages.humanoid_nav_msgs.StepTarget[arraylength];
            else
                Array.Resize(ref footsteps, arraylength);
            for (int i=0;i<footsteps.Length; i++) {
                //footsteps[i]
                footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget();
                footsteps[i].Randomize();
            }
            //feedback_frequency
            feedback_frequency = (rand.Next() + rand.NextDouble());
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            humanoid_nav_msgs.ExecFootstepsGoal other = (Messages.humanoid_nav_msgs.ExecFootstepsGoal)____other;

            if (footsteps.Length != other.footsteps.Length)
                return false;
            for (int __i__=0; __i__ < footsteps.Length; __i__++)
            {
                ret &= footsteps[__i__].Equals(other.footsteps[__i__]);
            }
            ret &= feedback_frequency == other.feedback_frequency;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
