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
namespace actionlib
{
	public class SimpleClientGoalState
	{
		//! \brief Defines the various states the goal can be in
		public enum StateEnum
		{
			PENDING,
			ACTIVE,
			RECALLED,
			REJECTED,
			PREEMPTED,
			ABORTED,
			SUCCEEDED,
			LOST
		}

		StateEnum state_;
		string text_;

		SimpleClientGoalState () {}

		public SimpleClientGoalState (StateEnum state, string text = "")
		{
			this.state_ = state;
			this.text_ = text;
		}

		public static bool operator == (SimpleClientGoalState lhs, SimpleClientGoalState rhs)
		{
			if ( object.ReferenceEquals ( lhs, rhs ) )
				return true;
			
			return ( lhs.state_ == rhs.state_ );
		}

		public static bool operator != (SimpleClientGoalState lhs, SimpleClientGoalState rhs)
		{
			return !( lhs == rhs );
		}

		public static bool operator == (SimpleClientGoalState lhs, SimpleClientGoalState.StateEnum rhs)
		{
			return ( !object.ReferenceEquals ( lhs, null ) && lhs.state_ == rhs );
		}

		public static bool operator != (SimpleClientGoalState lhs, SimpleClientGoalState.StateEnum rhs)
		{
			return !( lhs == rhs );
		}


		/**
		* \brief Determine if goal is done executing (ie. reached a terminal state)
		* \return True if in RECALLED, REJECTED, PREEMPTED, ABORTED, SUCCEEDED, or LOST. False otherwise
		*/
		public bool isDone()
		{
			switch ( state_ )
			{
			case StateEnum.RECALLED:
			case StateEnum.REJECTED:
			case StateEnum.PREEMPTED:
			case StateEnum.ABORTED:
			case StateEnum.SUCCEEDED:
			case StateEnum.LOST:
				return true;
			default:
				return false;
			}
		}

		public string getText ()
		{
			return text_;
		}

		//! \brief Convert the state to a string. Useful when printing debugging information
		public string toString ()
		{
			return state_.ToString ();
		}
	}
}