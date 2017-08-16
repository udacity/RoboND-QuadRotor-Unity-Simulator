/*********************************************************************
* Software License Agreement (BSD License)
*
*  Copyright (c) 2008, Willow Garage, Inc.
*  All rights reserved.
*
*  Redistribution and use in source and binary forms, with or without
*  modification, are permitted provided that the following conditions
*  are met:
*
*   * Redistributions of source code must retain the above copyright
*     notice, this list of conditions and the following disclaimer.
*   * Redistributions in binary form must reproduce the above
*     copyright notice, this list of conditions and the following
*     disclaimer in the documentation and/or other materials provided
*     with the distribution.
*   * Neither the name of the Willow Garage nor the names of its
*     contributors may be used to endorse or promote products derived
*     from this software without specific prior written permission.
*
*  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
*  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
*  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
*  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
*  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
*  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
*  BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
*  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
*  CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
*  LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
*  ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
*  POSSIBILITY OF SUCH DAMAGE.
*********************************************************************/
using System;
//using System.Diagnostics;
using System.Threading;
using Ros_CSharp;
using Messages;
using Messages.std_msgs;
using actionlib;
using Time = Messages.std_msgs.Time;
using TimeData = Messages.TimeData;

public static class DurationExtension
{
	public static double toSec (this Duration d)
	{
		return (double) d.data.sec + 1e-9 * (double) d.data.nsec;
	}

	public static Duration fromSec (this Duration d, double sec)
	{
		uint usec = (uint) sec;
		uint nsec = (uint) ( ( sec - usec ) * 1e9 );
		return new Duration ( new TimeData ( usec, nsec ) );
	}
}

namespace actionlib
{
	/**
	 * \brief A Simple client implementation of the ActionInterface which supports only one goal at a time
	 *
	 * The SimpleActionClient wraps the exisitng ActionClient, and exposes a limited set of easy-to-use hooks
	 * for the user. Note that the concept of GoalHandles has been completely hidden from the user, and that
	 * they must query the SimplyActionClient directly in order to monitor a goal.
	 */
	public class SimpleActionClient<ActionSpec> where ActionSpec : AAction, new ()
	{
		public delegate void SimpleDoneCallback (SimpleClientGoalState state, AResult result);
		public delegate void SimpleActiveCallback ();
		public delegate void SimpleFeedbackCallback (AFeedback feedback);

		protected SimpleActionClient () {}
		/**
		 * \brief Simple constructor
		 *
		 * Constructs a SingleGoalActionClient and sets up the necessary ros topics for the ActionInterface
		 * \param name The action name. Defines the namespace in which the action communicates
		 * \param spin_thread If true, spins up a thread to service this action's subscriptions. If false,
		 *                    then the user has to call ros.spin() themselves. Defaults to True
		 */
		public SimpleActionClient (string name, bool spin_thread = true)
		{
			goalState = new SimpleGoalState ( SimpleGoalState.StateEnum.PENDING );
			initSimpleClient (nodeHandle, name, spin_thread);
		}

		/**
		 * \brief Constructor with namespacing options
		 *
		 * Constructs a SingleGoalActionClient and sets up the necessary ros topics for
		 * the ActionInterface, and namespaces them according the a specified NodeHandle
		 * \param n The node handle on top of which we want to namespace our action
		 * \param name The action name. Defines the namespace in which the action communicates
		 * \param spin_thread If true, spins up a thread to service this action's subscriptions. If false,
		 *                    then the user has to call ros.spin() themselves. Defaults to True
		 */
		public SimpleActionClient (NodeHandle n, string name, bool spin_thread = true)
		{
			goalState = new SimpleGoalState ( SimpleGoalState.StateEnum.PENDING );
			initSimpleClient (n, name, spin_thread);
		}

		~SimpleActionClient()
		{
			if ( spinThread != null )
			{
				{
					lock ( terminateMutex )
					{
						needToTerminate = true;
					}
				}
				spinThread.Join ();
			}
			goalHandle.reset ();
			actionClient = null;
		}

		/**
		 * \brief Waits for the ActionServer to connect to this client
		 *
		 * Often, it can take a second for the action server & client to negotiate
		 * a connection, thus, risking the first few goals to be dropped. This call lets
		 * the user wait until the network connection to the server is negotiated.
		 * NOTE: Using this call in a single threaded ROS application, or any
		 * application where the action client's callback queue is not being
		 * serviced, will not work. Without a separate thread servicing the queue, or
		 * a multi-threaded spinner, there is no way for the client to tell whether
		 * or not the server is up because it can't receive a status message.
		 * \param timeout Max time to block before returning. A zero timeout is interpreted as an infinite timeout.
		 * \return True if the server connected in the allocated time. False on timeout
		 */
		public bool waitForServer (Duration timeout = default (Duration))
		{
			return actionClient.waitForActionServerToStart(timeout);
		}

		/**
		 * @brief  Checks if the action client is successfully connected to the action server
		 * @return True if the server is connected, false otherwise
		 */
		public bool isServerConnected()
		{
			return actionClient.isServerConnected ();
		}

		/**
		 * \brief Sends a goal to the ActionServer, and also registers callbacks
		 *
		 * If a previous goal is already active when this is called. We simply forget
		 * about that goal and start tracking the new goal. No cancel requests are made.
		 * \param done_cb     Callback that gets called on transitions to Done
		 * \param active_cb   Callback that gets called on transitions to Active
		 * \param feedback_cb Callback that gets called whenever feedback for this goal is received
		 */
		public void sendGoal (AGoal goal, SimpleDoneCallback done_cb = null, SimpleActiveCallback active_cb = null, SimpleFeedbackCallback feedback_cb = null)
		{
			// Reset the old GoalHandle, so that our callbacks won't get called anymore
			goalHandle.reset();

			// Store all the callbacks
			doneCallback     = done_cb;
			activeCallback   = active_cb;
			feedbackCallback = feedback_cb;

			goalState = new SimpleGoalState ( SimpleGoalState.StateEnum.PENDING );

			// Send the goal to the ActionServer
			goalHandle = actionClient.sendGoal ( goal, handleTransition, handleFeedback );
		}

		/**
		 * \brief Sends a goal to the ActionServer, and waits until the goal completes or a timeout is exceeded
		 *
		 * If the goal doesn't complete by the execute_timeout, then a preempt message is sent. This call
		 * then waits up to the preempt_timeout for the goal to then finish.
		 *
		 * \param goal             The goal to be sent to the ActionServer
		 * \param execute_timeout  Time to wait until a preempt is sent. 0 implies wait forever
		 * \param preempt_timeout  Time to wait after a preempt is sent. 0 implies wait forever
		 * \return The state of the goal when this call is completed
		 */
		public SimpleClientGoalState sendGoalAndWait (AGoal goal, Duration execute_timeout, Duration preempt_timeout)
		{
			sendGoal ( goal );

			// See if the goal finishes in time
			if (waitForResult(execute_timeout))
			{
				ROS.Debug("actionlib", "Goal finished within specified execute_timeout [%.2f]", execute_timeout.toSec());
				return getState();
			}

			ROS.Debug("actionlib", "Goal didn't finish within specified execute_timeout [%.2f]", execute_timeout.toSec());

			// It didn't finish in time, so we need to preempt it
			cancelGoal();

			// Now wait again and see if it finishes
			if (waitForResult(preempt_timeout))
				ROS.Debug("actionlib", "Preempt finished within specified preempt_timeout [%.2f]", preempt_timeout.toSec());
			else
				ROS.Debug("actionlib", "Preempt didn't finish specified preempt_timeout [%.2f]", preempt_timeout.toSec());
			return getState();
		}

		/**
		 * \brief Get the Result of the current goal
		 * \return shared pointer to the result. Note that this pointer will NEVER be NULL
		 */
		public AResult getResult ()
		{
			if (goalHandle.isExpired())
				ROS.Error("actionlib", "Trying to getResult() when no goal is running. You are incorrectly using SimpleActionClient");

			if ( goalHandle.getResult () != null )
				return goalHandle.getResult ();
			
			return null;
		}

		/**
		 * \brief Get the state information for this goal
		 *
		 * Possible States Are: PENDING, ACTIVE, RECALLED, REJECTED, PREEMPTED, ABORTED, SUCCEEDED, LOST.
		 * \return The goal's state. Returns LOST if this SimpleActionClient isn't tracking a goal.
		 */
		SimpleClientGoalState getState ()
		{
			if (goalHandle.isExpired())
			{
				ROS.Error("actionlib", "Trying to getState() when no goal is running. You are incorrectly using SimpleActionClient");
				return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.LOST );
			}

			CommState commState = goalHandle.getCommState();

			switch( commState.state)
			{
			case CommState.StateEnum.WAITING_FOR_GOAL_ACK:
			case CommState.StateEnum.PENDING:
			case CommState.StateEnum.RECALLING:
				return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.PENDING );
			case CommState.StateEnum.ACTIVE:
			case CommState.StateEnum.PREEMPTING:
				return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.ACTIVE );
			case CommState.StateEnum.DONE:
				{
					switch(goalHandle.getTerminalState().state)
					{
					case TerminalState.StateEnum.RECALLED:
						return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.RECALLED, goalHandle.getTerminalState ().getText () );
					case TerminalState.StateEnum.REJECTED:
						return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.REJECTED, goalHandle.getTerminalState ().getText () );
					case TerminalState.StateEnum.PREEMPTED:
						return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.PREEMPTED, goalHandle.getTerminalState ().getText () );
					case TerminalState.StateEnum.ABORTED:
						return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.ABORTED, goalHandle.getTerminalState ().getText () );
					case TerminalState.StateEnum.SUCCEEDED:
						return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.SUCCEEDED, goalHandle.getTerminalState ().getText () );
					case TerminalState.StateEnum.LOST:
						return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.LOST, goalHandle.getTerminalState ().getText () );
					default:
						ROS.Error ( "actionlib", "Unknown terminal state [%u]. This is a bug in SimpleActionClient", goalHandle.getTerminalState ().state );
						return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.LOST, goalHandle.getTerminalState ().getText () );
					}
				}
			case CommState.StateEnum.WAITING_FOR_RESULT:
			case CommState.StateEnum.WAITING_FOR_CANCEL_ACK:
				switch ( goalState.state )
				{
				case SimpleGoalState.StateEnum.PENDING:
					return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.PENDING );
				case SimpleGoalState.StateEnum.ACTIVE:
					return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.ACTIVE );
				case SimpleGoalState.StateEnum.DONE:
					ROS.Error ( "actionlib", "In WAITING_FOR_RESULT or WAITING_FOR_CANCEL_ACK, yet we are in SimpleGoalState DONE. This is a bug in SimpleActionClient" );
					return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.LOST );
				default:
					ROS.Error ( "actionlib", "Got a SimpleGoalState of [%u]. This is a bug in SimpleActionClient", goalState.state );
					break;
				}
				break;
			default:
				break;
			}
			ROS.Error("actionlib", "Error trying to interpret CommState - %u", commState.state);
			return new SimpleClientGoalState ( SimpleClientGoalState.StateEnum.LOST );
		}

		/**
		 * \brief Cancel all goals currently running on the action server
		 *
		 * This preempts all goals running on the action server at the point that
		 * this message is serviced by the ActionServer.
		 */
		public void cancelAllGoals ()
		{
			
			actionClient.cancelAllGoals();
		}

		/**
		 * \brief Cancel all goals that were stamped at and before the specified time
		 * \param time All goals stamped at or before `time` will be canceled
		 */
		public void cancelGoalsAtAndBeforeTime (Time time)
		{
			actionClient.cancelGoalsAtAndBeforeTime(time);
		}

		/**
		 * \brief Cancel the goal that we are currently pursuing
		 */
		public void cancelGoal()
		{
			if (goalHandle.isExpired())
				ROS.Error("actionlib", "Trying to cancelGoal() when no goal is running. You are incorrectly using SimpleActionClient");

			goalHandle.cancel();
		}

		/**
		 * \brief Stops tracking the state of the current goal. Unregisters this goal's callbacks
		 *
		 * This is useful if we want to make sure we stop calling our callbacks before sending a new goal.
		 * Note that this does not cancel the goal, it simply stops looking for status info about this goal.
		 */
		public void stopTrackingGoal ()
		{
			if (goalHandle.isExpired())
				ROS.Error("actionlib", "Trying to stopTrackingGoal() when no goal is running. You are incorrectly using SimpleActionClient");
			goalHandle = new ClientGoalHandle<ActionSpec> ();
//			goalHandle.reset();
		}

		NodeHandle nodeHandle;
		ClientGoalHandle<ActionSpec> goalHandle;

		SimpleGoalState goalState;

		// Signalling Stuff
		AutoResetEvent doneCondition;
//		object doneCondition;
//		object doneMutex;
		Mutex doneMutex;
//		boost.condition doneCondition;

		// User Callbacks
		SimpleDoneCallback doneCallback;
		SimpleActiveCallback activeCallback;
		SimpleFeedbackCallback feedbackCallback;

		// Spin Thread Stuff
		Mutex terminateMutex;
		bool needToTerminate;
		Thread spinThread;
		Ros_CSharp.CallbackQueue callback_queue;
//		ros.CallbackQueue callback_queue;

		ActionClient<ActionSpec> actionClient; // Action client depends on callback_queue, so it must be destroyed before callback_queue

		// ***** Private Funcs *****
		void initSimpleClient (NodeHandle n, string name, bool spin_thread)
		{
			if (spin_thread)
			{
				ROS.Debug("actionlib", "Spinning up a thread for the SimpleActionClient");
				needToTerminate = false;
				spinThread = new Thread ( DoSpin );
//				spinThread = new boost.thread(boost.bind(&SimpleActionClient<ActionSpec>.spinThread, this));
				actionClient = new ActionClient<ActionSpec> ( n, name, callback_queue );
//				actionClient.reset(new ActionClient<T>(n, name, &callback_queue));
			}
			else
			{
				spinThread = null;
				actionClient = new ActionClient<ActionSpec> ( n, name );
//				actionClient.reset(new ActionClient<T>(n, name));
			}
		}


		void handleTransition(ClientGoalHandle<ActionSpec> gh)
		{
			CommState commState = gh.getCommState ();
			switch (commState.state)
			{
			case CommState.StateEnum.WAITING_FOR_GOAL_ACK:
				ROS.Error("actionlib", "BUG: Shouldn't ever get a transition callback for WAITING_FOR_GOAL_ACK");
				break;
			case CommState.StateEnum.PENDING:
				if ( goalState != SimpleGoalState.StateEnum.PENDING )
					ROS.Error ( "BUG: Got a transition to CommState [%s] when our in SimpleGoalState [%s]",
						commState.toString (), goalState.toString () );
				break;
			case CommState.StateEnum.ACTIVE:
				switch (goalState.state)
				{
				case SimpleGoalState.StateEnum.PENDING:
					setSimpleState ( SimpleGoalState.StateEnum.ACTIVE );
					if ( activeCallback != null )
						activeCallback ();
					break;
				case SimpleGoalState.StateEnum.ACTIVE:
					break;
				case SimpleGoalState.StateEnum.DONE:
					ROS.Error("actionlib", "BUG: Got a transition to CommState [%s] when in SimpleGoalState [%s]",
						commState.ToString(), goalState.ToString());
					break;
				default:
					ROS.Error ( "Unknown SimpleGoalState %u", goalState.state );
					return;
					break;
				}
				break;
			case CommState.StateEnum.WAITING_FOR_RESULT:
				break;
			case CommState.StateEnum.WAITING_FOR_CANCEL_ACK:
				break;
			case CommState.StateEnum.RECALLING:
				if (goalState != SimpleGoalState.StateEnum.PENDING)
					ROS.Error ("BUG: Got a transition to CommState [%s] when our in SimpleGoalState [%s]",
						commState.ToString(), goalState.ToString());
				break;
			case CommState.StateEnum.PREEMPTING:
				switch (goalState.state)
				{
				case SimpleGoalState.StateEnum.PENDING:
					setSimpleState ( SimpleGoalState.StateEnum.ACTIVE );
					if ( activeCallback != null )
						activeCallback ();
					break;
				case SimpleGoalState.StateEnum.ACTIVE:
					break;
				case SimpleGoalState.StateEnum.DONE:
					ROS.Error("actionlib", "BUG: Got a transition to CommState [%s] when in SimpleGoalState [%s]",
						commState.ToString(), goalState.ToString());
					break;
				default:
					ROS.Error ( "Unknown SimpleGoalState %u", goalState.state );
					return;
					break;
				}
				break;
			case CommState.StateEnum.DONE:
				switch (goalState.state)
				{
				case SimpleGoalState.StateEnum.PENDING:
				case SimpleGoalState.StateEnum.ACTIVE:
					lock ( doneMutex )
					{
						setSimpleState ( SimpleGoalState.StateEnum.DONE );
					}

					if ( doneCallback != null )
						doneCallback ( getState (), gh.getResult () );

					doneCondition.Set ();
//					doneCondition.notify_all();
					break;
				case SimpleGoalState.StateEnum.DONE:
					ROS.Error("actionlib", "BUG: Got a second transition to DONE");
					break;
				default:
					ROS.Error("Unknown SimpleGoalState %u", goalState.state);
					return;
					break;
				}
				break;
			default:
				ROS.Error("actionlib", "Unknown CommState received [%u]", commState.state);
				break;
			}
		}


		public void handleFeedback (ClientGoalHandle<ActionSpec> gh, AFeedback feedback)
		{
			if (goalHandle != gh)
				ROS.Error("actionlib", @"Got a callback on a goalHandle that we're not tracking.
					This is an internal SimpleActionClient/ActionClient bug.
					This could also be a GoalID collision");
			if ( feedbackCallback != null )
				feedbackCallback ( feedback );
		}

		/**
		 * \brief Blocks until this goal finishes
		 * \param timeout Max time to block before returning. A zero timeout is interpreted as an infinite timeout.
		 * \return True if the goal finished. False if the goal didn't finish within the allocated timeout
		 */
		public bool waitForResult (Duration timeout)
		{
			if (goalHandle.isExpired())
			{
				ROS.Error("actionlib", "Trying to waitForGoalToFinish() when no goal is running. You are incorrectly using SimpleActionClient");
				return false;
			}

			if (timeout < new Duration (new TimeData(0,0)))
				ROS.Warn("actionlib", "Timeouts can't be negative. Timeout is [%.2fs]", timeout.toSec());

			Time timeout_time = ROS.GetTime () + timeout;
			lock ( doneMutex )
			{
				// Hardcode how often we check for node.ok()
				Duration loop_period = new Duration().fromSec(.1);
				
				while (nodeHandle.ok)
				{
					// Determine how long we should wait
					Duration time_left = timeout_time - ROS.GetTime ();
					
					// Check if we're past the timeout time
					if (timeout > new Duration() && time_left <= new Duration() )
					{
						break;
					}
					
					if (goalState == SimpleGoalState.StateEnum.DONE)
					{
						break;
					}
					
					
					// Truncate the time left
					if (time_left > loop_period || timeout == new Duration())
						time_left = loop_period;
				
			}
//				doneCondition.timed_wait(lock, boost.posix_time.milliseconds(time_left.toSec() * 1000.0f));
			}

			return (goalState == SimpleGoalState.StateEnum.DONE);
		}


		void setSimpleState (SimpleGoalState next_state)
		{
			ROS.Debug("actionlib", "Transitioning SimpleState from [%s] to [%s]",
				goalState.toString(),
				next_state.toString());
			goalState = next_state;
		}



		void setSimpleState (SimpleGoalState.StateEnum next_state)
		{
			setSimpleState ( new SimpleGoalState ( next_state ) );
		}

		void DoSpin ()
		{
			while (nodeHandle.ok)
			{
				{
					lock ( terminateMutex )
					{
						if (needToTerminate)
							break;
					}
				}
				callback_queue.callAvailable(100);
				Thread.Sleep ( 1 );
			}
		}
	}
}