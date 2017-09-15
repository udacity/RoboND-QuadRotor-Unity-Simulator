using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FollowType { None, Position, Transform, Sweep };

public class GimbalCamera : MonoBehaviour
{
	public Vector3 Position { get; protected set; }
	public Quaternion Rotation { get; protected set; }
	public Transform gimbalParent;
	public Camera colorCam;
	public Camera depthCam;
	public Camera maskCam;
	public Camera maskCam2;
	[Range (5, 180)]
	public float sweepCone = 90;
	public float sweepSpeed = 0.5f;

	public Shader whiteShader;
	public float timeScale = 1;
	public float recordFrequency = 0.33f;


	Camera cam1;
	Camera cam2;
	Camera cam3;
	RenderTexture tt2;
	RenderTexture tt3;
	Transform tr;
	Quaternion initialLocalRotation;

	FollowType followType;
	Transform followTarget;
	Vector3 followPosition;
//	Quaternion lastRotation;
	float sweepAccum;
	float lastSweepAngle;

	Transform vizSphere;

	float lastTimeChange;
	string timeLabel;
	bool recording;
	float nextRecordTime;
	int imageCount;
	bool focusTarget;

	void Awake ()
	{
		tr = colorCam.transform;
		if ( gimbalParent == null )
			gimbalParent = tr;
		initialLocalRotation = gimbalParent.localRotation;
//		lastRotation = gimbalParent.rotation;
		followType = FollowType.None;
		cam1 = colorCam.GetComponent<Camera> ();
		cam2 = maskCam.GetComponent<Camera> ();
		cam3 = maskCam2.GetComponent<Camera> ();
		cam2.SetReplacementShader ( whiteShader, "" );
		cam3.SetReplacementShader ( whiteShader, "" );

		Time.timeScale = timeScale;
		recordFrequency = 1f / recordFrequency;
		RecordingController.BeginRecordCallback = OnBeginRecording;
		RecordingController.EndRecordCallback = OnEndRecording;
		tt2 = cam2.targetTexture;
		tt3 = cam3.targetTexture;

//		vizSphere = GameObject.CreatePrimitive ( PrimitiveType.Sphere ).transform;
//		vizSphere.localScale = Vector3.one * 0.5f;
//		vizSphere.GetComponent<Renderer> ().material.color = Color.blue * 0.5f;
//		Destroy ( vizSphere.GetComponent<Collider> () );
	}

	void LateUpdate ()
	{
		Vector3 v = Vector3.RotateTowards ( tr.forward, Vector3.up, colorCam.fieldOfView * Mathf.Deg2Rad * 0.5f, 0 );
		v = v.normalized * colorCam.farClipPlane;
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
			Vector3 euler = gimbalParent.eulerAngles;
			euler.z = 0;
			euler.x = lastSweepAngle;
			gimbalParent.eulerAngles = euler;
//			gimbalParent.rotation = lastRotation;
			break;

		case FollowType.Position:
			gimbalParent.LookAt ( followPosition, Vector3.up );
//			vizSphere.position = followPosition;
			break;

		case FollowType.Transform:
			gimbalParent.LookAt ( followTarget.position, Vector3.up );
//			vizSphere.position = followTarget.position;
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
			euler = gimbalParent.eulerAngles;
			euler.y = sweepAccum;
			euler.z = 0;
			gimbalParent.eulerAngles = euler;
			break;
		}

		Position = tr.position;
		Rotation = tr.rotation;

		// adjust time scale
		int key0 = (int) KeyCode.Alpha0;
		for ( int i = 0; i < 10; i++ )
		{
			if ( Input.GetKeyDown ( (KeyCode) key0 + i ) )
			{
				timeScale = i;
				lastTimeChange = Time.unscaledTime;
				timeLabel = "Speed : ^0x".Replace ( "^0", i.ToString () );
				Time.timeScale = timeScale;
			}
		}

		if ( recording )
		{
			if ( Time.time >= nextRecordTime )
			{
				WriteImage ();
				nextRecordTime = Time.time + recordFrequency;
			}
		}
	}

	void OnGUI ()
	{
		float delta = Time.unscaledTime - lastTimeChange;
		if ( delta < 2 )
		{
			float a = 0;
			if ( delta < 0.2f )
				a = delta * 5;
			else
				if ( delta > 1.6f )
					a = ( 2f - delta ) * 2.5f;
				else
					a = 1;

			int fontSize = GUI.skin.label.fontSize;
			GUI.skin.label.fontSize = 40 * Screen.height / 1080;
			GUI.color = new Color ( 0, 0, 0, a / 2 );
			Rect r = new Rect ( 5, 5, 500, 100 );
			GUI.Label ( new Rect ( r.x + 1, r.y + 1, r.width, r.height ), timeLabel );
			GUI.color = new Color ( 1, 1, 1, a );
			GUI.Label ( r, timeLabel );
			GUI.skin.label.fontSize = fontSize;
		}

//		GUI.Box ( new Rect ( 5, Screen.height - 25, 60, 20 ), "" );
//		GUI.Label ( new Rect ( 10, Screen.height - 25, 60, 20 ), "Input " + (  ? "on" : "off" ) );
	}

	void OnBeginRecording ()
	{
		recording = true;
	}

	void OnEndRecording ()
	{
		recording = false;
	}

	void WriteImage ()
	{
		string prefix = imageCount.ToString ( "D5" );
//		Debug.Log ( "writing " + prefix );
		imageCount++;
		// needed to force camera update 
		RenderTexture targetTexture = cam1.targetTexture;
		RenderTexture.active = targetTexture;
		byte[] bytes;
		Texture2D tex = new Texture2D ( targetTexture.width, targetTexture.height, TextureFormat.RGB24, false );
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		string directory = RecordingController.SaveLocation;
		string path = Path.Combine ( directory, "cam1_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		targetTexture = tt2;
//		targetTexture = cam2.targetTexture;
		RenderTexture.active = targetTexture;
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		path = Path.Combine ( directory, "cam2_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		targetTexture = tt3;
//		targetTexture = cam3.targetTexture;
		RenderTexture.active = targetTexture;
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		path = Path.Combine ( directory, "cam3_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		bytes = null;
		RenderTexture.active = null;
		Destroy ( tex );
	}

	public void LookAt (Vector3 position)
	{
		followType = FollowType.Position;
		followPosition = position;
//		vizSphere.gameObject.SetActive ( true );
	}

	public void LookAt (Transform target)
	{
		followType = FollowType.Transform;
		followTarget = target;
//		vizSphere.gameObject.SetActive ( true );
	}

	public void StopLooking ()
	{
		followType = FollowType.None;
		gimbalParent.localRotation = Quaternion.Euler ( new Vector3 ( lastSweepAngle, 0, 0 ) );
//		lastRotation = gimbalParent.rotation;
//		vizSphere.gameObject.SetActive ( false );
	}

	public void Sweep (float vAngle = 45)
	{
		lastSweepAngle = vAngle;
		followType = FollowType.Sweep;
		gimbalParent.localRotation = Quaternion.Euler ( new Vector3 ( vAngle, 0, 0 ) );
//		gimbalParent.localRotation = Quaternion.identity;
//		vizSphere.gameObject.SetActive ( false );
		sweepAccum = 0;
	}

	public void SetSecondaryCam (int cam)
	{
		depthCam.gameObject.SetActive ( cam == 0 );
		maskCam.gameObject.SetActive ( cam == 1 );
		maskCam2.gameObject.SetActive ( cam == 1 );
//		depthCam.enabled = ( cam == 0 );
//		maskCam.enabled = maskCam2.enabled = ( cam == 1 );
	}
}