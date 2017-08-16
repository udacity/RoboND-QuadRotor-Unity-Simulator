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
    public class FollowJointTrajectoryGoal : IRosMessage
    {

			public Messages.trajectory_msgs.JointTrajectory trajectory; //woo
			public Messages.control_msgs.JointTolerance[] path_tolerance;
			public Messages.control_msgs.JointTolerance[] goal_tolerance;
			public Duration goal_time_tolerance; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "69636787b6ecbde4d61d711979bc7ecb"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"trajectory_msgs/JointTrajectory trajectory
JointTolerance[] path_tolerance
JointTolerance[] goal_tolerance
duration goal_time_tolerance"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.control_msgs__FollowJointTrajectoryGoal; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public FollowJointTrajectoryGoal()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public FollowJointTrajectoryGoal(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public FollowJointTrajectoryGoal(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //trajectory
            trajectory = new Messages.trajectory_msgs.JointTrajectory(SERIALIZEDSTUFF, ref currentIndex);
            //path_tolerance
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (path_tolerance == null)
                path_tolerance = new Messages.control_msgs.JointTolerance[arraylength];
            else
                Array.Resize(ref path_tolerance, arraylength);
            for (int i=0;i<path_tolerance.Length; i++) {
                //path_tolerance[i]
                path_tolerance[i] = new Messages.control_msgs.JointTolerance(SERIALIZEDSTUFF, ref currentIndex);
            }
            //goal_tolerance
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (goal_tolerance == null)
                goal_tolerance = new Messages.control_msgs.JointTolerance[arraylength];
            else
                Array.Resize(ref goal_tolerance, arraylength);
            for (int i=0;i<goal_tolerance.Length; i++) {
                //goal_tolerance[i]
                goal_tolerance[i] = new Messages.control_msgs.JointTolerance(SERIALIZEDSTUFF, ref currentIndex);
            }
            //goal_time_tolerance
            goal_time_tolerance = new Duration(new TimeData(
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex),
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex+Marshal.SizeOf(typeof(System.Int32)))));
            currentIndex += 2*Marshal.SizeOf(typeof(System.Int32));
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
            
            //trajectory
            if (trajectory == null)
                trajectory = new Messages.trajectory_msgs.JointTrajectory();
            pieces.Add(trajectory.Serialize(true));
            //path_tolerance
            hasmetacomponents |= true;
            if (path_tolerance == null)
                path_tolerance = new Messages.control_msgs.JointTolerance[0];
            pieces.Add(BitConverter.GetBytes(path_tolerance.Length));
            for (int i=0;i<path_tolerance.Length; i++) {
                //path_tolerance[i]
                if (path_tolerance[i] == null)
                    path_tolerance[i] = new Messages.control_msgs.JointTolerance();
                pieces.Add(path_tolerance[i].Serialize(true));
            }
            //goal_tolerance
            hasmetacomponents |= true;
            if (goal_tolerance == null)
                goal_tolerance = new Messages.control_msgs.JointTolerance[0];
            pieces.Add(BitConverter.GetBytes(goal_tolerance.Length));
            for (int i=0;i<goal_tolerance.Length; i++) {
                //goal_tolerance[i]
                if (goal_tolerance[i] == null)
                    goal_tolerance[i] = new Messages.control_msgs.JointTolerance();
                pieces.Add(goal_tolerance[i].Serialize(true));
            }
            //goal_time_tolerance
            pieces.Add(BitConverter.GetBytes(goal_time_tolerance.data.sec));
            pieces.Add(BitConverter.GetBytes(goal_time_tolerance.data.nsec));
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
            
            //trajectory
            trajectory = new Messages.trajectory_msgs.JointTrajectory();
            trajectory.Randomize();
            //path_tolerance
            arraylength = rand.Next(10);
            if (path_tolerance == null)
                path_tolerance = new Messages.control_msgs.JointTolerance[arraylength];
            else
                Array.Resize(ref path_tolerance, arraylength);
            for (int i=0;i<path_tolerance.Length; i++) {
                //path_tolerance[i]
                path_tolerance[i] = new Messages.control_msgs.JointTolerance();
                path_tolerance[i].Randomize();
            }
            //goal_tolerance
            arraylength = rand.Next(10);
            if (goal_tolerance == null)
                goal_tolerance = new Messages.control_msgs.JointTolerance[arraylength];
            else
                Array.Resize(ref goal_tolerance, arraylength);
            for (int i=0;i<goal_tolerance.Length; i++) {
                //goal_tolerance[i]
                goal_tolerance[i] = new Messages.control_msgs.JointTolerance();
                goal_tolerance[i].Randomize();
            }
            //goal_time_tolerance
            goal_time_tolerance = new Duration(new TimeData(
                    Convert.ToUInt32(rand.Next()),
                    Convert.ToUInt32(rand.Next())));
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            control_msgs.FollowJointTrajectoryGoal other = (Messages.control_msgs.FollowJointTrajectoryGoal)____other;

            ret &= trajectory.Equals(other.trajectory);
            if (path_tolerance.Length != other.path_tolerance.Length)
                return false;
            for (int __i__=0; __i__ < path_tolerance.Length; __i__++)
            {
                ret &= path_tolerance[__i__].Equals(other.path_tolerance[__i__]);
            }
            if (goal_tolerance.Length != other.goal_tolerance.Length)
                return false;
            for (int __i__=0; __i__ < goal_tolerance.Length; __i__++)
            {
                ret &= goal_tolerance[__i__].Equals(other.goal_tolerance[__i__]);
            }
            ret &= goal_time_tolerance.data.Equals(other.goal_time_tolerance.data);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
