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

namespace Messages.control_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class QueryTrajectoryState : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.control_msgs__QueryTrajectoryState; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"time time
---
string[] name
float64[] position
float64[] velocity
float64[] acceleration"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "ec93cdecbd8062d761aa52b7c90cd44b"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public QueryTrajectoryState()
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
				public Time time; //woo


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "556a4fb76023a469987922359d08a844"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"time time"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.control_msgs__QueryTrajectoryState__Request; }
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
                
                //time
                time = new Time(new TimeData(
                        BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex),
                        BitConverter.ToUInt32(SERIALIZEDSTUFF, currentIndex+Marshal.SizeOf(typeof(System.Int32)))));
                currentIndex += 2*Marshal.SizeOf(typeof(System.Int32));
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //time
                pieces.Add(BitConverter.GetBytes(time.data.sec));
                pieces.Add(BitConverter.GetBytes(time.data.nsec));
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
                
                //time
                time = new Time(new TimeData(
                        Convert.ToUInt32(rand.Next()),
                        Convert.ToUInt32(rand.Next())));
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                control_msgs.QueryTrajectoryState.Request other = (Messages.control_msgs.QueryTrajectoryState.Request)____other;

                ret &= time.data.Equals(other.time.data);
                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public string[] name;
				public double[] position;
				public double[] velocity;
				public double[] acceleration;



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "556a4fb76023a469987922359d08a844"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"string[] name
float64[] position
float64[] velocity
float64[] acceleration"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.control_msgs__QueryTrajectoryState__Response; }
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
                //velocity
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (velocity == null)
                    velocity = new double[arraylength];
                else
                    Array.Resize(ref velocity, arraylength);
                for (int i=0;i<velocity.Length; i++) {
                    //velocity[i]
                    piecesize = Marshal.SizeOf(typeof(double));
                    h = IntPtr.Zero;
                    if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                    {
                        h = Marshal.AllocHGlobal(piecesize);
                        Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                    }
                    if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                    velocity[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                    Marshal.FreeHGlobal(h);
                    currentIndex+= piecesize;
                }
                //acceleration
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (acceleration == null)
                    acceleration = new double[arraylength];
                else
                    Array.Resize(ref acceleration, arraylength);
                for (int i=0;i<acceleration.Length; i++) {
                    //acceleration[i]
                    piecesize = Marshal.SizeOf(typeof(double));
                    h = IntPtr.Zero;
                    if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                    {
                        h = Marshal.AllocHGlobal(piecesize);
                        Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                    }
                    if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                    acceleration[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                    Marshal.FreeHGlobal(h);
                    currentIndex+= piecesize;
                }
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
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
                //velocity
                hasmetacomponents |= false;
                if (velocity == null)
                    velocity = new double[0];
                pieces.Add(BitConverter.GetBytes(velocity.Length));
                for (int i=0;i<velocity.Length; i++) {
                    //velocity[i]
                    scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                    h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                    Marshal.StructureToPtr(velocity[i], h.AddrOfPinnedObject(), false);
                    h.Free();
                    pieces.Add(scratch1);
                }
                //acceleration
                hasmetacomponents |= false;
                if (acceleration == null)
                    acceleration = new double[0];
                pieces.Add(BitConverter.GetBytes(acceleration.Length));
                for (int i=0;i<acceleration.Length; i++) {
                    //acceleration[i]
                    scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                    h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                    Marshal.StructureToPtr(acceleration[i], h.AddrOfPinnedObject(), false);
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
                //velocity
                arraylength = rand.Next(10);
                if (velocity == null)
                    velocity = new double[arraylength];
                else
                    Array.Resize(ref velocity, arraylength);
                for (int i=0;i<velocity.Length; i++) {
                    //velocity[i]
                    velocity[i] = (rand.Next() + rand.NextDouble());
                }
                //acceleration
                arraylength = rand.Next(10);
                if (acceleration == null)
                    acceleration = new double[arraylength];
                else
                    Array.Resize(ref acceleration, arraylength);
                for (int i=0;i<acceleration.Length; i++) {
                    //acceleration[i]
                    acceleration[i] = (rand.Next() + rand.NextDouble());
                }
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                control_msgs.QueryTrajectoryState.Response other = (Messages.control_msgs.QueryTrajectoryState.Response)____other;

                if (name.Length != other.name.Length)
                    return false;
                for (int __i__=0; __i__ < name.Length; __i__++)
                {
                    ret &= name[__i__] == other.name[__i__];
                }
                if (position.Length != other.position.Length)
                    return false;
                for (int __i__=0; __i__ < position.Length; __i__++)
                {
                    ret &= position[__i__] == other.position[__i__];
                }
                if (velocity.Length != other.velocity.Length)
                    return false;
                for (int __i__=0; __i__ < velocity.Length; __i__++)
                {
                    ret &= velocity[__i__] == other.velocity[__i__];
                }
                if (acceleration.Length != other.acceleration.Length)
                    return false;
                for (int __i__=0; __i__ < acceleration.Length; __i__++)
                {
                    ret &= acceleration[__i__] == other.acceleration[__i__];
                }
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
