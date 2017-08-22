using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSpawner : MonoBehaviour
{
	public Transform[] spawnTargets;
	public Transform[] spawnPoints;
	public Transform targetInstance;
	public OrbitCamera followCam;
	public bool spawnNewPeople;
	public float spawnTimer = 60;

	float nextSpawnTime;

	void Awake ()
	{
		SpawnPerson ();
		nextSpawnTime = Time.time + spawnTimer;
	}

	void Update ()
	{
		if ( spawnNewPeople )
		{
			if ( Time.time > nextSpawnTime )
			{
				SpawnPerson ();
				nextSpawnTime = Time.time + spawnTimer;
			}
		}
	}

	void SpawnPerson ()
	{
		if ( targetInstance != null )
			Destroy ( targetInstance.gameObject );
		Transform target = spawnTargets [ Random.Range ( 0, spawnTargets.Length ) ];
		Transform spawn = GetRandomPoint ();
		targetInstance = Instantiate ( target );
		targetInstance.position = spawn.position;
		targetInstance.gameObject.SetActive ( true );
		followCam.target = targetInstance;
	}

	Transform GetRandomPoint ()
	{
		return spawnPoints [ Random.Range ( 0, spawnPoints.Length ) ];
	}
}