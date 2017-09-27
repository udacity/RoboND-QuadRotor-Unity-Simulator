using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

public class SpawnPointManager : MonoBehaviour
{
	public static int Count { get { if ( instance != null )
			return instance.path.Count; return 0; } }

	static SpawnPointManager instance;
	public LineRenderer pathPrefab;
	public Transform nodePrefab;


	List<PathSample> path;
	LineRenderer pathRenderer;
	List<Transform> nodeObjects;

	bool clearVizFlag;
	bool rebuildPathFlag;

	void Awake ()
	{
		instance = this;
		pathRenderer = Instantiate ( pathPrefab, transform );
		pathRenderer.numPositions = 0;
//		pathRenderer.SetPositions ( new Vector3[0] );
		path = new List<PathSample> ();
		nodeObjects = new List<Transform> ();
	}

	void Update ()
	{
		if ( clearVizFlag )
		{
			_ClearViz ();
		}

		if ( rebuildPathFlag )
		{
			RebuildPath ();
		}
	}

	public static void AddNode (Vector3 position, Quaternion orientation)
	{
		instance._AddNode ( position, orientation );
	}

	void _AddNode (Vector3 position, Quaternion orientation)
	{
		position.y = 1;

		pathRenderer.numPositions = pathRenderer.numPositions + 1;
		pathRenderer.SetPosition ( pathRenderer.numPositions - 1, position );

		PathSample sample = new PathSample ();
		sample.position = position;
		sample.orientation = orientation;
		sample.timestamp = Time.time;
		path.Add ( sample );

		Transform node = Instantiate ( nodePrefab, position, orientation, transform );
		nodeObjects.Add ( node );
	}

	public static PathSample[] GetPath ()
	{
		return instance.path.ToArray ();
	}

	public static void Clear (bool clearViz = true)
	{
		instance.path.Clear ();
		if ( clearViz )
			ClearViz ();
	}

	public static void ClearViz ()
	{
		instance.clearVizFlag = true;
	}

	void _ClearViz ()
	{
		pathRenderer.numPositions = 0;
		int count = nodeObjects.Count;
		for ( int i = 0; i < count; i++ )
			Destroy ( nodeObjects [ i ].gameObject );
		nodeObjects.Clear ();
		clearVizFlag = false;
	}

	public static void SetPath (List<PathSample> nodes)
	{
		instance.path = nodes;
		instance.rebuildPathFlag = true;
	}

	void RebuildPath ()
	{
		int count = nodeObjects.Count;
		for ( int i = 0; i < count; i++ )
			Destroy ( nodeObjects [ i ].gameObject );
		nodeObjects.Clear ();
		count = path.Count;
		pathRenderer.numPositions = count;
		for ( int i = 0; i < count; i++ )
		{
			if ( path [ i ].timestamp == -1 )
				path [ i ].timestamp = Time.time;
			pathRenderer.SetPosition ( i, path[i].position );
			Transform node = Instantiate ( nodePrefab, path [ i ].position, path [ i ].orientation, transform );
			nodeObjects.Add ( node );
		}
		rebuildPathFlag = false;
	}
}
