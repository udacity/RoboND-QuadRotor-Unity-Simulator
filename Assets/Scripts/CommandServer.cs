using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SocketIO;
using UnityStandardAssets.Vehicles.Car;
using System;
using System.Security.AccessControl;
using UnityEngine.UI;

public class CommandServer : MonoBehaviour
{
	public QuadController quad;
	public Camera colorCam;
	public Camera depthCam;
	private SocketIOComponent _socket;

//	public RawImage inset3;

	Texture2D inset1Tex;
	Texture2D inset2Tex;
//	Texture2D inset3Tex;

	DateTime origin = new DateTime (1970, 1, 1, 0, 0, 0);

	void Start()
	{
		_socket = GetComponent<SocketIOComponent> ();
		_socket.On ( "open", OnOpen );
		_socket.On ( "object_detected", OnObjectDetected );
		_socket.On ( "create_box_marker", OnCreateBoxMarker );
		_socket.On ( "delete_marker", OnDeleteMarker );
		inset1Tex = new Texture2D ( 1, 1 );
		inset2Tex = new Texture2D ( 1, 1 );
//		inset3Tex = new Texture2D ( 1, 1 );
	}

	void Update ()
	{
	}

	void OnOpen(SocketIOEvent obj)
	{
		Debug.Log ( "Connection Open" );
		EmitTelemetry ();
	}

	void OnObjectDetected (SocketIOEvent obj)
	{
		Debug.Log ( "Object detected" );

		JSONObject json = obj.data;
		int[] coords = {
			(int) json.GetField ( "x" ).n,
			(int) json.GetField ( "y" ).n
		};
		string name = json.GetField ( "object_name" ).str;
		double timestamp = json.GetField ( "timestamp" ).f;

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "object";
		data [ "name" ] = name;

		Ack ( new JSONObject ( data ) );
	}

	void OnCreateBoxMarker (SocketIOEvent obj)
	{
		Debug.Log ( "Create marker" );
		JSONObject json = obj.data;
		string id = json.GetField ( "id" ).str;
		string pose = json.GetField ( "pose" ).str;
//		float[] pose = {
//			json.GetField ( "x" ).f,
//			json.GetField ( "y" ).f,
//			json.GetField ( "z" ).f,
//			json.GetField ( "roll" ).f,
//			json.GetField ( "pitch" ).f,
//			json.GetField ( "yaw" ).f
//		};
		string color = json.GetField ( "color" ).str;
//		int[] color = {
//			json.GetField ( "r" ).n,
//			json.GetField ( "g" ).n,
//			json.GetField ( "b" ).n
//		};
		float alpha = json.GetField ( "alpha" ).f;
		float duration = json.GetField ( "seconds" ).f;

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "create";
		data [ "id" ] = id;

		Ack ( new JSONObject ( data ) );
	}

	void OnDeleteMarker (SocketIOEvent obj)
	{
		Debug.Log ( "Delete marker" );
		JSONObject json = obj.data;
		string id = json.GetField ( "id" ).str;

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "delete";
		data [ "id" ] = id;

		Ack ( new JSONObject ( data ) );
	}

	void Ack (JSONObject obj)
	{
		_socket.Emit ( "Ack", obj );
	}

/*	void OnSteer(SocketIOEvent obj)
	{
//		Debug.Log ( "Steer" );
		JSONObject jsonObject = obj.data;

		// try to load image1
		bool loaded = false;
		Vector2 size = Vector2.zero;
		Texture2D tex = new Texture2D ( 1, 1 );
		string imageInfo = "";
		byte[] imageBytes = null;
		if ( jsonObject.HasField ( "inset_image" ) )
			imageInfo = jsonObject.GetField ( "inset_image" ).str;
		if ( !string.IsNullOrEmpty ( imageInfo ) )
			imageBytes = Convert.FromBase64String ( imageInfo );
		if ( imageBytes != null && imageBytes.Length != 0 )
			loaded = tex.LoadImage ( imageBytes, true );
		if ( loaded && inset1 != null && tex.width > 1 && tex.height > 1 )
		{
			inset1Tex = tex;
			inset1.texture = inset1Tex;
			size = inset1.rectTransform.sizeDelta;
			size.x = 1f * inset1Tex.width / inset1Tex.height * size.y;
			inset1.rectTransform.sizeDelta = size;
			inset1.CrossFadeAlpha ( 1, 0.3f, false );
		} else
		if ( inset1 != null )
		{
//			if ( tex.width == 1 || tex.height == 1 )
//				inset1.CrossFadeAlpha ( 0, 0.0f, true );
		}
		// try to load image2
		loaded = false;
		tex = new Texture2D ( 1, 1 );
		imageInfo = "";
		imageBytes = null;
		if ( jsonObject.HasField ( "inset_image2" ) )
			imageInfo = jsonObject.GetField ( "inset_image2" ).str;
		if ( !string.IsNullOrEmpty ( imageInfo ) )
			imageBytes = Convert.FromBase64String ( imageInfo );
		if ( imageBytes != null && imageBytes.Length != 0 )
			loaded = tex.LoadImage ( imageBytes, true );
		if ( loaded && inset2Tex != null && tex.width > 1 && tex.height > 1 )
		{
			inset2Tex = tex;
			inset2.texture = inset2Tex;
			size = inset2.rectTransform.sizeDelta;
			size.x = 1f * inset2Tex.width / inset2Tex.height * size.y;
			inset2.rectTransform.sizeDelta = size;
			inset2.CrossFadeAlpha ( 1, 0.3f, false );
		} else
		if ( inset2 != null )
		{
//			if ( tex.width == 1 || tex.height == 1 )
//				inset2.CrossFadeAlpha ( 0, 0.0f, true );
		}
		// try to load image3
		loaded = false;
		tex = new Texture2D ( 1, 1 );
		imageInfo = "";
		imageBytes = null;
		if ( jsonObject.HasField ( "inset_image3" ) )
			imageInfo = jsonObject.GetField ( "inset_image3" ).str;
		if ( !string.IsNullOrEmpty ( imageInfo ) )
			imageBytes = Convert.FromBase64String ( imageInfo );
		if ( imageBytes != null && imageBytes.Length != 0 )
			loaded = tex.LoadImage ( imageBytes, true );
		if ( loaded && inset3Tex != null && tex.width > 1 && tex.height > 1 )
		{
			inset3Tex = tex;
			inset3.texture = inset3Tex;
			size = inset3.rectTransform.sizeDelta;
			size.x = 1f * inset3Tex.width / inset3Tex.height * size.y;
			inset3.rectTransform.sizeDelta = size;
			inset3.CrossFadeAlpha ( 1, 0.3f, false );
		} else
		if ( inset3 != null )
		{
//			if ( tex.width == 1 || tex.height == 1 )
//				inset3.CrossFadeAlpha ( 0, 0.0f, true );
		}
		EmitTelemetry(obj);
	}*/

	void EmitTelemetry ()
	{
//		Debug.Log ( "Emitting" );
		UnityMainThreadDispatcher.Instance ().Enqueue ( () =>
		{
			print ( "Attempting to Send..." );

			// Collect Data from the Car
			Dictionary<string, string> data = new Dictionary<string, string> ();

			data [ "timestamp" ] = Timestamp ().ToString ();
			Vector3 v = quad.Position.ToRos ();
			data [ "position" ] = v.x.ToString ( "N4" ) + "," + v.y.ToString ( "N4" ) + "," + v.z.ToString ( "N4" );
			v = ( -quad.Rotation.eulerAngles ).ToRos ();
			data [ "rpy" ] = v.x.ToString ( "N4" ) + "," + v.y.ToString ( "N4" ) + "," + v.z.ToString ( "N4" );
			data [ "rgb" ] = Convert.ToBase64String ( CameraHelper.CaptureFrame ( colorCam ) );
			data [ "depth" ] = Convert.ToBase64String ( CameraHelper.CaptureFrame ( depthCam ) );

//			Debug.Log ("sangle " + data["steering_angle"] + " vert " + data["vert_angle"] + " throt " + data["throttle"] + " speed " + data["speed"] + " image " + data["image"]);
			_socket.Emit ( "telemetry", new JSONObject ( data ) );
		} );
	}

	double Timestamp ()
	{
		return ( DateTime.UtcNow - origin ).TotalSeconds;
	}
}