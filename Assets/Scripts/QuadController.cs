using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class QuadController : MonoBehaviour
{
	const float MPHToMS = 2.23693629205f;
	public static QuadController ActiveController;
	public static int ImageWidth = 640;
	public static int ImageHeight = 480;

	public bool MotorsEnabled { get; set; }
	public Vector3 Force { get { return force; } }
	public Vector3 Torque { get { return torque; } }
	public Vector3 Position { get; protected set; }
	public Quaternion Rotation { get; protected set; }
	public Vector3 AngularVelocity { get; protected set; }
	public Vector3 LinearVelocity { get; protected set; }
	public Vector3 LinearAcceleration { get; protected set; }
	public Vector3 Forward { get; protected set; }
	public Vector3 Right { get; protected set; }
	public Vector3 Up { get; protected set; }
	public Vector3 YAxis { get; protected set; }
	public Vector3 XAxis { get; protected set; }
	public bool UseGravity { get; set; }
	public bool ConstrainForceX { get; set; }
	public bool ConstrainForceY { get; set; }
	public bool ConstrainForceZ { get; set; }
	public bool ConstrainTorqueX { get; set; }
	public bool ConstrainTorqueY { get; set; }
	public bool ConstrainTorqueZ { get; set; }

	public Transform frontLeftRotor;
	public Transform frontRightRotor;
	public Transform rearLeftRotor;
	public Transform rearRightRotor;
	public Transform yAxis;
	public Transform xAxis;
	public Transform forward;
	public Transform right;

	public Camera droneCam1;
	public PathFollower pather;
	public SimpleQuadController inputCtrl;

	public bool clampForce = true;
	public bool clampTorque = true;
	public float maxForce = 100;
	public float maxTorqueDegrees = 17;
	public float maxTorqueRadians;
//	public bool clampMaxSpeed = true;
//	public bool clampAngularVelocity = true;
//	public float maxSpeedMPH = 60;
//	public float maxSpeedMS;
//	public float maxAngularDegrees = 17;
//	public float maxAngularRadians;
//	public float thrustForce = 2000;
//	public float torqueForce = 500;
	public ForceMode forceMode = ForceMode.Force;
	public ForceMode torqueMode = ForceMode.Force;

	public Texture2D[] axisArrows;
	public Color[] axisColors;
	public float arrowScreenSize = 100f;
	public bool drawArrows;
	public bool drawArrowsAlways;
	public bool showLegend;

	public bool rotateWithTorque;

	public bool spinRotors = true;
	public float maxRotorRPM = 3600;
	[SerializeField]
	float curRotorSpeed;

	// recording vars
	public float pathRecordFrequency = 3;
	[System.NonSerialized]
	public bool isRecordingPath;
	float nextNodeTime;

	[System.NonSerialized]
	public Rigidbody rb;
	Transform[] rotors;
	Vector3 force;
	Vector3 torque;
	Vector3 lastVelocity;
	Ray ray;
	RaycastHit rayHit;
	BinarySerializer b = new BinarySerializer ( 1000 );

	[SerializeField]
	float curSpeed;

	byte[] cameraData;
	bool resetFlag;
	bool setPoseFlag;
	bool useTwist;

	Vector3 posePosition;
	Quaternion poseOrientation;
	Texture2D dot;

	void Awake ()
	{
		if ( ActiveController == null )
			ActiveController = this;
		rb = GetComponent<Rigidbody> ();
		rotors = new Transform[4] {
			frontLeftRotor,
			frontRightRotor,
			rearLeftRotor,
			rearRightRotor
		};
		MotorsEnabled = true;
//		UseGravity = false;
		Forward = forward.forward;
		Right = right.forward;
		Up = transform.up;
		CreateCameraTex ();
		transform.position = Vector3.up * 10;
		UseGravity = rb.useGravity;
		UpdateConstraints ();
		rb.maxAngularVelocity = Mathf.Infinity;
		inputCtrl = GetComponent<SimpleQuadController> ();
//		dot = new Texture2D ( 1, 1 );
//		dot.SetPixel ( 0, 0, Color.white );
//		dot.Apply ();
//		Debug.Log ( "it: " + rb.inertiaTensor + " itr: " + rb.inertiaTensorRotation );
//		rb.ResetInertiaTensor ();
//		rb.inertiaTensorRotation = Quaternion.identity;
//		Debug.Log ( "2 it: " + rb.inertiaTensor + " itr: " + rb.inertiaTensorRotation );
//		gameObject.SetActive ( false );
//		gameObject.SetActive ( true );
	}

	void Start ()
	{
		rb.inertiaTensorRotation = Quaternion.identity;
		// for whatever reason, setting inertiaTensorRotation stops the quad from accepting commands (mostly torque) until it's deactivated and activated
		QuadActivator.Activate ( gameObject );
	}

	void Update ()
	{
		if ( resetFlag )
		{
			ResetOrientation ();
			resetFlag = false;
		}
		CheckSetPose ();

		Position = transform.position;
		Rotation = transform.rotation;
		Forward = forward.forward;
		Right = right.forward;
		Up = transform.up;
		XAxis = xAxis.forward;
		YAxis = yAxis.forward;

		if ( isRecordingPath && Time.time > nextNodeTime )
		{
			PathPlanner.AddNode ( Position, Rotation );
			nextNodeTime = Time.time + pathRecordFrequency;
		}
	}

	void LateUpdate ()
	{
		if ( resetFlag )
		{
			ResetOrientation ();
			resetFlag = false;
		}
		CheckSetPose ();

		if ( Input.GetKeyDown ( KeyCode.Escape ) )
		{
			#if !UNITY_EDITOR
			ROSController.StopROS ( new System.Action ( () => { Application.Quit (); } ) );
			#endif
		}
		
		if ( Input.GetKeyDown ( KeyCode.L ) )
			showLegend = !showLegend;

		// use this to have a follow camera rotate with the quad. not proper torque!
		if ( rotateWithTorque )
		{
			float zAngle = 0;
			Vector3 up = transform.up;
			if ( up.y >= 0 )
				zAngle = transform.localEulerAngles.z;
			else
				zAngle = -transform.localEulerAngles.z;
			while ( zAngle > 180 )
				zAngle -= 360;
			while ( zAngle < -360 )
				zAngle += 360;
			transform.Rotate ( Vector3.up * -zAngle * Time.deltaTime, Space.World );
		}

		// spin rotors if we need
		if ( spinRotors )
		{
			float rps = maxRotorRPM / 60f;
			float degPerSec = rps * 360f;
			if ( useTwist )
			{
				curRotorSpeed = Mathf.InverseLerp ( Physics.gravity.y, -Physics.gravity.y, rb.velocity.y ) * degPerSec;
//				curRotorSpeed = 0.5f * degPerSec * ( rb.velocity.y + Physics.gravity.y ) / -Physics.gravity.y / rb.mass;
			} else
			{
				curRotorSpeed = 0.5f * degPerSec * force.y / -Physics.gravity.y / rb.mass;
			}
//			curRotorSpeed = degPerSec * force.y / thrustForce;

			// use forward for now because rotors are rotated -90x
			Vector3 rot = Vector3.forward * curRotorSpeed * Time.deltaTime;
			frontLeftRotor.Rotate ( rot );
			frontRightRotor.Rotate ( -rot );
			rearLeftRotor.Rotate ( -rot );
			rearRightRotor.Rotate ( rot );
		}
	}

	void FixedUpdate ()
	{
		if ( resetFlag )
		{
			ResetOrientation ();
			resetFlag = false;
		}
		CheckSetPose ();

		rb.useGravity = UseGravity;
		CheckConstraints ();

		if ( MotorsEnabled )
		{
			if ( useTwist )
			{
				// just set linear and angular velocities, ignoring forces
				rb.velocity = LinearVelocity;
//				rb.velocity = clampMaxSpeed ? Vector3.ClampMagnitude ( LinearVelocity, maxSpeedMS ) : LinearVelocity;
				// new: flip angular velocity to generate CCW rotations
//				Vector3 angVel = -AngularVelocity;
//				if ( ConstrainTorqueX )
//					angVel.z = 0;
//				if ( ConstrainTorqueY )
//					angVel.x = 0;
//				if ( ConstrainTorqueZ )
//					angVel.y = 0;
//				rb.angularVelocity = angVel;
				rb.angularVelocity = -AngularVelocity;

			} else
			{

				// add force
				if ( clampForce )
					force = Vector3.ClampMagnitude ( force, maxForce );
				rb.AddRelativeForce ( force, forceMode );
				
				// add torque. but first clamp it
				if ( maxTorqueDegrees != 0 )
					maxTorqueRadians = maxTorqueDegrees * Mathf.Deg2Rad;

				if ( clampTorque )
					torque = Vector3.ClampMagnitude ( torque, maxTorqueRadians );
//				rb.AddRelativeTorque ( newTorque, torqueMode );
				rb.AddRelativeTorque ( -torque, torqueMode );

				// update acceleration
				LinearAcceleration = ( rb.velocity - lastVelocity ) / Time.deltaTime;
				lastVelocity = rb.velocity;
				LinearVelocity = rb.velocity;
				// new: flip angular velocity to match flipped torque
//				rb.angularVelocity = angVel;
//				AngularVelocity = -angVel;
				AngularVelocity = -rb.angularVelocity;
			}
		}
		curSpeed = rb.velocity.magnitude;
	}

	void OnGUI ()
	{
		string info = @"Force: " + Force.ToRos ().ToString () +
		              "\nTorque: " + Torque.ToRos ().ToString () +
		              "\nPosition: " + Position.ToRos ().ToString () +
		              "\nRPY: " + ( -Rotation.eulerAngles ).ToRos ().ToString () +
		              "\nLinear Vel.: " + LinearVelocity.ToRos ().ToString () +
		              "\nAngular Vel.: " + AngularVelocity.ToRos ().ToString () +
		              "\nGravity " + ( UseGravity ? "on" : "off" ) +
		              "\nLocal input " + ( inputCtrl.active ? "on" : "off" );
		if ( ConstrainForceX )
			info += "\nX Movement constrained";
		if ( ConstrainForceY )
			info += "\nY Movement constrained";
		if ( ConstrainForceZ )
			info += "\nZ Movement constrained";
		if ( ConstrainTorqueX )
			info += "\nX Rotation constrained";
		if ( ConstrainTorqueY )
			info += "\nY Rotation constrained";
		if ( ConstrainTorqueZ )
			info += "\nZ Rotation constrained";

		GUIStyle label = GUI.skin.label;
		TextClipping clipping = label.clipping;
		label.clipping = TextClipping.Overflow;
		bool wrap = label.wordWrap;
		label.wordWrap = false;
		int fontSize = label.fontSize;
		label.fontSize = (int) ( 22f * Screen.height / 1080 );

		Vector2 size = label.CalcSize ( new GUIContent ( info ) );
		Rect r = new Rect ( 10, 10, size.x + 10, size.y );
		GUI.Box ( r, "" );
		GUI.Box ( r, "" );
		r.x += 5;

		GUILayout.BeginArea ( r );
		GUILayout.Label ( info );
		GUILayout.EndArea ();



		if ( drawArrows )
		{
			bool showMovement = ConstrainForceX || ConstrainForceY || ConstrainForceZ || drawArrowsAlways;
			bool showRotation = ConstrainTorqueX || ConstrainTorqueY || ConstrainTorqueZ || drawArrowsAlways;
			// x arrow
			Camera cam = Camera.main;
			float screenRatio = 1f * Screen.height / 1080;
			Vector2 texSize = new Vector2 ( 48, 8 ) * screenRatio;
			Vector2 texSize2 = new Vector2 ( 16, 16 ) * screenRatio;
			float arrowMag = texSize.magnitude + 14;
			Vector3 pos = transform.position;
			Vector2 screenPos = cam.WorldToScreenPoint ( pos );
			screenPos.y = Screen.height - screenPos.y;
			Vector2 top = cam.WorldToScreenPoint ( pos + Up * 0.5f );
			top.y = Screen.height - top.y;
			Vector2 tip = cam.WorldToScreenPoint ( pos + XAxis * 0.75f );
			tip.y = Screen.height - tip.y;
			Vector2 toTip = ( tip - screenPos ).normalized;
			Rect texRect = new Rect ( screenPos - texSize, texSize * 2 );
//			Rect texRect2 = new Rect ( screenPos + ( top - screenPos ).normalized * arrowMag - texSize2, texSize2 * 2 );
			Rect texRect2 = new Rect ( screenPos + toTip * arrowMag - texSize2, texSize2 * 2 );
			float angle = Vector2.Angle ( Vector2.right, toTip );
			if ( tip.y > screenPos.y )
				angle = -angle;
			GUIUtility.RotateAroundPivot ( -angle, screenPos );
			GUI.color = axisColors [ 0 ];
			if ( showMovement && !ConstrainForceX )
				GUI.DrawTexture ( texRect, axisArrows [ 0 ] );
			GUIUtility.RotateAroundPivot ( angle, screenPos );
			if ( showRotation && !ConstrainTorqueX )
				GUI.DrawTexture ( texRect2, axisArrows [ 1 ] );
//			GUI.DrawTexture ( new Rect ( tip.x - 2, tip.y - 2, 4, 4 ), dot );

			// y arrow
			tip = cam.WorldToScreenPoint ( pos + YAxis * 0.75f );
			tip.y = Screen.height - tip.y;
			toTip = ( tip - screenPos ).normalized;
			angle = Vector2.Angle ( Vector2.right, toTip );
			if ( tip.y > screenPos.y )
				angle = -angle;
			GUIUtility.RotateAroundPivot ( -angle, screenPos );
			GUI.color = axisColors [ 1 ];
			if ( showMovement && !ConstrainForceY )
				GUI.DrawTexture ( texRect, axisArrows [ 0 ] );
			GUIUtility.RotateAroundPivot ( angle, screenPos );
			texRect2.position = screenPos + toTip * arrowMag - texSize2;
			if ( showRotation && !ConstrainTorqueY )
				GUI.DrawTexture ( texRect2, axisArrows [ 1 ] );
//			GUI.DrawTexture ( new Rect ( tip.x - 2, tip.y - 2, 4, 4 ), dot );

			// z arrow
			tip = cam.WorldToScreenPoint ( pos + Up * 0.5f );
			tip.y = Screen.height - tip.y;
			toTip = ( tip - screenPos ).normalized;
			angle = Vector2.Angle ( Vector2.right, toTip );
			if ( tip.y > screenPos.y )
				angle = -angle;
			GUIUtility.RotateAroundPivot ( -angle, screenPos );
			GUI.color = axisColors [ 2 ];
			if ( showMovement && !ConstrainForceZ )
				GUI.DrawTexture ( texRect, axisArrows [ 0 ] );
			GUIUtility.RotateAroundPivot ( angle, screenPos );
			texRect2.position = screenPos + toTip * arrowMag - texSize2;
			if ( showRotation && !ConstrainTorqueZ )
				GUI.DrawTexture ( texRect2, axisArrows [ 1 ] );
//			GUI.DrawTexture ( new Rect ( tip.x - 2, tip.y - 2, 4, 4 ), dot );
//			GUI.color = Color.black;
//			GUI.DrawTexture ( new Rect ( screenPos.x - 2, screenPos.y - 2, 4, 4 ), dot );
		}

		GUI.color = Color.white;
//		GUIStyle label = GUI.skin.label;
//		TextClipping clipping = label.clipping;
//		label.clipping = TextClipping.Overflow;
//		bool wrap = label.wordWrap;
//		label.wordWrap = false;
//		int fontSize = label.fontSize;
//		label.fontSize = (int) ( 22f * Screen.height / 1080 );

//		info = "";

		if ( showLegend )
		{
			info = @"L: Legend on/off
F12: Control on/off
WSAD/Arrows: Move around
Space/C: Thrust up/down
Q/E: Turn around
Scroll wheel: zoom in/out
RMB (drag): Rotate camera
RMB: Reset camera
G: Gravity on/off
R: Reset Quad orientation
1-4: Cycle views
P: Plot waypoint
O: Follow path
I: Clear waypoints
Esc: Quit";

			size = label.CalcSize ( new GUIContent ( info ) );
			r = new Rect ( Screen.width - size.x - 20, 150, size.x + 10, size.y );
			GUI.Box ( r, "" );
			GUI.Box ( r, "" );
			r.x += 5;

			GUILayout.BeginArea ( r );
			GUILayout.Label ( info );
			GUILayout.EndArea ();
		} else
		{
			info = "L: Legend on/off";

			size = label.CalcSize ( new GUIContent ( info ) );
			r = new Rect ( Screen.width - size.x - 10, 150, size.x + 10, size.y );
			r.x -= 10;
			GUI.Box ( r, "" );
			GUI.Box ( r, "" );
			r.x += 5;

			GUILayout.BeginArea ( r );
			GUILayout.Label ( info );
			GUILayout.EndArea ();
		}

		label.clipping = clipping;
		label.wordWrap = wrap;
		label.fontSize = fontSize;
	}

	Vector3 FixEuler (Vector3 euler)
	{
		euler.x = FixAngle ( euler.x );
		euler.y = FixAngle ( euler.y );
		euler.z = FixAngle ( euler.z );
		return euler;
	}

	float FixAngle (float angle)
	{
		if ( angle > 180f )
			angle -= 360f;
		if ( angle < -180f )
			angle += 360f;

		return angle;
	}

	public void ApplyMotorForce (Vector3 v, bool convertFromRos = false)
	{
		useTwist = false;
		force = v;
		if ( convertFromRos )
			force = force.ToUnity ();
//		force *= thrustForce;
	}

	public void ApplyMotorTorque (Vector3 v, bool convertFromRos = false)
	{
		useTwist = false;
		torque = v;
		if ( convertFromRos )
			torque = torque.ToUnity ();
//		torque *= convertFromRos ? -torqueForce : torqueForce;
	}

	public void SetLinearVelocity (Vector3 v, bool convertFromRos = false)
	{
		useTwist = true;
		force = torque = Vector3.zero;
		LinearVelocity = convertFromRos ? v.ToUnity () : v;
	}

	public void SetAngularVelocity (Vector3 v, bool convertFromRos = false)
	{
		useTwist = true;
		force = torque = Vector3.zero;
		AngularVelocity = convertFromRos ? v.ToUnity () : v;
	}

	public void TriggerReset ()
	{
		resetFlag = true;
	}

	public void ResetOrientation ()
	{
		transform.rotation = Quaternion.identity;
		force = Vector3.zero;
		torque = Vector3.zero;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		LinearAcceleration = Vector3.zero;
		LinearVelocity = Vector3.zero;
		AngularVelocity = Vector3.zero;
		rb.isKinematic = true;
		rb.isKinematic = false;
	}

	void CheckSetPose ()
	{
		if ( setPoseFlag )
		{
			transform.position = posePosition;
			transform.rotation = poseOrientation;
			force = Vector3.zero;
			torque = Vector3.zero;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			LinearAcceleration = Vector3.zero;
			setPoseFlag = false;
		}
	}

	public void SetPositionAndOrientation (Vector3 pos, Quaternion orientation, bool convertFromRos = false)
	{
		setPoseFlag = true;
		if ( convertFromRos )
		{
			posePosition = pos.ToUnity ();
			poseOrientation = orientation.ToUnity ();
		} else
		{
			posePosition = pos;
			poseOrientation = orientation;
		}
	}

	void CreateCameraTex ()
	{
		// for now, just prep a byte[] that we can put raycast data into


//		cameraTex = new RenderTexture ( ImageWidth, ImageHeight, 0, RenderTextureFormat.RHalf );
//		cameraTex.enableRandomWrite = true;
//		cameraTex.Create ();
	}

	public byte[] GetImageData ()
	{
		return cameraData;
	}

	public void BeginRecordPath ()
	{
		isRecordingPath = true;
		PathPlanner.AddNode ( Position, Rotation );
		nextNodeTime = Time.time + pathRecordFrequency;
	}

	public void EndRecordPath ()
	{
		PathPlanner.AddNode ( Position, Rotation );
		isRecordingPath = false;
	}

	void CheckConstraints ()
	{
		RigidbodyConstraints c = RigidbodyConstraints.None;
		if ( ConstrainForceX )
			c |= RigidbodyConstraints.FreezePositionZ;
		if ( ConstrainForceY )
			c |= RigidbodyConstraints.FreezePositionX;
		if ( ConstrainForceZ )
			c |= RigidbodyConstraints.FreezePositionY;
		if ( ConstrainTorqueX )
			c |= RigidbodyConstraints.FreezeRotationZ;
		if ( ConstrainTorqueY )
			c |= RigidbodyConstraints.FreezeRotationX;
		if ( ConstrainTorqueZ )
			c |= RigidbodyConstraints.FreezeRotationY;
		rb.constraints = c;
	}

	public void UpdateConstraints ()
	{
		ConstrainForceX = ( rb.constraints & RigidbodyConstraints.FreezePositionZ ) != 0;
		ConstrainForceY = ( rb.constraints & RigidbodyConstraints.FreezePositionX ) != 0;
		ConstrainForceZ = ( rb.constraints & RigidbodyConstraints.FreezePositionY ) != 0;
		ConstrainTorqueX = ( rb.constraints & RigidbodyConstraints.FreezeRotationZ ) != 0;
		ConstrainTorqueY = ( rb.constraints & RigidbodyConstraints.FreezeRotationX ) != 0;
		ConstrainTorqueZ = ( rb.constraints & RigidbodyConstraints.FreezeRotationY ) != 0;
	}
}