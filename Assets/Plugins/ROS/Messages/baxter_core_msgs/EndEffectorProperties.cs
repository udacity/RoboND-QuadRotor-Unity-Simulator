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
    public class EndEffectorProperties : IRosMessage
    {

			public uint id; //woo
			public byte  ui_type; //woo
			public const byte  NO_GRIPPER = 0; //woo
			public const byte  SUCTION_CUP_GRIPPER = 1; //woo
			public const byte  ELECTRIC_GRIPPER = 2; //woo
			public const byte  PASSIVE_GRIPPER = 3; //woo
			public string manufacturer; //woo
			public string product; //woo
			public string serial_number; //woo
			public string hardware_rev; //woo
			public string firmware_rev; //woo
			public string firmware_date; //woo
			public bool   has_calibration; //woo
			public bool   controls_grip; //woo
			public bool   senses_grip; //woo
			public bool   reverses_grip; //woo
			public bool   controls_force; //woo
			public bool   senses_force; //woo
			public bool   controls_position; //woo
			public bool   senses_position; //woo
			public string properties; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "21b83773ab9a35216d11e427573c76cc"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"uint32 id
uint8 ui_type
uint8 NO_GRIPPER=0
uint8 SUCTION_CUP_GRIPPER=1
uint8 ELECTRIC_GRIPPER=2
uint8 PASSIVE_GRIPPER=3
string manufacturer
string product
string serial_number
string hardware_rev
string firmware_rev
string firmware_date
bool has_calibration
bool controls_grip
bool senses_grip
bool reverses_grip
bool controls_force
bool senses_force
bool controls_position
bool senses_position
string properties"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__EndEffectorProperties; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public EndEffectorProperties()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public EndEffectorProperties(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public EndEffectorProperties(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //ui_type
            ui_type=SERIALIZEDSTUFF[currentIndex++];
            //manufacturer
            manufacturer = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            manufacturer = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //product
            product = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            product = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //serial_number
            serial_number = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            serial_number = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //hardware_rev
            hardware_rev = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            hardware_rev = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //firmware_rev
            firmware_rev = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            firmware_rev = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //firmware_date
            firmware_date = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            firmware_date = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //has_calibration
            has_calibration = SERIALIZEDSTUFF[currentIndex++]==1;
            //controls_grip
            controls_grip = SERIALIZEDSTUFF[currentIndex++]==1;
            //senses_grip
            senses_grip = SERIALIZEDSTUFF[currentIndex++]==1;
            //reverses_grip
            reverses_grip = SERIALIZEDSTUFF[currentIndex++]==1;
            //controls_force
            controls_force = SERIALIZEDSTUFF[currentIndex++]==1;
            //senses_force
            senses_force = SERIALIZEDSTUFF[currentIndex++]==1;
            //controls_position
            controls_position = SERIALIZEDSTUFF[currentIndex++]==1;
            //senses_position
            senses_position = SERIALIZEDSTUFF[currentIndex++]==1;
            //properties
            properties = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            properties = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
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
            
            //id
            scratch1 = new byte[Marshal.SizeOf(typeof(uint))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(id, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //ui_type
            pieces.Add(new[] { (byte)ui_type });
            //manufacturer
            if (manufacturer == null)
                manufacturer = "";
            scratch1 = Encoding.ASCII.GetBytes((string)manufacturer);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //product
            if (product == null)
                product = "";
            scratch1 = Encoding.ASCII.GetBytes((string)product);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //serial_number
            if (serial_number == null)
                serial_number = "";
            scratch1 = Encoding.ASCII.GetBytes((string)serial_number);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //hardware_rev
            if (hardware_rev == null)
                hardware_rev = "";
            scratch1 = Encoding.ASCII.GetBytes((string)hardware_rev);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //firmware_rev
            if (firmware_rev == null)
                firmware_rev = "";
            scratch1 = Encoding.ASCII.GetBytes((string)firmware_rev);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //firmware_date
            if (firmware_date == null)
                firmware_date = "";
            scratch1 = Encoding.ASCII.GetBytes((string)firmware_date);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //has_calibration
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)has_calibration ? 1 : 0 );
            pieces.Add(thischunk);
            //controls_grip
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)controls_grip ? 1 : 0 );
            pieces.Add(thischunk);
            //senses_grip
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)senses_grip ? 1 : 0 );
            pieces.Add(thischunk);
            //reverses_grip
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)reverses_grip ? 1 : 0 );
            pieces.Add(thischunk);
            //controls_force
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)controls_force ? 1 : 0 );
            pieces.Add(thischunk);
            //senses_force
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)senses_force ? 1 : 0 );
            pieces.Add(thischunk);
            //controls_position
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)controls_position ? 1 : 0 );
            pieces.Add(thischunk);
            //senses_position
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)senses_position ? 1 : 0 );
            pieces.Add(thischunk);
            //properties
            if (properties == null)
                properties = "";
            scratch1 = Encoding.ASCII.GetBytes((string)properties);
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
            
            //id
            id = (uint)rand.Next();
            //ui_type
            myByte = new byte[1];
            rand.NextBytes(myByte);
            ui_type= myByte[0];
            //manufacturer
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            manufacturer = Encoding.ASCII.GetString(strbuf);
            //product
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            product = Encoding.ASCII.GetString(strbuf);
            //serial_number
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            serial_number = Encoding.ASCII.GetString(strbuf);
            //hardware_rev
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            hardware_rev = Encoding.ASCII.GetString(strbuf);
            //firmware_rev
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            firmware_rev = Encoding.ASCII.GetString(strbuf);
            //firmware_date
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            firmware_date = Encoding.ASCII.GetString(strbuf);
            //has_calibration
            has_calibration = rand.Next(2) == 1;
            //controls_grip
            controls_grip = rand.Next(2) == 1;
            //senses_grip
            senses_grip = rand.Next(2) == 1;
            //reverses_grip
            reverses_grip = rand.Next(2) == 1;
            //controls_force
            controls_force = rand.Next(2) == 1;
            //senses_force
            senses_force = rand.Next(2) == 1;
            //controls_position
            controls_position = rand.Next(2) == 1;
            //senses_position
            senses_position = rand.Next(2) == 1;
            //properties
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            properties = Encoding.ASCII.GetString(strbuf);
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            baxter_core_msgs.EndEffectorProperties other = (Messages.baxter_core_msgs.EndEffectorProperties)____other;

            ret &= id == other.id;
            ret &= ui_type == other.ui_type;
            ret &= manufacturer == other.manufacturer;
            ret &= product == other.product;
            ret &= serial_number == other.serial_number;
            ret &= hardware_rev == other.hardware_rev;
            ret &= firmware_rev == other.firmware_rev;
            ret &= firmware_date == other.firmware_date;
            ret &= has_calibration == other.has_calibration;
            ret &= controls_grip == other.controls_grip;
            ret &= senses_grip == other.senses_grip;
            ret &= reverses_grip == other.reverses_grip;
            ret &= controls_force == other.controls_force;
            ret &= senses_force == other.senses_force;
            ret &= controls_position == other.controls_position;
            ret &= senses_position == other.senses_position;
            ret &= properties == other.properties;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
