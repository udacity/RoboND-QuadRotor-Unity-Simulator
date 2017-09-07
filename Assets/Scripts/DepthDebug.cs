using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthDebug : MonoBehaviour {

	public bool debugText=false;
	public bool drawRays=false;
	Camera depth_cam;
	Transform depth_camera;
	Transform hit_target;
	float ray_length = 100f;//*1.6243f;

	void Awake ()
	{
		depth_cam = GameObject.Find("DepthCam").GetComponent<Camera>();
		depth_camera = depth_cam.transform;
	}

	// Update is called once per frame
	void Update ()
	{
		CastRay();
	}

	void CastRay()
	{
		RaycastHit hit1, hit2, hit3, hit4, hit5;

		Vector3 fwd = depth_camera.TransformDirection(Vector3.forward) * ray_length;

		Ray botLeft = depth_cam.ScreenPointToRay(new Vector3(0, 0, 0));
		Ray topLeft = depth_cam.ScreenPointToRay(new Vector3(0, 256, 0));
		Ray topRight = depth_cam.ScreenPointToRay(new Vector3(256, 256, 0));
		Ray botRight = depth_cam.ScreenPointToRay(new Vector3(256, 0, 0));

		if(Physics.Raycast(botLeft, out hit1, ray_length))
		{
			if(debugText)
			Debug.Log("Bottom-Left Ray hit: " + hit1.transform.name + " distance: " + hit1.distance + " point: " + hit1.point);
		}
		else
		{
			if(debugText)
			Debug.Log("Bottom-Left Ray hit nothing");
		}
		if(Physics.Raycast(topLeft, out hit2, ray_length))
		{
			if(debugText)
			Debug.Log("Top-Left Ray hit: " + hit2.transform.name + " distance: " + hit2.distance + " point: " + hit2.point);
		}
		else
		{
			if(debugText)
			Debug.Log("Top-Left Ray hit nothing");
		}
		if(Physics.Raycast(topRight, out hit3, ray_length))
		{
			if(debugText)
			Debug.Log("Top-Right Ray hit: " + hit3.transform.name + " distance: " + hit3.distance + " point: " + hit3.point);
		}
		else
		{
			if(debugText)
			Debug.Log("Top-Right Ray hit nothing");
		}
		if(Physics.Raycast(botRight, out hit4, ray_length))
		{
			if(debugText)
			Debug.Log("Bottom-Right Ray hit: " + hit4.transform.name + " distance: " + hit4.distance + " point: " + hit4.point);
		}
		else
		{
			if(debugText)
			Debug.Log("Bottom-Right Ray hit nothing");
		}
		if(Physics.Raycast(depth_camera.position, fwd, out hit5, ray_length))
		{
			if(debugText)
			Debug.Log("Center Ray hit: " + hit5.transform.name + " distance: " + hit5.distance + " point: " + hit5.point);
		}
		else
		{
			if(debugText)
			Debug.Log("Center Ray hit nothing");
		}

		if(debugText)
		Debug.Log("Quad: " + transform.position);

		if(drawRays)
		{
			Debug.DrawRay(botLeft.origin, botLeft.direction*ray_length, Color.blue);
			Debug.DrawRay(topLeft.origin, topLeft.direction*ray_length, Color.green);
			Debug.DrawRay(topRight.origin, topRight.direction*ray_length, Color.green);
			Debug.DrawRay(botRight.origin, botRight.direction*ray_length, Color.blue);
			Debug.DrawRay(depth_camera.position, fwd, Color.red);
		}

	}

}
