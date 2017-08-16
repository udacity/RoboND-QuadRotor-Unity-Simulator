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
using Ros_CSharp;
using Messages;
using Messages.actionlib_msgs;
using actionlib;
using System.Collections.Generic;
using gstat = Messages.actionlib_msgs.GoalStatus;
using gsarray = Messages.actionlib_msgs.GoalStatusArray;

namespace actionlib
{
	public delegate void TransitionCallback<T> (ClientGoalHandle<T> goalHandle) where T : AAction, new();
	public delegate void FeedbackCallback<T> (ClientGoalHandle<T> goalHandle, AFeedback feedback) where T : AAction, new();
	public delegate void SendGoalDelegate (AActionGoal goal);
	public delegate void CancelDelegate (Messages.actionlib_msgs.GoalID goalID);

	/*******************************************************************************
	* GoalManager
	*******************************************************************************/
	public class GoalManager<ActionSpec> where ActionSpec : AAction, new ()
	{
		// should be private
		public ManagedList<CommStateMachine<ActionSpec>> list = new ManagedList<CommStateMachine<ActionSpec>> ();
		public SendGoalDelegate sendGoalDelegate;
		public CancelDelegate cancelDelegate;

		DestructionGuard guard;
		object lockObject = new object ();
		GoalIDGenerator idGenerator;


		GoalManager () {}

		public GoalManager (DestructionGuard guard)
		{
			this.guard = guard;
		}

		public void registerSendGoalDelegate (SendGoalDelegate sendGoalDelegate)
		{
			this.sendGoalDelegate = sendGoalDelegate;
		}

		public void registerCancelFunc (CancelDelegate cancelDelegate)
		{
			this.cancelDelegate = cancelDelegate;
		}

		public ClientGoalHandle<ActionSpec> initGoal<TGoal> (TGoal goal, TransitionCallback<ActionSpec> transitionCallback, FeedbackCallback<ActionSpec> feedbackCallback ) where TGoal : AGoal
		{
			AActionGoal actionGoal = new ActionSpec ().NewActionGoal ();
//			AActionGoal actionGoal = new AActionGoal ();
			actionGoal.Header.Stamp = ROS.GetTime ();
			actionGoal.GoalID = idGenerator.generateID ();
			actionGoal.Goal = goal;
//			actionGoal.header.Stamp = ROS.GetTime ();
//			actionGoal.goal_id = idGenerator.generateID ();
//			actionGoal.goal = goal;

			CommStateMachine<ActionSpec> commStateMachine = new CommStateMachine<ActionSpec> (actionGoal, transitionCallback, feedbackCallback);

			lock ( lockObject )
			{
				var handle = list.Add ( commStateMachine );

				if ( sendGoalDelegate != null )
					sendGoalDelegate ( actionGoal );
				else
					ROS.Warn ("actionlib", "Possible coding error: sendGoalDelegate set to NULL. Not going to send goal");
				
				return new ClientGoalHandle<ActionSpec> ( this, handle, guard );
			}
		}

		public void listElemDeleter (List<CommStateMachine<ActionSpec>>.Enumerator it)
		{
			UnityEngine.Debug.Assert ( guard != null );
			DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( guard );
			if ( !protector.isProtected () )
			{
				ROS.Error ( "actionlib", "This action client associated with the goal handle has already been destructed. Not going to try delete the CommStateMachine associated with this goal" );
				return;
			}

			ROS.Debug ( "actionlib", "About to erase CommStateMachine" );
			lock ( lockObject )
			{
				list.Remove ( it.Current );
			}
			ROS.Debug ( "actionlib", "Done erasing CommStateMachine" );
		}

		public void updateStatuses (gsarray statusArray)
		{
			lock ( lockObject )
			{
				var iter = list.GetIterator ();
				while ( iter.GetElement () != null )
				{
					ClientGoalHandle<ActionSpec> gh = new ClientGoalHandle<ActionSpec> ( this, iter.CreateHandle (), guard );
					iter.GetElement ().updateStatus ( gh, statusArray );
					iter++;
				}

//				for ( int i = 0; i < list.Count; i++ )
//				foreach ( CommStateMachine<ActionSpec> item in list )
//				{
//					CommStateMachine<ActionSpec> item = list [ i ];
//					ClientGoalHandle<ActionSpec> gh = new ClientGoalHandle<ActionSpec> ( this, item.createHandle (), guard );
//					item.updateStatus ( gh, statusArray );
//				}
			}
		}

		public void updateFeedbacks (AActionFeedback actionFeedback)
		{
			lock ( lockObject )
			{
				var iter = list.GetIterator ();
				while ( iter.GetElement () != null )
				{
					ClientGoalHandle<ActionSpec> gh = new ClientGoalHandle<ActionSpec> ( this, iter.CreateHandle (), guard );
					iter.GetElement ().updateFeedback ( gh, actionFeedback );
					iter++;
				}
//				for ( int i = 0; i < list.Count; i++ )
//				foreach ( CommStateMachine<ActionSpec> item in list )
//				{
//					CommStateMachine<ActionSpec> item = list [ i ];
//					ClientGoalHandle<ActionSpec> gh = new ClientGoalHandle<ActionSpec> ( this, item.createHandle (), guard );
//					item.updateFeedback ( gh, actionFeedback );
//				}
			}
		}

		public void updateResults (AActionResult actionResult)
		{
			lock ( lockObject )
			{
				var iter = list.GetIterator ();
				while ( iter.GetElement () != null )
				{
					ClientGoalHandle<ActionSpec> gh = new ClientGoalHandle<ActionSpec> ( this, iter.CreateHandle (), guard );
					iter.GetElement ().updateResult ( gh, actionResult );
					iter++;
				}
//				for ( int i = 0; i < list.Count; i++ )
//				foreach ( CommStateMachine<ActionSpec> item in list )
//				{
//					CommStateMachine<ActionSpec> item = list [ i ];
//					ClientGoalHandle<ActionSpec> gh = new ClientGoalHandle<ActionSpec> ( this, item.createHandle (), guard );
//					item.updateResult ( gh, actionResult );
//				}
			}
		}

//		friend class ClientGoalHandle<T>;
	}

	/**
	* \brief Client side handle to monitor goal progress
	*
	* A ClientGoalHandle is a reference counted object that is used to manipulate and monitor the progress
	* of an already dispatched goal. Once all the goal handles go out of scope (or are reset), an
	* ActionClient stops maintaining state for that goal.
	*/
	/*******************************************************************************
	* ClientGoalHandle
	*******************************************************************************/
	public class ClientGoalHandle<ActionSpec> where ActionSpec : AAction, new ()
	{
		object lockObject = new object ();
		GoalManager<ActionSpec> goalManager;
		bool isActive;
		DestructionGuard guard = new DestructionGuard ();
		ManagedList<CommStateMachine<ActionSpec>>.Handle listHandle;

		//typename ManagedListT::iterator it_;
//		boost::shared_ptr<DestructionGuard> guard;   // Guard must still exist when the listHandle is destroyed
//		typename ManagedListT::Handle listHandle;


		/**
		* \brief Create an empty goal handle
		*
		* Constructs a goal handle that doesn't track any goal. Calling any method on an empty goal
		* handle other than operator= will trigger an assertion.
		*/
		public ClientGoalHandle ()
		{
			goalManager = null;
			isActive = false;
		}

		public ClientGoalHandle (GoalManager<ActionSpec> gm, ManagedList<CommStateMachine<ActionSpec>>.Handle handle, DestructionGuard guard)
		{
			this.goalManager = gm;
			this.isActive = true;
			this.listHandle = handle;
			this.guard = guard;
		}

		~ClientGoalHandle ()
		{
			reset ();
		}


		/**
		* \brief Stops goal handle from tracking a goal
		*
		* Useful if you want to stop tracking the progress of a goal, but it is inconvenient to force
		* the goal handle to go out of scope. Has pretty much the same semantics as boost::shared_ptr::reset()
		*/
		public void reset ()
		{
			if (isActive)
			{
				DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( guard );
				if ( !protector.isProtected () )
				{
					ROS.Error ( "actionlib", "This action client associated with the goal handle has already been destructed. Ignoring this reset() call" );
					return;
				}

				lock ( lockObject )
				{
//					listHandle.reset(); // not sure what to replace this stuff with
					isActive = false;
					goalManager = null;
				}
			}
		}

		/**
		* \brief Checks if this goal handle is tracking a goal
		*
		* Has pretty much the same semantics as boost::shared_ptr::expired()
		* \return True if this goal handle is not tracking a goal
		*/
		public bool isExpired ()
		{
			return !isActive;
		}

		/**
		* \brief Get the state of this goal's communication state machine from interaction with the server
		*
		* Possible States are: WAITING_FOR_GOAL_ACK, PENDING, ACTIVE, WAITING_FOR_RESULT,
		*                      WAITING_FOR_CANCEL_ACK, RECALLING, PREEMPTING, DONE
		* \return The current goal's communication state with the server
		*/
		public CommState getCommState ()
		{
			if (!isActive)
			{
				ROS.Error ( "actionlib", "Trying to getCommState on an inactive ClientGoalHandle. You are incorrectly using a ClientGoalHandle" );
				return new CommState ( CommState.StateEnum.DONE );
			}

			DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( guard );
			if ( !protector.isProtected () )
			{
				ROS.Error ( "actionlib", "This action client associated with the goal handle has already been destructed. Ignoring this getCommState() call" );
				return new CommState ( CommState.StateEnum.DONE );
			}

			UnityEngine.Debug.Assert ( goalManager != null );

			lock ( lockObject )
			{
				return listHandle.GetElement ().getCommState ();
//				return listHandle.getElem()->getCommState(); // ??
			}
		}

		/**
		* \brief Get the terminal state information for this goal
		*
		* Possible States Are: RECALLED, REJECTED, PREEMPTED, ABORTED, SUCCEEDED, LOST
		* This call only makes sense if CommState==DONE. This will send ROS_WARNs if we're not in DONE
		* \return The terminal state
		*/
		public TerminalState getTerminalState ()
		{
			if ( !isActive )
			{
				ROS.Error ( "actionlib", "Trying to getTerminalState on an inactive ClientGoalHandle. You are incorrectly using a ClientGoalHandle" );
				return new TerminalState ( TerminalState.StateEnum.LOST );
			}

			DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( guard );
			if ( !protector.isProtected () )
			{
				ROS.Error ( "actionlib", "This action client associated with the goal handle has already been destructed. Ignoring this getTerminalState() call" );
				return new TerminalState ( TerminalState.StateEnum.LOST );
			}

			UnityEngine.Debug.Assert ( goalManager != null );
			lock ( lockObject )
			{
				CommState comm_state = listHandle.GetElement ().getCommState ();
//				CommState comm_state = listHandle.getElem()->getCommState();
				if ( comm_state != CommState.StateEnum.DONE )
					ROS.Warn ( "actionlib", "Asking for the terminal state when we're in [%s]", comm_state.toString () );
				
				Messages.actionlib_msgs.GoalStatus goal_status = listHandle.GetElement ().getGoalStatus ();
//				Messages.actionlib_msgs.GoalStatus goal_status = listHandle.getElem()->getGoalStatus();

				switch ( goal_status.status )
				{
				case gstat.PENDING:
				case gstat.ACTIVE:
				case gstat.PREEMPTING:
				case gstat.RECALLING:
					ROS.Error ( "actionlib", "Asking for terminal state, but latest goal status is %u", goal_status.status );
					return new TerminalState ( TerminalState.StateEnum.LOST, goal_status.text );
				case gstat.PREEMPTED:
					return new TerminalState ( TerminalState.StateEnum.PREEMPTED, goal_status.text );
				case gstat.SUCCEEDED:
					return new TerminalState ( TerminalState.StateEnum.SUCCEEDED, goal_status.text );
				case gstat.ABORTED:
					return new TerminalState ( TerminalState.StateEnum.ABORTED, goal_status.text );
				case gstat.REJECTED:
					return new TerminalState ( TerminalState.StateEnum.REJECTED, goal_status.text );
				case gstat.RECALLED:
					return new TerminalState ( TerminalState.StateEnum.RECALLED, goal_status.text );
				case gstat.LOST:
					return new TerminalState ( TerminalState.StateEnum.LOST, goal_status.text );
				default:
					ROS.Error ( "actionlib", "Unknown goal status: %u", goal_status.status );
					break;
				}
				
				ROS.Error("actionlib", "Bug in determining terminal state");
				return new TerminalState ( TerminalState.StateEnum.LOST, goal_status.text);
			}
		}

		/**
		* \brief Get result associated with this goal
		*
		* \return NULL if no reseult received.  Otherwise returns shared_ptr to result.
		*/
		public AResult getResult ()
//		public AActionResult getResult ()
		{
			if ( !isActive )
				ROS.Error ( "actionlib", "Trying to getResult on an inactive ClientGoalHandle. You are incorrectly using a ClientGoalHandle" );
			UnityEngine.Debug.Assert ( goalManager != null );

			DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( guard );
			if ( !protector.isProtected () )
			{
				ROS.Error ( "actionlib", "This action client associated with the goal handle has already been destructed. Ignoring this getResult() call" );
				return null;
//				return typename ClientGoalHandle<ActionSpec>::ResultConstPtr() ;
			}

			lock ( lockObject )
			{
				return listHandle.GetElement ().getResult ();
//				return listHandle.GetElement ().getResult<TResult> ();
			}
		}

		/**
		* \brief Resends this goal [with the same GoalID] to the ActionServer
		*
		* Useful if the user thinks that the goal may have gotten lost in transit
		*/
		public void resend ()
		{
			if ( !isActive )
				ROS.Error ( "actionlib", "Trying to resend() on an inactive ClientGoalHandle. You are incorrectly using a ClientGoalHandle" );

			DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( guard );
			if ( !protector.isProtected () )
			{
				ROS.Error ( "actionlib", "This action client associated with the goal handle has already been destructed. Ignoring this resend() call" );
				return;
			}

			UnityEngine.Debug.Assert ( goalManager != null );
			AActionGoal actionGoal = null;
			lock ( lockObject )
			{
				actionGoal = listHandle.GetElement ().getActionGoal ();
//				actionGoal = listHandle.getElem()->getActionGoal();
			}

			if ( actionGoal == null )
				ROS.Error ( "actionlib", "BUG: Got a NULL actionGoal" );

			if ( goalManager.sendGoalDelegate != null )
				goalManager.sendGoalDelegate  ( actionGoal );
		}

		/**
		* \brief Sends a cancel message for this specific goal to the ActionServer
		*
		* Also transitions the Communication State Machine to WAITING_FOR_CANCEL_ACK
		*/
		public void cancel ()
		{
			if ( !isActive )
			{
				ROS.Error ( "actionlib", "Trying to cancel() on an inactive ClientGoalHandle. You are incorrectly using a ClientGoalHandle" );
				return;
			}

			UnityEngine.Debug.Assert ( goalManager != null );

			DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( guard );
			if ( !protector.isProtected () )
			{
				ROS.Error ( "actionlib", "This action client associated with the goal handle has already been destructed. Ignoring this call" );
				return;
			}

			lock ( lockObject )
			{
				switch ( listHandle.GetElement ().getCommState ().state )
				{
				case CommState.StateEnum.WAITING_FOR_GOAL_ACK:
				case CommState.StateEnum.PENDING:
				case CommState.StateEnum.ACTIVE:
				case CommState.StateEnum.WAITING_FOR_CANCEL_ACK:
					break; // Continue standard processing
				case CommState.StateEnum.WAITING_FOR_RESULT:
				case CommState.StateEnum.RECALLING:
				case CommState.StateEnum.PREEMPTING:
				case CommState.StateEnum.DONE:
					ROS.Debug ( "actionlib", "Got a cancel() request while in state [%s], so ignoring it", listHandle.GetElement ().getCommState ().toString () );
					return;
				default:
					ROS.Error ( "actionlib", "BUG: Unhandled CommState: %u", listHandle.GetElement ().getCommState ().state );
					return;
				}
			}

			AActionGoal actionGoal = listHandle.GetElement ().getActionGoal ();

			Messages.actionlib_msgs.GoalID cancelMsg = new Messages.actionlib_msgs.GoalID ();
			cancelMsg.stamp = ROS.GetTime ();
			cancelMsg.id = listHandle.GetElement ().getActionGoal ().GoalID.id;
//			cancelMsg.id = listHandle.GetElement ().getActionGoal ()->goal_id.id;

			if ( goalManager.cancelDelegate != null )
				goalManager.cancelDelegate ( cancelMsg );

			listHandle.GetElement ().transitionToState ( this, CommState.StateEnum.WAITING_FOR_CANCEL_ACK );
		}

		/**
		* \brief Check if two goal handles point to the same goal
		* \return TRUE if both point to the same goal. Also returns TRUE if both handles are inactive.
		*/
		public static bool operator == (ClientGoalHandle<ActionSpec> lhs, ClientGoalHandle<ActionSpec> rhs)
		{
			if ( object.ReferenceEquals ( lhs, rhs ) )
				return true;
			if ( !( lhs.isActive && rhs.isActive ) )
				return false;

			DestructionGuard.ScopedProtector protector = new DestructionGuard.ScopedProtector ( lhs.guard );
			if ( !protector.isProtected () )
			{
				ROS.Error("actionlib", "This action client associated with the goal handle has already been destructed. Ignoring this operator==() call");
				return false;
			}

			return lhs.listHandle == rhs.listHandle;
		}

		/**
		* \brief !(operator==())
		*/
		public static bool operator != (ClientGoalHandle<ActionSpec> lhs, ClientGoalHandle<ActionSpec> rhs)
		{
			return !( lhs == rhs );
		}

//		friend class GoalManager<T>;
//		typedef ManagedList< boost::shared_ptr<CommStateMachine<T> > > ManagedListT;
	}


	/*******************************************************************************
	* CommStateMachine
	*******************************************************************************/
	public class CommStateMachine<ActionSpec> where ActionSpec : AAction, new ()
	{
		// State
		CommState state;
		AActionGoal actionGoal;
		Messages.actionlib_msgs.GoalStatus latest_goal_status_;
		AActionResult latestResult;

		// Callbacks
		TransitionCallback<ActionSpec> transitionCallback;
		FeedbackCallback<ActionSpec> feedbackCallback;

		CommStateMachine () {}
		public CommStateMachine (AActionGoal actionGoal, TransitionCallback<ActionSpec> transitionCallback, FeedbackCallback<ActionSpec> feedbackCallback )
		{
			UnityEngine.Debug.Assert ( actionGoal != null );
			this.actionGoal = actionGoal;
			this.transitionCallback = transitionCallback;
			this.feedbackCallback = feedbackCallback;
			state = CommState.StateEnum.WAITING_FOR_GOAL_ACK;
		}

		public AActionGoal getActionGoal ()
		{
			return actionGoal;
		}

		public CommState getCommState ()
		{
			return state;
		}

		public GoalStatus getGoalStatus ()
		{
			return latest_goal_status_;
		}


		public AResult getResult ()
		{
			AResult result = null;
			if ( latestResult != null )
			{
				result = latestResult.Result.Clone ();// new TResult ( latestResult );
//				EnclosureDeleter<const ActionResult> d(latestResult);
//				result = ResultConstPtr(&(latestResult->result), d);
			}
			return result;
		}

		// Transitions caused by messages
		public void updateStatus (ClientGoalHandle<ActionSpec> gh, gsarray statusArray)
		{
			gstat goal_status = findGoalStatus ( statusArray.status_list );

			// It's possible to receive old GoalStatus messages over the wire, even after receiving Result with a terminal state.
			//   Thus, we want to ignore all status that we get after we're done, because it is irrelevant. (See trac #2721)
			if (state == CommState.StateEnum.DONE)
				return;

			if ( goal_status != null )
				latest_goal_status_ = goal_status;
			else
			{
				if (state != CommState.StateEnum.WAITING_FOR_GOAL_ACK &&
					state != CommState.StateEnum.WAITING_FOR_RESULT &&
					state != CommState.StateEnum.DONE)
				{
					processLost ( gh );
				}
				return;
			}

			switch ( state.state )
			{
			case CommState.StateEnum.WAITING_FOR_GOAL_ACK:
				{
					if ( goal_status != null )
					{
						switch ( goal_status.status )
						{
						case gstat.PENDING:
							transitionToState ( gh, CommState.StateEnum.PENDING );
							break;
						case gstat.ACTIVE:
							transitionToState ( gh, CommState.StateEnum.ACTIVE );
							break;
						case gstat.PREEMPTED:
							transitionToState ( gh, CommState.StateEnum.ACTIVE );
							transitionToState ( gh, CommState.StateEnum.PREEMPTING );
							transitionToState ( gh, CommState.StateEnum.WAITING_FOR_RESULT );
							break;
						case gstat.SUCCEEDED:
							transitionToState ( gh, CommState.StateEnum.ACTIVE );
							transitionToState ( gh, CommState.StateEnum.WAITING_FOR_RESULT );
							break;
						case gstat.ABORTED:
							transitionToState ( gh, CommState.StateEnum.ACTIVE );
							transitionToState ( gh, CommState.StateEnum.WAITING_FOR_RESULT );
							break;
						case gstat.REJECTED:
							transitionToState ( gh, CommState.StateEnum.PENDING );
							transitionToState ( gh, CommState.StateEnum.WAITING_FOR_RESULT );
							break;
						case gstat.RECALLED:
							transitionToState ( gh, CommState.StateEnum.PENDING );
							transitionToState ( gh, CommState.StateEnum.WAITING_FOR_RESULT );
							break;
						case gstat.PREEMPTING:
							transitionToState ( gh, CommState.StateEnum.ACTIVE );
							transitionToState ( gh, CommState.StateEnum.PREEMPTING );
							break;
						case gstat.RECALLING:
							transitionToState ( gh, CommState.StateEnum.PENDING );
							transitionToState ( gh, CommState.StateEnum.RECALLING );
							break;
						default:
							ROS.Error ( "actionlib", "BUG: Got an unknown status from the ActionServer. status = %u",  goal_status.status  );
							break;
						}
					}
					break;
				}
			case CommState.StateEnum.PENDING:
				{
					switch ( goal_status.status )
					{
					case gstat.PENDING:
						break;
					case gstat.ACTIVE:
						transitionToState(gh, CommState.StateEnum.ACTIVE);
						break;
					case gstat.PREEMPTED:
						transitionToState(gh, CommState.StateEnum.ACTIVE);
						transitionToState(gh, CommState.StateEnum.PREEMPTING);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.SUCCEEDED:
						transitionToState(gh, CommState.StateEnum.ACTIVE);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.ABORTED:
						transitionToState(gh, CommState.StateEnum.ACTIVE);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.REJECTED:
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.RECALLED:
						transitionToState(gh, CommState.StateEnum.RECALLING);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.PREEMPTING:
						transitionToState(gh, CommState.StateEnum.ACTIVE);
						transitionToState(gh, CommState.StateEnum.PREEMPTING);
						break;
					case gstat.RECALLING:
						transitionToState(gh, CommState.StateEnum.RECALLING);
						break;
					default:
						ROS.Error("actionlib", "BUG: Got an unknown goal status from the ActionServer. status = %u",  goal_status.status );
						break;
					}
					break;
				}
			case CommState.StateEnum.ACTIVE:
				{
					switch ( goal_status.status )
					{
					case gstat.PENDING:
						ROS.Error("actionlib", "Invalid transition from ACTIVE to PENDING"); break;
					case gstat.ACTIVE:
						break;
					case gstat.REJECTED:
						ROS.Error("actionlib", "Invalid transition from ACTIVE to REJECTED"); break;
					case gstat.RECALLING:
						ROS.Error("actionlib", "Invalid transition from ACTIVE to RECALLING"); break;
					case gstat.RECALLED:
						ROS.Error("actionlib", "Invalid transition from ACTIVE to RECALLED"); break;
					case gstat.PREEMPTED:
						transitionToState(gh, CommState.StateEnum.PREEMPTING);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.SUCCEEDED:
					case gstat.ABORTED:
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT); break;
					case gstat.PREEMPTING:
						transitionToState(gh, CommState.StateEnum.PREEMPTING); break;
					default:
						ROS.Error("actionlib", "BUG: Got an unknown goal status from the ActionServer. status = %u",  goal_status.status );
						break;
					}
					break;
				}
			case CommState.StateEnum.WAITING_FOR_RESULT:
				{
					switch ( goal_status.status )
					{
					case gstat.PENDING :
						ROS.Error("actionlib", "Invalid Transition from WAITING_FOR_RESUT to PENDING"); break;
					case gstat.PREEMPTING:
						ROS.Error("actionlib", "Invalid Transition from WAITING_FOR_RESUT to PREEMPTING"); break;
					case gstat.RECALLING:
						ROS.Error("actionlib", "Invalid Transition from WAITING_FOR_RESUT to RECALLING"); break;
					case gstat.ACTIVE:
					case gstat.PREEMPTED:
					case gstat.SUCCEEDED:
					case gstat.ABORTED:
					case gstat.REJECTED:
					case gstat.RECALLED:
						break;
					default:
						ROS.Error("actionlib", "BUG: Got an unknown state from the ActionServer. status = %u",  goal_status.status );
						break;
					}
					break;
				}
			case CommState.StateEnum.WAITING_FOR_CANCEL_ACK:
				{
					switch ( goal_status.status )
					{
					case gstat.PENDING:
						break;
					case gstat.ACTIVE:
						break;
					case gstat.SUCCEEDED:
					case gstat.ABORTED:
					case gstat.PREEMPTED:
						transitionToState(gh, CommState.StateEnum.PREEMPTING);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.RECALLED:
						transitionToState(gh, CommState.StateEnum.RECALLING);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.REJECTED:
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT); break;
					case gstat.PREEMPTING:
						transitionToState(gh, CommState.StateEnum.PREEMPTING); break;
					case gstat.RECALLING:
						transitionToState(gh, CommState.StateEnum.RECALLING); break;
					default:
						ROS.Error("actionlib", "BUG: Got an unknown state from the ActionServer. status = %u",  goal_status.status );
						break;
					}
					break;
				}
			case CommState.StateEnum.RECALLING:
				{
					switch ( goal_status.status )
					{
					case gstat.PENDING:
						ROS.Error("actionlib", "Invalid Transition from RECALLING to PENDING"); break;
					case gstat.ACTIVE:
						ROS.Error("actionlib", "Invalid Transition from RECALLING to ACTIVE"); break;
					case gstat.SUCCEEDED:
					case gstat.ABORTED:
					case gstat.PREEMPTED:
						transitionToState(gh, CommState.StateEnum.PREEMPTING);
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.RECALLED:
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT);
						break;
					case gstat.REJECTED:
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT); break;
					case gstat.PREEMPTING:
						transitionToState(gh, CommState.StateEnum.PREEMPTING); break;
					case gstat.RECALLING:
						break;
					default:
						ROS.Error("actionlib", "BUG: Got an unknown state from the ActionServer. status = %u",  goal_status.status );
						break;
					}
					break;
				}
			case CommState.StateEnum.PREEMPTING:
				{
					switch ( goal_status.status )
					{
					case gstat.PENDING:
						ROS.Error("actionlib", "Invalid Transition from PREEMPTING to PENDING"); break;
					case gstat.ACTIVE:
						ROS.Error("actionlib", "Invalid Transition from PREEMPTING to ACTIVE"); break;
					case gstat.REJECTED:
						ROS.Error("actionlib", "Invalid Transition from PREEMPTING to REJECTED"); break;
					case gstat.RECALLING:
						ROS.Error("actionlib", "Invalid Transition from PREEMPTING to RECALLING"); break;
					case gstat.RECALLED:
						ROS.Error("actionlib", "Invalid Transition from PREEMPTING to RECALLED"); break;
						break;
					case gstat.PREEMPTED:
					case gstat.SUCCEEDED:
					case gstat.ABORTED:
						transitionToState(gh, CommState.StateEnum.WAITING_FOR_RESULT); break;
					case gstat.PREEMPTING:
						break;
					default:
						ROS.Error("actionlib", "BUG: Got an unknown state from the ActionServer. status = %u",  goal_status.status );
						break;
					}
					break;
				}
			case CommState.StateEnum.DONE:
				{
					switch ( goal_status.status )
					{
					case gstat.PENDING:
						ROS.Error("actionlib", "Invalid Transition from DONE to PENDING"); break;
					case gstat.ACTIVE:
						ROS.Error("actionlib", "Invalid Transition from DONE to ACTIVE"); break;
					case gstat.RECALLING:
						ROS.Error("actionlib", "Invalid Transition from DONE to RECALLING"); break;
					case gstat.PREEMPTING:
						ROS.Error("actionlib", "Invalid Transition from DONE to PREEMPTING"); break;
					case gstat.PREEMPTED:
					case gstat.SUCCEEDED:
					case gstat.ABORTED:
					case gstat.RECALLED:
					case gstat.REJECTED:
						break;
					default:
						ROS.Error("actionlib", "BUG: Got an unknown state from the ActionServer. status = %u",  goal_status.status );
						break;
					}
					break;
				}
			default:
				ROS.Error("actionlib", "In a funny comm state: %u", state.state);
				break;
			}
		}


		public void updateFeedback (ClientGoalHandle<ActionSpec> gh, AActionFeedback actionFeedback)
		{
			if ( actionGoal.GoalID.id != actionFeedback.GoalStatus.goal_id.id )
				return;

			if ( feedbackCallback != null )
			{
				AFeedback feedback = actionFeedback.Feedback.Clone ();
				feedbackCallback ( gh, feedback );
			}
		}

/*		public void updateFeedback<TFeedback> (ClientGoalHandle<ActionSpec> gh, TFeedback actionFeedback) where TFeedback : AActionFeedback, new()
		{
			// Check if this feedback is for us
			if ( actionGoal.GoalID.id != actionFeedback.GoalStatus.goal_id.id )
//			if ( actionGoal.goal_id.id != actionFeedback.status.goal_id.id )
				return;

			if ( feedbackCallback != null )
			{
//				EnclosureDeleter<const ActionFeedback> d(actionFeedback);
//				FeedbackConstPtr feedback(&(actionFeedback->feedback), d);
				AFeedback feedback = actionFeedback.Feedback.Clone ();
				feedbackCallback ( gh, feedback );
			}
		}*/

		public void updateResult (ClientGoalHandle<ActionSpec> gh, AActionResult actionResult)
		{
			// Check if this feedback is for us
			if ( actionGoal.GoalID.id != actionResult.GoalStatus.goal_id.id )
//			if ( actionGoal.goal_id.id != actionResult.status.goal_id.id )
				return;
			latest_goal_status_ = actionResult.GoalStatus;
//			latest_goal_status_ = actionResult.status;
			latestResult = actionResult;
			switch ( state.state )
			{
			case CommState.StateEnum.WAITING_FOR_GOAL_ACK:
			case CommState.StateEnum.PENDING:
			case CommState.StateEnum.ACTIVE:
			case CommState.StateEnum.WAITING_FOR_RESULT:
			case CommState.StateEnum.WAITING_FOR_CANCEL_ACK:
			case CommState.StateEnum.RECALLING:
			case CommState.StateEnum.PREEMPTING:
				{
					// A little bit of hackery to call all the right state transitions before processing result
					gsarray statusArray = new gsarray ();
					List<gstat> list = new List<gstat> ( statusArray.status_list );
					list.Add ( actionResult.GoalStatus );
//					list.Add ( actionResult.status );
					statusArray.status_list = list.ToArray ();
					updateStatus ( gh, statusArray );

					transitionToState ( gh, CommState.StateEnum.DONE );
					break;
				}
			case CommState.StateEnum.DONE:
				ROS.Error ( "actionlib", "Got a result when we were already in the DONE state" );
				break;
			default:
				ROS.Error ( "actionlib", "In a funny comm state: %u", state.state );
				break;
			}
		}

		// Forced transitions
		public void transitionToState (ClientGoalHandle<ActionSpec> gh, CommState.StateEnum next_state)
		{
			transitionToState ( gh, new CommState ( next_state ) );
		}

		public void transitionToState (ClientGoalHandle<ActionSpec> gh, CommState next_state)
		{
			ROS.Debug ( "actionlib", "Trying to transition to %s", next_state.toString () );
			setCommState ( next_state );
			if ( transitionCallback != null )
				transitionCallback ( gh );
		}

		public void processLost (ClientGoalHandle<ActionSpec> gh)
		{
			ROS.Warn ( "actionlib", "Transitioning goal to LOST" );
			latest_goal_status_.status = gstat.LOST;
			transitionToState ( gh, CommState.StateEnum.DONE );
		}


		//! Change the state, as well as print out ROS_DEBUG info
		void setCommState (CommState.StateEnum state)
		{
			setCommState ( new CommState ( state ) );
		}

		void setCommState (CommState state)
		{
			ROS.Debug ( "actionlib", "Transitioning CommState from %s to %s", state.toString (), state.toString () );
			state = state;
		}

		gstat findGoalStatus (gstat[] statusList)
		{
			for (int i = 0; i < statusList.Length; i++)
				if ( statusList[i].goal_id.id == actionGoal.GoalID.id )
//				if ( statusList[i].goal_id.id == actionGoal.goal_id.id )
					return statusList[i];
			return null;
		}
	}
}