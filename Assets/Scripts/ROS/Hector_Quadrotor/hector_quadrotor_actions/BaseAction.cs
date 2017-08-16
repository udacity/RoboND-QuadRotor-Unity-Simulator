//=================================================================================================
// Copyright (c) 2012-2016, Institute of Flight Systems and Automatic Control,
// Technische Universität Darmstadt.
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of hector_quadrotor nor the names of its contributors
//       may be used to endorse or promote products derived from this software
//       without specific prior written permission.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//=================================================================================================
using Ros_CSharp;
using actionlib;
using hector_uav_msgs;
using Messages.geometry_msgs;

//namespace hector_quadrotor_actions
//{
//	public class BaseActionServer<ActionSpec>
//	{
//		SimpleActionServer<ActionSpec> as_;
//
//		string server_name_;
//		Ros_CSharp.WrappedTimer start_timer_;
//		hector_quadrotor_interface.PoseSubscriberHelper pose_sub_;
//		ServiceClient motor_enable_service_;
//
//		double connection_timeout_;
//
//		public BaseActionServer (NodeHandle nh, string server_name, SimpleActionServer<ActionSpec>.ExecuteCallback callback)
//		{
//			pose_sub_ = new hector_quadrotor_interface.PoseSubscriberHelper ( nh, "pose" );
//			as_ = new SimpleActionServer<ActionSpec> ( nh, server_name, callback, false );
////			nh.param<double>("connection_timeout", connection_timeout_, 10.0);
//
//			server_name_ = nh.resolveName(server_name);
//
//			motor_enable_service_ = nh.serviceClient<hector_uav_msgs.EnableMotors>("enable_motors");
//			if(!motor_enable_service_.waitForExistence()){
//					ROS.Error("Could not connect to ", nh.resolveName("enable_motors"));
//			}
//			start_timer_ = nh.createTimer(ros::Duration(0.1), boost::bind(&BaseActionServer::startCb, this));
//		}
//
//		/*
//		* Guaranteed to be available after server has started
//		*/
//		public PoseStamped getPose ()
//		{
//			return pose_sub_.get();
//		}
//
//		public SimpleActionServer<ActionSpec> get() { return as_; }
//
//		public bool enableMotors (bool enable)
//		{
//			EnableMotors srv = new EnableMotors ();
//			srv.req.enable = enable;
//			return motor_enable_service_.call ( srv );
//		}
//
//
//		void startCb ()
//		{
//			if ( !pose_sub_.get () )
//			{
//				ROS.Info ( "Waiting for position state to be available before starting " + server_name_ + " server" );
//			}
//			else
//			{
//				as_start ();
//				ROS.Info("Server " + server_name_ + " started");
//				start_timer_.stop();
//			}
//		}
//	}
//}