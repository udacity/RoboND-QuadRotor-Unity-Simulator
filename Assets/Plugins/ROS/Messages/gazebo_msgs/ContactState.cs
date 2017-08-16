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

namespace Messages.gazebo_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ContactState : IRosMessage
    {

			public string info; //woo
			public string collision1_name; //woo
			public string collision2_name; //woo
			public Wrench[] wrenches;
			public Wrench total_wrench; //woo
			public Vector3[] contact_positions;
			public Vector3[] contact_normals;
			public double[] depths;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "48c0ffb054b8c444f870cecea1ee50d9"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return true; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"string info
string collision1_name
string collision2_name
geometry_msgs/Wrench[] wrenches
geometry_msgs/Wrench total_wrench
geometry_msgs/Vector3[] contact_positions
geometry_msgs/Vector3[] contact_normals
float64[] depths"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__ContactState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ContactState()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ContactState(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ContactState(byte[] SERIALIZEDSTUFF, ref int currentIndex)
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
            
            //info
            info = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            info = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //collision1_name
            collision1_name = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            collision1_name = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //collision2_name
            collision2_name = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            collision2_name = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
            currentIndex += piecesize;
            //wrenches
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (wrenches == null)
                wrenches = new Wrench[arraylength];
            else
                Array.Resize(ref wrenches, arraylength);
            for (int i=0;i<wrenches.Length; i++) {
                //wrenches[i]
                wrenches[i] = new Wrench(SERIALIZEDSTUFF, ref currentIndex);
            }
            //total_wrench
            total_wrench = new Wrench(SERIALIZEDSTUFF, ref currentIndex);
            //contact_positions
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (contact_positions == null)
                contact_positions = new Vector3[arraylength];
            else
                Array.Resize(ref contact_positions, arraylength);
            for (int i=0;i<contact_positions.Length; i++) {
                //contact_positions[i]
                contact_positions[i] = new Vector3(SERIALIZEDSTUFF, ref currentIndex);
            }
            //contact_normals
            hasmetacomponents |= true;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (contact_normals == null)
                contact_normals = new Vector3[arraylength];
            else
                Array.Resize(ref contact_normals, arraylength);
            for (int i=0;i<contact_normals.Length; i++) {
                //contact_normals[i]
                contact_normals[i] = new Vector3(SERIALIZEDSTUFF, ref currentIndex);
            }
            //depths
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (depths == null)
                depths = new double[arraylength];
            else
                Array.Resize(ref depths, arraylength);
            for (int i=0;i<depths.Length; i++) {
                //depths[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                depths[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
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
            
            //info
            if (info == null)
                info = "";
            scratch1 = Encoding.ASCII.GetBytes((string)info);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //collision1_name
            if (collision1_name == null)
                collision1_name = "";
            scratch1 = Encoding.ASCII.GetBytes((string)collision1_name);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //collision2_name
            if (collision2_name == null)
                collision2_name = "";
            scratch1 = Encoding.ASCII.GetBytes((string)collision2_name);
            thischunk = new byte[scratch1.Length + 4];
            scratch2 = BitConverter.GetBytes(scratch1.Length);
            Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
            Array.Copy(scratch2, thischunk, 4);
            pieces.Add(thischunk);
            //wrenches
            hasmetacomponents |= true;
            if (wrenches == null)
                wrenches = new Wrench[0];
            pieces.Add(BitConverter.GetBytes(wrenches.Length));
            for (int i=0;i<wrenches.Length; i++) {
                //wrenches[i]
                if (wrenches[i] == null)
                    wrenches[i] = new Wrench();
                pieces.Add(wrenches[i].Serialize(true));
            }
            //total_wrench
            if (total_wrench == null)
                total_wrench = new Wrench();
            pieces.Add(total_wrench.Serialize(true));
            //contact_positions
            hasmetacomponents |= true;
            if (contact_positions == null)
                contact_positions = new Vector3[0];
            pieces.Add(BitConverter.GetBytes(contact_positions.Length));
            for (int i=0;i<contact_positions.Length; i++) {
                //contact_positions[i]
                if (contact_positions[i] == null)
                    contact_positions[i] = new Vector3();
                pieces.Add(contact_positions[i].Serialize(true));
            }
            //contact_normals
            hasmetacomponents |= true;
            if (contact_normals == null)
                contact_normals = new Vector3[0];
            pieces.Add(BitConverter.GetBytes(contact_normals.Length));
            for (int i=0;i<contact_normals.Length; i++) {
                //contact_normals[i]
                if (contact_normals[i] == null)
                    contact_normals[i] = new Vector3();
                pieces.Add(contact_normals[i].Serialize(true));
            }
            //depths
            hasmetacomponents |= false;
            if (depths == null)
                depths = new double[0];
            pieces.Add(BitConverter.GetBytes(depths.Length));
            for (int i=0;i<depths.Length; i++) {
                //depths[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(depths[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
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
            
            //info
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            info = Encoding.ASCII.GetString(strbuf);
            //collision1_name
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            collision1_name = Encoding.ASCII.GetString(strbuf);
            //collision2_name
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            collision2_name = Encoding.ASCII.GetString(strbuf);
            //wrenches
            arraylength = rand.Next(10);
            if (wrenches == null)
                wrenches = new Wrench[arraylength];
            else
                Array.Resize(ref wrenches, arraylength);
            for (int i=0;i<wrenches.Length; i++) {
                //wrenches[i]
                wrenches[i] = new Wrench();
                wrenches[i].Randomize();
            }
            //total_wrench
            total_wrench = new Wrench();
            total_wrench.Randomize();
            //contact_positions
            arraylength = rand.Next(10);
            if (contact_positions == null)
                contact_positions = new Vector3[arraylength];
            else
                Array.Resize(ref contact_positions, arraylength);
            for (int i=0;i<contact_positions.Length; i++) {
                //contact_positions[i]
                contact_positions[i] = new Vector3();
                contact_positions[i].Randomize();
            }
            //contact_normals
            arraylength = rand.Next(10);
            if (contact_normals == null)
                contact_normals = new Vector3[arraylength];
            else
                Array.Resize(ref contact_normals, arraylength);
            for (int i=0;i<contact_normals.Length; i++) {
                //contact_normals[i]
                contact_normals[i] = new Vector3();
                contact_normals[i].Randomize();
            }
            //depths
            arraylength = rand.Next(10);
            if (depths == null)
                depths = new double[arraylength];
            else
                Array.Resize(ref depths, arraylength);
            for (int i=0;i<depths.Length; i++) {
                //depths[i]
                depths[i] = (rand.Next() + rand.NextDouble());
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            gazebo_msgs.ContactState other = (Messages.gazebo_msgs.ContactState)____other;

            ret &= info == other.info;
            ret &= collision1_name == other.collision1_name;
            ret &= collision2_name == other.collision2_name;
            if (wrenches.Length != other.wrenches.Length)
                return false;
            for (int __i__=0; __i__ < wrenches.Length; __i__++)
            {
                ret &= wrenches[__i__].Equals(other.wrenches[__i__]);
            }
            ret &= total_wrench.Equals(other.total_wrench);
            if (contact_positions.Length != other.contact_positions.Length)
                return false;
            for (int __i__=0; __i__ < contact_positions.Length; __i__++)
            {
                ret &= contact_positions[__i__].Equals(other.contact_positions[__i__]);
            }
            if (contact_normals.Length != other.contact_normals.Length)
                return false;
            for (int __i__=0; __i__ < contact_normals.Length; __i__++)
            {
                ret &= contact_normals[__i__].Equals(other.contact_normals[__i__]);
            }
            if (depths.Length != other.depths.Length)
                return false;
            for (int __i__=0; __i__ < depths.Length; __i__++)
            {
                ret &= depths[__i__] == other.depths[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
