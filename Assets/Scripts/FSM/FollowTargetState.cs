using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : DroneState
{
	Transform followTarget;
	Vector3 followPoint;
	LayerMask targetMask;
	Rigidbody rb;

	public override void OnEnter ()
	{
		base.OnEnter ();

		rb = motor.rb;
		followPoint = control.LastTargetPoint;
		followTarget = PeopleSpawner.instance.targetInstance;
		follower.arriveCallback = OnArrived;
		rb.freezeRotation = true;
	}

	public override void OnUpdate ()
	{
		if ( !control.localInput )
		{
			followPoint = followTarget.position;
			Vector3 toTarget = followPoint - motor.Position;
			Vector3 backPoint = followPoint - followTarget.forward * 2 + Vector3.up * 2;
			follower.SetFollowPoint ( backPoint );
			gimbal.LookAt ( followPoint );
		}
	}

	public override void OnExit ()
	{
	}

	void OnArrived ()
	{
		
	}
}