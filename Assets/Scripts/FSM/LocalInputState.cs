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
	public GameObject spawner;

	Transform camTransform;
	Transform tr;
	Rigidbody rb;

	public override void OnEnter ()
	{
		base.OnEnter ();

		tr = motor.transform;
		rb = motor.rb;
		motor.UseGravity = false;
		rb.isKinematic = true;
		rb.isKinematic = false;
		rb.freezeRotation = true;
		if ( camTransform == null )
			camTransform = FollowCamera.ActiveCamera.transform;
		gimbal.StopLooking ();
	}

//	public override void OnUpdate ()
//	{
//		
//	}

	public override void OnLateUpdate ()
	{
//		Debug.Log ( "local" );
		if ( Input.GetKeyDown ( KeyCode.G ) )
		{
			motor.ResetOrientation ();
			control.followCam.ChangePoseType ( CameraPoseType.Iso );
		}

		if ( Input.GetKeyDown ( KeyCode.P ) )
		{
			PatrolPathManager.AddNode ( motor.Position, motor.Rotation );
		}

		if ( Input.GetKeyDown ( KeyCode.O ) )
		{
			HeroPathManager.AddNode ( motor.Position, motor.Rotation );
		}

		if ( Input.GetKeyDown ( KeyCode.I ) )
		{
			SpawnPointManager.AddNode ( motor.Position, motor.Rotation );
		}

		if ( Input.GetKeyDown ( KeyCode.N ) )
		{
			spawner.SetActive(false);
		}

		if ( Input.GetKeyDown ( KeyCode.M ) )
		{
		
			spawner.SetActive(true);

		}

		if ( Input.GetKeyDown ( KeyCode.L ) )
		{
			PatrolPathManager.Clear ();
		}

		if ( Input.GetKeyDown ( KeyCode.K ) )
		{
			HeroPathManager.Clear ();
		}

		if ( Input.GetKeyDown ( KeyCode.J ) )
		{
			SpawnPointManager.Clear ();
		}

		Vector3 input = new Vector3 ( Input.GetAxis ( "Horizontal" ), Input.GetAxis ( "Thrust" ), Input.GetAxis ( "Vertical" ) );

		Vector3 inputVelo = new Vector3 ( input.x * moveSpeed, input.y * thrustForce, input.z * moveSpeed );

		Vector3 forward = tr.forward;
		forward.y = 0;
		Quaternion rot = Quaternion.LookRotation ( forward.normalized, Vector3.up );

		rb.velocity = rot * inputVelo;
		Vector3 euler = tr.localEulerAngles;
		euler.x = maxTilt * input.z;
		euler.z = maxTilt * -input.x;
		tr.localEulerAngles = euler;

		float yaw = Input.GetAxis ( "Yaw" );
		if ( yaw != 0 )
		{
			tr.Rotate ( Vector3.up * yaw * turnSpeed * Time.deltaTime, Space.World );
			camTransform.Rotate ( Vector3.up * yaw * turnSpeed * Time.deltaTime, Space.World );
		}
	}

	public override void OnExit ()
	{
//		motor.rb.freezeRotation = false;
	}

	void OnGUI ()
	{
		Rect r = new Rect ( 10, Screen.height - 35, 150, 25 );
		GUILayout.BeginArea ( r );
		GUILayout.Box ( "Local input is on" );
		GUILayout.EndArea ();
	}
}
