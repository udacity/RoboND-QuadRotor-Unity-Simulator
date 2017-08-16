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
    public class ExecFootstepsFeedback : IRosMessage
    {

			public Messages.humanoid_nav_msgs.StepTarget[] executed_footsteps;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "5dfde2cb244d6c76567d3c52c40a988c"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"humanoid_nav_msgs/StepTarget[] executed_footsteps"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.humanoid_nav_msgs__ExecFootstepsFeedback; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ExecFootstepsFeedback()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ExecFootstepsFeedback(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ExecFootstepsFeedback(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //executed_footsteps
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (executed_footsteps == null)
                executed_footsteps = new Messages.humanoid_nav_msgs.StepTarget[arraylength];
            else
                Array.Resize(ref executed_footsteps, arraylength);
            for (int i=0;i<executed_footsteps.Length; i++) {
                //executed_footsteps[i]
                executed_footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget(SERIALIZEDSTUFF, ref currentIndex);
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
            
            //executed_footsteps
            hasmetacomponents |= true;
            if (executed_footsteps == null)
                executed_footsteps = new Messages.humanoid_nav_msgs.StepTarget[0];
            pieces.Add(BitConverter.GetBytes(executed_footsteps.Length));
            for (int i=0;i<executed_footsteps.Length; i++) {
                //executed_footsteps[i]
                if (executed_footsteps[i] == null)
                    executed_footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget();
                pieces.Add(executed_footsteps[i].Serialize(true));
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
            
            //executed_footsteps
            arraylength = rand.Next(10);
            if (executed_footsteps == null)
                executed_footsteps = new Messages.humanoid_nav_msgs.StepTarget[arraylength];
            else
                Array.Resize(ref executed_footsteps, arraylength);
            for (int i=0;i<executed_footsteps.Length; i++) {
                //executed_footsteps[i]
                executed_footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget();
                executed_footsteps[i].Randomize();
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            humanoid_nav_msgs.ExecFootstepsFeedback other = (Messages.humanoid_nav_msgs.ExecFootstepsFeedback)____other;

            if (executed_footsteps.Length != other.executed_footsteps.Length)
                return false;
            for (int __i__=0; __i__ < executed_footsteps.Length; __i__++)
            {
                ret &= executed_footsteps[__i__].Equals(other.executed_footsteps[__i__]);
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
