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
    public class GetPhysicsProperties : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.gazebo_msgs__GetPhysicsProperties; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"---
float64 time_step
bool pause
float64 max_update_rate
geometry_msgs/Vector3 gravity
gazebo_msgs/ODEPhysics ode_config
bool success
string status_message"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "575a5e74786981b7df2e3afc567693a6"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public GetPhysicsProperties()
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


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "d41d8cd98f00b204e9800998ecf8427e"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @""; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetPhysicsProperties__Request; }
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
                
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
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
                
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                gazebo_msgs.GetPhysicsProperties.Request other = (Messages.gazebo_msgs.GetPhysicsProperties.Request)____other;

                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public double time_step; //woo
				public bool pause; //woo
				public double max_update_rate; //woo
				public Vector3 gravity; //woo
				public Messages.gazebo_msgs.ODEPhysics ode_config; //woo
				public bool success; //woo
				public string status_message; //woo



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "d41d8cd98f00b204e9800998ecf8427e"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return true; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"float64 time_step
bool pause
float64 max_update_rate
geometry_msgs/Vector3 gravity
gazebo_msgs/ODEPhysics ode_config
bool success
string status_message"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetPhysicsProperties__Response; }
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
                
                //time_step
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                time_step = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
                //pause
                pause = SERIALIZEDSTUFF[currentIndex++]==1;
                //max_update_rate
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                max_update_rate = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
                //gravity
                gravity = new Vector3(SERIALIZEDSTUFF, ref currentIndex);
                //ode_config
                ode_config = new Messages.gazebo_msgs.ODEPhysics(SERIALIZEDSTUFF, ref currentIndex);
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
                
                //time_step
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(time_step, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
                //pause
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)pause ? 1 : 0 );
                pieces.Add(thischunk);
                //max_update_rate
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(max_update_rate, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
                //gravity
                if (gravity == null)
                    gravity = new Vector3();
                pieces.Add(gravity.Serialize(true));
                //ode_config
                if (ode_config == null)
                    ode_config = new Messages.gazebo_msgs.ODEPhysics();
                pieces.Add(ode_config.Serialize(true));
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
                
                //time_step
                time_step = (rand.Next() + rand.NextDouble());
                //pause
                pause = rand.Next(2) == 1;
                //max_update_rate
                max_update_rate = (rand.Next() + rand.NextDouble());
                //gravity
                gravity = new Vector3();
                gravity.Randomize();
                //ode_config
                ode_config = new Messages.gazebo_msgs.ODEPhysics();
                ode_config.Randomize();
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
                gazebo_msgs.GetPhysicsProperties.Response other = (Messages.gazebo_msgs.GetPhysicsProperties.Response)____other;

                ret &= time_step == other.time_step;
                ret &= pause == other.pause;
                ret &= max_update_rate == other.max_update_rate;
                ret &= gravity.Equals(other.gravity);
                ret &= ode_config.Equals(other.ode_config);
                ret &= success == other.success;
                ret &= status_message == other.status_message;
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
