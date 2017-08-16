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

namespace actionlib
{
	/**
	 * \brief Thin wrapper around an enum in order to help interpret the state of the communication state machine
	 **/
	public class CommState
	{
		// Defines the various states the Communication State Machine can be in
		public enum StateEnum
		{
			WAITING_FOR_GOAL_ACK    = 0,
			PENDING                 = 1,
			ACTIVE                  = 2,
			WAITING_FOR_RESULT      = 3,
			WAITING_FOR_CANCEL_ACK  = 4,
			RECALLING               = 5,
			PREEMPTING              = 6,
			DONE                    = 7
		}

		public StateEnum state;

		CommState () {}

		public CommState (StateEnum state)
		{
			state = state;
		}

		public static implicit operator CommState (CommState.StateEnum state)
		{
			return new CommState ( state );
		}

		public static bool operator == (CommState lhs, CommState rhs)
		{
			if ( System.Object.ReferenceEquals ( lhs, rhs ) )
				return true;
			
			if ( System.Object.ReferenceEquals ( lhs, null ) != System.Object.ReferenceEquals ( rhs, null ) )
				return false;
			
			return lhs.state == rhs.state;
		}

		public static bool operator != (CommState lhs, CommState rhs)
		{
			return !( lhs == rhs );
		}

		public static bool operator == (CommState lhs, CommState.StateEnum rhs)
		{
			if ( ReferenceEquals ( lhs, null ) )
				return false;

			return lhs.state == rhs;
		}

		public static bool operator != (CommState lhs, CommState.StateEnum rhs)
		{
			if ( ReferenceEquals ( lhs, null ) )
				return true;

			return lhs.state != rhs;
		}

		public string toString ()
		{
			return state.ToString ();
		}
	}
}