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
    public class Config : IRosMessage
    {

			public Messages.dynamic_reconfigure.BoolParameter[] bools;
			public Messages.dynamic_reconfigure.IntParameter[] ints;
			public Messages.dynamic_reconfigure.StrParameter[] strs;
			public Messages.dynamic_reconfigure.DoubleParameter[] doubles;
			public Messages.dynamic_reconfigure.GroupState[] groups;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "958f16a05573709014982821e6822580"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"BoolParameter[] bools
IntParameter[] ints
StrParameter[] strs
DoubleParameter[] doubles
GroupState[] groups"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.dynamic_reconfigure__Config; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Config()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Config(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Config(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //bools
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (bools == null)
                bools = new Messages.dynamic_reconfigure.BoolParameter[arraylength];
            else
                Array.Resize(ref bools, arraylength);
            for (int i=0;i<bools.Length; i++) {
                //bools[i]
                bools[i] = new Messages.dynamic_reconfigure.BoolParameter(SERIALIZEDSTUFF, ref currentIndex);
            }
            //ints
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (ints == null)
                ints = new Messages.dynamic_reconfigure.IntParameter[arraylength];
            else
                Array.Resize(ref ints, arraylength);
            for (int i=0;i<ints.Length; i++) {
                //ints[i]
                ints[i] = new Messages.dynamic_reconfigure.IntParameter(SERIALIZEDSTUFF, ref currentIndex);
            }
            //strs
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (strs == null)
                strs = new Messages.dynamic_reconfigure.StrParameter[arraylength];
            else
                Array.Resize(ref strs, arraylength);
            for (int i=0;i<strs.Length; i++) {
                //strs[i]
                strs[i] = new Messages.dynamic_reconfigure.StrParameter(SERIALIZEDSTUFF, ref currentIndex);
            }
            //doubles
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (doubles == null)
                doubles = new Messages.dynamic_reconfigure.DoubleParameter[arraylength];
            else
                Array.Resize(ref doubles, arraylength);
            for (int i=0;i<doubles.Length; i++) {
                //doubles[i]
                doubles[i] = new Messages.dynamic_reconfigure.DoubleParameter(SERIALIZEDSTUFF, ref currentIndex);
            }
            //groups
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (groups == null)
                groups = new Messages.dynamic_reconfigure.GroupState[arraylength];
            else
                Array.Resize(ref groups, arraylength);
            for (int i=0;i<groups.Length; i++) {
                //groups[i]
                groups[i] = new Messages.dynamic_reconfigure.GroupState(SERIALIZEDSTUFF, ref currentIndex);
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
            
            //bools
            hasmetacomponents |= true;
            if (bools == null)
                bools = new Messages.dynamic_reconfigure.BoolParameter[0];
            pieces.Add(BitConverter.GetBytes(bools.Length));
            for (int i=0;i<bools.Length; i++) {
                //bools[i]
                if (bools[i] == null)
                    bools[i] = new Messages.dynamic_reconfigure.BoolParameter();
                pieces.Add(bools[i].Serialize(true));
            }
            //ints
            hasmetacomponents |= true;
            if (ints == null)
                ints = new Messages.dynamic_reconfigure.IntParameter[0];
            pieces.Add(BitConverter.GetBytes(ints.Length));
            for (int i=0;i<ints.Length; i++) {
                //ints[i]
                if (ints[i] == null)
                    ints[i] = new Messages.dynamic_reconfigure.IntParameter();
                pieces.Add(ints[i].Serialize(true));
            }
            //strs
            hasmetacomponents |= true;
            if (strs == null)
                strs = new Messages.dynamic_reconfigure.StrParameter[0];
            pieces.Add(BitConverter.GetBytes(strs.Length));
            for (int i=0;i<strs.Length; i++) {
                //strs[i]
                if (strs[i] == null)
                    strs[i] = new Messages.dynamic_reconfigure.StrParameter();
                pieces.Add(strs[i].Serialize(true));
            }
            //doubles
            hasmetacomponents |= true;
            if (doubles == null)
                doubles = new Messages.dynamic_reconfigure.DoubleParameter[0];
            pieces.Add(BitConverter.GetBytes(doubles.Length));
            for (int i=0;i<doubles.Length; i++) {
                //doubles[i]
                if (doubles[i] == null)
                    doubles[i] = new Messages.dynamic_reconfigure.DoubleParameter();
                pieces.Add(doubles[i].Serialize(true));
            }
            //groups
            hasmetacomponents |= true;
            if (groups == null)
                groups = new Messages.dynamic_reconfigure.GroupState[0];
            pieces.Add(BitConverter.GetBytes(groups.Length));
            for (int i=0;i<groups.Length; i++) {
                //groups[i]
                if (groups[i] == null)
                    groups[i] = new Messages.dynamic_reconfigure.GroupState();
                pieces.Add(groups[i].Serialize(true));
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
            
            //bools
            arraylength = rand.Next(10);
            if (bools == null)
                bools = new Messages.dynamic_reconfigure.BoolParameter[arraylength];
            else
                Array.Resize(ref bools, arraylength);
            for (int i=0;i<bools.Length; i++) {
                //bools[i]
                bools[i] = new Messages.dynamic_reconfigure.BoolParameter();
                bools[i].Randomize();
            }
            //ints
            arraylength = rand.Next(10);
            if (ints == null)
                ints = new Messages.dynamic_reconfigure.IntParameter[arraylength];
            else
                Array.Resize(ref ints, arraylength);
            for (int i=0;i<ints.Length; i++) {
                //ints[i]
                ints[i] = new Messages.dynamic_reconfigure.IntParameter();
                ints[i].Randomize();
            }
            //strs
            arraylength = rand.Next(10);
            if (strs == null)
                strs = new Messages.dynamic_reconfigure.StrParameter[arraylength];
            else
                Array.Resize(ref strs, arraylength);
            for (int i=0;i<strs.Length; i++) {
                //strs[i]
                strs[i] = new Messages.dynamic_reconfigure.StrParameter();
                strs[i].Randomize();
            }
            //doubles
            arraylength = rand.Next(10);
            if (doubles == null)
                doubles = new Messages.dynamic_reconfigure.DoubleParameter[arraylength];
            else
                Array.Resize(ref doubles, arraylength);
            for (int i=0;i<doubles.Length; i++) {
                //doubles[i]
                doubles[i] = new Messages.dynamic_reconfigure.DoubleParameter();
                doubles[i].Randomize();
            }
            //groups
            arraylength = rand.Next(10);
            if (groups == null)
                groups = new Messages.dynamic_reconfigure.GroupState[arraylength];
            else
                Array.Resize(ref groups, arraylength);
            for (int i=0;i<groups.Length; i++) {
                //groups[i]
                groups[i] = new Messages.dynamic_reconfigure.GroupState();
                groups[i].Randomize();
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            dynamic_reconfigure.Config other = (Messages.dynamic_reconfigure.Config)____other;

            if (bools.Length != other.bools.Length)
                return false;
            for (int __i__=0; __i__ < bools.Length; __i__++)
            {
                ret &= bools[__i__].Equals(other.bools[__i__]);
            }
            if (ints.Length != other.ints.Length)
                return false;
            for (int __i__=0; __i__ < ints.Length; __i__++)
            {
                ret &= ints[__i__].Equals(other.ints[__i__]);
            }
            if (strs.Length != other.strs.Length)
                return false;
            for (int __i__=0; __i__ < strs.Length; __i__++)
            {
                ret &= strs[__i__].Equals(other.strs[__i__]);
            }
            if (doubles.Length != other.doubles.Length)
                return false;
            for (int __i__=0; __i__ < doubles.Length; __i__++)
            {
                ret &= doubles[__i__].Equals(other.doubles[__i__]);
            }
            if (groups.Length != other.groups.Length)
                return false;
            for (int __i__=0; __i__ < groups.Length; __i__++)
            {
                ret &= groups[__i__].Equals(other.groups[__i__]);
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
