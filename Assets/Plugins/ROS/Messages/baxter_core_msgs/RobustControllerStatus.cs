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

namespace Messages.baxter_core_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class RobustControllerStatus : IRosMessage
    {

			public bool isEnabled; //woo
			public int complete; //woo
			public const int NOT_COMPLETE = 0; //woo
			public const int COMPLETE_W_FAILURE = 1; //woo
			public const int COMPLETE_W_SUCCESS = 2; //woo
			public string controlUid; //woo
			public bool timedOut; //woo
			public string[] errorCodes;
			public string[] labels;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "2f15441b7285d915e7e59d3618e173f2"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"bool isEnabled
int32 complete
int32 NOT_COMPLETE=0
int32 COMPLETE_W_FAILURE=1
int32 COMPLETE_W_SUCCESS=2
string controlUid
bool timedOut
string[] errorCodes
string[] labels"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__RobustControllerStatus; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public RobustControllerStatus()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RobustControllerStatus(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public RobustControllerStatus(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //isEnabled
            isEnabled = SERIALIZEDSTUFF[currentIndex++]==1;
            //complete
            piecesize = Marshal.SizeOf(typeof(int));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            complete = (int)Marshal.PtrToStructure(h, typeof(int));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //controlUid
            controlUid = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            controlUid = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //timedOut
            timedOut = SERIALIZEDSTUFF[currentIndex++]==1;
            //errorCodes
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (errorCodes == null)
                errorCodes = new string[arraylength];
            else
                Array.Resize(ref errorCodes, arraylength);
            for (int i=0;i<errorCodes.Length; i++) {
                //errorCodes[i]
                errorCodes[i] = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                errorCodes[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
            }
            //labels
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (labels == null)
                labels = new string[arraylength];
            else
                Array.Resize(ref labels, arraylength);
            for (int i=0;i<labels.Length; i++) {
                //labels[i]
                labels[i] = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                labels[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
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
            
            //isEnabled
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)isEnabled ? 1 : 0 );
            pieces.Add(thischunk);
            //complete
            scratch1 = new byte[Marshal.SizeOf(typeof(int))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(complete, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //controlUid
            if (controlUid == null)
                controlUid = "";
            scratch1 = Encoding.ASCII.GetBytes((string)controlUid);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //timedOut
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)timedOut ? 1 : 0 );
            pieces.Add(thischunk);
            //errorCodes
            hasmetacomponents |= false;
            if (errorCodes == null)
                errorCodes = new string[0];
            pieces.Add(BitConverter.GetBytes(errorCodes.Length));
            for (int i=0;i<errorCodes.Length; i++) {
                //errorCodes[i]
                if (errorCodes[i] == null)
                    errorCodes[i] = "";
                scratch1 = Encoding.ASCII.GetBytes((string)errorCodes[i]);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
            }
            //labels
            hasmetacomponents |= false;
            if (labels == null)
                labels = new string[0];
            pieces.Add(BitConverter.GetBytes(labels.Length));
            for (int i=0;i<labels.Length; i++) {
                //labels[i]
                if (labels[i] == null)
                    labels[i] = "";
                scratch1 = Encoding.ASCII.GetBytes((string)labels[i]);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
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
            
            //isEnabled
            isEnabled = rand.Next(2) == 1;
            //complete
            complete = rand.Next();
            //controlUid
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            controlUid = Encoding.ASCII.GetString(strbuf);
            //timedOut
            timedOut = rand.Next(2) == 1;
            //errorCodes
            arraylength = rand.Next(10);
            if (errorCodes == null)
                errorCodes = new string[arraylength];
            else
                Array.Resize(ref errorCodes, arraylength);
            for (int i=0;i<errorCodes.Length; i++) {
                //errorCodes[i]
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                errorCodes[i] = Encoding.ASCII.GetString(strbuf);
            }
            //labels
            arraylength = rand.Next(10);
            if (labels == null)
                labels = new string[arraylength];
            else
                Array.Resize(ref labels, arraylength);
            for (int i=0;i<labels.Length; i++) {
                //labels[i]
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                labels[i] = Encoding.ASCII.GetString(strbuf);
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            baxter_core_msgs.RobustControllerStatus other = (Messages.baxter_core_msgs.RobustControllerStatus)____other;

            ret &= isEnabled == other.isEnabled;
            ret &= complete == other.complete;
            ret &= controlUid == other.controlUid;
            ret &= timedOut == other.timedOut;
            if (errorCodes.Length != other.errorCodes.Length)
                return false;
            for (int __i__=0; __i__ < errorCodes.Length; __i__++)
            {
                ret &= errorCodes[__i__] == other.errorCodes[__i__];
            }
            if (labels.Length != other.labels.Length)
                return false;
            for (int __i__=0; __i__ < labels.Length; __i__++)
            {
                ret &= labels[__i__] == other.labels[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
