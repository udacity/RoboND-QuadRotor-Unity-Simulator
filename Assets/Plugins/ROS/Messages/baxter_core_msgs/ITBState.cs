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
    public class ITBState : IRosMessage
    {

			public bool[] buttons = new bool[4];
			public bool    up; //woo
			public bool    down; //woo
			public bool    left; //woo
			public bool    right; //woo
			public byte   wheel; //woo
			public bool innerLight; //woo
			public bool outerLight; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "fd86ad89da05230247c94b4d8e7ed306"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"bool[4] buttons
bool up
bool down
bool left
bool right
uint8 wheel
bool innerLight
bool outerLight"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__ITBState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ITBState()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ITBState(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ITBState(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //buttons
            hasmetacomponents |= false;
            if (buttons == null)
                buttons = new bool[4];
            else
                Array.Resize(ref buttons, 4);
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                buttons[i] = SERIALIZEDSTUFF[currentIndex++]==1;
            }
            //up
            up = SERIALIZEDSTUFF[currentIndex++]==1;
            //down
            down = SERIALIZEDSTUFF[currentIndex++]==1;
            //left
            left = SERIALIZEDSTUFF[currentIndex++]==1;
            //right
            right = SERIALIZEDSTUFF[currentIndex++]==1;
            //wheel
            wheel=SERIALIZEDSTUFF[currentIndex++];
            //innerLight
            innerLight = SERIALIZEDSTUFF[currentIndex++]==1;
            //outerLight
            outerLight = SERIALIZEDSTUFF[currentIndex++]==1;
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
            
            //buttons
            hasmetacomponents |= false;
            if (buttons == null)
                buttons = new bool[0];
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)buttons[i] ? 1 : 0 );
                pieces.Add(thischunk);
            }
            //up
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)up ? 1 : 0 );
            pieces.Add(thischunk);
            //down
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)down ? 1 : 0 );
            pieces.Add(thischunk);
            //left
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)left ? 1 : 0 );
            pieces.Add(thischunk);
            //right
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)right ? 1 : 0 );
            pieces.Add(thischunk);
            //wheel
            pieces.Add(new[] { (byte)wheel });
            //innerLight
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)innerLight ? 1 : 0 );
            pieces.Add(thischunk);
            //outerLight
            thischunk = new byte[1];
            thischunk[0] = (byte) ((bool)outerLight ? 1 : 0 );
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
            
            //buttons
            if (buttons == null)
                buttons = new bool[4];
            else
                Array.Resize(ref buttons, 4);
            for (int i=0;i<buttons.Length; i++) {
                //buttons[i]
                buttons[i] = rand.Next(2) == 1;
            }
            //up
            up = rand.Next(2) == 1;
            //down
            down = rand.Next(2) == 1;
            //left
            left = rand.Next(2) == 1;
            //right
            right = rand.Next(2) == 1;
            //wheel
            myByte = new byte[1];
            rand.NextBytes(myByte);
            wheel= myByte[0];
            //innerLight
            innerLight = rand.Next(2) == 1;
            //outerLight
            outerLight = rand.Next(2) == 1;
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            baxter_core_msgs.ITBState other = (Messages.baxter_core_msgs.ITBState)____other;

            if (buttons.Length != other.buttons.Length)
                return false;
            for (int __i__=0; __i__ < buttons.Length; __i__++)
            {
                ret &= buttons[__i__] == other.buttons[__i__];
            }
            ret &= up == other.up;
            ret &= down == other.down;
            ret &= left == other.left;
            ret &= right == other.right;
            ret &= wheel == other.wheel;
            ret &= innerLight == other.innerLight;
            ret &= outerLight == other.outerLight;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
