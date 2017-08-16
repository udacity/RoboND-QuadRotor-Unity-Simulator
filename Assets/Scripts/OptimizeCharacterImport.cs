#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class OptimizeCharacterImport : MonoBehaviour
{
	public Transform[] exposedTransforms;

	[MenuItem ("Udacity/Optimize Model", false, 10)]
	static void OptimizeCharacter ()
	{
		GameObject active = Selection.activeGameObject;
		OptimizeCharacterImport import = active.GetComponent<OptimizeCharacterImport> ();
		if ( import != null )
			import.Optimize ();
	}

	void Optimize ()
	{
		ModelImporter importer = (ModelImporter) ModelImporter.GetAtPath ( AssetDatabase.GetAssetPath ( exposedTransforms [ 0 ].GetComponent<SkinnedMeshRenderer> ().sharedMesh.GetInstanceID () ) );
		if ( importer != null )
			Debug.Log ( "found one! " + importer.assetPath );

		importer.extraExposedTransformPaths =
			( from t in exposedTransforms
		  select t.name ).ToArray ();
		importer.optimizeGameObjects = true;
		EditorUtility.SetDirty ( importer );
		AssetDatabase.SaveAssets ();
	}
}
#endif