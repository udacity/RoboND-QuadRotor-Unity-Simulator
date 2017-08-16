using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hack script to fix whatever is broken with Rigidbody on the quad
// when called, it waits a moment, deactivates the quad, then shortly reactivates it

public class QuadActivator : MonoBehaviour
{
	static QuadActivator instance;

	public static void Activate (GameObject go)
	{
		if ( instance == null )
		{
			instance = new GameObject ( "QuadActivator" ).AddComponent<QuadActivator> ();
		}
		instance.Do ( go );
	}

	void Do (GameObject go)
	{
		StartCoroutine ( DoDo ( go ) );
	}

	IEnumerator DoDo (GameObject go)
	{
		yield return new WaitForSeconds ( 0.2f );
		go.SetActive ( false );
		yield return new WaitForSeconds ( 0.05f );
		go.SetActive ( true );
	}
}