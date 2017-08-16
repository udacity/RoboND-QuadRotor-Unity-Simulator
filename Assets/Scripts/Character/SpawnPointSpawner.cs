using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSpawner : MonoBehaviour
{
	public Transform[] spawnTargets;
	public Transform[] spawnPoints;
	public Transform targetInstance;
	public OrbitCamera followCam;

	void Awake ()
	{
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