using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
	public Transform viewCam;
	public Transform colorCam;
	public Transform bwCam;
	public Transform target;

	public float vRotSpeed = 90;
	public float hRotSpeed = 90;
	public float minvAngle = -70;
	public float maxVAngle = 20;
	public float minDist = 1;
	public float maxDist = 20;
	public float distSpeed = 1;
	public float timeScale = 1;
	public float recordFrequency = 10;

	public Shader whiteShader;


	Transform tr;
	Camera cam1;
	Camera cam2;
	float distanceDelta;
	float vAngleDelta;
	float desiredZ;
	float eulerX;

	float lastTimeChange;
	string timeLabel;
	bool recording;
	float nextRecordTime;
	int imageCount;

	void Awake ()
	{
		tr = transform;
		distanceDelta = distSpeed;
		vAngleDelta = vRotSpeed;
		colorCam.localPosition = bwCam.localPosition = -Vector3.forward * minDist;
		cam1 = colorCam.GetComponent<Camera> ();
		cam2 = bwCam.GetComponent<Camera> ();
		cam2.SetReplacementShader ( whiteShader, "" );
		Time.timeScale = timeScale;
		recordFrequency = 1f / recordFrequency;
		RecordingController.BeginRecordCallback = OnBeginRecording;
		RecordingController.EndRecordCallback = OnEndRecording;
	}

	void LateUpdate ()
	{
		if ( target == null )
			return;
		// position our camera base with the character's head, roughly
		tr.position = target.position + Vector3.up * 2;
		// rotate around the target horizontally
		tr.Rotate ( Vector3.up * hRotSpeed * Time.deltaTime );
		eulerX += vAngleDelta * Time.deltaTime;
		if ( eulerX <= minvAngle || eulerX >= maxVAngle )
		{
			eulerX = Mathf.Clamp ( eulerX, minvAngle, maxVAngle );
			vAngleDelta *= -1;
		}
		Vector3 euler = tr.localEulerAngles;
		euler.x = eulerX;
		euler.z = 0;
		tr.localEulerAngles = euler;

		// adjust the distance from target
		Vector3 lp = viewCam.localPosition;
		desiredZ += distanceDelta * Time.deltaTime;
//		lp.z += distanceDelta * Time.deltaTime;
		if ( desiredZ <= minDist || desiredZ >= maxDist )
		{
			desiredZ = Mathf.Clamp ( desiredZ, minDist, maxDist );
			distanceDelta *= -1;
		}

		// check if character is obscured from camera
		// use forward instead of -forward because cameras are rotated 180
		Ray ray = new Ray ( tr.position, tr.forward );
		RaycastHit hit;
		if ( Physics.SphereCast ( ray, 0.05f, out hit, desiredZ ) )
		{
//			Debug.Log ( "hitting " + hit.collider.name );
			lp.z = hit.distance - 0.1f;
			if ( lp.z < 1 )
				lp.z = 1;
			Debug.DrawRay ( ray.origin, ray.direction * hit.distance, Color.red );

		} else
			lp.z = desiredZ;
//			lp.z = Mathf.MoveTowards ( lp.z, desiredZ, 10 * Time.deltaTime );
		// and assign the new distance
		viewCam.localPosition = colorCam.localPosition = bwCam.localPosition = lp;


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

		targetTexture = cam2.targetTexture;
		RenderTexture.active = targetTexture;
		tex.ReadPixels ( new Rect ( 0, 0, targetTexture.width, targetTexture.height ), 0, 0 );
		tex.Apply ();
		bytes = tex.EncodeToPNG ();
		path = Path.Combine ( directory, "cam2_" + prefix + ".png" );
		File.WriteAllBytes ( path, bytes );

		bytes = null;
		RenderTexture.active = null;
		Destroy ( tex );
	}
}