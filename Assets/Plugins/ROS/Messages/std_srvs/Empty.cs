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

namespace Messages.std_srvs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Empty : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.std_srvs__Empty; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"---"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "d41d8cd98f00b204e9800998ecf8427e"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public Empty()
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
            public override MsgTypes msgtype() { return MsgTypes.std_srvs__Empty__Request; }
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
//                int arraylength=-1;
//                bool hasmetacomponents = false;
//                byte[] thischunk, scratch1, scratch2;
//                object __thing;
//                int piecesize=0;
//                IntPtr h;
                
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
//                int currentIndex=0, length=0;
//                bool hasmetacomponents = false;
//                byte[] thischunk, scratch1, scratch2;
//                List<byte[]> pieces = new List<byte[]>();
//                GCHandle h;
//                
//                //combine every array in pieces into one array and return it
//                int __a_b__f = pieces.Sum((__a_b__c)=>__a_b__c.Length);
//                int __a_b__e=0;
//                byte[] __a_b__d = new byte[__a_b__f];
//                foreach(var __p__ in pieces)
//				{
//					Array.Copy(__p__,0,__a_b__d,__a_b__e,__p__.Length);
//					__a_b__e += __p__.Length;
//				}
//                return __a_b__d;
				return new byte[0];
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
                std_srvs.Empty.Request other = (Messages.std_srvs.Empty.Request)____other;

                return ret;
            }
        }

        public class Response : IRosMessage
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
            public override MsgTypes msgtype() { return MsgTypes.std_srvs__Empty__Response; }
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

//                int arraylength=-1;
//                bool hasmetacomponents = false;
//                int piecesize=0;
//                byte[] thischunk, scratch1, scratch2;
//                IntPtr h;
//                object __thing;
                
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
//                int currentIndex=0, length=0;
//                bool hasmetacomponents = false;
//                byte[] thischunk, scratch1, scratch2;
//                List<byte[]> pieces = new List<byte[]>();
//                GCHandle h;
//                
//                //combine every array in pieces into one array and return it
//                int __a_b__f = pieces.Sum((__a_b__c)=>__a_b__c.Length);
//                int __a_b__e=0;
//                byte[] __a_b__d = new byte[__a_b__f];
//                foreach(var __p__ in pieces)
//				{
//					Array.Copy(__p__,0,__a_b__d,__a_b__e,__p__.Length);
//					__a_b__e += __p__.Length;
//				}
//                return __a_b__d;
				return new byte[0];
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
                std_srvs.Empty.Response other = (Messages.std_srvs.Empty.Response)____other;

                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
