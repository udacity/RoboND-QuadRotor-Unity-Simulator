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
	bool showError;
	float errorTime;
	string error;
	public Color warningColor;

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
		if ( showError && Time.unscaledTime - errorTime >= 3f )
			showError = false;
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
			if ( PatrolPathManager.Count < 2 )
			{
				showError = true;
				errorTime = Time.unscaledTime;
				error = "Patrol path needs at least 2 waypoints";
				return;
			}
			if ( HeroPathManager.Count < 2 )
			{
				showError = true;
				errorTime = Time.unscaledTime;
				error = "Hero path needs at least 2 waypoints";
				return;
			}
			if ( SpawnPointManager.Count < 1 )
			{
				showError = true;
				errorTime = Time.unscaledTime;
				error = "Need at least 1 crowd spawn point";
				return;
			}
			showError = false;
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

		if ( showError )
		{
			float a = 1;
			float delta = Time.unscaledTime - errorTime;
			if ( delta < 0.15f )
				a = delta / 0.15f;
			else
			if ( delta > 2.7f )
				a = ( 3f - delta ) / 0.3f;

			float heightRatio = 1f * Screen.height / 1080f;
			GUI.skin.label.fontSize = (int) ( heightRatio * 32 );
			GUI.skin.label.clipping = TextClipping.Overflow;
			GUI.skin.label.wordWrap = false;
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			Vector2 size = new Vector2 ( 100, 17.5f ) * heightRatio;
			r = new Rect ( Screen.width / 2 - size.x, 0.5f * Screen.height - size.y, size.x * 2, size.y * 2 );
			GUI.color = new Color ( 0, 0, 0, a );
			GUI.Label ( r, error );
			r.x--;
			r.y--;
			warningColor.a = a;
			GUI.color = warningColor;
			GUI.Label ( r, error );
		}
	}
}
