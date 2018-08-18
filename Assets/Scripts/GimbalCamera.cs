﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FollowType { None, Position, Transform, Sweep, Sweep360 };

public class GimbalCamera : MonoBehaviour
{
	public Vector3 Position { get; protected set; }
	public Quaternion Rotation { get; protected set; }
	public Transform gimbalParent;
	public Camera colorCam;
//	public Camera depthCam;
	public Camera maskCam;
	public Camera maskCam2;
	public Camera envMaskCam;
	public RenderTexture rt1;
	public RenderTexture rt2;
	public RenderTexture rt3;
	public RenderTexture rt4;
	[Range (5, 180)]
	public float sweepCone = 90;
	public float sweepSpeed = 0.5f;

	public Shader whiteShader;
	public float timeScale = 1;
	public float recordFrequency = 0.33f;

	Transform tr;
	Quaternion initialLocalRotation;

	FollowType followType;
	Transform followTarget;
	Vector3 followPosition;
//	Quaternion lastRotation;
	float sweepAccum;
	float lastSweepAngle;

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
//		maskCam.depthTextureMode = DepthTextureMode.Depth;
//		maskCam2.depthTextureMode = DepthTextureMode.Depth;
//		envMaskCam.depthTextureMode = DepthTextureMode.Depth;
//		maskCam.enabled = true;
//		maskCam2.enabled = true;
//		envMaskCam.enabled = true;

		Time.timeScale = timeScale;
		recordFrequency = 1f / recordFrequency;
		RecordingController.BeginRecordCallback = OnBeginRecording;
		RecordingController.EndRecordCallback = OnEndRecording;
	}

	void LateUpdate ()
	{
//		Vector3 v = Vector3.RotateTowards ( tr.forward, Vector3.up, colorCam.fieldOfView * Mathf.Deg2Rad * 0.5f, 0 );
//		v = v.normalized * colorCam.farClipPlane;
//		Debug.DrawRay ( tr.position, v, Color.green, Time.deltaTime, false );
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

//		Vector3 euler;
		switch ( followType )
		{
		case FollowType.None:
			Vector3 euler = gimbalParent.eulerAngles;
			euler.z = 0;
			euler.y = transform.root.eulerAngles.y;
			euler.x = lastSweepAngle;
			gimbalParent.eulerAngles = euler;
			break;

		case FollowType.Position:
			gimbalParent.LookAt ( followPosition, Vector3.up );
			break;

		case FollowType.Transform:
			gimbalParent.LookAt ( followTarget.position, Vector3.up );
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

		case FollowType.Sweep360:
			sweepAccum += sweepSpeed * Time.deltaTime;
			if ( sweepAccum > 360f )
				sweepAccum -= 360f;
			else
			if ( sweepAccum < -360f )
				sweepAccum += 360f;
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
                if ( DataExtractionManager.INSTANCE.isExtractionRunning() )
                {
                    DataExtractionManager.INSTANCE.onFrameSaved();
                }
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
		StartCoroutine ( _WriteImage () );
	}

	IEnumerator _WriteImage ()
	{
		// start by turning the cameras on
		maskCam.enabled = true;
		maskCam2.enabled = true;
		envMaskCam.enabled = true;

		// yield a frame so they render
		yield return null;

		// then turn them back off again
		maskCam.enabled = false;
		maskCam2.enabled = false;
		envMaskCam.enabled = false;

		string prefix = imageCount.ToString ( "D5" );
//		Debug.Log ( "writing " + prefix );
		imageCount++;

		// render the cameras
		// test: leave cameras on instead of manually rendering them, to check weird offset/lag issue
//		maskCam.Render ();
//		maskCam2.Render ();
//		envMaskCam.Render ();
//		maskCam.RenderWithShader ( whiteShader, "" );
//		maskCam2.RenderWithShader ( whiteShader, "" );
//		envMaskCam.RenderWithShader ( whiteShader, "" );

		// needed to force camera update 
		RenderTexture targetTexture = rt1;
		RenderTexture.active = targetTexture;
		byte[] bytes;
		Texture2D tex = new Texture2D ( targetTexture.width, targetTexture.height, TextureFormat.RGB24, false );
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		string directory = RecordingController.SaveLocation;
		string path = Path.Combine ( directory, "cam1_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		targetTexture = rt2;
		RenderTexture.active = targetTexture;
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		path = Path.Combine ( directory, "cam2_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		targetTexture = rt3;
		RenderTexture.active = targetTexture;
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		path = Path.Combine ( directory, "cam3_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		targetTexture = rt4;
		RenderTexture.active = targetTexture;
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		path = Path.Combine ( directory, "cam4_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		bytes = null;
		RenderTexture.active = null;
		Destroy ( tex );
	}

	public void LookAt (Vector3 position)
	{
		followType = FollowType.Position;
		followPosition = position;
	}

	public void LookAt (Transform target)
	{
		followType = FollowType.Transform;
		followTarget = target;
	}

	public void StopLooking ()
	{
		followType = FollowType.None;
		gimbalParent.localRotation = Quaternion.Euler ( new Vector3 ( lastSweepAngle, 0, 0 ) );
//		lastRotation = gimbalParent.rotation;
	}

	public void Sweep (float vAngle = 45)
	{
		lastSweepAngle = vAngle;
		followType = FollowType.Sweep;
		gimbalParent.localRotation = Quaternion.Euler ( new Vector3 ( vAngle, 0, 0 ) );
//		gimbalParent.localRotation = Quaternion.identity;
		sweepAccum = 0;
	}

	public void Sweep360 (bool sweepRight = true)
	{
		followType = FollowType.Sweep360;
		sweepSpeed = Mathf.Abs ( sweepSpeed );
		if ( !sweepRight )
			sweepSpeed = -sweepSpeed;
	}

	public void SetSecondaryCam (int cam)
	{
//		depthCam.gameObject.SetActive ( false );
//		depthCam.gameObject.SetActive ( cam == 0 );
		maskCam.gameObject.SetActive ( cam == 1 );
		maskCam2.gameObject.SetActive ( cam == 1 );
		envMaskCam.gameObject.SetActive ( cam == 1 );
	}
}