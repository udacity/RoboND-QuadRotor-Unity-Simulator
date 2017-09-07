using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneState : StateBase
{
	protected SimpleQuadController control;
	protected QuadMotor motor;
	protected TargetFollower follower;
	protected GimbalCamera gimbal;

	public override void OnEnter ()
	{
		if ( control == null )
		{
			control = SimpleQuadController.ActiveController;
			motor = control.controller;
			follower = control.follower;
			gimbal = control.gimbal;
		}
	}
}