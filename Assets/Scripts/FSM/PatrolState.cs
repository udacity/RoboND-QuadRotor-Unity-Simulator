using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

public class PatrolState : DroneState
{
	public float arriveDist = 0.05f;

	PathSample[] points;
	int curPoint;
	float speedPercent;
	float startTime;
	bool waiting;
	bool idleWait;

	public override void OnEnter ()
	{
		base.OnEnter ();

		points = PatrolPathManager.GetPath ();
		if ( points.Length < 2 )
			return;

		gimbal.Sweep360 ();
		waiting = false;
		idleWait = true;
		startTime = Time.time;
		speedPercent = 0;

		Debug.Log ( "idling for " + ( 360f / gimbal.sweepSpeed ) + " seconds" );
	}

//	public override void OnUpdate ()
//	{
//		
//	}

	public override void OnLateUpdate ()
	{
		if ( idleWait )
		{
			if ( motor.rb.velocity.sqrMagnitude > 0.001f )
				motor.rb.velocity *= 0.5f;
			else
				motor.rb.velocity = Vector3.zero;
			if ( Time.time - startTime > 360f / gimbal.sweepSpeed )
			{
				gimbal.Sweep ( motor.gimbalSweepVAngle );
				waiting = false;
				idleWait = false;
				startTime = Time.time;
				speedPercent = 0;
				FindNearestPoint ();
			}
			return;
		}

		if ( waiting )
		{
			if ( Time.time - startTime > motor.patrolWaitTime )
			{
				startTime = Time.time;
				waiting = false;
			}
			return;
		}
		
		if ( points.Length < 2 )
			return;

//		Debug.Log ( points.Length );

		Vector3 dest = points [ curPoint ].position;
		float distance = ( dest - motor.Position ).sqrMagnitude;
		if ( distance < arriveDist * arriveDist )
		{
			OnArrived ();
			return;
		}

		float stoppingDist = 25;
		if ( distance < stoppingDist )
			speedPercent = Mathf.Sqrt ( distance / stoppingDist );
		else
		if ( Time.time - startTime < motor.parolAccelTime )
			speedPercent = ( Time.time - startTime ) / motor.parolAccelTime;
		else
			speedPercent = 1;

		float speed = motor.maxPatrolSpeed * speedPercent;
		Vector3 velocity = ( dest - motor.Position ).normalized;

		// add some avoidance
		BoxCollider bc = motor.boxCollider;
		RaycastHit hit;
		float rayDist = 5;
		if ( Physics.BoxCast ( motor.transform.position, bc.size/2, velocity.normalized, out hit, bc.transform.rotation, rayDist, control.collisionMask.value ) )
		{
//			Debug.DrawLine ( motor.transform.position, hit.point, Color.red, Time.deltaTime * 2, false );
//			Debug.DrawRay ( hit.point, hit.normal, Color.blue, Time.deltaTime * 2, false );
//			Debug.DrawLine ( motor.transform.position, motor.transform.position + velocity * speed, Color.white, Time.deltaTime * 2, false );
			Vector3 localTarget = motor.transform.InverseTransformPoint ( dest );
			Vector3 direction = new Vector3 ( hit.normal.z, hit.normal.y, hit.normal.x );
			if ( Vector3.Angle ( velocity, direction ) > 90 )
				direction = -direction;
			float distPercent = Mathf.InverseLerp ( ( bc.size / 2 ).sqrMagnitude, rayDist * rayDist, hit.distance * hit.distance ); // using squares instead of sqrts
			velocity = Vector3.Lerp ( velocity, direction, 1f - distPercent );
		}

		motor.rb.velocity = velocity.normalized * speed;

		Vector3 look = dest - motor.transform.position;
		look.y = 0;
		motor.transform.rotation = Quaternion.RotateTowards ( motor.transform.rotation, Quaternion.LookRotation ( look, Vector3.up ), 90 * Time.deltaTime );

	}
	
	public override void OnExit ()
	{
	}

	void FindNearestPoint ()
	{
		if ( points == null )
			return;

		int index = 0;
		float dist = ( points [ 0 ].position - motor.Position ).sqrMagnitude;
		for ( int i = 1; i < points.Length; i++ )
		{
			float thisDist = ( points [ i ].position - motor.Position ).sqrMagnitude;
			if ( thisDist < dist )
			{
				dist = thisDist;
				index = i;
			}
		}
		curPoint = index;
//		follower.SetFollowPoint ( points [ curPoint ].position );
//		Debug.Log ( "setting nearest patrol point to " + points [ curPoint ].position );
	}

	void OnArrived ()
	{
		motor.rb.velocity = Vector3.zero;
		curPoint = ++curPoint % points.Length;
		startTime = Time.time;
		speedPercent = 0;
		waiting = true;
//		follower.SetFollowPoint ( points [ curPoint ].position );
//		Debug.Log ( "setting next patrol point to " + points [ curPoint ].position );
	}

	void OnGUI ()
	{
		Rect r = new Rect ( 10, Screen.height - 35, 150, 25 );
		GUILayout.BeginArea ( r );
		GUILayout.Box ( "Quad is patrolling" );
		GUILayout.EndArea ();
	}
}
