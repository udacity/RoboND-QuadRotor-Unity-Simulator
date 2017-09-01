using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FollowType { None, Position, Transform, Sweep };

public class GimbalCamera : MonoBehaviour
{
	public Vector3 Position { get; protected set; }
	public Quaternion Rotation { get; protected set; }
	public Transform gimbalParent;
	[Range (5, 180)]
	public float sweepCone = 90;
	public float sweepSpeed = 0.5f;
//	public bool verticalSweep;
//	public float vSweepCone = 45;



	Camera cam;
	Transform tr;
	Quaternion initialLocalRotation;

	FollowType followType;
	Transform followTarget;
	Vector3 followPosition;
	Quaternion lastRotation;
	float sweepAccum;

	Transform vizSphere;

	void Awake ()
	{
		cam = GetComponent<Camera> ();
		tr = transform;
		if ( gimbalParent == null )
			gimbalParent = tr;
		initialLocalRotation = gimbalParent.localRotation;
		lastRotation = gimbalParent.rotation;
		followType = FollowType.None;
		vizSphere = GameObject.CreatePrimitive ( PrimitiveType.Sphere ).transform;
		vizSphere.localScale = Vector3.one * 0.5f;
		vizSphere.GetComponent<Renderer> ().material.color = Color.blue * 0.5f;
		Destroy ( vizSphere.GetComponent<Collider> () );
	}

	void LateUpdate ()
	{
		Vector3 v = Vector3.RotateTowards ( tr.forward, Vector3.up, cam.fieldOfView * Mathf.Deg2Rad * 0.5f, 0 );
		v = v.normalized * cam.farClipPlane;
		Debug.DrawRay ( tr.position, v, Color.green, Time.deltaTime, false );
		// manual input test
/*		if ( Input.GetMouseButtonDown ( 0 ) )
		{
			Vector3 point = Input.mousePosition;
			point.z = FollowCamera.ActiveCamera.cam.nearClipPlane;
			Ray ray = FollowCamera.ActiveCamera.cam.ScreenPointToRay ( point );
			RaycastHit hit;
			LayerMask mask = 1 << LayerMask.NameToLayer ( "Environment" );
			if ( Physics.Raycast ( ray, out hit, Mathf.Infinity, mask.value ) )
			{
				LookAt ( hit.point );
				Debug.DrawLine ( gimbalParent.position, hit.point, Color.red, 3 );
				Debug.DrawRay ( hit.point, Vector3.up, Color.green, 3 );
			}

		} else
		if ( Input.GetMouseButtonDown ( 1 ) )
		{
			StopLooking ();
		} else
		if ( Input.GetMouseButtonDown ( 2 ) )
		{
			Sweep ();
		}*/

		switch ( followType )
		{
		case FollowType.None:
//			gimbalParent.rotation = lastRotation;
			break;

		case FollowType.Position:
			gimbalParent.LookAt ( followPosition, Vector3.up );
			vizSphere.position = followPosition;
			break;

		case FollowType.Transform:
			gimbalParent.LookAt ( followTarget.position, Vector3.up );
			vizSphere.position = followTarget.position;
			break;

		case FollowType.Sweep:
			sweepAccum += sweepSpeed * Time.deltaTime;

			if ( sweepAccum > sweepCone )
			{
				sweepAccum = sweepCone;
				sweepSpeed *= -1;
			} else
			if ( sweepAccum < -sweepCone )
			{
				sweepAccum = -sweepCone;
				sweepSpeed *= -1;
			}
			Vector3 euler = gimbalParent.eulerAngles;
			euler.y = sweepAccum;
			gimbalParent.eulerAngles = euler;
			break;
		}

		Position = tr.position;
		Rotation = tr.rotation;
	}

	public void LookAt (Vector3 position)
	{
		followType = FollowType.Position;
		followPosition = position;
		vizSphere.gameObject.SetActive ( true );
	}

	public void LookAt (Transform target)
	{
		followType = FollowType.Transform;
		followTarget = target;
		vizSphere.gameObject.SetActive ( true );
	}

	public void StopLooking ()
	{
		followType = FollowType.None;
		lastRotation = gimbalParent.rotation;
		vizSphere.gameObject.SetActive ( false );
	}

	public void Sweep ()
	{
		followType = FollowType.Sweep;
		gimbalParent.localRotation = Quaternion.identity;
		vizSphere.gameObject.SetActive ( false );
		sweepAccum = 0;
	}
}