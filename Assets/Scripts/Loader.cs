using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Loader script: looks for command line args to load a scene. if none exist, chooses a scene by default
 */

public class Loader : MonoBehaviour
{
	public int defaultScene;

	void Awake ()
	{
		string[] args = System.Environment.GetCommandLineArgs ();
		int sceneSelect = defaultScene;
		foreach ( string arg in args )
		{
			string lower = arg.ToLower ();
			if ( lower.Contains ( "indoor" ) )
			{
				sceneSelect = 0;
				break;
			}
			if ( lower.Contains ( "outdoor" ) || lower.Contains ( "city" ) )
			{
				sceneSelect = 1;
				break;
			}
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene ( sceneSelect + 1 ); // 0 is the loader
	}
}