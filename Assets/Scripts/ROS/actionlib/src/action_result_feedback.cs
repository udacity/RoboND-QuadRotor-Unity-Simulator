using System;
namespace actionlib
{
	public class Action<T>
	{
		public class ActionGoal // (_action_goal_type)
		{
			public T Goal { get { return goal; } }
			protected T goal;
		}

		public class ActionResult // (_action_result_type)
		{
			public T Result { get { return result; } }
			protected T result;
		}

		public class ActionFeedback // (_action_feedback_type)
		{
			public T Feedback { get { return feedback; } }
			protected T feedback;
		}
	}
}