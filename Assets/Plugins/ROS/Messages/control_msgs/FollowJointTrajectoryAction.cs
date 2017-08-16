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

namespace Messages.control_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class FollowJointTrajectoryAction : IRosMessage
    {

			public Messages.control_msgs.FollowJointTrajectoryActionGoal action_goal; //woo
			public Messages.control_msgs.FollowJointTrajectoryActionResult action_result; //woo
			public Messages.control_msgs.FollowJointTrajectoryActionFeedback action_feedback; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "bc4f9b743838566551c0390c65f1a248"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"FollowJointTrajectoryActionGoal action_goal
FollowJointTrajectoryActionResult action_result
FollowJointTrajectoryActionFeedback action_feedback"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.control_msgs__FollowJointTrajectoryAction; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public FollowJointTrajectoryAction()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public FollowJointTrajectoryAction(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public FollowJointTrajectoryAction(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //action_goal
            action_goal = new Messages.control_msgs.FollowJointTrajectoryActionGoal(SERIALIZEDSTUFF, ref currentIndex);
            //action_result
            action_result = new Messages.control_msgs.FollowJointTrajectoryActionResult(SERIALIZEDSTUFF, ref currentIndex);
            //action_feedback
            action_feedback = new Messages.control_msgs.FollowJointTrajectoryActionFeedback(SERIALIZEDSTUFF, ref currentIndex);
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
            
            //action_goal
            if (action_goal == null)
                action_goal = new Messages.control_msgs.FollowJointTrajectoryActionGoal();
            pieces.Add(action_goal.Serialize(true));
            //action_result
            if (action_result == null)
                action_result = new Messages.control_msgs.FollowJointTrajectoryActionResult();
            pieces.Add(action_result.Serialize(true));
            //action_feedback
            if (action_feedback == null)
                action_feedback = new Messages.control_msgs.FollowJointTrajectoryActionFeedback();
            pieces.Add(action_feedback.Serialize(true));
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
            
            //action_goal
            action_goal = new Messages.control_msgs.FollowJointTrajectoryActionGoal();
            action_goal.Randomize();
            //action_result
            action_result = new Messages.control_msgs.FollowJointTrajectoryActionResult();
            action_result.Randomize();
            //action_feedback
            action_feedback = new Messages.control_msgs.FollowJointTrajectoryActionFeedback();
            action_feedback.Randomize();
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            control_msgs.FollowJointTrajectoryAction other = (Messages.control_msgs.FollowJointTrajectoryAction)____other;

            ret &= action_goal.Equals(other.action_goal);
            ret &= action_result.Equals(other.action_result);
            ret &= action_feedback.Equals(other.action_feedback);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
