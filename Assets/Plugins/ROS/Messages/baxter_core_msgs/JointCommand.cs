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
    public class JointCommand : IRosMessage
    {

			public int mode; //woo
			public double[] command;
			public string[]  names;
			public const int POSITION_MODE = 1; //woo
			public const int VELOCITY_MODE = 2; //woo
			public const int TORQUE_MODE = 3; //woo
			public const int RAW_POSITION_MODE = 4; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "19bfec8434dd568ab3c633d187c36f2e"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"int32 mode
float64[] command
string[] names
int32 POSITION_MODE=1
int32 VELOCITY_MODE=2
int32 TORQUE_MODE=3
int32 RAW_POSITION_MODE=4"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__JointCommand; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public JointCommand()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public JointCommand(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public JointCommand(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //mode
            piecesize = Marshal.SizeOf(typeof(int));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            mode = (int)Marshal.PtrToStructure(h, typeof(int));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //command
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (command == null)
                command = new double[arraylength];
            else
                Array.Resize(ref command, arraylength);
            for (int i=0;i<command.Length; i++) {
                //command[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                command[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //names
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (names == null)
                names = new string[arraylength];
            else
                Array.Resize(ref names, arraylength);
            for (int i=0;i<names.Length; i++) {
                //names[i]
                names[i] = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
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
            
            //mode
            scratch1 = new byte[Marshal.SizeOf(typeof(int))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(mode, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //command
            hasmetacomponents |= false;
            if (command == null)
                command = new double[0];
            pieces.Add(BitConverter.GetBytes(command.Length));
            for (int i=0;i<command.Length; i++) {
                //command[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(command[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //names
            hasmetacomponents |= false;
            if (names == null)
                names = new string[0];
            pieces.Add(BitConverter.GetBytes(names.Length));
            for (int i=0;i<names.Length; i++) {
                //names[i]
                if (names[i] == null)
                    names[i] = "";
                scratch1 = Encoding.ASCII.GetBytes((string)names[i]);
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
            
            //mode
            mode = rand.Next();
            //command
            arraylength = rand.Next(10);
            if (command == null)
                command = new double[arraylength];
            else
                Array.Resize(ref command, arraylength);
            for (int i=0;i<command.Length; i++) {
                //command[i]
                command[i] = (rand.Next() + rand.NextDouble());
            }
            //names
            arraylength = rand.Next(10);
            if (names == null)
                names = new string[arraylength];
            else
                Array.Resize(ref names, arraylength);
            for (int i=0;i<names.Length; i++) {
                //names[i]
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                names[i] = Encoding.ASCII.GetString(strbuf);
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            baxter_core_msgs.JointCommand other = (Messages.baxter_core_msgs.JointCommand)____other;

            ret &= mode == other.mode;
            if (command.Length != other.command.Length)
                return false;
            for (int __i__=0; __i__ < command.Length; __i__++)
            {
                ret &= command[__i__] == other.command[__i__];
            }
            if (names.Length != other.names.Length)
                return false;
            for (int __i__=0; __i__ < names.Length; __i__++)
            {
                ret &= names[__i__] == other.names[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
