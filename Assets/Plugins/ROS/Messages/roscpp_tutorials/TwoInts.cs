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

namespace Messages.roscpp_tutorials
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class TwoInts : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.roscpp_tutorials__TwoInts; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"int64 a
int64 b
---
int64 sum"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "6a2e34150c00229791cc89ff309fff21"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public TwoInts()
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
				public long a; //woo
				public long b; //woo


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "36d09b846be0b371c5f190354dd3153e"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"int64 a
int64 b"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.roscpp_tutorials__TwoInts__Request; }
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
                
                //a
                piecesize = Marshal.SizeOf(typeof(long));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                a = (long)Marshal.PtrToStructure(h, typeof(long));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
                //b
                piecesize = Marshal.SizeOf(typeof(long));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                b = (long)Marshal.PtrToStructure(h, typeof(long));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //a
                scratch1 = new byte[Marshal.SizeOf(typeof(long))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(a, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
                //b
                scratch1 = new byte[Marshal.SizeOf(typeof(long))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(b, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
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
                
                //a
                a = (System.Int64)(rand.Next() << 32) | rand.Next();
                //b
                b = (System.Int64)(rand.Next() << 32) | rand.Next();
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                roscpp_tutorials.TwoInts.Request other = (Messages.roscpp_tutorials.TwoInts.Request)____other;

                ret &= a == other.a;
                ret &= b == other.b;
                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public long sum; //woo



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "36d09b846be0b371c5f190354dd3153e"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"int64 sum"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.roscpp_tutorials__TwoInts__Response; }
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
                
                //sum
                piecesize = Marshal.SizeOf(typeof(long));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                sum = (long)Marshal.PtrToStructure(h, typeof(long));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //sum
                scratch1 = new byte[Marshal.SizeOf(typeof(long))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(sum, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
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
                
                //sum
                sum = (System.Int64)(rand.Next() << 32) | rand.Next();
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                roscpp_tutorials.TwoInts.Response other = (Messages.roscpp_tutorials.TwoInts.Response)____other;

                ret &= sum == other.sum;
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
