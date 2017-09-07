using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollection : MonoBehaviour
{
	static PersonPath[] paths;

	void Awake ()
	{
		paths = GetComponentsInChildren<PersonPath> ();
	}

	public static PersonPath GetPath ()
	{
		return paths [ Random.Range ( 0, paths.Length ) ];
	}
}