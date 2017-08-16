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

namespace Messages.baxter_core_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class SolvePositionIK : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.baxter_core_msgs__SolvePositionIK; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"geometry_msgs/PoseStamped[] pose_stamp
sensor_msgs/JointState[] seed_angles
uint8 SEED_AUTO=0
uint8 SEED_USER=1
uint8 SEED_CURRENT=2
uint8 SEED_NS_MAP=3
uint8 seed_mode
---
sensor_msgs/JointState[] joints
bool[] isValid
uint8 RESULT_INVALID=0
uint8[] result_type"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "18cc92fd96853eb10b34de0b5c7d3cba"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public SolvePositionIK()
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
				public PoseStamped[] pose_stamp;
				public Messages.sensor_msgs.JointState[] seed_angles;
				public const byte SEED_AUTO    = 0; //woo
				public const byte SEED_USER    = 1; //woo
				public const byte SEED_CURRENT = 2; //woo
				public const byte SEED_NS_MAP  = 3; //woo
				public byte seed_mode; //woo


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "2587e42983d0081d0a2288230991073b"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return true; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"geometry_msgs/PoseStamped[] pose_stamp
sensor_msgs/JointState[] seed_angles
uint8 SEED_AUTO=0
uint8 SEED_USER=1
uint8 SEED_CURRENT=2
uint8 SEED_NS_MAP=3
uint8 seed_mode"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__SolvePositionIK__Request; }
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
                
                //pose_stamp
                hasmetacomponents |= true;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (pose_stamp == null)
                    pose_stamp = new PoseStamped[arraylength];
                else
                    Array.Resize(ref pose_stamp, arraylength);
                for (int i=0;i<pose_stamp.Length; i++) {
                    //pose_stamp[i]
                    pose_stamp[i] = new PoseStamped(SERIALIZEDSTUFF, ref currentIndex);
                }
                //seed_angles
                hasmetacomponents |= true;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (seed_angles == null)
                    seed_angles = new Messages.sensor_msgs.JointState[arraylength];
                else
                    Array.Resize(ref seed_angles, arraylength);
                for (int i=0;i<seed_angles.Length; i++) {
                    //seed_angles[i]
                    seed_angles[i] = new Messages.sensor_msgs.JointState(SERIALIZEDSTUFF, ref currentIndex);
                }
                //seed_mode
                seed_mode=SERIALIZEDSTUFF[currentIndex++];
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //pose_stamp
                hasmetacomponents |= true;
                if (pose_stamp == null)
                    pose_stamp = new PoseStamped[0];
                pieces.Add(BitConverter.GetBytes(pose_stamp.Length));
                for (int i=0;i<pose_stamp.Length; i++) {
                    //pose_stamp[i]
                    if (pose_stamp[i] == null)
                        pose_stamp[i] = new PoseStamped();
                    pieces.Add(pose_stamp[i].Serialize(true));
                }
                //seed_angles
                hasmetacomponents |= true;
                if (seed_angles == null)
                    seed_angles = new Messages.sensor_msgs.JointState[0];
                pieces.Add(BitConverter.GetBytes(seed_angles.Length));
                for (int i=0;i<seed_angles.Length; i++) {
                    //seed_angles[i]
                    if (seed_angles[i] == null)
                        seed_angles[i] = new Messages.sensor_msgs.JointState();
                    pieces.Add(seed_angles[i].Serialize(true));
                }
                //seed_mode
                pieces.Add(new[] { (byte)seed_mode });
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
                
                //pose_stamp
                arraylength = rand.Next(10);
                if (pose_stamp == null)
                    pose_stamp = new PoseStamped[arraylength];
                else
                    Array.Resize(ref pose_stamp, arraylength);
                for (int i=0;i<pose_stamp.Length; i++) {
                    //pose_stamp[i]
                    pose_stamp[i] = new PoseStamped();
                    pose_stamp[i].Randomize();
                }
                //seed_angles
                arraylength = rand.Next(10);
                if (seed_angles == null)
                    seed_angles = new Messages.sensor_msgs.JointState[arraylength];
                else
                    Array.Resize(ref seed_angles, arraylength);
                for (int i=0;i<seed_angles.Length; i++) {
                    //seed_angles[i]
                    seed_angles[i] = new Messages.sensor_msgs.JointState();
                    seed_angles[i].Randomize();
                }
                //seed_mode
                myByte = new byte[1];
                rand.NextBytes(myByte);
                seed_mode= myByte[0];
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                baxter_core_msgs.SolvePositionIK.Request other = (Messages.baxter_core_msgs.SolvePositionIK.Request)____other;

                if (pose_stamp.Length != other.pose_stamp.Length)
                    return false;
                for (int __i__=0; __i__ < pose_stamp.Length; __i__++)
                {
                    ret &= pose_stamp[__i__].Equals(other.pose_stamp[__i__]);
                }
                if (seed_angles.Length != other.seed_angles.Length)
                    return false;
                for (int __i__=0; __i__ < seed_angles.Length; __i__++)
                {
                    ret &= seed_angles[__i__].Equals(other.seed_angles[__i__]);
                }
                ret &= seed_mode == other.seed_mode;
                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public Messages.sensor_msgs.JointState[] joints;
				public bool[] isValid;
				public const byte RESULT_INVALID = 0; //woo
				public byte[] result_type;



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "2587e42983d0081d0a2288230991073b"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return true; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"sensor_msgs/JointState[] joints
bool[] isValid
uint8 RESULT_INVALID=0
uint8[] result_type"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.baxter_core_msgs__SolvePositionIK__Response; }
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
                
                //joints
                hasmetacomponents |= true;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (joints == null)
                    joints = new Messages.sensor_msgs.JointState[arraylength];
                else
                    Array.Resize(ref joints, arraylength);
                for (int i=0;i<joints.Length; i++) {
                    //joints[i]
                    joints[i] = new Messages.sensor_msgs.JointState(SERIALIZEDSTUFF, ref currentIndex);
                }
                //isValid
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (isValid == null)
                    isValid = new bool[arraylength];
                else
                    Array.Resize(ref isValid, arraylength);
                for (int i=0;i<isValid.Length; i++) {
                    //isValid[i]
                    isValid[i] = SERIALIZEDSTUFF[currentIndex++]==1;
                }
                //result_type
                hasmetacomponents |= false;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (result_type == null)
                    result_type = new byte[arraylength];
                else
                    Array.Resize(ref result_type, arraylength);
                Array.Copy(SERIALIZEDSTUFF, currentIndex, result_type, 0, result_type.Length);
                currentIndex += result_type.Length;
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //joints
                hasmetacomponents |= true;
                if (joints == null)
                    joints = new Messages.sensor_msgs.JointState[0];
                pieces.Add(BitConverter.GetBytes(joints.Length));
                for (int i=0;i<joints.Length; i++) {
                    //joints[i]
                    if (joints[i] == null)
                        joints[i] = new Messages.sensor_msgs.JointState();
                    pieces.Add(joints[i].Serialize(true));
                }
                //isValid
                hasmetacomponents |= false;
                if (isValid == null)
                    isValid = new bool[0];
                pieces.Add(BitConverter.GetBytes(isValid.Length));
                for (int i=0;i<isValid.Length; i++) {
                    //isValid[i]
                    thischunk = new byte[1];
                    thischunk[0] = (byte) ((bool)isValid[i] ? 1 : 0 );
                    pieces.Add(thischunk);
                }
                //result_type
                hasmetacomponents |= false;
                if (result_type == null)
                    result_type = new byte[0];
                pieces.Add(BitConverter.GetBytes(result_type.Length));
                pieces.Add((byte[])result_type);
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
                
                //joints
                arraylength = rand.Next(10);
                if (joints == null)
                    joints = new Messages.sensor_msgs.JointState[arraylength];
                else
                    Array.Resize(ref joints, arraylength);
                for (int i=0;i<joints.Length; i++) {
                    //joints[i]
                    joints[i] = new Messages.sensor_msgs.JointState();
                    joints[i].Randomize();
                }
                //isValid
                arraylength = rand.Next(10);
                if (isValid == null)
                    isValid = new bool[arraylength];
                else
                    Array.Resize(ref isValid, arraylength);
                for (int i=0;i<isValid.Length; i++) {
                    //isValid[i]
                    isValid[i] = rand.Next(2) == 1;
                }
                //result_type
                arraylength = rand.Next(10);
                if (result_type == null)
                    result_type = new byte[arraylength];
                else
                    Array.Resize(ref result_type, arraylength);
                for (int i=0;i<result_type.Length; i++) {
                    //result_type[i]
                    myByte = new byte[1];
                    rand.NextBytes(myByte);
                    result_type[i]= myByte[0];
                }
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                baxter_core_msgs.SolvePositionIK.Response other = (Messages.baxter_core_msgs.SolvePositionIK.Response)____other;

                if (joints.Length != other.joints.Length)
                    return false;
                for (int __i__=0; __i__ < joints.Length; __i__++)
                {
                    ret &= joints[__i__].Equals(other.joints[__i__]);
                }
                if (isValid.Length != other.isValid.Length)
                    return false;
                for (int __i__=0; __i__ < isValid.Length; __i__++)
                {
                    ret &= isValid[__i__] == other.isValid[__i__];
                }
                if (result_type.Length != other.result_type.Length)
                    return false;
                for (int __i__=0; __i__ < result_type.Length; __i__++)
                {
                    ret &= result_type[__i__] == other.result_type[__i__];
                }
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
