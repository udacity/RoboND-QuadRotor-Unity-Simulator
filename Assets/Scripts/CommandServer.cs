using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SocketIO;
//using UnityStandardAssets.Vehicles.Car;
using System;
//using System.Security.AccessControl;
using UnityEngine.UI;
using System.Threading;

public class CommandServer : MonoBehaviour
{
	public QuadMotor quad;
	public TargetFollower follower;
	public Camera colorCam;
	public Camera depthCam;
	private SocketIOComponent _socket;
	SimpleQuadController control;
	GimbalCamera gimbal;

//	public RawImage inset3;

	Texture2D inset1Tex;
	Texture2D inset2Tex;
//	Texture2D inset3Tex;

	Thread broadcastThread;
	int broadcastFrequency;
	bool paused;

	DateTime origin = new DateTime (1970, 1, 1, 0, 0, 0);

	void Start ()
	{
		_socket = GetComponent<SocketIOComponent> ();
		_socket.On ( "open", OnOpen );
		_socket.On ( "object_detected", OnObjectDetected );
		_socket.On ( "object_lost", OnObjectLost );
		_socket.On ( "create_box_marker", OnCreateBoxMarker );
		_socket.On ( "delete_marker", OnDeleteMarker );
		inset1Tex = new Texture2D ( 1, 1 );
		inset2Tex = new Texture2D ( 1, 1 );
//		inset3Tex = new Texture2D ( 1, 1 );

		broadcastFrequency = 10; //30; //60;
		broadcastThread = new Thread ( ThreadFunc );
		broadcastThread.Start ();
		control = quad.inputCtrl;
		gimbal = control.gimbal;
		Debug.Log ( "starting" );
//		EmitTelemetry ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.playmodeStateChanged += HandleCallbackFunction;
		#endif
	}

	void HandleCallbackFunction ()
	{
		#if UNITY_EDITOR
		paused = UnityEditor.EditorApplication.isPaused;
		#endif
	}

	void OnDestroy ()
	{
		if ( broadcastThread != null )
			broadcastThread.Abort ();
	}

	void ThreadFunc ()
	{
		int sleepTime = (int) ( 1000f / broadcastFrequency );

		while ( true )
		{
			#if UNITY_EDITOR
			if ( !paused )
			#endif
			EmitTelemetry ();
			Thread.Sleep ( sleepTime );
		}
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
		List<JSONObject> list = json.GetField ( "coords" ).list;
		float[] pos = {
			list[0].f,
			list[1].f,
			list[2].f
		};
//		int[] camera_coords = {
//			(int) json.GetField ( "x" ).n,
//			(int) json.GetField ( "y" ).n
//		};
//		string name = json.GetField ( "object_name" ).str;
//		float timestamp = json.GetField ( "timestamp" ).f;

		Vector3 position = new Vector3 ( pos [ 0 ], pos [ 1 ], pos [ 2 ] ).ToUnity ();
		Debug.Log ( "Target detected at " + position );
		Transform target = PeopleSpawner.instance.targetInstance;
		Transform cam = gimbal.colorCam.transform;
		bool success = false;
		if ( !Physics.Linecast ( cam.position, target.position + Vector3.up * 1.8f ) )
		{
			control.OnTargetDetected ( position );
			success = true;
		}
//		follower.SetFollowPoint ( position );

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "object";
		data [ "result" ] = success ? "true" : "false";
//		data [ "name" ] = name;

		Ack ( new JSONObject ( data ) );
	}

	void OnObjectLost (SocketIOEvent obj)
	{
		Debug.Log ( "Object lost" );
		control.OnTargetLost ();
	}

	void OnCreateBoxMarker (SocketIOEvent obj)
	{
		Debug.Log ( "Create marker" );
		JSONObject json = obj.data;
		// grab the info from json. info is set up as:
		// "id": "id"
		// "pose": [x,y,z,roll,pitch,yaw]
		// "dimensions": [w,h,d]
		// "color": [r,g,b,a]
		// "duration": #.#
		Debug.Log ( "json:\n" + json.Print ( true ) );
		Debug.Log ( "id is a " + json.GetField ( "id" ).type );
		Debug.Log ( "pose is a " + json.GetField ( "pose" ).type );
		Debug.Log ( "dimensions is a " + json.GetField ( "dimensions" ).type );
		Debug.Log ( "color is a " + json.GetField ( "color" ).type );
		Debug.Log ( "duration is a " + json.GetField ( "duration" ).type );

		int id = (int) json.GetField ( "id" ).n;
		List<JSONObject> values = json.GetField ( "pose" ).list;
		float[] pose = new float[values.Count];
		for ( int i = 0; i < pose.Length; i++ )
			pose [ i ] = values [ i ].f;
		values = json.GetField ( "dimensions" ).list;
		float[] dimensions = new float[values.Count];
		for ( int i = 0; i < dimensions.Length; i++ )
			dimensions [ i ] = values [ i ].f;
		values = json.GetField ( "color" ).list;
		float[] color = new float[values.Count];
		for ( int i = 0; i < color.Length; i++ )
			color [ i ] = values [ i ].f;
		float duration = json.GetField ( "duration" ).f;

		Vector3 pos = new Vector3 ( (float) pose [ 0 ], (float) pose [ 1 ], (float) pose [ 2 ] ).ToUnity ();
		Vector3 euler = new Vector3 ( (float) pose [ 3 ], (float) pose [ 4 ], (float) pose [ 5 ] ).ToUnity ();
		Quaternion rot = Quaternion.Euler ( euler );
		Vector3 size = new Vector3 ( (float) dimensions [ 0 ], (float) dimensions [ 1 ], (float) dimensions [ 2 ] ).ToUnity ();
		Color c = new Color ( color [ 0 ], color [ 1 ], color [ 2 ], color [ 3 ] );

		MarkerMaker.AddMarker ( id.ToString (), pos, rot, size, c, duration );

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "create";
		data [ "id" ] = id.ToString ();

		Ack ( new JSONObject ( data ) );
	}

/*	void OnCreateBoxMarker (SocketIOEvent obj)
	{
		Debug.Log ( "Create marker" );
		JSONObject json = obj.data;
		// grab the info from json. info is set up as:
		// "id": "id"
		// "pose": [x,y,z,roll,pitch,yaw]
		// "dimensions": [w,h,d]
		// "color": [r,g,b,a]
		// "duration": #.#

		// check that all the fields exist
		Debug.Log ( "json is:\n" + json.Print ( true ) );
		ValidateField ( json, "id", JSONObject.Type.STRING );
		ValidateField ( json, "pose", JSONObject.Type.STRING );
		ValidateField ( json, "dimensions", JSONObject.Type.STRING );
		ValidateField ( json, "color", JSONObject.Type.STRING );
		ValidateField ( json, "duration", JSONObject.Type.NUMBER );

		string id = json.GetField ( "id" ).str;
		string pose = json.GetField ( "pose" ).str;
		string dims = json.GetField ( "dimensions" ).str;
		string color = json.GetField ( "color" ).str;
		string[] values = pose.Split ( ',' );
		double[] vals = new double[9];
		for ( int i = 0; i < values.Length; i++ )
			vals [ i ] = double.Parse ( values [ i ] );
		values = dims.Split ( ',' );
		for ( int i = 0; i < values.Length; i++ )
			vals [ 6 + i ] = double.Parse ( values [ i ] );

		string[] channels = color.Split ( ',' );
		float[] newChannels = new float[4];
		for ( int i = 0; i < 4; i++ )
			newChannels [ i ] = (float) double.Parse ( channels [ i ] );
		Color c = new Color ( newChannels [ 0 ], newChannels [ 1 ], newChannels [ 2 ], newChannels [ 3 ] );
		float duration = json.GetField ( "duration" ).f;

		Vector3 pos = new Vector3 ( (float) vals [ 0 ], (float) vals [ 1 ], (float) vals [ 2 ] ).ToUnity ();
		Vector3 euler = new Vector3 ( (float) vals [ 3 ], (float) vals [ 4 ], (float) vals [ 5 ] ).ToUnity ();
		Quaternion rot = Quaternion.Euler ( euler );
		Vector3 size = new Vector3 ( (float) vals [ 6 ], (float) vals [ 7 ], (float) vals [ 8 ] ).ToUnity ();

		MarkerMaker.AddMarker ( id, pos, rot, size, c, duration );


		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "create";
		data [ "id" ] = id;

		Ack ( new JSONObject ( data ) );
	}*/
	void ValidateField (JSONObject obj, string fieldName, JSONObject.Type fieldType)
	{
		if ( obj.HasField ( fieldName ) && obj.GetField ( fieldName ).type == fieldType )
			Debug.Log ( "Field " + fieldName + "ok" );
		else
		if ( obj.HasField ( fieldName ) )
			Debug.Log ( "Field " + fieldName + " is type " + obj.GetField ( fieldName ).type + ", expected: " + fieldType );
		else
			Debug.Log ( "No field named " + fieldName );
	}

	void OnDeleteMarker (SocketIOEvent obj)
	{
		Debug.Log ( "Delete marker" );
		JSONObject json = obj.data;
		int id = (int) json.GetField ( "id" ).n;
//		string id = json.GetField ( "id" ).str;

		MarkerMaker.DeleteMarker ( id.ToString () );

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "delete";
		data [ "id" ] = id.ToString ();

		Ack ( new JSONObject ( data ) );
	}

	void Ack (JSONObject obj)
	{
		_socket.Emit ( "Ack", obj );
	}

	void EmitTelemetry ()
	{
//		Debug.Log ( "Emitting" );
		UnityMainThreadDispatcher.Instance ().Enqueue ( () =>
		{
//			print ( "Attempting to Send..." );

			// Collect Data from the Car
			Dictionary<string, string> data = new Dictionary<string, string> ();

			data [ "timestamp" ] = Timestamp ().ToString ();
			Vector3 v = quad.Position.ToRos ();
			Vector3 v2 = ( -quad.Rotation.eulerAngles ).ToRos ();
			data [ "pose" ] = v.x.ToString ( "N4" ) + "," + v.y.ToString ( "N4" ) + "," + v.z.ToString ( "N4" ) + "," + v2.x.ToString ( "N4" ) + "," + v2.y.ToString ( "N4" ) + "," + v2.z.ToString ( "N4" );
			v = gimbal.Position.ToRos ();
			v2 = ( -gimbal.Rotation.eulerAngles ).ToRos ();
			data [ "gimbal_pose" ] = v.x.ToString ( "N4" ) + "," + v.y.ToString ( "N4" ) + "," + v.z.ToString ( "N4" ) + "," + v2.x.ToString ( "N4" ) + "," + v2.y.ToString ( "N4" ) + "," + v2.z.ToString ( "N4" );
//			System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch ();
//			w.Start ();
//			CameraHelper.CaptureFrame (colorCam);
//			CameraHelper.CaptureDepthFrame (depthCam);
			data [ "rgb_image" ] = Convert.ToBase64String ( CameraHelper.CaptureFrame ( colorCam ) );
			data [ "depth_image" ] = Convert.ToBase64String ( CameraHelper.CaptureDepthFrame ( depthCam ) );
//			w.Stop ();
//			Debug.Log ("capture took " + w.ElapsedMilliseconds + "ms");

//			Debug.Log ("sangle " + data["steering_angle"] + " vert " + data["vert_angle"] + " throt " + data["throttle"] + " speed " + data["speed"] + " image " + data["image"]);
			_socket.Emit ( "sensor_frame", new JSONObject ( data ) );
//			Debug.Log ("Emitted");
		} );
	}

	double Timestamp ()
	{
		return ( DateTime.UtcNow - origin ).TotalSeconds;
	}
}
