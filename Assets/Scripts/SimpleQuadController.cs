using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleQuadController : MonoBehaviour
{
	public Vector3 LastTargetPoint { get { return lastTargetPoint; } }

	public static SimpleQuadController ActiveController;
	public Transform chassis;
	public Transform camTransform;
	public QuadMotor controller;
	public FollowCamera followCam;
	public PathFollower pather;
	public GimbalCamera gimbal;
	public TargetFollower follower;

	public float moveSpeed = 10;
	public float thrustForce = 25;
	public float maxTilt = 22.5f;
	public float tiltSpeed = 22.5f;
	public float turnSpeed = 90;
	public StateController stateController;
	public bool allowPathPlanning;

	Rigidbody rb;
	float tiltX;
	float tiltZ;

	public bool localInput;
	Vector3 lastTargetPoint;
	
	void Awake ()
	{
		ActiveController = this;
		rb = GetComponent<Rigidbody> ();
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		if ( controller == null )
			controller = GetComponent<QuadMotor> ();
		if ( followCam == null )
			followCam = camTransform.GetComponent<FollowCamera> ();
	}

	void Start ()
	{
//		stateController.SetState ( "Local" );
		stateController.SetState ( "Patrol" );
	}

	void LateUpdate ()
	{
		if ( Input.GetKeyDown ( KeyCode.F12 ) )
		{
			if ( stateController.IsCurrentStateName ( "Local" ) )
				stateController.RevertState ();
			else
				stateController.SetState ( "Local" );
				
		}
		if ( Input.GetMouseButtonDown ( 0 ) )
		{
			OnTargetDetected ( Vector3.zero );
//			Ray ray = followCam.cam.ScreenPointToRay ( Input.mousePosition );
//			RaycastHit hit;
//			if ( Physics.Raycast ( ray, out hit ) )
//			{
//				follower.SetFollowPoint ( hit.point );
//			}
		}
		if ( Input.GetMouseButtonDown ( 2 ) )
			OnTargetLost ();
	}

/*	void LateUpdate ()
	{
		if ( Input.GetKeyDown ( KeyCode.F12 ) )
		{
			localInput = !localInput;
			if ( localInput )
			{
				controller.UseGravity = false;
				controller.rb.isKinematic = true;
				controller.rb.isKinematic = false;
				controller.rb.freezeRotation = true;
			} else
				controller.rb.freezeRotation = false;
		}

		if ( Input.GetKeyDown ( KeyCode.R ) )
		{
			controller.ResetOrientation ();
			followCam.ChangePoseType ( CameraPoseType.Iso );
		}

		if ( Input.GetKeyDown ( KeyCode.G ) )
		{
			controller.UseGravity = !controller.UseGravity;
		}

		if ( !localInput )
			return;
		
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

		if ( allowPathPlanning )
		{
			if ( Input.GetKeyDown ( KeyCode.P ) )
			{
				PathPlanner.AddNode ( controller.Position, controller.Rotation );
			}
			if ( Input.GetKeyDown ( KeyCode.O ) )
			{
				controller.ResetOrientation ();
				pather.SetPath ( new Pathing.Path ( PathPlanner.GetPath () ) );
				PathPlanner.Clear ( false ); // clear the path but keep the visualization
			}
			if ( Input.GetKeyDown ( KeyCode.I ) )
			{
				PathPlanner.Clear ();
			}
		}
	}*/

	void OnGUI ()
	{
		GUI.backgroundColor = localInput ? Color.green : Color.red;
//		GUI.contentColor = Color.white;
		Rect r = new Rect ( 10, Screen.height - 100, 60, 25 );
		if ( GUI.Button ( r, "Input " + ( localInput ? "on" : "off" ) ) )
		{
			if ( stateController.IsCurrentStateName ( "Local" ) )
				stateController.RevertState ();
			else
				stateController.SetState ( "Local" );
		}
	}

	public void OnTargetDetected (Vector3 point)
	{
		lastTargetPoint = point;
		stateController.SetState ( "Follow" );
	}

	public void OnTargetLost ()
	{
		stateController.SetState ( "Patrol" );
	}
}