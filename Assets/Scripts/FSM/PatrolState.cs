using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : DroneState
{
	public float arriveDist = 0.05f;
	public bool debug;

	Transform[] points;
	int curPoint;
	float speedPercent;
	float startTime;
	bool waiting;

	public override void OnEnter ()
	{
		base.OnEnter ();

		if ( points == null )
		{
			GameObject pointsParent = GameObject.Find ( "Patrol Points" );
			points = new Transform[ pointsParent.transform.childCount ];
			for ( int i = 0; i < points.Length; i++ )
				points [ i ] = pointsParent.transform.GetChild ( i );
		}


//		follower.arriveCallback = OnArrived;
		FindNearestPoint ();
		gimbal.Sweep ( motor.gimbalSweepVAngle );

		startTime = Time.time;
		speedPercent = 0;
	}

//	public override void OnUpdate ()
//	{
//		
//	}

	public override void OnLateUpdate ()
	{
		if ( waiting )
		{
			if ( Time.time - startTime > motor.patrolWaitTime )
			{
				startTime = Time.time;
				waiting = false;
			}
			return;
		}
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

		motor.rb.velocity = ( dest - motor.Position ).normalized * motor.maxPatrolSpeed * speedPercent;
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
		if ( !debug )
			return;
		Rect r = new Rect ( Screen.width/2 - 50, Screen.height/2 - 10, 100, 20 );
		if ( waiting )
		{
			GUI.Box ( r, "" );
			GUI.Label ( r, "Waiting: " + ( Time.time - startTime ).ToString ( "F1" ) );
		} else
		{
			GUI.Box ( r, "" );
			GUI.Label ( r, "Speed: " + ( speedPercent * motor.maxPatrolSpeed ).ToString ( "F1" ) );
		}
	}
}