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
    public class EndEffectorState : IRosMessage
    {

			public Time timestamp; //woo
			public uint id; //woo
			public const byte   STATE_FALSE = 0; //woo
			public const byte   STATE_TRUE = 1; //woo
			public const byte   STATE_UNKNOWN = 2; //woo
			public byte   enabled; //woo
			public byte   calibrated; //woo
			public byte   ready; //woo
			public byte   moving; //woo
			public byte   gripping; //woo
			public byte   missed; //woo
			public byte   error; //woo
			public byte   reverse; //woo
			public Single position; //woo
			public const Single POSITION_CLOSED = 0.0f; //woo
			public const Single POSITION_OPEN   = 100.0f; //woo
			public Single force; //woo
			public const Single FORCE_MIN = 0.0f; //woo
			public const Single FORCE_MAX = 100.0f; //woo
			public string state; //woo
			public string command; //woo
			public string command_sender; //woo
			public uint command_sequence; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "ade777f069d738595bc19e246b8ec7a0"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"time timestamp
uint32 id
uint8 STATE_FALSE=0
uint8 STATE_TRUE=1
uint8 STATE_UNKNOWN=2
uint8 enabled
uint8 calibrated
uint8 ready
uint8 moving
uint8 gripping
uint8 missed
uint8 error
uint8 reverse
float32 position
float32 POSITION_CLOSED=0.0
float32 POSITION_OPEN=100.0
float32 force
float32 FORCE_MIN=0.0
float32 FORCE_MAX=100.0
string state
string command
string command_sender
uint32 command_sequence"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__EndEffectorState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public EndEffectorState()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public EndEffectorState(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public EndEffectorState(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //timestamp
            timestamp = new Time(new TimeData(
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex),
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex+Marshal.SizeOf(typeof(System.Int32)))));
            currentIndex += 2*Marshal.SizeOf(typeof(System.Int32));
            //id
            piecesize = Marshal.SizeOf(typeof(uint));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            id = (uint)Marshal.PtrToStructure(h, typeof(uint));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //enabled
            enabled=SERIALIZEDSTUFF[currentIndex++];
            //calibrated
            calibrated=SERIALIZEDSTUFF[currentIndex++];
            //ready
            ready=SERIALIZEDSTUFF[currentIndex++];
            //moving
            moving=SERIALIZEDSTUFF[currentIndex++];
            //gripping
            gripping=SERIALIZEDSTUFF[currentIndex++];
            //missed
            missed=SERIALIZEDSTUFF[currentIndex++];
            //error
            error=SERIALIZEDSTUFF[currentIndex++];
            //reverse
            reverse=SERIALIZEDSTUFF[currentIndex++];
            //position
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            position = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //force
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            force = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //state
            state = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            state = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //command
            command = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            command = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //command_sender
            command_sender = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            command_sender = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //command_sequence
            piecesize = Marshal.SizeOf(typeof(uint));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            command_sequence = (uint)Marshal.PtrToStructure(h, typeof(uint));
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
            
            //timestamp
            pieces.Add(BitConverter.GetBytes(timestamp.data.sec));
            pieces.Add(BitConverter.GetBytes(timestamp.data.nsec));
            //id
            scratch1 = new byte[Marshal.SizeOf(typeof(uint))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(id, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //enabled
            pieces.Add(new[] { (byte)enabled });
            //calibrated
            pieces.Add(new[] { (byte)calibrated });
            //ready
            pieces.Add(new[] { (byte)ready });
            //moving
            pieces.Add(new[] { (byte)moving });
            //gripping
            pieces.Add(new[] { (byte)gripping });
            //missed
            pieces.Add(new[] { (byte)missed });
            //error
            pieces.Add(new[] { (byte)error });
            //reverse
            pieces.Add(new[] { (byte)reverse });
            //position
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(position, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //force
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(force, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //state
            if (state == null)
                state = "";
            scratch1 = Encoding.ASCII.GetBytes((string)state);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //command
            if (command == null)
                command = "";
            scratch1 = Encoding.ASCII.GetBytes((string)command);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //command_sender
            if (command_sender == null)
                command_sender = "";
            scratch1 = Encoding.ASCII.GetBytes((string)command_sender);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //command_sequence
            scratch1 = new byte[Marshal.SizeOf(typeof(uint))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(command_sequence, h.AddrOfPinnedObject(), false);
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
            
            //timestamp
            timestamp = new Time(new TimeData(
                    Convert.ToUInt32(rand.Next()),
                    Convert.ToUInt32(rand.Next())));
            //id
            id = (uint)rand.Next();
            //enabled
            myByte = new byte[1];
            rand.NextBytes(myByte);
            enabled= myByte[0];
            //calibrated
            myByte = new byte[1];
            rand.NextBytes(myByte);
            calibrated= myByte[0];
            //ready
            myByte = new byte[1];
            rand.NextBytes(myByte);
            ready= myByte[0];
            //moving
            myByte = new byte[1];
            rand.NextBytes(myByte);
            moving= myByte[0];
            //gripping
            myByte = new byte[1];
            rand.NextBytes(myByte);
            gripping= myByte[0];
            //missed
            myByte = new byte[1];
            rand.NextBytes(myByte);
            missed= myByte[0];
            //error
            myByte = new byte[1];
            rand.NextBytes(myByte);
            error= myByte[0];
            //reverse
            myByte = new byte[1];
            rand.NextBytes(myByte);
            reverse= myByte[0];
            //position
            position = (float)(rand.Next() + rand.NextDouble());
            //force
            force = (float)(rand.Next() + rand.NextDouble());
            //state
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            state = Encoding.ASCII.GetString(strbuf);
            //command
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            command = Encoding.ASCII.GetString(strbuf);
            //command_sender
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            command_sender = Encoding.ASCII.GetString(strbuf);
            //command_sequence
            command_sequence = (uint)rand.Next();
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            baxter_core_msgs.EndEffectorState other = (Messages.baxter_core_msgs.EndEffectorState)____other;

            ret &= timestamp.data.Equals(other.timestamp.data);
            ret &= id == other.id;
            ret &= enabled == other.enabled;
            ret &= calibrated == other.calibrated;
            ret &= ready == other.ready;
            ret &= moving == other.moving;
            ret &= gripping == other.gripping;
            ret &= missed == other.missed;
            ret &= error == other.error;
            ret &= reverse == other.reverse;
            ret &= position == other.position;
            ret &= force == other.force;
            ret &= state == other.state;
            ret &= command == other.command;
            ret &= command_sender == other.command_sender;
            ret &= command_sequence == other.command_sequence;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
