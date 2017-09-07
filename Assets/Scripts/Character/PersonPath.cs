using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PersonPath : MonoBehaviour
{
	public Transform[] points = new Transform[0];
	public bool showPath;
	public bool updatePoints;

	void Awake ()
	{
		GetPoints ();
	}

	#if UNITY_EDITOR
	void Update ()
	{
		if ( !UnityEditor.EditorApplication.isPlaying )
		{
			if ( updatePoints )
			{
				GetPoints ();
			}
		}
	}
	#endif

	void GetPoints ()
	{
		points = new Transform [ transform.childCount ];
		for ( int i = 0; i < points.Length; i++ )
			points [ i ] = transform.GetChild ( i );
	}

	void OnDrawGizmosSelected ()
	{
		if ( !showPath )
			return;
		
		Gizmos.color = Color.green;
		for ( int i = 0; i < points.Length - 1; i++ )
		{
			Gizmos.DrawSphere ( points [ i ].position, 0.5f );
			Gizmos.DrawLine ( points [ i ].position, points [ i + 1 ].position );
		}
		Gizmos.DrawSphere ( points [ points.Length - 1 ].position, 0.5f );
	}
}