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

namespace Messages.tf2_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class TF2Error : IRosMessage
    {

			public const byte NO_ERROR = 0; //woo
			public const byte LOOKUP_ERROR = 1; //woo
			public const byte CONNECTIVITY_ERROR = 2; //woo
			public const byte EXTRAPOLATION_ERROR = 3; //woo
			public const byte INVALID_ARGUMENT_ERROR = 4; //woo
			public const byte TIMEOUT_ERROR = 5; //woo
			public const byte TRANSFORM_ERROR = 6; //woo
			public byte error; //woo
			public string error_string; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "bc6848fd6fd750c92e38575618a4917d"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"uint8 NO_ERROR=0
uint8 LOOKUP_ERROR=1
uint8 CONNECTIVITY_ERROR=2
uint8 EXTRAPOLATION_ERROR=3
uint8 INVALID_ARGUMENT_ERROR=4
uint8 TIMEOUT_ERROR=5
uint8 TRANSFORM_ERROR=6
uint8 error
string error_string"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.tf2_msgs__TF2Error; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public TF2Error()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public TF2Error(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public TF2Error(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //error
            error=SERIALIZEDSTUFF[currentIndex++];
            //error_string
            error_string = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            error_string = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
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
            
            //error
            pieces.Add(new[] { (byte)error });
            //error_string
            if (error_string == null)
                error_string = "";
            scratch1 = Encoding.ASCII.GetBytes((string)error_string);
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
            
            //error
            myByte = new byte[1];
            rand.NextBytes(myByte);
            error= myByte[0];
            //error_string
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            error_string = Encoding.ASCII.GetString(strbuf);
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            tf2_msgs.TF2Error other = (Messages.tf2_msgs.TF2Error)____other;

            ret &= error == other.error;
            ret &= error_string == other.error_string;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
