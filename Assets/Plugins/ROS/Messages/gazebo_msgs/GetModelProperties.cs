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
    public class GetModelProperties : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.gazebo_msgs__GetModelProperties; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"string model_name
---
string parent_model_name
string canonical_body_name
string[] body_names
string[] geom_names
string[] joint_names
string[] child_model_names
bool is_static
bool success
string status_message"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "5717f7bd34ed990fa80e28b3015027a3"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public GetModelProperties()
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
				public string model_name; //woo


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "ea31c8eab6fc401383cf528a7c0984ba"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"string model_name"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetModelProperties__Request; }
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
                
                //model_name
                model_name = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                model_name = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
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
                
                //model_name
                if (model_name == null)
                    model_name = "";
                scratch1 = Encoding.ASCII.GetBytes((string)model_name);
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
                
                //model_name
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                model_name = Encoding.ASCII.GetString(strbuf);
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                gazebo_msgs.GetModelProperties.Request other = (Messages.gazebo_msgs.GetModelProperties.Request)____other;

                ret &= model_name == other.model_name;
                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public string parent_model_name; //woo
				public string canonical_body_name; //woo
				public string[] body_names;
				public string[] geom_names;
				public string[] joint_names;
				public string[] child_model_names;
				public bool is_static; //woo
				public bool success; //woo
				public string status_message; //woo



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "ea31c8eab6fc401383cf528a7c0984ba"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"string parent_model_name
string canonical_body_name
string[] body_names
string[] geom_names
string[] joint_names
string[] child_model_names
bool is_static
bool success
string status_message"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__GetModelProperties__Response; }
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
                
                //parent_model_name
                parent_model_name = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                parent_model_name = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
                //canonical_body_name
                canonical_body_name = "";
                piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += 4;
                canonical_body_name = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                currentIndex += piecesize;
                //body_names
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (body_names == null)
                    body_names = new string[arraylength];
                else
                    Array.Resize(ref body_names, arraylength);
                for (int i=0;i<body_names.Length; i++) {
                    //body_names[i]
                    body_names[i] = "";
                    piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                    currentIndex += 4;
                    body_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                    currentIndex += piecesize;
                }
                //geom_names
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (geom_names == null)
                    geom_names = new string[arraylength];
                else
                    Array.Resize(ref geom_names, arraylength);
                for (int i=0;i<geom_names.Length; i++) {
                    //geom_names[i]
                    geom_names[i] = "";
                    piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                    currentIndex += 4;
                    geom_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                    currentIndex += piecesize;
                }
                //joint_names
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (joint_names == null)
                    joint_names = new string[arraylength];
                else
                    Array.Resize(ref joint_names, arraylength);
                for (int i=0;i<joint_names.Length; i++) {
                    //joint_names[i]
                    joint_names[i] = "";
                    piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                    currentIndex += 4;
                    joint_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                    currentIndex += piecesize;
                }
                //child_model_names
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (child_model_names == null)
                    child_model_names = new string[arraylength];
                else
                    Array.Resize(ref child_model_names, arraylength);
                for (int i=0;i<child_model_names.Length; i++) {
                    //child_model_names[i]
                    child_model_names[i] = "";
                    piecesize = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                    currentIndex += 4;
                    child_model_names[i] = Encoding.ASCII.GetString(SERIALIZEDSTUFF, currentIndex, piecesize);
                    currentIndex += piecesize;
                }
                //is_static
                is_static = SERIALIZEDSTUFF[currentIndex++]==1;
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
                
                //parent_model_name
                if (parent_model_name == null)
                    parent_model_name = "";
                scratch1 = Encoding.ASCII.GetBytes((string)parent_model_name);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
                //canonical_body_name
                if (canonical_body_name == null)
                    canonical_body_name = "";
                scratch1 = Encoding.ASCII.GetBytes((string)canonical_body_name);
                thischunk = new byte[scratch1.Length + 4];
                scratch2 = BitConverter.GetBytes(scratch1.Length);
                Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                Array.Copy(scratch2, thischunk, 4);
                pieces.Add(thischunk);
                //body_names
                hasmetacomponents |= false;
                if (body_names == null)
                    body_names = new string[0];
                pieces.Add(BitConverter.GetBytes(body_names.Length));
                for (int i=0;i<body_names.Length; i++) {
                    //body_names[i]
                    if (body_names[i] == null)
                        body_names[i] = "";
                    scratch1 = Encoding.ASCII.GetBytes((string)body_names[i]);
                    thischunk = new byte[scratch1.Length + 4];
                    scratch2 = BitConverter.GetBytes(scratch1.Length);
                    Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                    Array.Copy(scratch2, thischunk, 4);
                    pieces.Add(thischunk);
                }
                //geom_names
                hasmetacomponents |= false;
                if (geom_names == null)
                    geom_names = new string[0];
                pieces.Add(BitConverter.GetBytes(geom_names.Length));
                for (int i=0;i<geom_names.Length; i++) {
                    //geom_names[i]
                    if (geom_names[i] == null)
                        geom_names[i] = "";
                    scratch1 = Encoding.ASCII.GetBytes((string)geom_names[i]);
                    thischunk = new byte[scratch1.Length + 4];
                    scratch2 = BitConverter.GetBytes(scratch1.Length);
                    Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                    Array.Copy(scratch2, thischunk, 4);
                    pieces.Add(thischunk);
                }
                //joint_names
                hasmetacomponents |= false;
                if (joint_names == null)
                    joint_names = new string[0];
                pieces.Add(BitConverter.GetBytes(joint_names.Length));
                for (int i=0;i<joint_names.Length; i++) {
                    //joint_names[i]
                    if (joint_names[i] == null)
                        joint_names[i] = "";
                    scratch1 = Encoding.ASCII.GetBytes((string)joint_names[i]);
                    thischunk = new byte[scratch1.Length + 4];
                    scratch2 = BitConverter.GetBytes(scratch1.Length);
                    Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                    Array.Copy(scratch2, thischunk, 4);
                    pieces.Add(thischunk);
                }
                //child_model_names
                hasmetacomponents |= false;
                if (child_model_names == null)
                    child_model_names = new string[0];
                pieces.Add(BitConverter.GetBytes(child_model_names.Length));
                for (int i=0;i<child_model_names.Length; i++) {
                    //child_model_names[i]
                    if (child_model_names[i] == null)
                        child_model_names[i] = "";
                    scratch1 = Encoding.ASCII.GetBytes((string)child_model_names[i]);
                    thischunk = new byte[scratch1.Length + 4];
                    scratch2 = BitConverter.GetBytes(scratch1.Length);
                    Array.Copy(scratch1, 0, thischunk, 4, scratch1.Length);
                    Array.Copy(scratch2, thischunk, 4);
                    pieces.Add(thischunk);
                }
                //is_static
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)is_static ? 1 : 0 );
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
                
                //parent_model_name
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                parent_model_name = Encoding.ASCII.GetString(strbuf);
                //canonical_body_name
                strlength = rand.Next(100) + 1;
                strbuf = new byte[strlength];
                rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                for (int __x__ = 0; __x__ < strlength; __x__++)
                    if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                        strbuf[__x__] = (byte)(rand.Next(254) + 1);
                strbuf[strlength - 1] = 0; //null terminate
                canonical_body_name = Encoding.ASCII.GetString(strbuf);
                //body_names
                arraylength = rand.Next(10);
                if (body_names == null)
                    body_names = new string[arraylength];
                else
                    Array.Resize(ref body_names, arraylength);
                for (int i=0;i<body_names.Length; i++) {
                    //body_names[i]
                    strlength = rand.Next(100) + 1;
                    strbuf = new byte[strlength];
                    rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                    for (int __x__ = 0; __x__ < strlength; __x__++)
                        if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                            strbuf[__x__] = (byte)(rand.Next(254) + 1);
                    strbuf[strlength - 1] = 0; //null terminate
                    body_names[i] = Encoding.ASCII.GetString(strbuf);
                }
                //geom_names
                arraylength = rand.Next(10);
                if (geom_names == null)
                    geom_names = new string[arraylength];
                else
                    Array.Resize(ref geom_names, arraylength);
                for (int i=0;i<geom_names.Length; i++) {
                    //geom_names[i]
                    strlength = rand.Next(100) + 1;
                    strbuf = new byte[strlength];
                    rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                    for (int __x__ = 0; __x__ < strlength; __x__++)
                        if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                            strbuf[__x__] = (byte)(rand.Next(254) + 1);
                    strbuf[strlength - 1] = 0; //null terminate
                    geom_names[i] = Encoding.ASCII.GetString(strbuf);
                }
                //joint_names
                arraylength = rand.Next(10);
                if (joint_names == null)
                    joint_names = new string[arraylength];
                else
                    Array.Resize(ref joint_names, arraylength);
                for (int i=0;i<joint_names.Length; i++) {
                    //joint_names[i]
                    strlength = rand.Next(100) + 1;
                    strbuf = new byte[strlength];
                    rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                    for (int __x__ = 0; __x__ < strlength; __x__++)
                        if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                            strbuf[__x__] = (byte)(rand.Next(254) + 1);
                    strbuf[strlength - 1] = 0; //null terminate
                    joint_names[i] = Encoding.ASCII.GetString(strbuf);
                }
                //child_model_names
                arraylength = rand.Next(10);
                if (child_model_names == null)
                    child_model_names = new string[arraylength];
                else
                    Array.Resize(ref child_model_names, arraylength);
                for (int i=0;i<child_model_names.Length; i++) {
                    //child_model_names[i]
                    strlength = rand.Next(100) + 1;
                    strbuf = new byte[strlength];
                    rand.NextBytes(strbuf);  //fill the whole buffer with random bytes
                    for (int __x__ = 0; __x__ < strlength; __x__++)
                        if (strbuf[__x__] == 0) //replace null chars with non-null random ones
                            strbuf[__x__] = (byte)(rand.Next(254) + 1);
                    strbuf[strlength - 1] = 0; //null terminate
                    child_model_names[i] = Encoding.ASCII.GetString(strbuf);
                }
                //is_static
                is_static = rand.Next(2) == 1;
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
                gazebo_msgs.GetModelProperties.Response other = (Messages.gazebo_msgs.GetModelProperties.Response)____other;

                ret &= parent_model_name == other.parent_model_name;
                ret &= canonical_body_name == other.canonical_body_name;
                if (body_names.Length != other.body_names.Length)
                    return false;
                for (int __i__=0; __i__ < body_names.Length; __i__++)
                {
                    ret &= body_names[__i__] == other.body_names[__i__];
                }
                if (geom_names.Length != other.geom_names.Length)
                    return false;
                for (int __i__=0; __i__ < geom_names.Length; __i__++)
                {
                    ret &= geom_names[__i__] == other.geom_names[__i__];
                }
                if (joint_names.Length != other.joint_names.Length)
                    return false;
                for (int __i__=0; __i__ < joint_names.Length; __i__++)
                {
                    ret &= joint_names[__i__] == other.joint_names[__i__];
                }
                if (child_model_names.Length != other.child_model_names.Length)
                    return false;
                for (int __i__=0; __i__ < child_model_names.Length; __i__++)
                {
                    ret &= child_model_names[__i__] == other.child_model_names[__i__];
                }
                ret &= is_static == other.is_static;
                ret &= success == other.success;
                ret &= status_message == other.status_message;
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
