using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderMovement : MonoBehaviour
{
	public NavMeshAgent agent;

	Transform myTransform;
	float start;

	void Awake ()
	{
		myTransform = GetComponent<Transform> ();
		agent.autoBraking = false;

		start = Random.value * 1000f;
		float y = Random.value * 360f;
		Vector3 euler = myTransform.eulerAngles;
		euler.y = y;
		myTransform.eulerAngles = euler;
	}

	void LateUpdate ()
	{
		Vector3 euler = transform.eulerAngles;
		if ( Time.timeScale != 0 )
			euler.y += 0.25f - Mathf.PerlinNoise ( start + Time.time, start + Time.time ) / 2;

		NavMeshHit navHit;
		float rayDist = 2f;
		bool didHit = agent.Raycast ( myTransform.position + myTransform.forward * rayDist, out navHit );
		if ( didHit )
		{
			Vector3 normal = new Vector3 ( navHit.normal.x, 0, navHit.normal.z ).normalized;
			Debug.DrawRay ( myTransform.position + Vector3.up, navHit.normal, Color.red );
			Vector3 targetEuler = Quaternion.LookRotation ( normal, Vector3.up ).eulerAngles;
			euler.y = Mathf.Lerp ( euler.y, targetEuler.y, 1f - navHit.distance / rayDist );
		}

		myTransform.eulerAngles = euler;
		agent.Move ( myTransform.forward * agent.speed * Time.deltaTime );
	}
}