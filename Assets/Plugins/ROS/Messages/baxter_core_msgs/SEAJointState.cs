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
    public class SEAJointState : IRosMessage
    {

			public Header header; //woo
			public string[]  name;
			public double[] commanded_position;
			public double[] commanded_velocity;
			public double[] commanded_acceleration;
			public double[] commanded_effort;
			public double[] actual_position;
			public double[] actual_velocity;
			public double[] actual_effort;
			public double[] gravity_model_effort;
			public double[] gravity_only;
			public double[] hysteresis_model_effort;
			public double[] crosstalk_model_effort;
			public double   hystState; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "d36406dcbb6d860b1b39c4e28f81352b"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Header header
string[] name
float64[] commanded_position
float64[] commanded_velocity
float64[] commanded_acceleration
float64[] commanded_effort
float64[] actual_position
float64[] actual_velocity
float64[] actual_effort
float64[] gravity_model_effort
float64[] gravity_only
float64[] hysteresis_model_effort
float64[] crosstalk_model_effort
float64 hystState"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__SEAJointState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public SEAJointState()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public SEAJointState(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public SEAJointState(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //name
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (name == null)
                name = new string[arraylength];
            else
                Array.Resize(ref name, arraylength);
            for (int i=0;i<name.Length; i++) {
                //name[i]
                name[i] = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                name[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
            }
            //commanded_position
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (commanded_position == null)
                commanded_position = new double[arraylength];
            else
                Array.Resize(ref commanded_position, arraylength);
            for (int i=0;i<commanded_position.Length; i++) {
                //commanded_position[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                commanded_position[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //commanded_velocity
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (commanded_velocity == null)
                commanded_velocity = new double[arraylength];
            else
                Array.Resize(ref commanded_velocity, arraylength);
            for (int i=0;i<commanded_velocity.Length; i++) {
                //commanded_velocity[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                commanded_velocity[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //commanded_acceleration
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (commanded_acceleration == null)
                commanded_acceleration = new double[arraylength];
            else
                Array.Resize(ref commanded_acceleration, arraylength);
            for (int i=0;i<commanded_acceleration.Length; i++) {
                //commanded_acceleration[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                commanded_acceleration[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //commanded_effort
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (commanded_effort == null)
                commanded_effort = new double[arraylength];
            else
                Array.Resize(ref commanded_effort, arraylength);
            for (int i=0;i<commanded_effort.Length; i++) {
                //commanded_effort[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                commanded_effort[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //actual_position
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (actual_position == null)
                actual_position = new double[arraylength];
            else
                Array.Resize(ref actual_position, arraylength);
            for (int i=0;i<actual_position.Length; i++) {
                //actual_position[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                actual_position[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //actual_velocity
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (actual_velocity == null)
                actual_velocity = new double[arraylength];
            else
                Array.Resize(ref actual_velocity, arraylength);
            for (int i=0;i<actual_velocity.Length; i++) {
                //actual_velocity[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                actual_velocity[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //actual_effort
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (actual_effort == null)
                actual_effort = new double[arraylength];
            else
                Array.Resize(ref actual_effort, arraylength);
            for (int i=0;i<actual_effort.Length; i++) {
                //actual_effort[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                actual_effort[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //gravity_model_effort
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (gravity_model_effort == null)
                gravity_model_effort = new double[arraylength];
            else
                Array.Resize(ref gravity_model_effort, arraylength);
            for (int i=0;i<gravity_model_effort.Length; i++) {
                //gravity_model_effort[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                gravity_model_effort[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //gravity_only
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (gravity_only == null)
                gravity_only = new double[arraylength];
            else
                Array.Resize(ref gravity_only, arraylength);
            for (int i=0;i<gravity_only.Length; i++) {
                //gravity_only[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                gravity_only[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //hysteresis_model_effort
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (hysteresis_model_effort == null)
                hysteresis_model_effort = new double[arraylength];
            else
                Array.Resize(ref hysteresis_model_effort, arraylength);
            for (int i=0;i<hysteresis_model_effort.Length; i++) {
                //hysteresis_model_effort[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                hysteresis_model_effort[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //crosstalk_model_effort
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (crosstalk_model_effort == null)
                crosstalk_model_effort = new double[arraylength];
            else
                Array.Resize(ref crosstalk_model_effort, arraylength);
            for (int i=0;i<crosstalk_model_effort.Length; i++) {
                //crosstalk_model_effort[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                crosstalk_model_effort[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //hystState
            piecesize = Marshal.SizeOf(typeof(double));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            hystState = (double)Marshal.PtrToStructure(h, typeof(double));
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
            
            //header
            if (header == null)
                header = new Header();
            pieces.Add(header.Serialize(true));
            //name
            hasmetacomponents |= false;
            if (name == null)
                name = new string[0];
            pieces.Add(BitConverter.GetBytes(name.Length));
            for (int i=0;i<name.Length; i++) {
                //name[i]
                if (name[i] == null)
                    name[i] = "";
                scratch1 = Encoding.ASCII.GetBytes((string)name[i]);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
            }
            //commanded_position
            hasmetacomponents |= false;
            if (commanded_position == null)
                commanded_position = new double[0];
            pieces.Add(BitConverter.GetBytes(commanded_position.Length));
            for (int i=0;i<commanded_position.Length; i++) {
                //commanded_position[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(commanded_position[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //commanded_velocity
            hasmetacomponents |= false;
            if (commanded_velocity == null)
                commanded_velocity = new double[0];
            pieces.Add(BitConverter.GetBytes(commanded_velocity.Length));
            for (int i=0;i<commanded_velocity.Length; i++) {
                //commanded_velocity[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(commanded_velocity[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //commanded_acceleration
            hasmetacomponents |= false;
            if (commanded_acceleration == null)
                commanded_acceleration = new double[0];
            pieces.Add(BitConverter.GetBytes(commanded_acceleration.Length));
            for (int i=0;i<commanded_acceleration.Length; i++) {
                //commanded_acceleration[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(commanded_acceleration[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //commanded_effort
            hasmetacomponents |= false;
            if (commanded_effort == null)
                commanded_effort = new double[0];
            pieces.Add(BitConverter.GetBytes(commanded_effort.Length));
            for (int i=0;i<commanded_effort.Length; i++) {
                //commanded_effort[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(commanded_effort[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //actual_position
            hasmetacomponents |= false;
            if (actual_position == null)
                actual_position = new double[0];
            pieces.Add(BitConverter.GetBytes(actual_position.Length));
            for (int i=0;i<actual_position.Length; i++) {
                //actual_position[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(actual_position[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //actual_velocity
            hasmetacomponents |= false;
            if (actual_velocity == null)
                actual_velocity = new double[0];
            pieces.Add(BitConverter.GetBytes(actual_velocity.Length));
            for (int i=0;i<actual_velocity.Length; i++) {
                //actual_velocity[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(actual_velocity[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //actual_effort
            hasmetacomponents |= false;
            if (actual_effort == null)
                actual_effort = new double[0];
            pieces.Add(BitConverter.GetBytes(actual_effort.Length));
            for (int i=0;i<actual_effort.Length; i++) {
                //actual_effort[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(actual_effort[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //gravity_model_effort
            hasmetacomponents |= false;
            if (gravity_model_effort == null)
                gravity_model_effort = new double[0];
            pieces.Add(BitConverter.GetBytes(gravity_model_effort.Length));
            for (int i=0;i<gravity_model_effort.Length; i++) {
                //gravity_model_effort[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(gravity_model_effort[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //gravity_only
            hasmetacomponents |= false;
            if (gravity_only == null)
                gravity_only = new double[0];
            pieces.Add(BitConverter.GetBytes(gravity_only.Length));
            for (int i=0;i<gravity_only.Length; i++) {
                //gravity_only[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(gravity_only[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //hysteresis_model_effort
            hasmetacomponents |= false;
            if (hysteresis_model_effort == null)
                hysteresis_model_effort = new double[0];
            pieces.Add(BitConverter.GetBytes(hysteresis_model_effort.Length));
            for (int i=0;i<hysteresis_model_effort.Length; i++) {
                //hysteresis_model_effort[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(hysteresis_model_effort[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //crosstalk_model_effort
            hasmetacomponents |= false;
            if (crosstalk_model_effort == null)
                crosstalk_model_effort = new double[0];
            pieces.Add(BitConverter.GetBytes(crosstalk_model_effort.Length));
            for (int i=0;i<crosstalk_model_effort.Length; i++) {
                //crosstalk_model_effort[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(crosstalk_model_effort[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //hystState
            scratch1 = new byte[Marshal.SizeOf(typeof(double))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(hystState, h.AddrOfPinnedObject(), false);
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
            
            //header
            header = new Header();
            header.Randomize();
            //name
            arraylength = rand.Next(10);
            if (name == null)
                name = new string[arraylength];
            else
                Array.Resize(ref name, arraylength);
            for (int i=0;i<name.Length; i++) {
                //name[i]
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                name[i] = Encoding.ASCII.GetString(strbuf);
            }
            //commanded_position
            arraylength = rand.Next(10);
            if (commanded_position == null)
                commanded_position = new double[arraylength];
            else
                Array.Resize(ref commanded_position, arraylength);
            for (int i=0;i<commanded_position.Length; i++) {
                //commanded_position[i]
                commanded_position[i] = (rand.Next() + rand.NextDouble());
            }
            //commanded_velocity
            arraylength = rand.Next(10);
            if (commanded_velocity == null)
                commanded_velocity = new double[arraylength];
            else
                Array.Resize(ref commanded_velocity, arraylength);
            for (int i=0;i<commanded_velocity.Length; i++) {
                //commanded_velocity[i]
                commanded_velocity[i] = (rand.Next() + rand.NextDouble());
            }
            //commanded_acceleration
            arraylength = rand.Next(10);
            if (commanded_acceleration == null)
                commanded_acceleration = new double[arraylength];
            else
                Array.Resize(ref commanded_acceleration, arraylength);
            for (int i=0;i<commanded_acceleration.Length; i++) {
                //commanded_acceleration[i]
                commanded_acceleration[i] = (rand.Next() + rand.NextDouble());
            }
            //commanded_effort
            arraylength = rand.Next(10);
            if (commanded_effort == null)
                commanded_effort = new double[arraylength];
            else
                Array.Resize(ref commanded_effort, arraylength);
            for (int i=0;i<commanded_effort.Length; i++) {
                //commanded_effort[i]
                commanded_effort[i] = (rand.Next() + rand.NextDouble());
            }
            //actual_position
            arraylength = rand.Next(10);
            if (actual_position == null)
                actual_position = new double[arraylength];
            else
                Array.Resize(ref actual_position, arraylength);
            for (int i=0;i<actual_position.Length; i++) {
                //actual_position[i]
                actual_position[i] = (rand.Next() + rand.NextDouble());
            }
            //actual_velocity
            arraylength = rand.Next(10);
            if (actual_velocity == null)
                actual_velocity = new double[arraylength];
            else
                Array.Resize(ref actual_velocity, arraylength);
            for (int i=0;i<actual_velocity.Length; i++) {
                //actual_velocity[i]
                actual_velocity[i] = (rand.Next() + rand.NextDouble());
            }
            //actual_effort
            arraylength = rand.Next(10);
            if (actual_effort == null)
                actual_effort = new double[arraylength];
            else
                Array.Resize(ref actual_effort, arraylength);
            for (int i=0;i<actual_effort.Length; i++) {
                //actual_effort[i]
                actual_effort[i] = (rand.Next() + rand.NextDouble());
            }
            //gravity_model_effort
            arraylength = rand.Next(10);
            if (gravity_model_effort == null)
                gravity_model_effort = new double[arraylength];
            else
                Array.Resize(ref gravity_model_effort, arraylength);
            for (int i=0;i<gravity_model_effort.Length; i++) {
                //gravity_model_effort[i]
                gravity_model_effort[i] = (rand.Next() + rand.NextDouble());
            }
            //gravity_only
            arraylength = rand.Next(10);
            if (gravity_only == null)
                gravity_only = new double[arraylength];
            else
                Array.Resize(ref gravity_only, arraylength);
            for (int i=0;i<gravity_only.Length; i++) {
                //gravity_only[i]
                gravity_only[i] = (rand.Next() + rand.NextDouble());
            }
            //hysteresis_model_effort
            arraylength = rand.Next(10);
            if (hysteresis_model_effort == null)
                hysteresis_model_effort = new double[arraylength];
            else
                Array.Resize(ref hysteresis_model_effort, arraylength);
            for (int i=0;i<hysteresis_model_effort.Length; i++) {
                //hysteresis_model_effort[i]
                hysteresis_model_effort[i] = (rand.Next() + rand.NextDouble());
            }
            //crosstalk_model_effort
            arraylength = rand.Next(10);
            if (crosstalk_model_effort == null)
                crosstalk_model_effort = new double[arraylength];
            else
                Array.Resize(ref crosstalk_model_effort, arraylength);
            for (int i=0;i<crosstalk_model_effort.Length; i++) {
                //crosstalk_model_effort[i]
                crosstalk_model_effort[i] = (rand.Next() + rand.NextDouble());
            }
            //hystState
            hystState = (rand.Next() + rand.NextDouble());
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            baxter_core_msgs.SEAJointState other = (Messages.baxter_core_msgs.SEAJointState)____other;

            ret &= header.Equals(other.header);
            if (name.Length != other.name.Length)
                return false;
            for (int __i__=0; __i__ < name.Length; __i__++)
            {
                ret &= name[__i__] == other.name[__i__];
            }
            if (commanded_position.Length != other.commanded_position.Length)
                return false;
            for (int __i__=0; __i__ < commanded_position.Length; __i__++)
            {
                ret &= commanded_position[__i__] == other.commanded_position[__i__];
            }
            if (commanded_velocity.Length != other.commanded_velocity.Length)
                return false;
            for (int __i__=0; __i__ < commanded_velocity.Length; __i__++)
            {
                ret &= commanded_velocity[__i__] == other.commanded_velocity[__i__];
            }
            if (commanded_acceleration.Length != other.commanded_acceleration.Length)
                return false;
            for (int __i__=0; __i__ < commanded_acceleration.Length; __i__++)
            {
                ret &= commanded_acceleration[__i__] == other.commanded_acceleration[__i__];
            }
            if (commanded_effort.Length != other.commanded_effort.Length)
                return false;
            for (int __i__=0; __i__ < commanded_effort.Length; __i__++)
            {
                ret &= commanded_effort[__i__] == other.commanded_effort[__i__];
            }
            if (actual_position.Length != other.actual_position.Length)
                return false;
            for (int __i__=0; __i__ < actual_position.Length; __i__++)
            {
                ret &= actual_position[__i__] == other.actual_position[__i__];
            }
            if (actual_velocity.Length != other.actual_velocity.Length)
                return false;
            for (int __i__=0; __i__ < actual_velocity.Length; __i__++)
            {
                ret &= actual_velocity[__i__] == other.actual_velocity[__i__];
            }
            if (actual_effort.Length != other.actual_effort.Length)
                return false;
            for (int __i__=0; __i__ < actual_effort.Length; __i__++)
            {
                ret &= actual_effort[__i__] == other.actual_effort[__i__];
            }
            if (gravity_model_effort.Length != other.gravity_model_effort.Length)
                return false;
            for (int __i__=0; __i__ < gravity_model_effort.Length; __i__++)
            {
                ret &= gravity_model_effort[__i__] == other.gravity_model_effort[__i__];
            }
            if (gravity_only.Length != other.gravity_only.Length)
                return false;
            for (int __i__=0; __i__ < gravity_only.Length; __i__++)
            {
                ret &= gravity_only[__i__] == other.gravity_only[__i__];
            }
            if (hysteresis_model_effort.Length != other.hysteresis_model_effort.Length)
                return false;
            for (int __i__=0; __i__ < hysteresis_model_effort.Length; __i__++)
            {
                ret &= hysteresis_model_effort[__i__] == other.hysteresis_model_effort[__i__];
            }
            if (crosstalk_model_effort.Length != other.crosstalk_model_effort.Length)
                return false;
            for (int __i__=0; __i__ < crosstalk_model_effort.Length; __i__++)
            {
                ret &= crosstalk_model_effort[__i__] == other.crosstalk_model_effort[__i__];
            }
            ret &= hystState == other.hystState;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
