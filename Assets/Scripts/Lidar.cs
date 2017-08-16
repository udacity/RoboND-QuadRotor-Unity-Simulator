using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using System;

public class Lidar : MonoBehaviour
{
	static Transform LidarParent;
	public GameObject layer;
	public float lineWidth = 0.05f;
	public int rayCount = 100;
	public int layerCount = 15;

	private List<LineRenderer> layers;
	private List<List<Vector3>> lidar_points;
	private bool isRay;
	private bool isLayer;

	// Use this for initialization
	void Start () {

		if ( LidarParent == null )
			LidarParent = new GameObject ( "LidarParent" ).transform;
		isRay = false;
		isLayer = true;

		layers = new List<LineRenderer> ();
		lidar_points = new List<List<Vector3>> ();

		if ( isLayer )
		{
			for ( int j = 0; j < layerCount; j++ )
			{
				GameObject get_layer = (GameObject) Instantiate ( layer );
				get_layer.transform.SetParent ( LidarParent );
				layers.Add ( get_layer.GetComponent<LineRenderer> () );
				layers [ j ].numPositions = rayCount;
				layers [ j ].SetWidth ( lineWidth, lineWidth );
				lidar_points.Add ( new List<Vector3> ( new Vector3[rayCount] ) );
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		SenseDistance ();
	}

	public void SenseDistance()
	{
//		lidar_points.Clear();

		float angle_range = Mathf.PI;


		for (int j = 0; j < layerCount; j++)
		{
			List<Vector3> layer_points = lidar_points [ j ];

			for (int i = 0; i < rayCount; i++) 
			{
				LineRenderer lineRenderer;

				RaycastHit hit;

				float angle = -angle_range / 2 + i * ( angle_range ) / ( rayCount - 1 );

				Vector3 raycastDir = transform.TransformPoint ( ( j * 1.0f + 2.5f ) * Mathf.Cos ( angle ), -1.0f, ( j * 1.0f + 2.5f ) * Mathf.Sin ( angle ) ) - transform.position;
				Physics.Raycast ( transform.position, raycastDir, out hit );

				float base_angle = transform.eulerAngles.y * Mathf.Deg2Rad;
				Vector3 raycastRender = transform.TransformPoint ( ( j * 1.0f + 2.5f ) * Mathf.Cos ( base_angle + angle ), -1.0f, ( j * 1.0f + 2.5f ) * Mathf.Sin ( base_angle + angle ) ) - transform.position;

				if ( hit.collider )
				{
					Vector3 vector_point = transform.TransformPoint ( raycastRender.normalized * hit.distance * 4 );
					if ( isLayer )
					{
						layer_points [ i ] = vector_point;
					}
				} else
				{
					Vector3 vector_point = transform.TransformPoint ( raycastRender.normalized * hit.distance * 1000 );
					if ( isLayer )
					{
						layer_points [ i ] = vector_point;
					}
				}

			}
			if (isLayer) 
			{
				LineRenderer lineRenderer2 = layers [ j ];
				lineRenderer2.SetPositions ( layer_points.ToArray () );
			}
		}

	}
	public List<List<Vector3>> SendLidarData ()
	{
		return lidar_points;
	}

}