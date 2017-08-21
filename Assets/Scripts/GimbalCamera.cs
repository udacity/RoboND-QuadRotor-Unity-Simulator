using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FollowType { None, Position, Transform };

public class GimbalCamera : MonoBehaviour
{
	public Transform gimbalParent;

	Camera cam;
	Transform tr;
	Quaternion initialLocalRotation;

	FollowType followType;
	Transform followTarget;
	Vector3 followPosition;
	Quaternion lastRotation;


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
	}

	void LateUpdate ()
	{
		// manual input test
		if ( Input.GetMouseButtonDown ( 0 ) )
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
		}

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
		}
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
}