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
    public class JointTrajectoryControllerState : IRosMessage
    {

			public Header header; //woo
			public string[] joint_names;
			public Messages.trajectory_msgs.JointTrajectoryPoint desired; //woo
			public Messages.trajectory_msgs.JointTrajectoryPoint actual; //woo
			public Messages.trajectory_msgs.JointTrajectoryPoint error; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "10817c60c2486ef6b33e97dcd87f4474"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
string[] joint_names
trajectory_msgs/JointTrajectoryPoint desired
trajectory_msgs/JointTrajectoryPoint actual
trajectory_msgs/JointTrajectoryPoint error"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.control_msgs__JointTrajectoryControllerState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public JointTrajectoryControllerState()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public JointTrajectoryControllerState(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public JointTrajectoryControllerState(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //joint_names
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (joint_names == null)
                joint_names = new string[arraylength];
            else
                Array.Resize(ref joint_names, arraylength);
            for (int i=0;i<joint_names.Length; i++) {
                //joint_names[i]
                joint_names[i] = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                joint_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
            }
            //desired
            desired = new Messages.trajectory_msgs.JointTrajectoryPoint(SERIALIZEDSTUFF, ref currentIndex);
            //actual
            actual = new Messages.trajectory_msgs.JointTrajectoryPoint(SERIALIZEDSTUFF, ref currentIndex);
            //error
            error = new Messages.trajectory_msgs.JointTrajectoryPoint(SERIALIZEDSTUFF, ref currentIndex);
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
            //joint_names
            hasmetacomponents |= false;
            if (joint_names == null)
                joint_names = new string[0];
            pieces.Add(BitConverter.GetBytes(joint_names.Length));
            for (int i=0;i<joint_names.Length; i++) {
                //joint_names[i]
                if (joint_names[i] == null)
                    joint_names[i] = "";
                scratch1 = Encoding.ASCII.GetBytes((string)joint_names[i]);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
            }
            //desired
            if (desired == null)
                desired = new Messages.trajectory_msgs.JointTrajectoryPoint();
            pieces.Add(desired.Serialize(true));
            //actual
            if (actual == null)
                actual = new Messages.trajectory_msgs.JointTrajectoryPoint();
            pieces.Add(actual.Serialize(true));
            //error
            if (error == null)
                error = new Messages.trajectory_msgs.JointTrajectoryPoint();
            pieces.Add(error.Serialize(true));
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
            //joint_names
            arraylength = rand.Next(10);
            if (joint_names == null)
                joint_names = new string[arraylength];
            else
                Array.Resize(ref joint_names, arraylength);
            for (int i=0;i<joint_names.Length; i++) {
                //joint_names[i]
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                joint_names[i] = Encoding.ASCII.GetString(strbuf);
            }
            //desired
            desired = new Messages.trajectory_msgs.JointTrajectoryPoint();
            desired.Randomize();
            //actual
            actual = new Messages.trajectory_msgs.JointTrajectoryPoint();
            actual.Randomize();
            //error
            error = new Messages.trajectory_msgs.JointTrajectoryPoint();
            error.Randomize();
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            control_msgs.JointTrajectoryControllerState other = (Messages.control_msgs.JointTrajectoryControllerState)____other;

            ret &= header.Equals(other.header);
            if (joint_names.Length != other.joint_names.Length)
                return false;
            for (int __i__=0; __i__ < joint_names.Length; __i__++)
            {
                ret &= joint_names[__i__] == other.joint_names[__i__];
            }
            ret &= desired.Equals(other.desired);
            ret &= actual.Equals(other.actual);
            ret &= error.Equals(other.error);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
