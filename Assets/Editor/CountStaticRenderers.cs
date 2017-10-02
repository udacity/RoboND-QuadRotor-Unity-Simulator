using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CountStaticRenderers : MonoBehaviour
{
	[MenuItem ("Utility/Count Static Renderers", false, 21)]
	static void CountStatics ()
	{
		Renderer[] renderers = SceneView.FindObjectsOfType<Renderer> ();
		int staticCount = 0;
		int nonStaticCount = 0;
		foreach ( Renderer r in renderers )
		{
			if ( GameObjectUtility.AreStaticEditorFlagsSet ( r.gameObject, StaticEditorFlags.LightmapStatic ) )
				staticCount++;
			else
				nonStaticCount++;
		}

		Debug.Log ( "There are " + staticCount + " static renderers and " + nonStaticCount + " non static ones." );
	}
}