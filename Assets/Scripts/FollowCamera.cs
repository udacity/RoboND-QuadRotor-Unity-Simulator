using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Ros_CSharp;
using Messages;
using Empty = Messages.std_srvs.Empty;
using SetInt = Messages.quad_controller.SetInt;
using SetFloat = Messages.quad_controller.SetFloat;

public enum CameraPoseType
{
	XNorm,
	YNorm,
	ZNorm,
	Iso,
	Free
}

public class FollowCamera : MonoBehaviour
{
	public static FollowCamera ActiveCamera;
	public QuadMotor target;
	public CameraMotionBlur blurScript;
	public float followDistance = 5;
	public float height = 4;
	public float zoomSpeed = 4;
	public float rotateSpeed = 2;
	public float minDist = 1.5f;
	public float maxDist = 20f;

	public bool autoAlign = false;
//	public Vector3 forward;
	public CameraPoseType poseType;

	public bool blurRotors = true;

	NodeHandle nh;
	ServiceServer distanceSrv;
	ServiceServer poseTypeSrv;

	[HideInInspector]
	public Camera cam;
	bool setRotationFlag;
	Quaternion targetRotation;
	float initialFollowDistance;

	float rmbTime;

	void Awake ()
	{
		if ( ActiveCamera == null )
			ActiveCamera = this;
		initialFollowDistance = followDistance;
		cam = GetComponent<Camera> ();
		cam.depthTextureMode |= DepthTextureMode.MotionVectors;
	}

	void Start ()
	{
//		forward = target.Forward;
		if ( ROSController.instance != null )
			ROSController.StartROS ( OnRosInit );
	}

	void LateUpdate ()
	{
		if ( setRotationFlag )
		{
			setRotationFlag = false;
			transform.rotation = targetRotation;
		}

		Vector3 look = target.Position + Vector3.up * followDistance / 3;
		transform.position = look - transform.forward * followDistance;
//		transform.position = target.Position - transform.forward * followDistance + Vector3.up * height;
		if ( blurRotors )
		{
			float forcePercent = Mathf.Abs ( target.Force.y / target.maxForce );
			blurScript.velocityScale = forcePercent * forcePercent * forcePercent;
			if ( !blurScript.enabled )
				blurScript.enabled = true;
		} else
		{
			if ( blurScript.enabled )
				blurScript.enabled = false;
		}

		float scroll = Input.GetAxis ( "Mouse ScrollWheel" );
		float zoom = -scroll * zoomSpeed;
		followDistance += zoom;
		followDistance = Mathf.Clamp ( followDistance, minDist, maxDist );

		if ( Input.GetMouseButtonDown ( 1 ) )
			rmbTime = Time.time;
		if ( Input.GetMouseButtonUp ( 1 ) && Time.time - rmbTime < 0.1f )
		{
//			transform.position = look - target.transform.forward * followDistance;
			transform.rotation = Quaternion.Euler ( new Vector3 ( 30, target.transform.eulerAngles.y, 0 ) );
//			transform.rotation = Quaternion.LookRotation ( target.transform.forward - Vector3.up, target.transform.forward + Vector3.up );
		}

		if ( Input.GetMouseButton ( 1 ) && Time.time - rmbTime > 0.2f )
		{
			float x = Input.GetAxis ( "Mouse X" );
			transform.RotateAround ( look, Vector3.up, x * rotateSpeed );
//			transform.RotateAround ( target.Position, Vector3.up, x * rotateSpeed );
			float y = Input.GetAxis ( "Mouse Y" );
			transform.RotateAround ( look, transform.right, -y * rotateSpeed );
//			transform.RotateAround ( target.Position, transform.right, -y * rotateSpeed );
		}

//		if ( Input.GetKeyDown ( KeyCode.Alpha1 ) || Input.GetKeyDown ( KeyCode.Alpha2 ) || Input.GetKeyDown ( KeyCode.Alpha3 ) || Input.GetKeyDown ( KeyCode.Alpha4 ) || Input.GetKeyDown ( KeyCode.Alpha5 ) )
//		{
//			int pose = 0;
//			if ( Input.GetKeyDown ( KeyCode.Alpha2 ) )
//				pose = 1;
//			if ( Input.GetKeyDown ( KeyCode.Alpha3 ) )
//				pose = 2;
//			if ( Input.GetKeyDown ( KeyCode.Alpha4 ) )
//				pose = 3;
//			if ( Input.GetKeyDown ( KeyCode.Alpha5 ) )
//				pose = 4;
//			new System.Threading.Thread ( () =>
//			{
//				SetInt.Request req = new SetInt.Request ();
//				SetInt.Response resp = new SetInt.Response ();
//				req.data = pose;
//
//				if ( nh.serviceClient<SetInt.Request, SetInt.Response> ( "/quad_rotor/camera_pose_type" ).call ( req, ref resp ) )
//					Debug.Log ( resp.success + " " + resp.newData );
//				else
//					Debug.Log ( "Failed" );
//			} ).Start ();
//		}
	}

	public void ChangePoseType (CameraPoseType newType)
	{
		poseType = newType;

		switch ( poseType )
		{
		case CameraPoseType.XNorm:
			targetRotation = Quaternion.LookRotation ( Vector3.forward, Vector3.up );
			break;

		case CameraPoseType.YNorm:
			targetRotation = Quaternion.LookRotation ( -Vector3.right, Vector3.up );
			break;

		case CameraPoseType.ZNorm:
			targetRotation = Quaternion.LookRotation ( -Vector3.up, Vector3.forward.normalized );
			break;

		case CameraPoseType.Iso:
		case CameraPoseType.Free:
			targetRotation = Quaternion.Euler ( new Vector3 ( 30, target.transform.eulerAngles.y, 0 ) );
//			targetRotation = Quaternion.LookRotation ( ( Vector3.forward - Vector3.up ).normalized, ( Vector3.forward + Vector3.up ).normalized );
			break;
		}

		setRotationFlag = true;
	}

	void OnRosInit ()
	{
		nh = ROS.GlobalNodeHandle;
		poseTypeSrv = nh.advertiseService<SetInt.Request, SetInt.Response> ( "/quad_rotor/camera_pose_type", SetCameraPoseType );
		distanceSrv = nh.advertiseService<SetFloat.Request, SetFloat.Response> ( "/quad_rotor/camera_distance", SetFollowDistance );
	}

	bool SetFollowDistance (SetFloat.Request req, ref SetFloat.Response resp)
	{
		followDistance = req.data;

		resp.newData = followDistance;
		resp.success = true;

		return true;
	}

	bool SetCameraPoseType (SetInt.Request req, ref SetInt.Response resp)
	{
		ChangePoseType ( (CameraPoseType) req.data );

		resp.newData = (int) poseType;
		resp.success = true;

		return true;
	}
}