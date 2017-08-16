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
using Messages;
using Messages.std_msgs;
using String=System.String;
using hector_uav_msgs;
using actionlib;

namespace hector_uav_msgs
{
	#if !TRACE
	[System.Diagnostics.DebuggerStepThrough]
	#endif
	public class PoseGoal : AGoal
	{
		public PoseStamped target_pose;


		PoseGoal (PoseGoal pg)
		{
			target_pose = new PoseStamped ();
			target_pose.pose = new Pose ();
			target_pose.pose.orientation = new Quaternion ();
			target_pose.pose.orientation.x = pg.target_pose.pose.orientation.x;
			target_pose.pose.orientation.y = pg.target_pose.pose.orientation.y;
			target_pose.pose.orientation.z = pg.target_pose.pose.orientation.z;
			target_pose.pose.orientation.w = pg.target_pose.pose.orientation.w;
			target_pose.pose.position = new Point ();
			target_pose.pose.position.x = pg.target_pose.pose.position.x;
			target_pose.pose.position.y = pg.target_pose.pose.position.y;
			target_pose.pose.position.z = pg.target_pose.pose.position.z;
			target_pose.header = new Header ();
			target_pose.header.Frame_id = pg.target_pose.header.Frame_id;
			target_pose.header.Seq = pg.target_pose.header.Seq;
			target_pose.header.Stamp = new Time ( pg.target_pose.header.Stamp.data );
		}

		public override AGoal Clone ()
		{
			return new PoseGoal ( this );
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override string MD5Sum() { return "257d089627d7eb7136c24d3593d05a16"; }
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override bool HasHeader() { return false; }
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override bool IsMetaType() { return false; }
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override string MessageDefinition() { return @"PoseStamped target_pose"; }
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override MsgTypes msgtype() { return MsgTypes.hector_uav_msgs__PoseGoal; }
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override bool IsServiceComponent() { return false; }

		[System.Diagnostics.DebuggerStepThrough]
		public PoseGoal()
		{

		}

		[System.Diagnostics.DebuggerStepThrough]
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public PoseGoal(byte[] SERIALIZEDSTUFF)
		{
			Deserialize(SERIALIZEDSTUFF);
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public PoseGoal(byte[] SERIALIZEDSTUFF, ref int currentIndex)
		{
			Deserialize(SERIALIZEDSTUFF, ref currentIndex);
		}



		[System.Diagnostics.DebuggerStepThrough]
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override void Deserialize(byte[] SERIALIZEDSTUFF, ref int currentIndex)
		{
			target_pose.Deserialize ( SERIALIZEDSTUFF, ref currentIndex );
		}

		[System.Diagnostics.DebuggerStepThrough]
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override byte[] Serialize(bool partofsomethingelse)
		{
			return target_pose.Serialize ( partofsomethingelse );
		}

		public override void Randomize()
		{
			target_pose.Randomize ();
		}

		public override bool Equals(IRosMessage ____other)
		{
			if (____other == null) return false;
			bool ret = true;
			hector_uav_msgs.PoseGoal other = (hector_uav_msgs.PoseGoal)____other;

			ret &= target_pose.Equals ( other.target_pose );
			return ret;
		}
	}
}
