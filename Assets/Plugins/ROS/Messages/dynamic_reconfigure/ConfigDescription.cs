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

namespace Messages.dynamic_reconfigure
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ConfigDescription : IRosMessage
    {

			public Messages.dynamic_reconfigure.Group[] groups;
			public Messages.dynamic_reconfigure.Config max; //woo
			public Messages.dynamic_reconfigure.Config min; //woo
			public Messages.dynamic_reconfigure.Config dflt; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "757ce9d44ba8ddd801bb30bc456f946f"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"Group[] groups
Config max
Config min
Config dflt"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.dynamic_reconfigure__ConfigDescription; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ConfigDescription()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ConfigDescription(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ConfigDescription(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //groups
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (groups == null)
                groups = new Messages.dynamic_reconfigure.Group[arraylength];
            else
                Array.Resize(ref groups, arraylength);
            for (int i=0;i<groups.Length; i++) {
                //groups[i]
                groups[i] = new Messages.dynamic_reconfigure.Group(SERIALIZEDSTUFF, ref currentIndex);
            }
            //max
            max = new Messages.dynamic_reconfigure.Config(SERIALIZEDSTUFF, ref currentIndex);
            //min
            min = new Messages.dynamic_reconfigure.Config(SERIALIZEDSTUFF, ref currentIndex);
            //dflt
            dflt = new Messages.dynamic_reconfigure.Config(SERIALIZEDSTUFF, ref currentIndex);
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
            
            //groups
            hasmetacomponents |= true;
            if (groups == null)
                groups = new Messages.dynamic_reconfigure.Group[0];
            pieces.Add(BitConverter.GetBytes(groups.Length));
            for (int i=0;i<groups.Length; i++) {
                //groups[i]
                if (groups[i] == null)
                    groups[i] = new Messages.dynamic_reconfigure.Group();
                pieces.Add(groups[i].Serialize(true));
            }
            //max
            if (max == null)
                max = new Messages.dynamic_reconfigure.Config();
            pieces.Add(max.Serialize(true));
            //min
            if (min == null)
                min = new Messages.dynamic_reconfigure.Config();
            pieces.Add(min.Serialize(true));
            //dflt
            if (dflt == null)
                dflt = new Messages.dynamic_reconfigure.Config();
            pieces.Add(dflt.Serialize(true));
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
            
            //groups
            arraylength = rand.Next(10);
            if (groups == null)
                groups = new Messages.dynamic_reconfigure.Group[arraylength];
            else
                Array.Resize(ref groups, arraylength);
            for (int i=0;i<groups.Length; i++) {
                //groups[i]
                groups[i] = new Messages.dynamic_reconfigure.Group();
                groups[i].Randomize();
            }
            //max
            max = new Messages.dynamic_reconfigure.Config();
            max.Randomize();
            //min
            min = new Messages.dynamic_reconfigure.Config();
            min.Randomize();
            //dflt
            dflt = new Messages.dynamic_reconfigure.Config();
            dflt.Randomize();
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            dynamic_reconfigure.ConfigDescription other = (Messages.dynamic_reconfigure.ConfigDescription)____other;

            if (groups.Length != other.groups.Length)
                return false;
            for (int __i__=0; __i__ < groups.Length; __i__++)
            {
                ret &= groups[__i__].Equals(other.groups[__i__]);
            }
            ret &= max.Equals(other.max);
            ret &= min.Equals(other.min);
            ret &= dflt.Equals(other.dflt);
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
