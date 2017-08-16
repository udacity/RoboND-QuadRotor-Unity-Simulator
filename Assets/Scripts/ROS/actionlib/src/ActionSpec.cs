using Messages;
using Messages.std_msgs;
using Messages.actionlib_msgs;

namespace actionlib
{
	public abstract class AGoal : IRosMessage
	{
		public AGoal () : base () {}
		public virtual void SetValue ( string valueName, object value ) {}
		public virtual object GetValue (string valueName) { return null; }
		public abstract AGoal Clone ();
	}
	
	public abstract class AResult : IRosMessage
	{
		public AResult () : base () {}
		public virtual void SetValue ( string valueName, object value ) {}
		public virtual object GetValue (string valueName) { return null; }
		public abstract AResult Clone ();
	}
	
	public abstract class AFeedback : IRosMessage
	{
		public AFeedback () : base () {}
		public virtual void SetValue ( string valueName, object value ) {}
		public virtual object GetValue (string valueName) { return null; }
		public abstract AFeedback Clone ();
	}
	
	public abstract class AActionGoal : IRosMessage
	{
		public AActionGoal () : base () {}
		public virtual Header Header { get; set; }
		public virtual GoalID GoalID { get; set; }
		public virtual AGoal Goal { get; set; }
	}
	
	public abstract class AActionResult : IRosMessage
	{
		public AActionResult () : base () {}
		public abstract AActionResult Clone ();
		public virtual Header Header { get; set; }
		public virtual GoalStatus GoalStatus { get; set; }
		public virtual AResult Result { get; set; }
	}
	
	public abstract class AActionFeedback : IRosMessage
	{
		public AActionFeedback () : base () {}
		public virtual Header Header { get; set; }
		public virtual GoalStatus GoalStatus { get; set; }
		public virtual AFeedback Feedback { get; set; }
	}
	
	public abstract class AAction : IRosMessage
	{
		public virtual AActionGoal ActionGoal { get; }
		public virtual AActionResult ActionResult { get; }
		public virtual AActionFeedback ActionFeedback { get; }
		public abstract AActionGoal NewActionGoal ();

		public AAction () : base () {}
	}
}