using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : DroneState
{
	Transform[] points;
	int curPoint;

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


		follower.arriveCallback = OnArrived;
		FindNearestPoint ();
		gimbal.Sweep ();
	}

	public override void OnUpdate ()
	{
		
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
		follower.SetFollowPoint ( points [ curPoint ].position );
//		Debug.Log ( "setting nearest patrol point to " + points [ curPoint ].position );
	}

	void OnArrived ()
	{
		curPoint = ++curPoint % points.Length;
		follower.SetFollowPoint ( points [ curPoint ].position );
//		Debug.Log ( "setting next patrol point to " + points [ curPoint ].position );
	}
}