using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

namespace Pathing
{
	public class Path
	{
		public PathSample[] Nodes { get { return nodes; } }
		PathSample[] nodes;
		
		public Path (PathSample[] samples)
		{
			nodes = samples;
		}
	}
	
	public class PathSample
	{
		public Vector3 position;
		public Quaternion orientation;
		public float timestamp;

		public PathSample ()
		{
			
		}

		public PathSample (Vector3 pos, Quaternion q, float t)
		{
			position = pos;
			orientation = q;
			timestamp = t;
		}
	}
}

public class PathPlanner : MonoBehaviour
{
	static PathPlanner instance;
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