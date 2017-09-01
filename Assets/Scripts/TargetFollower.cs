using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class TargetFollower : MonoBehaviour
{
	public QuadController quad;
	public SimpleQuadController inputCtrl;
	public LayerMask mask;
	public bool active;
	public bool following;

	public float maxSpeed = 10;
	public float rotationSpeed = 5;
	public float minDistFromTarget = 5;


	Transform tr;
	Rigidbody rb;
	Vector3 followPoint;

	void Awake ()
	{
		tr = transform;
		rb = GetComponent<Rigidbody> ();
	}

	void LateUpdate ()
	{
		// test target following with left click
//		if ( Input.GetMouseButtonDown ( 0 ) )
//		{
//			Ray ray = FollowCamera.ActiveCamera.cam.ScreenPointToRay ( Input.mousePosition );
//			RaycastHit hit;
//			if ( Physics.Raycast ( ray, out hit, Mathf.Infinity, mask.value ) )
//			{
//				SetFollowPoint ( hit.point );
//				inputCtrl.active = false;
//			}
//		}

		// test marker maker with middle click
		if ( Input.GetMouseButtonDown ( 2 ) )
		{
			Ray ray = FollowCamera.ActiveCamera.cam.ScreenPointToRay ( Input.mousePosition );
			RaycastHit hit;
			if ( Physics.Raycast ( ray, out hit, Mathf.Infinity, mask.value ) )
			{
				Color c = Random.ColorHSV ();
				c.a = Random.value * 0.5f + 0.5f;
				MarkerMaker.AddMarker ( Time.time.ToString (), hit.point, Quaternion.LookRotation ( hit.point - FollowCamera.ActiveCamera.transform.position, FollowCamera.ActiveCamera.transform.up ), Vector3.one, c, Random.Range ( 3f, 8f ) );
			}
		}

		if ( active && following )
		{
			Vector3 toTarget = followPoint - transform.position;
			if ( toTarget.sqrMagnitude < minDistFromTarget * minDistFromTarget )
			{
				following = false;
				inputCtrl.active = true;
				return;
			}

			UpdateSteering ();
		}
	}

	void UpdateSteering ()
	{
		Vector3 force = Vector3.zero;
		AdjustTilt ();
		force += Seek ();
//		force += Arrive ();
		force = Vector3.ClampMagnitude ( force, maxSpeed );
		rb.velocity = force;
	}

	Vector3 Seek ()
	{
		Vector3 toTarget = followPoint - tr.position;
		Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
		return desiredVelocity - rb.velocity;
	}

	void AdjustTilt ()
	{
		Vector3 toTarget = followPoint - tr.position;
		Quaternion q1 = Quaternion.LookRotation ( quad.Forward, tr.up );
		Quaternion q2 = Quaternion.LookRotation ( toTarget );
		Quaternion qOffset = Quaternion.FromToRotation ( quad.Forward, tr.forward );

		tr.rotation = qOffset * Quaternion.RotateTowards ( q1, q2, rotationSpeed * Time.deltaTime );

		float stoppingDist = minDistFromTarget * minDistFromTarget;
		Vector3 axis = Vector3.Cross ( quad.Forward, toTarget );
		if ( toTarget.sqrMagnitude > stoppingDist )
		{

		} else
		{

		}

	}

	public void SetFollowPoint (Vector3 point)
	{
		followPoint = point;
		following = true;
		inputCtrl.active = false;
	}
}