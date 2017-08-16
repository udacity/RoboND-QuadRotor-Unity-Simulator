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

namespace Messages.humanoid_nav_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class PlanFootstepsBetweenFeet : IRosService
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override SrvTypes srvtype() { return SrvTypes.humanoid_nav_msgs__PlanFootstepsBetweenFeet; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string ServiceDefinition() { return @"humanoid_nav_msgs/StepTarget start_left
humanoid_nav_msgs/StepTarget start_right
humanoid_nav_msgs/StepTarget goal_left
humanoid_nav_msgs/StepTarget goal_right
---
bool result
humanoid_nav_msgs/StepTarget[] footsteps
float64 costs
float64 final_eps
float64 planning_time
int64 expanded_states"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "a4b63c1d84c3783139a04f9abafe7214"; }
        
        [System.Diagnostics.DebuggerStepThrough]
        public PlanFootstepsBetweenFeet()
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
				public Messages.humanoid_nav_msgs.StepTarget start_left; //woo
				public Messages.humanoid_nav_msgs.StepTarget start_right; //woo
				public Messages.humanoid_nav_msgs.StepTarget goal_left; //woo
				public Messages.humanoid_nav_msgs.StepTarget goal_right; //woo


            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "a081711eb51a4a4283b2b9f345efe272"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return true; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"humanoid_nav_msgs/StepTarget start_left
humanoid_nav_msgs/StepTarget start_right
humanoid_nav_msgs/StepTarget goal_left
humanoid_nav_msgs/StepTarget goal_right"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.humanoid_nav_msgs__PlanFootstepsBetweenFeet__Request; }
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
                
                //start_left
                start_left = new Messages.humanoid_nav_msgs.StepTarget(SERIALIZEDSTUFF, ref currentIndex);
                //start_right
                start_right = new Messages.humanoid_nav_msgs.StepTarget(SERIALIZEDSTUFF, ref currentIndex);
                //goal_left
                goal_left = new Messages.humanoid_nav_msgs.StepTarget(SERIALIZEDSTUFF, ref currentIndex);
                //goal_right
                goal_right = new Messages.humanoid_nav_msgs.StepTarget(SERIALIZEDSTUFF, ref currentIndex);
            }

            [System.Diagnostics.DebuggerStepThrough]
            public override byte[] Serialize(bool partofsomethingelse)
            {
                int currentIndex=0, length=0;
                bool hasmetacomponents = false;
                byte[] thischunk, scratch1, scratch2;
                List<byte[]> pieces = new List<byte[]>();
                GCHandle h;
                
                //start_left
                if (start_left == null)
                    start_left = new Messages.humanoid_nav_msgs.StepTarget();
                pieces.Add(start_left.Serialize(true));
                //start_right
                if (start_right == null)
                    start_right = new Messages.humanoid_nav_msgs.StepTarget();
                pieces.Add(start_right.Serialize(true));
                //goal_left
                if (goal_left == null)
                    goal_left = new Messages.humanoid_nav_msgs.StepTarget();
                pieces.Add(goal_left.Serialize(true));
                //goal_right
                if (goal_right == null)
                    goal_right = new Messages.humanoid_nav_msgs.StepTarget();
                pieces.Add(goal_right.Serialize(true));
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
                
                //start_left
                start_left = new Messages.humanoid_nav_msgs.StepTarget();
                start_left.Randomize();
                //start_right
                start_right = new Messages.humanoid_nav_msgs.StepTarget();
                start_right.Randomize();
                //goal_left
                goal_left = new Messages.humanoid_nav_msgs.StepTarget();
                goal_left.Randomize();
                //goal_right
                goal_right = new Messages.humanoid_nav_msgs.StepTarget();
                goal_right.Randomize();
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                humanoid_nav_msgs.PlanFootstepsBetweenFeet.Request other = (Messages.humanoid_nav_msgs.PlanFootstepsBetweenFeet.Request)____other;

                ret &= start_left.Equals(other.start_left);
                ret &= start_right.Equals(other.start_right);
                ret &= goal_left.Equals(other.goal_left);
                ret &= goal_right.Equals(other.goal_right);
                return ret;
            }
        }

        public class Response : IRosMessage
        {
				public bool result; //woo
				public Messages.humanoid_nav_msgs.StepTarget[] footsteps;
				public double costs; //woo
				public double final_eps; //woo
				public double planning_time; //woo
				public long expanded_states; //woo



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MD5Sum() { return "a081711eb51a4a4283b2b9f345efe272"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool HasHeader() { return false; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool IsMetaType() { return true; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string MessageDefinition() { return @"bool result
humanoid_nav_msgs/StepTarget[] footsteps
float64 costs
float64 final_eps
float64 planning_time
int64 expanded_states"; }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override MsgTypes msgtype() { return MsgTypes.humanoid_nav_msgs__PlanFootstepsBetweenFeet__Response; }
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
                
                //result
                result = SERIALIZEDSTUFF[currentIndex++]==1;
                //footsteps
                hasmetacomponents |= true;
                arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
                currentIndex += Marshal.SizeOf(typeof(System.Int32));
                if (footsteps == null)
                    footsteps = new Messages.humanoid_nav_msgs.StepTarget[arraylength];
                else
                    Array.Resize(ref footsteps, arraylength);
                for (int i=0;i<footsteps.Length; i++) {
                    //footsteps[i]
                    footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget(SERIALIZEDSTUFF, ref currentIndex);
                }
                //costs
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                costs = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
                //final_eps
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                final_eps = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
                //planning_time
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                planning_time = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
                //expanded_states
                piecesize = Marshal.SizeOf(typeof(long));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                expanded_states = (long)Marshal.PtrToStructure(h, typeof(long));
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
                
                //result
                thischunk = new byte[1];
                thischunk[0] = (byte) ((bool)result ? 1 : 0 );
                pieces.Add(thischunk);
                //footsteps
                hasmetacomponents |= true;
                if (footsteps == null)
                    footsteps = new Messages.humanoid_nav_msgs.StepTarget[0];
                pieces.Add(BitConverter.GetBytes(footsteps.Length));
                for (int i=0;i<footsteps.Length; i++) {
                    //footsteps[i]
                    if (footsteps[i] == null)
                        footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget();
                    pieces.Add(footsteps[i].Serialize(true));
                }
                //costs
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(costs, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
                //final_eps
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(final_eps, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
                //planning_time
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(planning_time, h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
                //expanded_states
                scratch1 = new byte[Marshal.SizeOf(typeof(long))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(expanded_states, h.AddrOfPinnedObject(), false);
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
                
                //result
                result = rand.Next(2) == 1;
                //footsteps
                arraylength = rand.Next(10);
                if (footsteps == null)
                    footsteps = new Messages.humanoid_nav_msgs.StepTarget[arraylength];
                else
                    Array.Resize(ref footsteps, arraylength);
                for (int i=0;i<footsteps.Length; i++) {
                    //footsteps[i]
                    footsteps[i] = new Messages.humanoid_nav_msgs.StepTarget();
                    footsteps[i].Randomize();
                }
                //costs
                costs = (rand.Next() + rand.NextDouble());
                //final_eps
                final_eps = (rand.Next() + rand.NextDouble());
                //planning_time
                planning_time = (rand.Next() + rand.NextDouble());
                //expanded_states
                expanded_states = (System.Int64)(rand.Next() << 32) | rand.Next();
            }

            public override bool Equals(IRosMessage ____other)
            {
                if (____other == null) return false;
                bool ret = true;
                humanoid_nav_msgs.PlanFootstepsBetweenFeet.Response other = (Messages.humanoid_nav_msgs.PlanFootstepsBetweenFeet.Response)____other;

                ret &= result == other.result;
                if (footsteps.Length != other.footsteps.Length)
                    return false;
                for (int __i__=0; __i__ < footsteps.Length; __i__++)
                {
                    ret &= footsteps[__i__].Equals(other.footsteps[__i__]);
                }
                ret &= costs == other.costs;
                ret &= final_eps == other.final_eps;
                ret &= planning_time == other.planning_time;
                ret &= expanded_states == other.expanded_states;
                // for each SingleType st:
                //    ret &= {st.Name} == other.{st.Name};
                return ret;
            }
        }
    }
}
