using System;// Change ///////////////////////////////////////////////
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
    
    [Serializable]// Change ///////////////////////////////////////////////
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

    // Change ///////////////////////////////////////////////
    [Serializable]
    public class PathSampleCompound
    {
        public PathSample[] patrol;
        public PathSample[] hero;
        public PathSample[] spawns;

        public int mode;
        public string name;

        public const int MODE_FREE = 0;// Just patrol
        public const int MODE_FORCE_FOLLOW_FIXED = 1;// Follow target from quite close
        public const int MODE_FORCE_FOLLOW_FIXED_FAR = 2;// Follow target from quite far
        public const int MODE_FORCE_FOLLOW_DYNAMIC = 3;// Follow target and change some parameters
        public const int TOTAL_MODES = 4;

        public PathSampleCompound()
        {
            
        }

        public PathSampleCompound( PathSample[] pPatrol,
                                   PathSample[] pHero,
                                   PathSample[] pSpawns )
        {
            this.patrol = pPatrol;
            this.hero = pHero;
            this.spawns = pSpawns;
            this.mode = PathSampleCompound.MODE_FREE;
        }

        public PathSampleCompound( PathSample[] pPatrol,
                                   PathSample[] pHero,
                                   PathSample[] pSpawns,
                                   int pMode )
        {
            this.patrol = pPatrol;
            this.hero = pHero;
            this.spawns = pSpawns;
            this.mode = pMode;
        }

		public void dump()
		{
			Debug.Log( "LOG> PathSampleCompound *********" );
			for ( int i = 0; i < patrol.Length; i++ )
			{
				Debug.Log( "patrol(" + i.ToString() + ").pos : " + patrol[i].position.ToString() );
			}
			for (int i = 0; i < hero.Length; i++)
            {
				Debug.Log( "hero(" + i.ToString() + ").pos : " + hero[i].position.ToString() );
            }
			for (int i = 0; i < spawns.Length; i++)
            {
				Debug.Log( "spawns(" + i.ToString() + ").pos : " + spawns[i].position.ToString() );
            }
			Debug.Log("LOG> PathSampleCompound *********");
		}
    }
    /////////////////////////////////////////////////////////
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
		pathRenderer.positionCount = 0;
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
		pathRenderer.positionCount = pathRenderer.positionCount + 1;
		pathRenderer.SetPosition ( pathRenderer.positionCount - 1, position );

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
		pathRenderer.positionCount = 0;
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