using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerObject : MonoBehaviour
{
	[HideInInspector]
	public Material material;
	[HideInInspector]
	public float startTime;
	[HideInInspector]
	public float duration;

	void Awake ()
	{
		Renderer r = GetComponent<Renderer> ();
		material = r.material;
		startTime = Time.time;
		name = startTime.ToString ();
	}
}