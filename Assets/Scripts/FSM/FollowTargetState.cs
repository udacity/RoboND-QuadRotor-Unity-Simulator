using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : DroneState
{
	public float smoothTime = 5;
	public float maxSpeed = 10;

	Transform followTarget;
	Vector3 followPoint;
	LayerMask targetMask;
	Rigidbody rb;

	Vector3 velocity;

	public override void OnEnter ()
	{
		base.OnEnter ();

		rb = motor.rb;
		followPoint = control.LastTargetPoint;
		followTarget = PeopleSpawner.instance.targetInstance;
		rb.freezeRotation = true;
	}

	public override void OnUpdate ()
	{
		if ( followTarget != null )
			followPoint = followTarget.position;
		Vector3 toTarget = followPoint - motor.Position;
		Vector3 backPoint;
		if ( followTarget != null )
			backPoint = followPoint - followTarget.forward * motor.followDistance + Vector3.up * motor.followHeight;
		else
		{
			toTarget.y = 0;
			backPoint = followPoint - toTarget.normalized * motor.followDistance + Vector3.up * motor.followHeight;
		}

		motor.transform.position = Vector3.SmoothDamp ( motor.transform.position, backPoint, ref velocity, smoothTime, maxSpeed );
		Vector3 look = followPoint - motor.transform.position;
		look.y = 0;
		motor.transform.rotation = Quaternion.RotateTowards ( motor.transform.rotation, Quaternion.LookRotation ( look, Vector3.up ), 90 * Time.deltaTime );
		gimbal.LookAt ( followPoint + Vector3.up );
	}

	public override void OnExit ()
	{
	}
}