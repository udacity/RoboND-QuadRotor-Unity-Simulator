using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using uint8 = System.Byte;


using Messages.std_msgs;
using String=System.String;
using Messages.geometry_msgs;

namespace Messages.gazebo_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class GetJointProperties : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.gazebo_msgs__GetJointProperties; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"string joint_name
---
uint8 type
uint8 REVOLUTE=0
uint8 CONTINUOUS=1
uint8 PRISMATIC=2
uint8 FIXED=3
uint8 BALL=4
uint8 UNIVERSAL=5
float64[] damping
float64[] position
float64[] rate
bool success
string status_message"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "7b30be900f50aa21efec4a0ec92d91c9"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public GetJointProperties()
        {
            InitSubtypes(new Request(), new Response());
        }

        public Response Invoke(Func<Request, Response> fn, Request req)
        {
            RosServiceDelegate rsd = (m)=>{
                Request r = m as Request;
                if (r == null)
                    throw new Exception("Invalid Service Request Type");
                return fn(r);
            };
            return (Response)GeneralInvoke(rsd, (IRosMessage)req);
        }

        public Request req { get { return (Request)RequestMessage; } set { RequestMessage = (IRosMessage)value; } }
        public Response resp { get { return (Response)ResponseMessage; } set { ResponseMessage = (IRosMessage)value; } }

        public class Request : IRosMessage
        {
				public string joint_name; //woo


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "0be1351618e1dc030eb7959d9a4902de"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"string joint_name"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetJointProperties__Request; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsServiceComponent() { return true; }

            [System.Diagnostics.DebuggerStepThrough]
            public Request()
            {
                
            }

            [System.Diagnostics.DebuggerStepThrough]
            public Request(byte[] SERIALIZEDSTUFF)
            {
                Deserialize(SERIALIZEDSTUFF);
            }

            [System.Diagnostics.DebuggerStepThrough]
            public Request(byte[] SERIALIZEDSTUFF, ref int currentIndex)
            {
                Deserialize(SERIALIZEDSTUFF, ref currentIndex);
            }

    

            public override void Deserialize(byte[] SERIALIZEDSTUFF, ref int currentIndex)
            {
                int arraylength=-1;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                object __thing;
                int piecesize=0;
                IntPtr h;
                
                //joint_name
                joint_name = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                joint_name = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //joint_name
                if (joint_name == null)
                    joint_name = "";
                scratch1 = Encoding.ASCII.GetBytes((string)joint_name);
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
                
                //joint_name
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                joint_name = Encoding.ASCII.GetString(strbuf);
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                gazebo_msgs.GetJointProperties.Request other = (Messages.gazebo_msgs.GetJointProperties.Request)____other;

                ret &= joint_name == other.joint_name;
                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public byte type; //woo
				public const byte REVOLUTE    = 0; //woo
				public const byte CONTINUOUS  = 1; //woo
				public const byte PRISMATIC   = 2; //woo
				public const byte FIXED       = 3; //woo
				public const byte BALL        = 4; //woo
				public const byte UNIVERSAL   = 5; //woo
				public double[] damping;
				public double[] position;
				public double[] rate;
				public bool success; //woo
				public string status_message; //woo



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "0be1351618e1dc030eb7959d9a4902de"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"uint8 type
uint8 REVOLUTE=0
uint8 CONTINUOUS=1
uint8 PRISMATIC=2
uint8 FIXED=3
uint8 BALL=4
uint8 UNIVERSAL=5
float64[] damping
float64[] position
float64[] rate
bool success
string status_message"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetJointProperties__Response; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsServiceComponent() { return true; }

            [System.Diagnostics.DebuggerStepThrough]
            public Response()
            {
                
            }

            [System.Diagnostics.DebuggerStepThrough]
            public Response(byte[] SERIALIZEDSTUFF)
            {
                Deserialize(SERIALIZEDSTUFF);
            }
            [System.Diagnostics.DebuggerStepThrough]
            public Response(byte[] SERIALIZEDSTUFF, ref int currentIndex)
            {
                Deserialize(SERIALIZEDSTUFF, ref currentIndex);
            }

    

            //[System.Diagnostics.DebuggerStepThrough]
            public override void Deserialize(byte[] SERIALIZEDSTUFF, ref int currentIndex)
            {

                int arraylength=-1;
                bool hasmetacomponents = false;
                int piecesize=0;
                byte[] thischunk, scratch1, scratch2;
                IntPtr h;
                object __thing;
                
                //type
                type=SERIALIZEDSTUFF[currentIndex++];
                //damping
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (damping == null)
                    damping = new double[arraylength];
                else
                    Array.Resize(ref damping, arraylength);
                for (int i=0;i<damping.Length; i++) {
                    //damping[i]
                    piecesize = Marshal.SizeOf(typeof(double));
                    h = IntPtr.Zero;
                    if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                    {
                        h = Marshal.AllocHGlobal(piecesize);
                        Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                    }
                    if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                    damping[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                    Marshal.FreeHGlobal(h);
                    currentIndex+= piecesize;
                }
                //position
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (position == null)
                    position = new double[arraylength];
                else
                    Array.Resize(ref position, arraylength);
                for (int i=0;i<position.Length; i++) {
                    //position[i]
                    piecesize = Marshal.SizeOf(typeof(double));
                    h = IntPtr.Zero;
                    if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                    {
                        h = Marshal.AllocHGlobal(piecesize);
                        Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                    }
                    if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                    position[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                    Marshal.FreeHGlobal(h);
                    currentIndex+= piecesize;
                }
                //rate
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (rate == null)
                    rate = new double[arraylength];
                else
                    Array.Resize(ref rate, arraylength);
                for (int i=0;i<rate.Length; i++) {
                    //rate[i]
                    piecesize = Marshal.SizeOf(typeof(double));
                    h = IntPtr.Zero;
                    if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                    {
                        h = Marshal.AllocHGlobal(piecesize);
                        Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                    }
                    if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                    rate[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                    Marshal.FreeHGlobal(h);
                    currentIndex+= piecesize;
                }
                //success
                success = SERIALIZEDSTUFF[currentIndex++]==1;
                //status_message
                status_message = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                status_message = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //type
                pieces.Add(new[] { (byte)type });
                //damping
                hasmetacomponents |= false;
                if (damping == null)
                    damping = new double[0];
                pieces.Add(BitConverter.GetBytes(damping.Length));
                for (int i=0;i<damping.Length; i++) {
                    //damping[i]
                    scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                    h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                    Marshal.StructureToPtr(damping[i], h.AddrOfPinnedObject(), false);
                    h.Free();
                    pieces.Add(scratch1);
                }
                //position
                hasmetacomponents |= false;
                if (position == null)
                    position = new double[0];
                pieces.Add(BitConverter.GetBytes(position.Length));
                for (int i=0;i<position.Length; i++) {
                    //position[i]
                    scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                    h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                    Marshal.StructureToPtr(position[i], h.AddrOfPinnedObject(), false);
                    h.Free();
                    pieces.Add(scratch1);
                }
                //rate
                hasmetacomponents |= false;
                if (rate == null)
                    rate = new double[0];
                pieces.Add(BitConverter.GetBytes(rate.Length));
                for (int i=0;i<rate.Length; i++) {
                    //rate[i]
                    scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                    h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                    Marshal.StructureToPtr(rate[i], h.AddrOfPinnedObject(), false);
                    h.Free();
                    pieces.Add(scratch1);
                }
                //success
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)success ? 1 : 0 );
                pieces.Add(thischunk);
                //status_message
                if (status_message == null)
                    status_message = "";
                scratch1 = Encoding.ASCII.GetBytes((string)status_message);
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
                
                //type
                myByte = new byte[1];
                rand.NextBytes(myByte);
                type= myByte[0];
                //damping
                arraylength = rand.Next(10);
                if (damping == null)
                    damping = new double[arraylength];
                else
                    Array.Resize(ref damping, arraylength);
                for (int i=0;i<damping.Length; i++) {
                    //damping[i]
                    damping[i] = (rand.Next() + rand.NextDouble());
                }
                //position
                arraylength = rand.Next(10);
                if (position == null)
                    position = new double[arraylength];
                else
                    Array.Resize(ref position, arraylength);
                for (int i=0;i<position.Length; i++) {
                    //position[i]
                    position[i] = (rand.Next() + rand.NextDouble());
                }
                //rate
                arraylength = rand.Next(10);
                if (rate == null)
                    rate = new double[arraylength];
                else
                    Array.Resize(ref rate, arraylength);
                for (int i=0;i<rate.Length; i++) {
                    //rate[i]
                    rate[i] = (rand.Next() + rand.NextDouble());
                }
                //success
                success = rand.Next(2) == 1;
                //status_message
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                status_message = Encoding.ASCII.GetString(strbuf);
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                gazebo_msgs.GetJointProperties.Response other = (Messages.gazebo_msgs.GetJointProperties.Response)____other;

                ret &= type == other.type;
                if (damping.Length != other.damping.Length)
                    return false;
                for (int __i__=0; __i__ < damping.Length; __i__++)
                {
                    ret &= damping[__i__] == other.damping[__i__];
                }
                if (position.Length != other.position.Length)
                    return false;
                for (int __i__=0; __i__ < position.Length; __i__++)
                {
                    ret &= position[__i__] == other.position[__i__];
                }
                if (rate.Length != other.rate.Length)
                    return false;
                for (int __i__=0; __i__ < rate.Length; __i__++)
                {
                    ret &= rate[__i__] == other.rate[__i__];
                }
                ret &= success == other.success;
                ret &= status_message == other.status_message;
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
