// enable this define to try and log when values in the header are set
//#define VALUE_LOGGING
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

namespace Messages.std_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Header : IRosMessage
    {
		public uint Seq
		{
			get { return seq; }
			set
			{
				#if VALUE_LOGGING
				UnityEngine.Debug.Log ("Setting seq to " + value);
				#endif
				seq = value;
			}
		}

		public Time Stamp
		{
			get { return stamp; }
			set 
			{
				#if VALUE_LOGGING
				UnityEngine.Debug.Log ("Setting stamp to " + value.data.toSec ().ToString ());
				#endif
				stamp = value;
			}
		}

		public string Frame_id
		{
			get { return frame_id; }
			set
			{
				#if VALUE_LOGGING
				UnityEngine.Debug.Log ("Setting frame_id to " + value);
				#endif
				frame_id = value;
			}
		}

		uint seq; //woo
		Time stamp; //woo
		string frame_id; //woo


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "2176decaecbce78abc3b96ef049fabed"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"uint32 seq
time stamp
string frame_id"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.std_msgs__Header; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public Header()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Header(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Header(byte[] SERIALIZEDSTUFF, ref int currentIndex)
        {
            Deserialize(SERIALIZEDSTUFF, ref currentIndex);
        }

		public Header (Header other)
		{
			this.Frame_id = other.Frame_id;
			this.Seq = other.Seq;
			this.Stamp = new Time ( other.Stamp );
		}

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override void Deserialize(byte[] SERIALIZEDSTUFF, ref int currentIndex)
        {
			UnityEngine.Debug.Log ( "deserializing header" );
            int arraylength=-1;
            bool hasmetacomponents = false;
            object __thing;
            int piecesize=0;
            byte[] thischunk, scratch1, scratch2;
            IntPtr h;
            
            //seq
            piecesize = Marshal.SizeOf(typeof(uint));
            h = IntPtr.Zero;
            if (SERIALIZEDSTUFF.Length - currentIndex != 0)
            {
				try
				{h = Marshal.AllocHGlobal(piecesize);}
				catch (Exception e)
				{
					UnityEngine.Debug.LogError ( "exception: " + e.GetType ().ToString () + " : " + ( e.Message != null ? e.Message : "" ) );
					return;
				}
                Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
            }
			if ( h == IntPtr.Zero )
			{
				UnityEngine.Debug.LogError ( "Zero pointer!" );
				return;
			}
//            if (h == IntPtr.Zero) throw new Exception("Alloc failed");
            seq = (uint)Marshal.PtrToStructure(h, typeof(uint));
            Marshal.FreeHGlobal(h);
            currentIndex+= piecesize;
            //stamp
            stamp = new Time(new TimeData(
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex),
                    BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex+Marshal.SizeOf(typeof(System.Int32)))));
            currentIndex += 2*Marshal.SizeOf(typeof(System.Int32));
            //frame_id
            frame_id = "";
            piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += 4;
            frame_id = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
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
            
            //seq
            scratch1 = new byte[Marshal.SizeOf(typeof(uint))];
            h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
            Marshal.StructureToPtr(seq, h.AddrOfPinnedObject(), false);
            h.Free();
            pieces.Add(scratch1);
            //stamp
            pieces.Add(BitConverter.GetBytes(stamp.data.sec));
            pieces.Add(BitConverter.GetBytes(stamp.data.nsec));
            //frame_id
            if (frame_id == null)
                frame_id = "";
            scratch1 = Encoding.ASCII.GetBytes((string)frame_id);
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
//			UnityEngine.Debug.Log ( "Header: " + __a_b__d.GetString () );
            return __a_b__d;
        }

        public override void Randomize()
        {
            int arraylength=-1;
            Random rand = new Random();
            int strlength;
            byte[] strbuf, myByte;
            
            //seq
            seq = (uint)rand.Next();
            //stamp
            stamp = new Time(new TimeData(
                    Convert.ToUInt32(rand.Next()),
                    Convert.ToUInt32(rand.Next())));
            //frame_id
            strlength = rand.Next(100) + 1;
            strbuf = new byte[strlength];
            rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
            for (int __x__ = 0; __x__ < strlength; __x__++)
                if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                    strbuf[__x__] = (byte)(rand.Next(254) + 1);
            strbuf[strlength - 1] = 0; //null terminate
            frame_id = Encoding.ASCII.GetString(strbuf);
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            std_msgs.Header other = (Messages.std_msgs.Header)____other;

            ret &= seq == other.Seq;
            ret &= stamp.data.Equals(other.Stamp.data);
            ret &= frame_id == other.Frame_id;
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
