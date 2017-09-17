using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

// a place to define the default task for FollowMe mode
public class DefaultTask : MonoBehaviour
{
	public Transform patrolPathPoints;
	public Transform heroPathPoints;
	public Transform spawnPoints;

	void Awake ()
	{
		List<PathSample> patrol = new List<PathSample> ();
		List<PathSample> hero = new List<PathSample> ();
		List<PathSample> spawn = new List<PathSample> ();

		foreach ( Transform child in patrolPathPoints )
		{
			patrol.Add( new PathSample ( child.position, child.rotation, 0 ) );
		}

		foreach ( Transform child in heroPathPoints )
		{
			hero.Add( new PathSample ( child.position, child.rotation, 0 ) );
		}

		foreach ( Transform child in spawnPoints )
		{
			spawn.Add( new PathSample ( child.position, child.rotation, 0 ) );
		}

		PatrolPathManager.SetPath ( patrol );
		HeroPathManager.SetPath ( hero );
		SpawnPointManager.SetPath ( spawn );
	}
}
