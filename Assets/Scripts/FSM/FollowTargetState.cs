using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : DroneState
{
	public float smoothTime = 5;
	public float maxSpeed = 10;
	public bool lockOnTarget = true;

	Transform followTarget;
	Vector3 followPoint;
	LayerMask targetMask;
	Rigidbody rb;

	Vector3 velocity;
	float startTime;
	float speedPercent;

	public override void OnEnter ()
	{
		base.OnEnter ();

		rb = motor.rb;
		followPoint = control.LastTargetPoint;
		if ( lockOnTarget )
			followTarget = PeopleSpawner.instance.targetInstance;
		rb.freezeRotation = true;
		rb.velocity = Vector3.zero;
		startTime = Time.time;
		speedPercent = 0;
	}

	public override void OnLateUpdate ()
	{
		if ( followTarget != null )
			followPoint = followTarget.position;
		else
			followPoint = control.LastTargetPoint;
		Vector3 toTarget = followPoint - motor.transform.position;
		Vector3 backPoint;
		if ( followTarget != null )
			backPoint = followPoint - followTarget.forward * motor.followDistance + Vector3.up * ( motor.followHeight + 1 );
		else
		{
			toTarget.y = 0;
			backPoint = followPoint - toTarget.normalized * motor.followDistance + Vector3.up * motor.followHeight;
		}

		float distance = ( backPoint - motor.transform.position ).sqrMagnitude;
		float stoppingDist = 100;

		if ( distance < stoppingDist )
			speedPercent = Mathf.Sqrt ( distance / stoppingDist );
		else
		if ( Time.time - startTime < motor.followAccelTime )
			speedPercent = ( Time.time - startTime ) / motor.parolAccelTime;
		else
			speedPercent = 1;

		motor.rb.velocity = ( backPoint - motor.transform.position ).normalized * motor.maxFollowSpeed * speedPercent;

//		motor.transform.position = Vector3.MoveTowards ( motor.transform.position, backPoint, maxSpeed * Time.deltaTime );
//		motor.transform.position = Vector3.SmoothDamp ( motor.transform.position, backPoint, ref velocity, smoothTime, maxSpeed );
		Vector3 look = followPoint - motor.transform.position;
		look.y = 0;
		motor.transform.rotation = Quaternion.RotateTowards ( motor.transform.rotation, Quaternion.LookRotation ( look, Vector3.up ), 90 * Time.deltaTime );
		gimbal.LookAt ( followPoint + Vector3.up );
	}

	public override void OnExit ()
	{
	}

	void OnGUI ()
	{
		Rect r = new Rect ( 10, Screen.height - 35, 150, 25 );
		GUILayout.BeginArea ( r );
		GUILayout.Box ( "Quad is following" );
		GUILayout.EndArea ();
	}
}