using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
	public Vector3 rosDirection;
	public Vector3 rosRotation;

	Vector3 forward, right, up;
	Matrix4x4 toRos;
	Matrix4x4 toUnity;

	void Awake ()
	{
		toUnity = MatrixExtension.RosToUnity ();
//		toRos = transform.localToWorldMatrix.ToUnity ();
		toRos = toUnity.inverse;
//		Debug.Log ( "ToUnity: " + toUnity );
//		Debug.Log ( "ToRos: " + toRos );
//		Debug.Log ( transform.worldToLocalMatrix.ToUnity () );
//		Debug.Log ( transform.worldToLocalMatrix.ToUnity ().inverse );
//		Debug.Log ( "forward to ros: " + Vector3.forward.ToRos () );
//		Quaternion q = QuaternionToRos ( transform.rotation );
//		Vector3 v = q * Vector3.forward;
//		Debug.Log ( "v " + v );
//		Debug.Log ( "rot " + transform.rotation + " q " + q );
//		Debug.Log ( "forward " + Vector3.up.ToRos () );
	}

	void LateUpdate ()
	{
		Vector3 rosForward = transform.forward.ToRos ();
		Vector3 rosRight = transform.right.ToRos ();
		Vector3 rosUp = transform.up.ToRos ();
		Quaternion q = transform.rotation.ToRos ();
		Debug.DrawRay ( transform.position, rosForward * 5, Color.blue );
		Debug.DrawRay ( transform.position, rosRight * 5, Color.red );
		Debug.DrawRay ( transform.position, rosUp * 5, Color.green );

		Vector3 up = Vector3.up * 0.2f;
		Debug.DrawRay ( transform.position + up, rosForward.ToUnity () * 5, Color.cyan );
		Debug.DrawRay ( transform.position + up, rosRight.ToUnity () * 5, Color.magenta );
		Debug.DrawRay ( transform.position + up, rosUp.ToUnity () * 5, Color.yellow );
//		Debug.DrawRay ( transform.position, q * Vector3.up * 5, Color.magenta );

		transform.rotation = Quaternion.identity;

		transform.Rotate ( rosRotation.ToUnity () );

		return;



		Debug.DrawRay ( transform.position, Vector3.forward * 5, Color.blue );
		Debug.DrawRay ( transform.position, Vector3.right * 5, Color.red );
		Debug.DrawRay ( transform.position, Vector3.up * 5, Color.green );

		Matrix4x4 mat = Matrix4x4.identity;
		mat.SetTRS ( rosDirection, Quaternion.identity, Vector3.one );
//		Vector3 unityDirection = Matrix4x4.identity.ToUnity ().MultiplyPoint3x4 ( rosDirection );
		Vector3 unityDirection = mat.ToUnity ().ExtractPosition ();
		Debug.DrawRay ( transform.position, unityDirection * 5, Color.white );
		mat.SetTRS ( Vector3.forward, Quaternion.identity, Vector3.one );
//		unityDirection = Matrix4x4.identity.ToUnity ().MultiplyPoint3x4 ( Vector3.forward );
		unityDirection = mat.ToUnity ().ExtractPosition ();
		Debug.DrawRay ( transform.position, unityDirection * 5, Color.magenta );



//		Vector3 v = ( toUnity * new Vector3 ( 0, 5, 0 ) * toRos );
//		Vector3 unityDirection = toUnity * rosDirection.normalized;
//		Vector3 rosDir = toRos * rosDirection.normalized;

//		Debug.DrawRay ( transform.position, unityDirection * 5, Color.magenta );
//		Debug.DrawRay ( transform.position, rosDir * 5, Color.cyan );

//		transform.rotation = 

//		toRos = transform.localToWorldMatrix.ToUnity ();
//		toUnity = toRos.inverse;

//		forward = toUnity * Vector3.forward;
//		right = toUnity * Vector3.right;
//		up = toUnity * Vector3.up;
//		forward = toRos * Vector3.forward;
//		right = toRos * Vector3.right;
//		up = toRos * Vector3.up;
//		forward = toRos * transform.forward;
//		right = toRos * transform.right;
//		up = toRos * transform.up;
//		Debug.DrawRay ( transform.position, forward * 5, Color.magenta );
//		Debug.DrawRay ( transform.position, right * 5, Color.cyan );
//		Debug.DrawRay ( transform.position, up * 5, Color.yellow );
	}
}