using Messages;
using goalid = Messages.actionlib_msgs.GoalID;

// non-abstract decorators for AActionGoal, AActionResult, AActionFeedback to pass to publisher (for errors about needing a parameterless constructor even though they all have one

namespace actionlib
{
	/*******************************************************************************
	* ActionGoalDecorator
	*******************************************************************************/
	public class ActionGoalDecorator : AActionGoal
	{
		AActionGoal _actionGoal;
		public ActionGoalDecorator () {}
		public ActionGoalDecorator (AActionGoal ag)
		{
			_actionGoal = ag;
		}

		public override AGoal Goal
		{
			get {
				return _actionGoal.Goal;
			}
			set {
				_actionGoal.Goal = value;
			}
		}

		public override goalid GoalID
		{
			get {
				return _actionGoal.GoalID;
			}
			set {
				_actionGoal.GoalID = value;
			}
		}

		public override Messages.std_msgs.Header Header
		{
			get {
				return _actionGoal.Header;
			}
			set {
				_actionGoal.Header = value;
			}
		}

		public override void Deserialize (byte[] SERIALIZEDSTUFF, ref int currentIndex)
		{
			_actionGoal.Deserialize (SERIALIZEDSTUFF, ref currentIndex);
		}

		public override bool Equals (IRosMessage msg)
		{
			return _actionGoal.Equals (msg);
		}

		public override bool Equals (object obj)
		{
			return _actionGoal.Equals (obj);
		}

		public override int GetHashCode ()
		{
			return _actionGoal.GetHashCode ();
		}

		public override bool HasHeader ()
		{
			return _actionGoal.HasHeader ();
		}

		public override bool IsMetaType ()
		{
			return _actionGoal.IsMetaType ();
		}

		public override bool IsServiceComponent ()
		{
			return _actionGoal.IsServiceComponent ();
		}

		public override string MD5Sum ()
		{
			return _actionGoal.MD5Sum ();
		}

		public override string MessageDefinition ()
		{
			return _actionGoal.MessageDefinition ();
		}

		public override MsgTypes msgtype ()
		{
			return _actionGoal.msgtype ();
		}

		public override void Randomize ()
		{
			_actionGoal.Randomize ();
		}

		public override byte[] Serialize (bool partofsomethingelse)
		{
			return _actionGoal.Serialize (partofsomethingelse);
		}
	}

	/*******************************************************************************
	* ActionResultDecorator
	*******************************************************************************/
	public class ActionResultDecorator : AActionResult
	{
		AActionResult _actionResult;
		public ActionResultDecorator () {}
		public ActionResultDecorator (AActionResult ar)
		{
			_actionResult = ar;
		}

		public override AResult Result
		{
			get {
				return _actionResult.Result;
			}
			set {
				_actionResult.Result = value;
			}
		}

		public override Messages.actionlib_msgs.GoalStatus GoalStatus
		{
			get {
				return _actionResult.GoalStatus;
			}
			set {
				_actionResult.GoalStatus = value;
			}
		}

		public override Messages.std_msgs.Header Header
		{
			get {
				return _actionResult.Header;
			}
			set {
				_actionResult.Header = value;
			}
		}

		public override void Deserialize (byte[] SERIALIZEDSTUFF, ref int currentIndex)
		{
			_actionResult.Deserialize (SERIALIZEDSTUFF, ref currentIndex);
		}

		public override bool Equals (IRosMessage msg)
		{
			return _actionResult.Equals (msg);
		}

		public override bool Equals (object obj)
		{
			return _actionResult.Equals (obj);
		}

		public override int GetHashCode ()
		{
			return _actionResult.GetHashCode ();
		}

		public override bool HasHeader ()
		{
			return _actionResult.HasHeader ();
		}

		public override bool IsMetaType ()
		{
			return _actionResult.IsMetaType ();
		}

		public override bool IsServiceComponent ()
		{
			return _actionResult.IsServiceComponent ();
		}

		public override string MD5Sum ()
		{
			return _actionResult.MD5Sum ();
		}

		public override string MessageDefinition ()
		{
			return _actionResult.MessageDefinition ();
		}

		public override MsgTypes msgtype ()
		{
			return _actionResult.msgtype ();
		}

		public override void Randomize ()
		{
			_actionResult.Randomize ();
		}

		public override byte[] Serialize (bool partofsomethingelse)
		{
			return _actionResult.Serialize (partofsomethingelse);
		}

		public override AActionResult Clone ()
		{
			return _actionResult.Clone ();
		}
	}

	/*******************************************************************************
	* ActionFeedbackDecorator
	*******************************************************************************/
	public class ActionFeedbackDecorator : AActionFeedback
	{
		AActionFeedback _actionFeedback;
		public ActionFeedbackDecorator () {}
		public ActionFeedbackDecorator (AActionFeedback af)
		{
			_actionFeedback = af;
		}

		public override AFeedback Feedback
		{
			get {
				return _actionFeedback.Feedback;
			}
			set {
				_actionFeedback.Feedback = value;
			}
		}

		public override Messages.actionlib_msgs.GoalStatus GoalStatus
		{
			get {
				return _actionFeedback.GoalStatus;
			}
			set {
				_actionFeedback.GoalStatus = value;
			}
		}

		public override Messages.std_msgs.Header Header
		{
			get {
				return _actionFeedback.Header;
			}
			set {
				_actionFeedback.Header = value;
			}
		}

		public override void Deserialize (byte[] SERIALIZEDSTUFF, ref int currentIndex)
		{
			_actionFeedback.Deserialize (SERIALIZEDSTUFF, ref currentIndex);
		}

		public override bool Equals (IRosMessage msg)
		{
			return _actionFeedback.Equals (msg);
		}

		public override bool Equals (object obj)
		{
			return _actionFeedback.Equals (obj);
		}

		public override int GetHashCode ()
		{
			return _actionFeedback.GetHashCode ();
		}

		public override bool HasHeader ()
		{
			return _actionFeedback.HasHeader ();
		}

		public override bool IsMetaType ()
		{
			return _actionFeedback.IsMetaType ();
		}

		public override bool IsServiceComponent ()
		{
			return _actionFeedback.IsServiceComponent ();
		}

		public override string MD5Sum ()
		{
			return _actionFeedback.MD5Sum ();
		}

		public override string MessageDefinition ()
		{
			return _actionFeedback.MessageDefinition ();
		}

		public override MsgTypes msgtype ()
		{
			return _actionFeedback.msgtype ();
		}

		public override void Randomize ()
		{
			_actionFeedback.Randomize ();
		}

		public override byte[] Serialize (bool partofsomethingelse)
		{
			return _actionFeedback.Serialize (partofsomethingelse);
		}
	}
}