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
		if ( followTarget == null && lockOnTarget )
			followTarget = PeopleSpawner.instance.targetInstance;

		if ( followTarget != null )
		{
			followPoint = followTarget.position;
			control.LastTargetPoint = followPoint;
		} else
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

		float speed = motor.maxPatrolSpeed * speedPercent;
		Vector3 velocity = ( backPoint - motor.transform.position ).normalized;
		float rayDist = 5;

		// add some avoidance
		BoxCollider bc = motor.boxCollider;
		RaycastHit hit;
		if ( Physics.BoxCast ( motor.transform.position, bc.size/2, velocity.normalized, out hit, bc.transform.rotation, rayDist, control.collisionMask.value ) )
		{
			Debug.DrawLine ( motor.transform.position, hit.point, Color.red, Time.deltaTime * 2, false );
			Debug.DrawRay ( hit.point, hit.normal, Color.blue, Time.deltaTime * 2, false );
			Debug.DrawLine ( motor.transform.position, motor.transform.position + velocity * speed, Color.white, Time.deltaTime * 2, false );
			Vector3 localTarget = motor.transform.InverseTransformPoint ( backPoint );
			Vector3 direction = new Vector3 ( hit.normal.z, hit.normal.y, hit.normal.x );
			if ( Vector3.Angle ( velocity, direction ) > 90 )
				direction = -direction;
			float distPercent = Mathf.InverseLerp ( ( bc.size / 2 ).sqrMagnitude, rayDist * rayDist, hit.distance * hit.distance ); // using squares instead of sqrts
			velocity = Vector3.Lerp ( velocity, direction, 1f - distPercent );
		}

		motor.rb.velocity = velocity * speed;

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