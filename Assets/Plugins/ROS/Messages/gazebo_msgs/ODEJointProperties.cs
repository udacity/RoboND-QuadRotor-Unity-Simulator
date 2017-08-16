using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using uint8 = System.Byte;
using Messages.geometry_msgs;
using Messages.sensor_msgs;
using Messages.actionlib_msgs;

using Messages.std_msgs;
using String=System.String;

namespace Messages.gazebo_msgs
{
#if !TRACE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ODEJointProperties : IRosMessage
    {

			public double[] damping;
			public double[] hiStop;
			public double[] loStop;
			public double[] erp;
			public double[] cfm;
			public double[] stop_erp;
			public double[] stop_cfm;
			public double[] fudge_factor;
			public double[] fmax;
			public double[] vel;


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MD5Sum() { return "1b744c32a920af979f53afe2f9c3511f"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool HasHeader() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsMetaType() { return false; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string MessageDefinition() { return @"float64[] damping
float64[] hiStop
float64[] loStop
float64[] erp
float64[] cfm
float64[] stop_erp
float64[] stop_cfm
float64[] fudge_factor
float64[] fmax
float64[] vel"; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override MsgTypes msgtype() { return MsgTypes.gazebo_msgs__ODEJointProperties; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override bool IsServiceComponent() { return false; }

        [System.Diagnostics.DebuggerStepThrough]
        public ODEJointProperties()
        {
            
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ODEJointProperties(byte[] SERIALIZEDSTUFF)
        {
            Deserialize(SERIALIZEDSTUFF);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ODEJointProperties(byte[] SERIALIZEDSTUFF, ref int currentIndex)
        {
            Deserialize(SERIALIZEDSTUFF, ref currentIndex);
        }



        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override void Deserialize(byte[] SERIALIZEDSTUFF, ref int currentIndex)
        {
            int arraylength=-1;
            bool hasmetacomponents = false;
            object __thing;
            int piecesize=0;
            byte[] thischunk, scratch1, scratch2;
            IntPtr h;
            
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
            //hiStop
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (hiStop == null)
                hiStop = new double[arraylength];
            else
                Array.Resize(ref hiStop, arraylength);
            for (int i=0;i<hiStop.Length; i++) {
                //hiStop[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                hiStop[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //loStop
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (loStop == null)
                loStop = new double[arraylength];
            else
                Array.Resize(ref loStop, arraylength);
            for (int i=0;i<loStop.Length; i++) {
                //loStop[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                loStop[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //erp
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (erp == null)
                erp = new double[arraylength];
            else
                Array.Resize(ref erp, arraylength);
            for (int i=0;i<erp.Length; i++) {
                //erp[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                erp[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //cfm
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (cfm == null)
                cfm = new double[arraylength];
            else
                Array.Resize(ref cfm, arraylength);
            for (int i=0;i<cfm.Length; i++) {
                //cfm[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                cfm[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //stop_erp
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (stop_erp == null)
                stop_erp = new double[arraylength];
            else
                Array.Resize(ref stop_erp, arraylength);
            for (int i=0;i<stop_erp.Length; i++) {
                //stop_erp[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                stop_erp[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //stop_cfm
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (stop_cfm == null)
                stop_cfm = new double[arraylength];
            else
                Array.Resize(ref stop_cfm, arraylength);
            for (int i=0;i<stop_cfm.Length; i++) {
                //stop_cfm[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                stop_cfm[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //fudge_factor
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (fudge_factor == null)
                fudge_factor = new double[arraylength];
            else
                Array.Resize(ref fudge_factor, arraylength);
            for (int i=0;i<fudge_factor.Length; i++) {
                //fudge_factor[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                fudge_factor[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //fmax
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (fmax == null)
                fmax = new double[arraylength];
            else
                Array.Resize(ref fmax, arraylength);
            for (int i=0;i<fmax.Length; i++) {
                //fmax[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                fmax[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
            //vel
            hasmetacomponents |= false;
            arraylength = BitConverter.ToInt32(SERIALIZEDSTUFF, currentIndex);
            currentIndex += Marshal.SizeOf(typeof(System.Int32));
            if (vel == null)
                vel = new double[arraylength];
            else
                Array.Resize(ref vel, arraylength);
            for (int i=0;i<vel.Length; i++) {
                //vel[i]
                piecesize = Marshal.SizeOf(typeof(double));
                h = IntPtr.Zero;
                if (SERIALIZEDSTUFF.Length - currentIndex != 0)
                {
                    h = Marshal.AllocHGlobal(piecesize);
                    Marshal.Copy(SERIALIZEDSTUFF, currentIndex, h, piecesize);
                }
                if (h == IntPtr.Zero) throw new Exception("Alloc failed");
                vel[i] = (double)Marshal.PtrToStructure(h, typeof(double));
                Marshal.FreeHGlobal(h);
                currentIndex+= piecesize;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override byte[] Serialize(bool partofsomethingelse)
        {
            int currentIndex=0, length=0;
            bool hasmetacomponents = false;
            byte[] thischunk, scratch1, scratch2;
            List<byte[]> pieces = new List<byte[]>();
            GCHandle h;
            
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
            //hiStop
            hasmetacomponents |= false;
            if (hiStop == null)
                hiStop = new double[0];
            pieces.Add(BitConverter.GetBytes(hiStop.Length));
            for (int i=0;i<hiStop.Length; i++) {
                //hiStop[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(hiStop[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //loStop
            hasmetacomponents |= false;
            if (loStop == null)
                loStop = new double[0];
            pieces.Add(BitConverter.GetBytes(loStop.Length));
            for (int i=0;i<loStop.Length; i++) {
                //loStop[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(loStop[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //erp
            hasmetacomponents |= false;
            if (erp == null)
                erp = new double[0];
            pieces.Add(BitConverter.GetBytes(erp.Length));
            for (int i=0;i<erp.Length; i++) {
                //erp[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(erp[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //cfm
            hasmetacomponents |= false;
            if (cfm == null)
                cfm = new double[0];
            pieces.Add(BitConverter.GetBytes(cfm.Length));
            for (int i=0;i<cfm.Length; i++) {
                //cfm[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(cfm[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //stop_erp
            hasmetacomponents |= false;
            if (stop_erp == null)
                stop_erp = new double[0];
            pieces.Add(BitConverter.GetBytes(stop_erp.Length));
            for (int i=0;i<stop_erp.Length; i++) {
                //stop_erp[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(stop_erp[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //stop_cfm
            hasmetacomponents |= false;
            if (stop_cfm == null)
                stop_cfm = new double[0];
            pieces.Add(BitConverter.GetBytes(stop_cfm.Length));
            for (int i=0;i<stop_cfm.Length; i++) {
                //stop_cfm[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(stop_cfm[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //fudge_factor
            hasmetacomponents |= false;
            if (fudge_factor == null)
                fudge_factor = new double[0];
            pieces.Add(BitConverter.GetBytes(fudge_factor.Length));
            for (int i=0;i<fudge_factor.Length; i++) {
                //fudge_factor[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(fudge_factor[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //fmax
            hasmetacomponents |= false;
            if (fmax == null)
                fmax = new double[0];
            pieces.Add(BitConverter.GetBytes(fmax.Length));
            for (int i=0;i<fmax.Length; i++) {
                //fmax[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(fmax[i], h.AddrOfPinnedObject(), false);
                h.Free();
                pieces.Add(scratch1);
            }
            //vel
            hasmetacomponents |= false;
            if (vel == null)
                vel = new double[0];
            pieces.Add(BitConverter.GetBytes(vel.Length));
            for (int i=0;i<vel.Length; i++) {
                //vel[i]
                scratch1 = new byte[Marshal.SizeOf(typeof(double))];
                h = GCHandle.Alloc(scratch1, GCHandleType.Pinned);
                Marshal.StructureToPtr(vel[i], h.AddrOfPinnedObject(), false);
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
            //hiStop
            arraylength = rand.Next(10);
            if (hiStop == null)
                hiStop = new double[arraylength];
            else
                Array.Resize(ref hiStop, arraylength);
            for (int i=0;i<hiStop.Length; i++) {
                //hiStop[i]
                hiStop[i] = (rand.Next() + rand.NextDouble());
            }
            //loStop
            arraylength = rand.Next(10);
            if (loStop == null)
                loStop = new double[arraylength];
            else
                Array.Resize(ref loStop, arraylength);
            for (int i=0;i<loStop.Length; i++) {
                //loStop[i]
                loStop[i] = (rand.Next() + rand.NextDouble());
            }
            //erp
            arraylength = rand.Next(10);
            if (erp == null)
                erp = new double[arraylength];
            else
                Array.Resize(ref erp, arraylength);
            for (int i=0;i<erp.Length; i++) {
                //erp[i]
                erp[i] = (rand.Next() + rand.NextDouble());
            }
            //cfm
            arraylength = rand.Next(10);
            if (cfm == null)
                cfm = new double[arraylength];
            else
                Array.Resize(ref cfm, arraylength);
            for (int i=0;i<cfm.Length; i++) {
                //cfm[i]
                cfm[i] = (rand.Next() + rand.NextDouble());
            }
            //stop_erp
            arraylength = rand.Next(10);
            if (stop_erp == null)
                stop_erp = new double[arraylength];
            else
                Array.Resize(ref stop_erp, arraylength);
            for (int i=0;i<stop_erp.Length; i++) {
                //stop_erp[i]
                stop_erp[i] = (rand.Next() + rand.NextDouble());
            }
            //stop_cfm
            arraylength = rand.Next(10);
            if (stop_cfm == null)
                stop_cfm = new double[arraylength];
            else
                Array.Resize(ref stop_cfm, arraylength);
            for (int i=0;i<stop_cfm.Length; i++) {
                //stop_cfm[i]
                stop_cfm[i] = (rand.Next() + rand.NextDouble());
            }
            //fudge_factor
            arraylength = rand.Next(10);
            if (fudge_factor == null)
                fudge_factor = new double[arraylength];
            else
                Array.Resize(ref fudge_factor, arraylength);
            for (int i=0;i<fudge_factor.Length; i++) {
                //fudge_factor[i]
                fudge_factor[i] = (rand.Next() + rand.NextDouble());
            }
            //fmax
            arraylength = rand.Next(10);
            if (fmax == null)
                fmax = new double[arraylength];
            else
                Array.Resize(ref fmax, arraylength);
            for (int i=0;i<fmax.Length; i++) {
                //fmax[i]
                fmax[i] = (rand.Next() + rand.NextDouble());
            }
            //vel
            arraylength = rand.Next(10);
            if (vel == null)
                vel = new double[arraylength];
            else
                Array.Resize(ref vel, arraylength);
            for (int i=0;i<vel.Length; i++) {
                //vel[i]
                vel[i] = (rand.Next() + rand.NextDouble());
            }
        }

        public override bool Equals(IRosMessage ____other)
        {
            if (____other == null) return false;
            bool ret = true;
            gazebo_msgs.ODEJointProperties other = (Messages.gazebo_msgs.ODEJointProperties)____other;

            if (damping.Length != other.damping.Length)
                return false;
            for (int __i__=0; __i__ < damping.Length; __i__++)
            {
                ret &= damping[__i__] == other.damping[__i__];
            }
            if (hiStop.Length != other.hiStop.Length)
                return false;
            for (int __i__=0; __i__ < hiStop.Length; __i__++)
            {
                ret &= hiStop[__i__] == other.hiStop[__i__];
            }
            if (loStop.Length != other.loStop.Length)
                return false;
            for (int __i__=0; __i__ < loStop.Length; __i__++)
            {
                ret &= loStop[__i__] == other.loStop[__i__];
            }
            if (erp.Length != other.erp.Length)
                return false;
            for (int __i__=0; __i__ < erp.Length; __i__++)
            {
                ret &= erp[__i__] == other.erp[__i__];
            }
            if (cfm.Length != other.cfm.Length)
                return false;
            for (int __i__=0; __i__ < cfm.Length; __i__++)
            {
                ret &= cfm[__i__] == other.cfm[__i__];
            }
            if (stop_erp.Length != other.stop_erp.Length)
                return false;
            for (int __i__=0; __i__ < stop_erp.Length; __i__++)
            {
                ret &= stop_erp[__i__] == other.stop_erp[__i__];
            }
            if (stop_cfm.Length != other.stop_cfm.Length)
                return false;
            for (int __i__=0; __i__ < stop_cfm.Length; __i__++)
            {
                ret &= stop_cfm[__i__] == other.stop_cfm[__i__];
            }
            if (fudge_factor.Length != other.fudge_factor.Length)
                return false;
            for (int __i__=0; __i__ < fudge_factor.Length; __i__++)
            {
                ret &= fudge_factor[__i__] == other.fudge_factor[__i__];
            }
            if (fmax.Length != other.fmax.Length)
                return false;
            for (int __i__=0; __i__ < fmax.Length; __i__++)
            {
                ret &= fmax[__i__] == other.fmax[__i__];
            }
            if (vel.Length != other.vel.Length)
                return false;
            for (int __i__=0; __i__ < vel.Length; __i__++)
            {
                ret &= vel[__i__] == other.vel[__i__];
            }
            // for each SingleType st:
            //    ret &= {st.Name} == other.{st.Name};
            return ret;
        }
    }
}
