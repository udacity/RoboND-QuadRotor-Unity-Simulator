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

namespace Messages.sensor_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class BatteryState : IRosMessage
    {

			public const byte POWER_SUPPLY_STATUS_UNKNOWN = 0; //woo
			public const byte POWER_SUPPLY_STATUS_CHARGING = 1; //woo
			public const byte POWER_SUPPLY_STATUS_DISCHARGING = 2; //woo
			public const byte POWER_SUPPLY_STATUS_NOT_CHARGING = 3; //woo
			public const byte POWER_SUPPLY_STATUS_FULL = 4; //woo
			public const byte POWER_SUPPLY_HEALTH_UNKNOWN = 0; //woo
			public const byte POWER_SUPPLY_HEALTH_GOOD = 1; //woo
			public const byte POWER_SUPPLY_HEALTH_OVERHEAT = 2; //woo
			public const byte POWER_SUPPLY_HEALTH_DEAD = 3; //woo
			public const byte POWER_SUPPLY_HEALTH_OVERVOLTAGE = 4; //woo
			public const byte POWER_SUPPLY_HEALTH_UNSPEC_FAILURE = 5; //woo
			public const byte POWER_SUPPLY_HEALTH_COLD = 6; //woo
			public const byte POWER_SUPPLY_HEALTH_WATCHDOG_TIMER_EXPIRE = 7; //woo
			public const byte POWER_SUPPLY_HEALTH_SAFETY_TIMER_EXPIRE = 8; //woo
			public const byte POWER_SUPPLY_TECHNOLOGY_UNKNOWN = 0; //woo
			public const byte POWER_SUPPLY_TECHNOLOGY_NIMH = 1; //woo
			public const byte POWER_SUPPLY_TECHNOLOGY_LION = 2; //woo
			public const byte POWER_SUPPLY_TECHNOLOGY_LIPO = 3; //woo
			public const byte POWER_SUPPLY_TECHNOLOGY_LIFE = 4; //woo
			public const byte POWER_SUPPLY_TECHNOLOGY_NICD = 5; //woo
			public const byte POWER_SUPPLY_TECHNOLOGY_LIMN = 6; //woo
			public Header header; //woo
			public Single voltage; //woo
			public Single current; //woo
			public Single charge; //woo
			public Single capacity; //woo
			public Single design_capacity; //woo
			public Single percentage; //woo
			public byte   power_supply_status; //woo
			public byte   power_supply_health; //woo
			public byte   power_supply_technology; //woo
			public bool    present; //woo
			public Single[] cell_voltage;
			public string location; //woo
			public string serial_number; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "476f837fa6771f6e16e3bf4ef96f8770"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"uint8 POWER_SUPPLY_STATUS_UNKNOWN=0
uint8 POWER_SUPPLY_STATUS_CHARGING=1
uint8 POWER_SUPPLY_STATUS_DISCHARGING=2
uint8 POWER_SUPPLY_STATUS_NOT_CHARGING=3
uint8 POWER_SUPPLY_STATUS_FULL=4
uint8 POWER_SUPPLY_HEALTH_UNKNOWN=0
uint8 POWER_SUPPLY_HEALTH_GOOD=1
uint8 POWER_SUPPLY_HEALTH_OVERHEAT=2
uint8 POWER_SUPPLY_HEALTH_DEAD=3
uint8 POWER_SUPPLY_HEALTH_OVERVOLTAGE=4
uint8 POWER_SUPPLY_HEALTH_UNSPEC_FAILURE=5
uint8 POWER_SUPPLY_HEALTH_COLD=6
uint8 POWER_SUPPLY_HEALTH_WATCHDOG_TIMER_EXPIRE=7
uint8 POWER_SUPPLY_HEALTH_SAFETY_TIMER_EXPIRE=8
uint8 POWER_SUPPLY_TECHNOLOGY_UNKNOWN=0
uint8 POWER_SUPPLY_TECHNOLOGY_NIMH=1
uint8 POWER_SUPPLY_TECHNOLOGY_LION=2
uint8 POWER_SUPPLY_TECHNOLOGY_LIPO=3
uint8 POWER_SUPPLY_TECHNOLOGY_LIFE=4
uint8 POWER_SUPPLY_TECHNOLOGY_NICD=5
uint8 POWER_SUPPLY_TECHNOLOGY_LIMN=6
Header header
float32 voltage
float32 current
float32 charge
float32 capacity
float32 design_capacity
float32 percentage
uint8 power_supply_status
uint8 power_supply_health
uint8 power_supply_technology
bool present
float32[] cell_voltage
string location
string serial_number"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.sensor_msgs__BatteryState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public BatteryState()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public BatteryState(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public BatteryState(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            //voltage
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            voltage = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //current
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            current = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //charge
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            charge = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //capacity
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            capacity = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //design_capacity
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            design_capacity = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //percentage
            piecesize = Marshal.SizeOf(typeof(Single));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
                h = Marshal.AllocHGlobal(piecesize);
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            percentage = (Single)Marshal.PtrToStructure(h, typeof(Single));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //power_supply_status
            power_supply_status=SERIALIZEDSTUFF[currentIndex++];
            //power_supply_health
            power_supply_health=SERIALIZEDSTUFF[currentIndex++];
            //power_supply_technology
            power_supply_technology=SERIALIZEDSTUFF[currentIndex++];
            //present
            present = SERIALIZEDSTUFF[currentIndex++]==1;
            //cell_voltage
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (cell_voltage == null)
                cell_voltage = new Single[arraylength];
            else
                Array.Resize(ref cell_voltage, arraylength);
            for (int i=0;i<cell_voltage.Length; i++) {
                //cell_voltage[i]
                piecesize = Marshal.SizeOf(typeof(Single));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                cell_voltage[i] = (Single)Marshal.PtrToStructure(h, typeof(Single));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //location
            location = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            location = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //serial_number
            serial_number = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            serial_number = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
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
            
            //header
            if (header == null)
                header = new Header();
            pieces.Add(header.Serialize(true));
            //voltage
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(voltage, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //current
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(current, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //charge
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(charge, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //capacity
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(capacity, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //design_capacity
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(design_capacity, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //percentage
            scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(percentage, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //power_supply_status
            pieces.Add(new[] { (byte)power_supply_status });
            //power_supply_health
            pieces.Add(new[] { (byte)power_supply_health });
            //power_supply_technology
            pieces.Add(new[] { (byte)power_supply_technology });
            //present
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)present ? 1 : 0 );
            pieces.Add(thischunk);
            //cell_voltage
            hasmetacomponents |= false;
            if (cell_voltage == null)
                cell_voltage = new Single[0];
            pieces.Add(BitConverter.GetBytes(cell_voltage.Length));
            for (int i=0;i<cell_voltage.Length; i++) {
                //cell_voltage[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(Single))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(cell_voltage[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //location
            if (location == null)
                location = "";
            scratch1 = Encoding.ASCII.GetBytes((string)location);
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
            //voltage
            voltage = (float)(rand.Next() + rand.NextDouble());
            //current
            current = (float)(rand.Next() + rand.NextDouble());
            //charge
            charge = (float)(rand.Next() + rand.NextDouble());
            //capacity
            capacity = (float)(rand.Next() + rand.NextDouble());
            //design_capacity
            design_capacity = (float)(rand.Next() + rand.NextDouble());
            //percentage
            percentage = (float)(rand.Next() + rand.NextDouble());
            //power_supply_status
            myByte = new byte[1];
            rand.NextBytes(myByte);
            power_supply_status= myByte[0];
            //power_supply_health
            myByte = new byte[1];
            rand.NextBytes(myByte);
            power_supply_health= myByte[0];
            //power_supply_technology
            myByte = new byte[1];
            rand.NextBytes(myByte);
            power_supply_technology= myByte[0];
            //present
            present = rand.Next(2) == 1;
            //cell_voltage
            arraylength = rand.Next(10);
            if (cell_voltage == null)
                cell_voltage = new Single[arraylength];
            else
                Array.Resize(ref cell_voltage, arraylength);
            for (int i=0;i<cell_voltage.Length; i++) {
                //cell_voltage[i]
                cell_voltage[i] = (float)(rand.Next() + rand.NextDouble());
            }
            //location
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            location = Encoding.ASCII.GetString(strbuf);
            //serial_number
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            serial_number = Encoding.ASCII.GetString(strbuf);
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            sensor_msgs.BatteryState other = (Messages.sensor_msgs.BatteryState)____other;

            ret &= header.Equals(other.header);
            ret &= voltage == other.voltage;
            ret &= current == other.current;
            ret &= charge == other.charge;
            ret &= capacity == other.capacity;
            ret &= design_capacity == other.design_capacity;
            ret &= percentage == other.percentage;
            ret &= power_supply_status == other.power_supply_status;
            ret &= power_supply_health == other.power_supply_health;
            ret &= power_supply_technology == other.power_supply_technology;
            ret &= present == other.present;
            if (cell_voltage.Length != other.cell_voltage.Length)
                return false;
            for (int __i__=0; __i__ < cell_voltage.Length; __i__++)
            {
                ret &= cell_voltage[__i__] == other.cell_voltage[__i__];
            }
            ret &= location == other.location;
            ret &= serial_number == other.serial_number;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
