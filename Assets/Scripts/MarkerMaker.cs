using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMaker : MonoBehaviour
{
	static MarkerMaker instance;

	public MarkerObject prefab;
	Dictionary<string, MarkerObject> markers;
	Queue<string> markersToDelete;

	void Awake ()
	{
		instance = this;
		markers = new Dictionary<string, MarkerObject> ();
		markersToDelete = new Queue<string> ();
	}

	void Update ()
	{
		foreach ( var pair in markers )
		{
			if ( Time.time > pair.Value.startTime + pair.Value.duration )
			{
				markersToDelete.Enqueue ( pair.Key );
			}
		}
		while ( markersToDelete.Count > 0 )
		{
			string id = markersToDelete.Dequeue ();
			var obj = markers [ id ];
			markers.Remove ( id );
			Destroy ( obj.gameObject );
		}
	}

	public static void AddMarker (string id, Vector3 pos, Quaternion rot, Vector3 size, Color c, float duration)
	{
		if ( duration == -1 )
			duration = Mathf.Infinity;
		if ( instance.markers.ContainsKey ( id ) )
		{
			var marker = instance.markers [ id ];
			marker.transform.position = pos;
			marker.transform.rotation = rot;
			marker.transform.localScale = size;
			marker.material.color = c;
			marker.UpdateDuration ( duration );
			return;
		}

		var inst = Instantiate ( instance.prefab );
		inst.transform.position = pos;
		inst.transform.rotation = rot;
		inst.transform.localScale = size;
		inst.material.color = c;
		inst.duration = duration;
		instance.markers.Add ( id, inst );
	}

	public static void DeleteMarker (string id)
	{
		var marks = instance.markers;
		if ( marks.ContainsKey ( id ) )
		{
			MarkerObject obj = marks [ id ];
			marks.Remove ( id );
			Destroy ( obj.gameObject );
		}
	}
}