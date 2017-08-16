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
using System.Threading;
using Ros_CSharp;
using actionlib;
using Messages;
using Messages.std_msgs;
using Messages.actionlib_msgs;
using goalid = Messages.actionlib_msgs.GoalID;
using duration = Messages.std_msgs.Duration;
//using gstat = Messages.actionlib_msgs.GoalStatus;
using gsa = Messages.actionlib_msgs.GoalStatusArray;

namespace actionlib
{
	/**
	 * \brief Full interface to an ActionServer
	 *
	 * ActionClient provides a complete client side implementation of the ActionInterface protocol.
	 * It provides callbacks for every client side transition, giving the user full observation into
	 * the client side state machine.
	*/

	public class ActionClient<ActionSpec> where ActionSpec : AAction, new ()
	{
		public delegate void MessageEventDelegate<M> (MessageEvent<M> ev) where M : IRosMessage;
		public delegate void TransitionCallback (ClientGoalHandle<ActionSpec> goalHandle);
		public delegate void FeedbackCallback (ClientGoalHandle<ActionSpec> goalHandle);
		public delegate void SendGoalDelegate (AActionGoal goal);
		public delegate void CancelDelegate (GoalID goalID);


		NodeHandle nodeHandle;
		DestructionGuard guard_;
		GoalManager<ActionSpec> goalManager;


		Subscriber<MessageEvent<ActionResultDecorator>> resultSubscriber;
		Subscriber<MessageEvent<ActionFeedbackDecorator>> feedbackSubscriber;
		Subscriber<MessageEvent<gsa>> statusSubscriber;

		Publisher<ActionGoalDecorator> goalPublisher;
//		Publisher<AActionGoal> goalPublisher;
		Publisher<goalid> cancelPublisher;
		ConnectionMonitor connectionMonitor;   // Have to destroy subscribers and publishers before the connectionMonitor, since we call callbacks in the connectionMonitor


		/**
		* \brief Simple constructor
		*
		* Constructs an ActionClient and sets up the necessary ros topics for the ActionInterface
		* \param name The action name. Defines the namespace in which the action communicates
		* \param queue CallbackQueue from which this action will process messages.
		*              The default (NULL) is to use the global queue
		*/
		public ActionClient (string name, CallbackQueueInterface queue = null)
		{
			nodeHandle = new NodeHandle ( name );
			guard_ = new DestructionGuard ();
			goalManager = new GoalManager<ActionSpec> ( guard_ );
			initClient ( queue );
		}

		/**
		* \brief Constructor with namespacing options
		*
		* Constructs an ActionClient and sets up the necessary ros topics for the ActionInterface,
		* and namespaces them according the a specified NodeHandle
		* \param n The node handle on top of which we want to namespace our action
		* \param name The action name. Defines the namespace in which the action communicates
		* \param queue CallbackQueue from which this action will process messages.
		*              The default (NULL) is to use the global queue
		*/
		public ActionClient (NodeHandle nh, string name, CallbackQueueInterface queue = null)
		{
			nodeHandle = new NodeHandle ( nh, name );
			guard_ = new DestructionGuard ();
			goalManager = new GoalManager<ActionSpec> ( guard_ );
			initClient ( queue );
		}

		~ActionClient()
		{
			ROS.Debug ( "actionlib", "ActionClient: Waiting for destruction guard to clean up" );
			guard_.destruct ();
			ROS.Debug ( "actionlib", "ActionClient: destruction guard destruct() done" );
		}


		/**
		* \brief Sends a goal to the ActionServer, and also registers callbacks
		* \param transitionCallback Callback that gets called on every client state transition
		* \param feedbackCallback Callback that gets called whenever feedback for this goal is received
		*/
		public ClientGoalHandle<ActionSpec> sendGoal<TGoal> (TGoal goal, TransitionCallback<ActionSpec> transitionCallback, FeedbackCallback<ActionSpec> feedbackCallback ) where TGoal : AGoal
		{
			ROS.Debug ( "actionlib", "about to start initGoal()" );
			ClientGoalHandle<ActionSpec> gh = goalManager.initGoal ( goal, transitionCallback, feedbackCallback );
			ROS.Debug ( "actionlib", "Done with initGoal()" );

			return gh;
		}

		/**
		* \brief Cancel all goals currently running on the action server
		*
		* This preempts all goals running on the action server at the point that
		* this message is serviced by the ActionServer.
		*/
		public void cancelAllGoals ()
		{
			goalid cancel_msg = new goalid ();
			// CancelAll policy encoded by stamp=0, id=0
			cancel_msg.stamp = new Time ();
			cancel_msg.id = "";
			cancelPublisher.publish ( cancel_msg );
		}

		/**
		* \brief Cancel all goals that were stamped at and before the specified time
		* \param time All goals stamped at or before `time` will be canceled
		*/
		public void cancelGoalsAtAndBeforeTime (Time time)
		{
			goalid cancel_msg = new goalid ();
			cancel_msg.stamp = time;
			cancel_msg.id = "";
			cancelPublisher.publish ( cancel_msg );
		}

		/**
		* \brief Waits for the ActionServer to connect to this client
		* Often, it can take a second for the action server & client to negotiate
		* a connection, thus, risking the first few goals to be dropped. This call lets
		* the user wait until the network connection to the server is negotiated
		* NOTE: Using this call in a single threaded ROS application, or any
		* application where the action client's callback queue is not being
		* serviced, will not work. Without a separate thread servicing the queue, or
		* a multi-threaded spinner, there is no way for the client to tell whether
		* or not the server is up because it can't receive a status message.
		* \param timeout Max time to block before returning. A zero timeout is interpreted as an infinite timeout.
		* \return True if the server connected in the allocated time. False on timeout
		*/
		public bool waitForActionServerToStart (duration timeout = default (duration))
		{
			int sleepTime = (int) ( timeout.data.sec * 1000 + timeout.data.nsec / 1000000 );
			Thread.Sleep ( sleepTime );
			// if ros::Time::isSimTime(), then wait for it to become valid
//			if(!ros::Time::waitForValid(ros::WallDuration(timeout.sec, timeout.nsec)))
//				return false;

			if ( connectionMonitor != null)
				return connectionMonitor.waitForActionServerToStart ( timeout, nodeHandle );
			else
				return false;
		}

		/**
		* @brief  Checks if the action client is successfully connected to the action server
		* @return True if the server is connected, false otherwise
		*/
		public bool isServerConnected ()
		{
			return connectionMonitor.isServerConnected ();
		}

		void SendGoalFunc (AActionGoal actionGoal)
		{
			goalPublisher.publish ( new ActionGoalDecorator ( actionGoal ) );
		}

		void sendCancelFunc (goalid cancel_msg)
		{
			cancelPublisher.publish ( cancel_msg );
		}

		void initClient (CallbackQueueInterface queue)
		{
			// assume this means wait until ros is good to go?
//			ros::Time::waitForValid ();
			// read parameters indicating publish/subscribe queue sizes
			int pub_queue_size = 10;
			int sub_queue_size = 1;
//			nodeHandle.param ( "actionlib_client_pub_queue_size", pub_queue_size, 10 );
//			nodeHandle.param ( "actionlib_client_sub_queue_size", sub_queue_size, 1 );
			if ( pub_queue_size < 0 )
				pub_queue_size = 10;
			if ( sub_queue_size < 0 )
				sub_queue_size = 1;

			statusSubscriber = queue_subscribe<gsa> ("status", (uint) sub_queue_size, statusCb, queue );
			feedbackSubscriber = queue_subscribe<ActionFeedbackDecorator> ("feedback", (uint) sub_queue_size, feedbackCb, queue);
			resultSubscriber = queue_subscribe<ActionResultDecorator> ("result",   (uint) sub_queue_size, resultCb, queue);

			connectionMonitor = new ConnectionMonitor ( feedbackSubscriber, resultSubscriber );
//			connectionMonitor.reset ( new ConnectionMonitor ( feedbackSubscriber, resultSubscriber ) );

			// Start publishers and subscribers
			goalPublisher = queue_advertise<ActionGoalDecorator> ( "goal", (uint) pub_queue_size, connectionMonitor.goalConnectCallback, connectionMonitor.goalDisconnectCallback, queue );
//			goalPublisher = queue_advertise<ActionGoal>( "goal", (uint) pub_queue_size,
//			            boost::bind(&ConnectionMonitor::goalConnectCallback,    connectionMonitor, _1),
//			            boost::bind(&ConnectionMonitor::goalDisconnectCallback, connectionMonitor, _1),
//			            queue);
			cancelPublisher = queue_advertise<goalid> ( "cancel", (uint) pub_queue_size, connectionMonitor.cancelConnectCallback, connectionMonitor.cancelDisconnectCallback, queue );
//			cancelPublisher = queue_advertise<goalid>("cancel", static_cast<uint32_t>(pub_queue_size),
//			            boost::bind(&ConnectionMonitor::cancelConnectCallback,    connectionMonitor, _1),
//			            boost::bind(&ConnectionMonitor::cancelDisconnectCallback, connectionMonitor, _1),
//			            queue);

			goalManager.registerSendGoalDelegate ( SendGoalFunc );
//			goalManager.registerSendGoalDelegate(boost::bind(&ActionClient<ActionSpec>::SendGoalDelegate, this, _1));
			goalManager.registerCancelFunc ( sendCancelFunc );
//			goalManager.registerCancelFunc(boost::bind(&ActionClient<ActionSpec>::sendCancelFunc, this, _1));
		}

		Publisher<M> queue_advertise<M> (string topic, uint queue_size, SubscriberStatusCallback connect_cb, SubscriberStatusCallback disconnect_cb, CallbackQueueInterface queue) where M : IRosMessage, new()
		{
			AdvertiseOptions<M> ops = new AdvertiseOptions<M> ( topic, (int) queue_size, connect_cb, disconnect_cb );
//			ops.tracked_object = ros::VoidPtr();
			ops.latch = false;
			ops.callback_queue = queue;
			return nodeHandle.advertise ( ops );
		}

		Subscriber<MessageEvent<M>> queue_subscribe<M> ( string topic, uint queue_size, MessageEventDelegate<M> msgEvent, CallbackQueueInterface queue ) where M : IRosMessage, new()
//		Subscriber<M, T> queue_subscribe (string topic, uint queue_size, void(T::*fp)(const ros::MessageEvent<M const>&), T* obj, ros::CallbackQueueInterface* queue)
		{
			SubscribeOptions<MessageEvent<M>> ops = new SubscribeOptions<MessageEvent<M>> ( topic, queue_size, new CallbackDelegate<MessageEvent<M>> ( msgEvent ) );
			ops.callback_queue = queue;
			ops.topic = topic;
			ops.queue_size = queue_size;
			// md5 and datatype get generated in constructor
//			ops.md5sum = ros::message_traits::md5sum<M>();
//			ops.datatype = ros::message_traits::datatype<M>();
			ops.helper = new SubscriptionCallbackHelper<MessageEvent<M>> ( Messages.MsgTypes.MessageEvent, new CallbackDelegate<MessageEvent<M>> (msgEvent) );
//			ops.helper = ros::SubscriptionCallbackHelperPtr(
//				new ros::SubscriptionCallbackHelperT<const ros::MessageEvent<M const>& >(
//				boost::bind(fp, obj, _1)
//				)
//			);
			return nodeHandle.subscribe(ops);
		}
		void statusCb (MessageEvent<gsa> statusArrayEvent)
		{
			ROS.Debug ( "actionlib", "Getting status over the wire." );
			if ( connectionMonitor != null )
				connectionMonitor.processStatus ( statusArrayEvent.getMessage (), statusArrayEvent.getPublisherName () );
			goalManager.updateStatuses ( statusArrayEvent.getMessage () );
		}

		void feedbackCb (MessageEvent<ActionFeedbackDecorator> actionFeedback)
		{
			goalManager.updateFeedbacks ( actionFeedback.getMessage () );
		}

		void resultCb (MessageEvent<ActionResultDecorator> actionResult)
		{
			goalManager.updateResults ( actionResult.getMessage () );
		}
	}
}