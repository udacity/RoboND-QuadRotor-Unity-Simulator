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

namespace Messages.quad_controller
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class GetPath : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.quad_controller__GetPath; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"bool ignoreMe
---
nav_msgs/Path path"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "f65fd00b2e3f675220a83a94307b7f12"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public GetPath()
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
				public bool ignoreMe; //woo


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "9b6724bbfcc986844015a2e2d7750a81"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"bool ignoreMe"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.quad_controller__GetPath__Request; }
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
                
                //ignoreMe
                ignoreMe = SERIALIZEDSTUFF[currentIndex++]==1;
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //ignoreMe
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)ignoreMe ? 1 : 0 );
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
                
                //ignoreMe
                ignoreMe = rand.Next(2) == 1;
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                quad_controller.GetPath.Request other = (Messages.quad_controller.GetPath.Request)____other;

                ret &= ignoreMe == other.ignoreMe;
                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public Messages.nav_msgs.Path path; //woo



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "9b6724bbfcc986844015a2e2d7750a81"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return true; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"nav_msgs/Path path"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.quad_controller__GetPath__Response; }
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
                
                //path
                path = new Messages.nav_msgs.Path(SERIALIZEDSTUFF, ref currentIndex);
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //path
                if (path == null)
                    path = new Messages.nav_msgs.Path();
                pieces.Add(path.Serialize(true));
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
                
                //path
                path = new Messages.nav_msgs.Path();
                path.Randomize();
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                quad_controller.GetPath.Response other = (Messages.quad_controller.GetPath.Response)____other;

                ret &= path.Equals(other.path);
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
