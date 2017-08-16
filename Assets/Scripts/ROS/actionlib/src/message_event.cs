using Ros_CSharp;
//using ROS;
using Messages;
using Messages.std_msgs;
using System.Collections.Generic;

namespace Ros_CSharp
{
	public class MessageEvent<T> : IRosMessage where T : IRosMessage
	{
		public MessageEvent () {}
		public MessageEvent (T msg)
		{
			message = msg;
			connectionHeader = new Dictionary<string, string> ();
			receiptTime = ROS.GetTime ();
		}

//		public MessageEvent (T msg, string header)
//		{
//			message = msg;
//			connectionHeader = header;
//			receiptTime = ROS.GetTime ();
//		}

		T message;
		Dictionary<string, string> connectionHeader;
		Time receiptTime;

		public T getMessage () { return message; }
		public string getConnectionHeader ()
		{
			string s = "unknown_publisher";
			if ( connectionHeader != null )
				connectionHeader.TryGetValue ( "callerid", out s );
			return s;
		}
		public Time getReceiptTime () { return receiptTime; }

		public string getPublisherName () { return "Zippity-doo"; }

		/*******************************************************************************
		* Overrides from IRosMessage
		*******************************************************************************/

		public override string MD5Sum ()
		{
			return message.MD5Sum ();
		}

		public override bool Equals (IRosMessage msg)
		{
			if ( ReferenceEquals ( msg, null ) )
				return false;

			bool ret = true;
			MessageEvent<T> evt = (MessageEvent<T>) msg;
			ret &= ( evt != null && message == evt.message );

			return ret;
		}

		public override bool HasHeader ()
		{
			return message.HasHeader ();
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}

		public override bool IsMetaType ()
		{
			return message.IsMetaType ();
		}

		public override MsgTypes msgtype ()
		{
			return message.msgtype ();
		}

		public override string MessageDefinition ()
		{
			return message.MessageDefinition ();
		}

		public override void Randomize ()
		{
			message.Randomize ();
		}

		public override void Deserialize (byte[] SERIALIZEDSTUFF, ref int currentIndex)
		{
			message.Deserialize (SERIALIZEDSTUFF, ref currentIndex);
		}

		public override bool IsServiceComponent ()
		{
			return message.IsServiceComponent ();
		}

		public override byte[] Serialize (bool partofsomethingelse)
		{
			return message.Serialize (partofsomethingelse);
		}
	}
}