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
using System.Collections.Generic;
using Ros_CSharp;
using System.Threading;
using gsa = Messages.actionlib_msgs.GoalStatusArray;
using duration = Messages.std_msgs.Duration;

namespace actionlib
{
	public class ConnectionMonitor
	{
		
		// status stuff
		string statusCallerID;
		bool statusReceived;
		Messages.std_msgs.Time latestStatusTime;

		object lockObject = new object ();
//		boost::condition check_connection_condition_;
//		boost::recursive_mutex data_mutex_;

		Dictionary<string, int> goalSubscribers;
		Dictionary<string, int> cancelSubscribers;

		string goalSubscribersString ()
		{
			string s = "";
			lock ( lockObject )
			{
				s = "Goal Subscribers (" + goalSubscribers.Count + " total)";
				foreach ( KeyValuePair<string, int> pair in goalSubscribers )
				{
					s += "\n - " + pair.Key;
				}
			}
			return s;
		}

		string cancelSubscribersString ()
		{
			string s = "";
			lock ( lockObject )
			{
				s = "Cancel Subscribers (" + cancelSubscribers.Count + " total)";
				foreach ( KeyValuePair<string, int> pair in cancelSubscribers )
				{
					s += "\n - " + pair.Key;
				}
			}
			return s;
		}

		Subscriber<MessageEvent<ActionFeedbackDecorator>> feedbackSubscriber;
		Subscriber<MessageEvent<ActionResultDecorator>> resultSubscriber;


		public ConnectionMonitor ( Subscriber<MessageEvent<ActionFeedbackDecorator>> feedback_sub, Subscriber<MessageEvent<ActionResultDecorator>> result_sub )
		{
			this.feedbackSubscriber = feedback_sub;
			this.resultSubscriber = result_sub;
			statusReceived = false;
		}

		public void goalConnectCallback (SingleSubscriberPublisher pub)
		{
			lock ( lockObject )
			{
				// check if the dictionary contains this publisher
				if ( goalSubscribers.ContainsKey ( pub.subscriber_name ) )
				{
					ROS.Warn ( "goalConnectCallback: Trying to add [%s] to goalSubscribers, but it is already in the goalSubscribers list", pub.subscriber_name );
					goalSubscribers [ pub.subscriber_name ]++;
					
				} else
				{
					ROS.Debug ( "goalConnectCallback: Adding [%s] to goalSubscribers", pub.subscriber_name );
					goalSubscribers.Add ( pub.subscriber_name, 1 );
				}
			}

			ROS.Debug ( "%s", goalSubscribersString () );

			// notify all threads waiting on this condition. Monitor.Pulse? might not need all this
//			check_connection_condition_.notify_all();
		}

		public void goalDisconnectCallback (SingleSubscriberPublisher pub)
		{
			lock ( lockObject )
			{
				if ( goalSubscribers.ContainsKey ( pub.subscriber_name ) )
				{
					ROS.Debug ( "goalDisconnectCallback: Removing [%s] from goalSubscribers", pub.subscriber_name );

					goalSubscribers [ pub.subscriber_name ]--;
					if ( goalSubscribers [ pub.subscriber_name ] == 0 )
					{
						goalSubscribers.Remove ( pub.subscriber_name );
					}
					Dictionary<string, int>.Enumerator en = goalSubscribers.GetEnumerator ();

				} else
				{
					
					ROS.Warn ( "goalDisconnectCallback: Trying to remove [%s] to goalSubscribers, but it is not in the goalSubscribers list", pub.subscriber_name );
				}
			
				ROS.Debug ( "%s", goalSubscribersString () );
			}
		}

		public void cancelConnectCallback (SingleSubscriberPublisher pub)
		{
			lock ( lockObject )
			{
				if ( cancelSubscribers.ContainsKey ( pub.subscriber_name ) )
				{
					ROS.Warn ( "cancelConnectCallback: Trying to add [%s] to cancelSubscribers, but it is already in the cancelSubscribers list", pub.subscriber_name );
					cancelSubscribers [ pub.subscriber_name ]++;

				} else
				{
					ROS.Debug ( "cancelConnectCallback: Adding [%s] to cancelSubscribers", pub.subscriber_name );
					cancelSubscribers [ pub.subscriber_name ] = 1;
					
				}
				ROS.Debug ( "%s", cancelSubscribersString () );
			}
//			check_connection_condition_.notify_all();
		}

		public void cancelDisconnectCallback (SingleSubscriberPublisher pub)
		{
			lock ( lockObject )
			{
				if ( cancelSubscribers.ContainsKey ( pub.subscriber_name ) )
				{
					ROS.Debug ( "cancelDisconnectCallback: Removing [%s] from cancelSubscribers", pub.subscriber_name );
					cancelSubscribers [ pub.subscriber_name ]--;
					if (cancelSubscribers[pub.subscriber_name] == 0)
					{
						cancelSubscribers.Remove ( pub.subscriber_name );
					}
				} else
				{
					ROS.Warn ( "cancelDisconnectCallback: Trying to remove [%s] to cancelSubscribers, but it is not in the cancelSubscribers list", pub.subscriber_name );
				}
			}
			ROS.Debug ( "%s", cancelSubscribersString () );
		}

		// ********* GoalStatus Connections *********
		public void processStatus (gsa status, string curStatusCallerID)
		{
			lock ( lockObject )
			{
				if ( statusReceived )
				{
					if ( statusCallerID != curStatusCallerID )
					{
						ROS.Warn ( "processStatus: Previously received status from [%s], but we now received status from [%s]. Did the ActionServer change?",
							statusCallerID, curStatusCallerID );
						statusCallerID = curStatusCallerID;
					}
					latestStatusTime = status.header.Stamp;
				} else
				{
					ROS.Debug ( "processStatus: Just got our first status message from the ActionServer at node [%s]", curStatusCallerID );
					statusReceived = true;
					statusCallerID = curStatusCallerID;
					latestStatusTime = status.header.Stamp;
				}
			}
//			check_connection_condition_.notify_all();
		}

		public bool waitForActionServerToStart (duration timeout, NodeHandle nh)
		{
			if ( timeout < new duration () )
				ROS.Error ( "actionlib", "Timeouts can't be negative. Timeout is [%.2fs]", timeout.data.sec.ToString () + ":" + timeout.data.nsec.ToString () );

			Messages.std_msgs.Time timeout_time = ROS.GetTime () + timeout;

			lock ( lockObject )
			{
				if ( isServerConnected () )
					return true;

				// Hardcode how often we check for node.ok()
				duration loop_period = new duration ( new Messages.TimeData ( 0, 500000000 ) ); // ".fromSec(.5)"

				while (nh.ok && !isServerConnected())
				{
					// Determine how long we should wait
					duration time_left = timeout_time - ROS.GetTime ();
					
					// Check if we're past the timeout time
					if ( timeout != new duration ( new Messages.TimeData ( 0, 0 ) ) && time_left <= new duration ( new Messages.TimeData ( 0, 0 ) ) )
						break;
					
					// Truncate the time left
					if ( time_left > loop_period || timeout == new duration () )
						time_left = loop_period;

					uint msWait = time_left.data.sec * 1000 + time_left.data.nsec / 1000000;
					Monitor.Wait ( lockObject, UnityEngine.Mathf.Max ( (int) msWait, 0 ) );
				}
				
				return isServerConnected();
			}
		}


		public bool isServerConnected ()
		{
			lock ( lockObject )
			{
				if (!statusReceived)
				{
					ROS.Debug ("isServerConnected: Didn't receive status yet, so not connected yet");
					return false;
				}

				if ( !goalSubscribers.ContainsKey ( statusCallerID ) )
				{
					ROS.Debug ("isServerConnected: Server [%s] has not yet subscribed to the goal topic, so not connected yet", statusCallerID);
					ROS.Debug ("%s", goalSubscribersString());
					return false;
				}

				if ( !cancelSubscribers.ContainsKey (statusCallerID) )
				{
					ROS.Debug ("isServerConnected: Server [%s] has not yet subscribed to the cancel topic, so not connected yet", statusCallerID);
					ROS.Debug ("%s", cancelSubscribersString());
					return false;
				}

				if ( feedbackSubscriber.NumPublishers == 0 )
				{
					ROS.Debug ("isServerConnected: Client has not yet connected to feedback topic of server [%s]", statusCallerID);
					return false;
				}
				
				if(resultSubscriber.NumPublishers == 0)
				{
					ROS.Debug ("isServerConnected: Client has not yet connected to result topic of server [%s]", statusCallerID);
					return false;
				}

				ROS.Debug ("isServerConnected: Server [%s] is fully connected", statusCallerID);
				return true;
			}
		}
	}
}