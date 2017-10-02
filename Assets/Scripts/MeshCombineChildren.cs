using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// very simple mesh combiner
// for now assumes all children are either identical and have the same material&textures
// might improve to also atlas textures

public class MeshCombineChildren : MonoBehaviour
{
	GameObject combinedObject;

	void Awake ()
	{
		Combine ();
	}

	void Combine ()
	{
		MeshFilter[] children = GetComponentsInChildren<MeshFilter> ();
//		List<Material> materials = new List<Material> ();
		CombineInstance[] combines = new CombineInstance[children.Length];
		MeshRenderer r;
		MeshFilter f;

		for ( int i = 0; i < children.Length; i++ )
		{
			f = children [ i ];
//			foreach ( Material m in r.sharedMaterials )
//				if ( !materials.Contains ( m ) )
//					materials.Add ( m );
			CombineInstance combine = new CombineInstance ();
			combine.mesh = f.sharedMesh;
			combine.transform = f.transform.localToWorldMatrix;
			combines [ i ] = combine;
			f.gameObject.SetActive ( false );
		}

		Mesh mesh = new Mesh ();
		mesh.CombineMeshes ( combines, true );

		combinedObject = new GameObject ( gameObject.name + "_Combined" );
		combinedObject.transform.parent = transform;
		combinedObject.transform.localPosition = Vector3.zero;
		combinedObject.transform.localRotation = Quaternion.identity;
//		combinedObject.transform.localScale = Vector3.one;
		r = combinedObject.AddComponent<MeshRenderer> ();
		r.sharedMaterials = children [ 0 ].GetComponent<MeshRenderer> ().sharedMaterials;
		f = combinedObject.AddComponent<MeshFilter> ();
		f.mesh = mesh;
	}
}