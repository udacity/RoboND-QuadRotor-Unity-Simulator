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

namespace Messages.actionlib_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class GoalStatus : IRosMessage
    {

			public const byte PENDING         = 0; //woo
			public const byte ACTIVE          = 1; //woo
			public const byte PREEMPTED       = 2; //woo
			public const byte SUCCEEDED       = 3; //woo
			public const byte ABORTED         = 4; //woo
			public const byte REJECTED        = 5; //woo
			public const byte PREEMPTING      = 6; //woo
			public const byte RECALLING       = 7; //woo
			public const byte RECALLED        = 8; //woo
			public const byte LOST            = 9; //woo
			public Messages.actionlib_msgs.GoalID goal_id; //woo
			public byte status; //woo
			public string text; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "d388f9b87b3c471f784434d671988d4a"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"uint8 PENDING=0
uint8 ACTIVE=1
uint8 PREEMPTED=2
uint8 SUCCEEDED=3
uint8 ABORTED=4
uint8 REJECTED=5
uint8 PREEMPTING=6
uint8 RECALLING=7
uint8 RECALLED=8
uint8 LOST=9
GoalID goal_id
uint8 status
string text"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.actionlib_msgs__GoalStatus; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public GoalStatus()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public GoalStatus(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public GoalStatus(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //goal_id
            goal_id = new Messages.actionlib_msgs.GoalID(SERIALIZEDSTUFF, ref currentIndex);
            //status
            status=SERIALIZEDSTUFF[currentIndex++];
            //text
            text = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            text = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
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
            
            //goal_id
            if (goal_id == null)
                goal_id = new Messages.actionlib_msgs.GoalID();
            pieces.Add(goal_id.Serialize(true));
            //status
            pieces.Add(new[] { (byte)status });
            //text
            if (text == null)
                text = "";
            scratch1 = Encoding.ASCII.GetBytes((string)text);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
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
            
            //goal_id
            goal_id = new Messages.actionlib_msgs.GoalID();
            goal_id.Randomize();
            //status
            myByte = new byte[1];
            rand.NextBytes(myByte);
            status= myByte[0];
            //text
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            text = Encoding.ASCII.GetString(strbuf);
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            actionlib_msgs.GoalStatus other = (Messages.actionlib_msgs.GoalStatus)____other;

            ret &= goal_id.Equals(other.goal_id);
            ret &= status == other.status;
            ret &= text == other.text;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
