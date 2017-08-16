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

namespace Messages.trust_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class RunDescription : IRosMessage
    {

			public string    participant_id; //woo
			public string    run_id; //woo
			public string    map_id; //woo
			public string    run_reliability; //woo
			public string  school_id; //woo
			public string  experiment_type; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "7fcfe3d29eaf9c4c378a3bfc42d4053a"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"string participant_id
string run_id
string map_id
string run_reliability
string school_id
string experiment_type"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.trust_msgs__RunDescription; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public RunDescription()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RunDescription(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RunDescription(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //participant_id
            participant_id = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            participant_id = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //run_id
            run_id = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            run_id = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //map_id
            map_id = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            map_id = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //run_reliability
            run_reliability = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            run_reliability = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //school_id
            school_id = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            school_id = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //experiment_type
            experiment_type = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            experiment_type = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
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
            
            //participant_id
            if (participant_id == null)
                participant_id = "";
            scratch1 = Encoding.ASCII.GetBytes((string)participant_id);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //run_id
            if (run_id == null)
                run_id = "";
            scratch1 = Encoding.ASCII.GetBytes((string)run_id);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //map_id
            if (map_id == null)
                map_id = "";
            scratch1 = Encoding.ASCII.GetBytes((string)map_id);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //run_reliability
            if (run_reliability == null)
                run_reliability = "";
            scratch1 = Encoding.ASCII.GetBytes((string)run_reliability);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //school_id
            if (school_id == null)
                school_id = "";
            scratch1 = Encoding.ASCII.GetBytes((string)school_id);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //experiment_type
            if (experiment_type == null)
                experiment_type = "";
            scratch1 = Encoding.ASCII.GetBytes((string)experiment_type);
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
            
            //participant_id
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            participant_id = Encoding.ASCII.GetString(strbuf);
            //run_id
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            run_id = Encoding.ASCII.GetString(strbuf);
            //map_id
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            map_id = Encoding.ASCII.GetString(strbuf);
            //run_reliability
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            run_reliability = Encoding.ASCII.GetString(strbuf);
            //school_id
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            school_id = Encoding.ASCII.GetString(strbuf);
            //experiment_type
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            experiment_type = Encoding.ASCII.GetString(strbuf);
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            trust_msgs.RunDescription other = (Messages.trust_msgs.RunDescription)____other;

            ret &= participant_id == other.participant_id;
            ret &= run_id == other.run_id;
            ret &= map_id == other.map_id;
            ret &= run_reliability == other.run_reliability;
            ret &= school_id == other.school_id;
            ret &= experiment_type == other.experiment_type;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
