using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInputState : DroneState
{
	public float moveSpeed = 10;
	public float thrustForce = 25;
	public float maxTilt = 22.5f;
	public float tiltSpeed = 22.5f;
	public float turnSpeed = 90;

	Transform camTransform;
	Rigidbody rb;

	public override void OnEnter ()
	{
		base.OnEnter ();

		rb = motor.rb;
		motor.UseGravity = false;
		rb.isKinematic = true;
		rb.isKinematic = false;
		rb.freezeRotation = true;
		if ( camTransform == null )
			camTransform = FollowCamera.ActiveCamera.transform;
	}

	public override void OnUpdate ()
	{
		
	}

	public override void OnLateUpdate ()
	{
		if ( Input.GetKeyDown ( KeyCode.R ) )
		{
			motor.ResetOrientation ();
			control.followCam.ChangePoseType ( CameraPoseType.Iso );
		}

		Vector3 input = new Vector3 ( Input.GetAxis ( "Horizontal" ), Input.GetAxis ( "Thrust" ), Input.GetAxis ( "Vertical" ) );

		Vector3 inputVelo = new Vector3 ( input.x * moveSpeed, input.y * thrustForce, input.z * moveSpeed );

		Vector3 forward = transform.forward;
		forward.y = 0;
		Quaternion rot = Quaternion.LookRotation ( forward.normalized, Vector3.up );

		rb.velocity = rot * inputVelo;
		Vector3 euler = transform.localEulerAngles;
		euler.x = maxTilt * input.z;
		euler.z = maxTilt * -input.x;
		transform.localEulerAngles = euler;

		float yaw = Input.GetAxis ( "Yaw" );
		if ( yaw != 0 )
		{
			transform.Rotate ( Vector3.up * yaw * turnSpeed * Time.deltaTime, Space.World );
			camTransform.Rotate ( Vector3.up * yaw * turnSpeed * Time.deltaTime, Space.World );
		}
	}

	public override void OnExit ()
	{
		motor.rb.freezeRotation = false;
	}
}