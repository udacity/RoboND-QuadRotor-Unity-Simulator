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
    public class NavigatorState : IRosMessage
    {

			public string[] button_names;
			public bool[] buttons;
			public byte   wheel; //woo
			public string[] light_names;
			public bool[] lights;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "680d121a1f16a32647298b292492fffd"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"string[] button_names
bool[] buttons
uint8 wheel
string[] light_names
bool[] lights"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__NavigatorState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public NavigatorState()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public NavigatorState(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public NavigatorState(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //button_names
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (button_names == null)
                button_names = new string[arraylength];
            else
                Array.Resize(ref button_names, arraylength);
            for (int i=0;i<button_names.Length; i++) {
                //button_names[i]
                button_names[i] = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                button_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
            }
            //buttons
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (buttons == null)
                buttons = new bool[arraylength];
            else
                Array.Resize(ref buttons, arraylength);
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                buttons[i] = SERIALIZEDSTUFF[currentIndex++]==1;
            }
            //wheel
            wheel=SERIALIZEDSTUFF[currentIndex++];
            //light_names
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (light_names == null)
                light_names = new string[arraylength];
            else
                Array.Resize(ref light_names, arraylength);
            for (int i=0;i<light_names.Length; i++) {
                //light_names[i]
                light_names[i] = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                light_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
            }
            //lights
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (lights == null)
                lights = new bool[arraylength];
            else
                Array.Resize(ref lights, arraylength);
            for (int i=0;i<lights.Length; i++) {
                //lights[i]
                lights[i] = SERIALIZEDSTUFF[currentIndex++]==1;
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
            
            //button_names
            hasmetacomponents |= false;
            if (button_names == null)
                button_names = new string[0];
            pieces.Add(BitConverter.GetBytes(button_names.Length));
            for (int i=0;i<button_names.Length; i++) {
                //button_names[i]
                if (button_names[i] == null)
                    button_names[i] = "";
                scratch1 = Encoding.ASCII.GetBytes((string)button_names[i]);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
            }
            //buttons
            hasmetacomponents |= false;
            if (buttons == null)
                buttons = new bool[0];
            pieces.Add(BitConverter.GetBytes(buttons.Length));
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)buttons[i] ? 1 : 0 );
                pieces.Add(thischunk);
            }
            //wheel
            pieces.Add(new[] { (byte)wheel });
            //light_names
            hasmetacomponents |= false;
            if (light_names == null)
                light_names = new string[0];
            pieces.Add(BitConverter.GetBytes(light_names.Length));
            for (int i=0;i<light_names.Length; i++) {
                //light_names[i]
                if (light_names[i] == null)
                    light_names[i] = "";
                scratch1 = Encoding.ASCII.GetBytes((string)light_names[i]);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
            }
            //lights
            hasmetacomponents |= false;
            if (lights == null)
                lights = new bool[0];
            pieces.Add(BitConverter.GetBytes(lights.Length));
            for (int i=0;i<lights.Length; i++) {
                //lights[i]
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)lights[i] ? 1 : 0 );
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
            
            //button_names
            arraylength = rand.Next(10);
            if (button_names == null)
                button_names = new string[arraylength];
            else
                Array.Resize(ref button_names, arraylength);
            for (int i=0;i<button_names.Length; i++) {
                //button_names[i]
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                button_names[i] = Encoding.ASCII.GetString(strbuf);
            }
            //buttons
            arraylength = rand.Next(10);
            if (buttons == null)
                buttons = new bool[arraylength];
            else
                Array.Resize(ref buttons, arraylength);
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                buttons[i] = rand.Next(2) == 1;
            }
            //wheel
            myByte = new byte[1];
            rand.NextBytes(myByte);
            wheel= myByte[0];
            //light_names
            arraylength = rand.Next(10);
            if (light_names == null)
                light_names = new string[arraylength];
            else
                Array.Resize(ref light_names, arraylength);
            for (int i=0;i<light_names.Length; i++) {
                //light_names[i]
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                light_names[i] = Encoding.ASCII.GetString(strbuf);
            }
            //lights
            arraylength = rand.Next(10);
            if (lights == null)
                lights = new bool[arraylength];
            else
                Array.Resize(ref lights, arraylength);
            for (int i=0;i<lights.Length; i++) {
                //lights[i]
                lights[i] = rand.Next(2) == 1;
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            baxter_core_msgs.NavigatorState other = (Messages.baxter_core_msgs.NavigatorState)____other;

            if (button_names.Length != other.button_names.Length)
                return false;
            for (int __i__=0; __i__ < button_names.Length; __i__++)
            {
                ret &= button_names[__i__] == other.button_names[__i__];
            }
            if (buttons.Length != other.buttons.Length)
                return false;
            for (int __i__=0; __i__ < buttons.Length; __i__++)
            {
                ret &= buttons[__i__] == other.buttons[__i__];
            }
            ret &= wheel == other.wheel;
            if (light_names.Length != other.light_names.Length)
                return false;
            for (int __i__=0; __i__ < light_names.Length; __i__++)
            {
                ret &= light_names[__i__] == other.light_names[__i__];
            }
            if (lights.Length != other.lights.Length)
                return false;
            for (int __i__=0; __i__ < lights.Length; __i__++)
            {
                ret &= lights[__i__] == other.lights[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
