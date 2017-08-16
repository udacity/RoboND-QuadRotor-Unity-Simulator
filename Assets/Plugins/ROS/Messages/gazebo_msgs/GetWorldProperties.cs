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
    public class GetWorldProperties : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.gazebo_msgs__GetWorldProperties; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"---
float64 sim_time
string[] model_names
bool rendering_enabled
bool success
string status_message"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "36bb0f2eccf4d8be971410c22818ba3f"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public GetWorldProperties()
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
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetWorldProperties__Request; }
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
                gazebo_msgs.GetWorldProperties.Request other = (Messages.gazebo_msgs.GetWorldProperties.Request)____other;

                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public double sim_time; //woo
				public string[] model_names;
				public bool rendering_enabled; //woo
				public bool success; //woo
				public string status_message; //woo



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "d41d8cd98f00b204e9800998ecf8427e"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"float64 sim_time
string[] model_names
bool rendering_enabled
bool success
string status_message"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetWorldProperties__Response; }
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
                
                //sim_time
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                sim_time = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
                //model_names
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (model_names == null)
                    model_names = new string[arraylength];
                else
                    Array.Resize(ref model_names, arraylength);
                for (int i=0;i<model_names.Length; i++) {
                    //model_names[i]
                    model_names[i] = "";
                    piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                    currentIndex += 4;
                    model_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                    currentIndex += piecesize;
                }
                //rendering_enabled
                rendering_enabled = SERIALIZEDSTUFF[currentIndex++]==1;
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
                
                //sim_time
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(sim_time, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
                //model_names
                hasmetacomponents |= false;
                if (model_names == null)
                    model_names = new string[0];
                pieces.Add(BitConverter.GetBytes(model_names.Length));
                for (int i=0;i<model_names.Length; i++) {
                    //model_names[i]
                    if (model_names[i] == null)
                        model_names[i] = "";
                    scratch1 = Encoding.ASCII.GetBytes((string)model_names[i]);
                    thischunk = new byte[scratch1.Length + 4];
                    scratch2 = BitConverter.GetBytes(scratch1.Length);
                    Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                    Array.Copy(scratch2, thischunk, 4);
                    pieces.Add(thischunk);
                }
                //rendering_enabled
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)rendering_enabled ? 1 : 0 );
                pieces.Add(thischunk);
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
                
                //sim_time
                sim_time = (rand.Next() + rand.NextDouble());
                //model_names
                arraylength = rand.Next(10);
                if (model_names == null)
                    model_names = new string[arraylength];
                else
                    Array.Resize(ref model_names, arraylength);
                for (int i=0;i<model_names.Length; i++) {
                    //model_names[i]
                    strlength = rand.Next(100) + 1;
                    strbuf = new byte[strlength];
                    rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                    for (int __x__ = 0; __x__ < strlength; __x__++)
                        if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                            strbuf[__x__] = (byte)(rand.Next(254) + 1);
                    strbuf[strlength - 1] = 0; //null terminate
                    model_names[i] = Encoding.ASCII.GetString(strbuf);
                }
                //rendering_enabled
                rendering_enabled = rand.Next(2) == 1;
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
                gazebo_msgs.GetWorldProperties.Response other = (Messages.gazebo_msgs.GetWorldProperties.Response)____other;

                ret &= sim_time == other.sim_time;
                if (model_names.Length != other.model_names.Length)
                    return false;
                for (int __i__=0; __i__ < model_names.Length; __i__++)
                {
                    ret &= model_names[__i__] == other.model_names[__i__];
                }
                ret &= rendering_enabled == other.rendering_enabled;
                ret &= success == other.success;
                ret &= status_message == other.status_message;
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
