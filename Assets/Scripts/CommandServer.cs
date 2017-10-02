#define OUTPUT_EVENTS // uncomment this to log when events are called
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
	public RenderTexture depthImage;
//	public Camera depthCam;
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
	float nextBroadcast;
	bool canBroadcast;

	DateTime origin = new DateTime (1970, 1, 1, 0, 0, 0);

	string rgbString;
	string depthString;
	object locker;

	float pingTimeout = 3;
	float lastPingTime;

	void Start ()
	{
		_socket = GetComponent<SocketIOComponent> ();
		_socket.On ( "open", OnOpen );
		_socket.On ( "object_detected", OnObjectDetected );
		_socket.On ( "object_lost", OnObjectLost );
		_socket.On ( "create_box_marker", OnCreateBoxMarker );
		_socket.On ( "delete_marker", OnDeleteMarker );
		_socket.On ( "error", OnError );
		_socket.On ( "ping", OnPing );
		inset1Tex = new Texture2D ( 1, 1 );
		inset2Tex = new Texture2D ( 1, 1 );
//		inset3Tex = new Texture2D ( 1, 1 );

		broadcastFrequency = 5; // 10; //30; //60;
		control = quad.inputCtrl;
		gimbal = control.gimbal;
		Debug.Log ( "starting" );
//		EmitTelemetry ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.playmodeStateChanged += HandleCallbackFunction;
		#endif
		depthImage = gimbal.colorCam.GetComponent<RGBToDepth> ().destTex;
//		StartCoroutine ( BroadcastFunc () );
		nextBroadcast = Time.realtimeSinceStartup + 1f / broadcastFrequency;
		locker = new object ();
		lastPingTime = Mathf.Infinity;
//		broadcastThread = new Thread ( ThreadFunc );
//		broadcastThread.Start ();
	}

	void Update ()
	{
		if ( !_socket.IsConnected )
			return;
		#if UNITY_EDITOR
		if ( paused )
			return;
		#endif
		if ( Time.realtimeSinceStartup > nextBroadcast )
		{
//			lock ( locker )
//			{
				rgbString = Convert.ToBase64String ( CameraHelper.CaptureFrame ( colorCam ) );
				depthString = Convert.ToBase64String ( CameraHelper.CaptureDepthFrame ( depthImage ) );
//			}
			canBroadcast = true;
			NewTelemetry ();

			nextBroadcast = Time.realtimeSinceStartup + 1f / broadcastFrequency;
		}

		if ( Time.unscaledTime - lastPingTime > pingTimeout )
		{
			Debug.Log ( "Ping has timed out" );
			lastPingTime = Mathf.Infinity;
			control.OnPingTimeout ();
		}
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
		int sleepTime = 10;

		while ( true )
		{
			if ( canBroadcast )
			{
				NewTelemetry ();
				canBroadcast = false;
			}
			Thread.Sleep ( sleepTime);
		}
	}

	IEnumerator BroadcastFunc ()
	{
		float sleepTime = 1f / broadcastFrequency;
		while ( !_socket.IsConnected )
			yield return null;

		Debug.Log ( "Initiating telemetry emission" );

		while ( true )
		{
			#if UNITY_EDITOR
			if ( !paused )
			#endif
			EmitTelemetry ();
			yield return new WaitForSecondsRealtime ( sleepTime );
		}
	}

	void OnOpen(SocketIOEvent obj)
	{
		#if OUTPUT_EVENTS
		Debug.Log ( "Connection Open" );
		#endif
//		EmitTelemetry ();
	}

	void OnObjectDetected (SocketIOEvent obj)
	{
		#if OUTPUT_EVENTS
		Debug.Log ( "Object detected" );
		#endif

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
		#if OUTPUT_EVENTS
		Debug.Log ( "Target detected at " + position );
		#endif
		Transform target = PeopleSpawner.instance.targetInstance;
		Transform cam = gimbal.colorCam.transform;
		bool success = false;
		RaycastHit hit;
		// do a quick obstacle check and a quick visibility check, with the angle to the target being less than the fov cone
		if ( !Physics.Linecast ( cam.position, target.position + Vector3.up, out hit ) && Vector3.Angle ( (target.position + Vector3.up - cam.position).normalized, cam.forward ) < gimbal.colorCam.fieldOfView )
		{
			control.OnTargetDetected ( position );
			success = true;
			Debug.DrawLine ( cam.position, target.position + Vector3.up * 1.8f, Color.green, 1, false );
		} else
			Debug.DrawLine ( cam.position, hit.point, Color.red, 1, false );
			
//		follower.SetFollowPoint ( position );

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "object";
		data [ "result" ] = success ? "true" : "false";
//		data [ "name" ] = name;

		Ack ( new JSONObject ( data ) );
	}

	void OnObjectLost (SocketIOEvent obj)
	{
		#if OUTPUT_EVENTS
		Debug.Log ( "Object lost" );
		#endif
		control.OnTargetLost ();
	}

	void OnCreateBoxMarker (SocketIOEvent obj)
	{
		#if OUTPUT_EVENTS
		Debug.Log ( "Create marker" );
		#endif
		JSONObject json = obj.data;
		// grab the info from json. info is set up as:
		// "id": "id"
		// "pose": [x,y,z,roll,pitch,yaw]
		// "dimensions": [w,h,d]
		// "color": [r,g,b,a]
		// "duration": #.#
//		Debug.Log ( "json:\n" + json.Print ( true ) );
//		Debug.Log ( "id is a " + json.GetField ( "id" ).type );
//		Debug.Log ( "pose is a " + json.GetField ( "pose" ).type );
//		Debug.Log ( "dimensions is a " + json.GetField ( "dimensions" ).type );
//		Debug.Log ( "color is a " + json.GetField ( "color" ).type );
//		Debug.Log ( "duration is a " + json.GetField ( "duration" ).type );

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

	void OnDeleteMarker (SocketIOEvent obj)
	{
		#if OUTPUT_EVENTS
		Debug.Log ( "Delete marker" );
		#endif
		JSONObject json = obj.data;
		int id = (int) json.GetField ( "id" ).n;
//		string id = json.GetField ( "id" ).str;

		MarkerMaker.DeleteMarker ( id.ToString () );

		Dictionary<string, string> data = new Dictionary<string, string> ();
		data [ "action" ] = "delete";
		data [ "id" ] = id.ToString ();

		Ack ( new JSONObject ( data ) );
	}

	void OnError (SocketIOEvent obj)
	{
		#if OUTPUT_EVENTS
		Debug.Log ( "error!" );
		#endif
	}

	void OnPing (SocketIOEvent obj)
	{
		lastPingTime = Time.unscaledTime;
	}

	void Ack (JSONObject obj)
	{
//		_socket.Emit ( "Ack", obj );
	}

	void EmitTelemetry ()
	{
//		if ( UnityEngine.Random.value < 0.1f )
//			Debug.Log ( "Emitting (random)" );
//		UnityMainThreadDispatcher.Instance ().Enqueue ( () =>
//		{
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
			data [ "depth_image" ] = Convert.ToBase64String ( CameraHelper.CaptureDepthFrame ( depthImage ) );
//			w.Stop ();
//			Debug.Log ("capture took " + w.ElapsedMilliseconds + "ms");

//			Debug.Log ("sangle " + data["steering_angle"] + " vert " + data["vert_angle"] + " throt " + data["throttle"] + " speed " + data["speed"] + " image " + data["image"]);
//			new Thread ( () =>{
//				_socket.Emit ( "sensor_frame", new JSONObject ( data ) );}
//			).Start ();
		_socket.Emit ( "sensor_frame", new JSONObject ( data ) );
//			Debug.Log ("Emitted");
//		} );
	}

	void NewTelemetry ()
	{
		#if OUTPUT_EVENTS
//		Debug.Log ( "Emitting" );
		#endif

//		UnityMainThreadDispatcher.Instance ().Enqueue ( () =>
//		{
			Dictionary<string, string> data = new Dictionary<string, string> ();

			data [ "timestamp" ] = Timestamp ().ToString ();
			Vector3 v = quad.Position.ToRos ();
			Vector3 v2 = ( -quad.Rotation.eulerAngles ).ToRos ();
			data [ "pose" ] = v.x.ToString ( "N4" ) + "," + v.y.ToString ( "N4" ) + "," + v.z.ToString ( "N4" ) + "," + v2.x.ToString ( "N4" ) + "," + v2.y.ToString ( "N4" ) + "," + v2.z.ToString ( "N4" );
			v = gimbal.Position.ToRos ();
			v2 = ( -gimbal.Rotation.eulerAngles ).ToRos ();
			data [ "gimbal_pose" ] = v.x.ToString ( "N4" ) + "," + v.y.ToString ( "N4" ) + "," + v.z.ToString ( "N4" ) + "," + v2.x.ToString ( "N4" ) + "," + v2.y.ToString ( "N4" ) + "," + v2.z.ToString ( "N4" );
			lock ( locker )
			{
				data [ "rgb_image" ] = rgbString;
				data [ "depth_image" ] = depthString;
			}
			_socket.Emit ( "sensor_frame", new JSONObject ( data ) );
		JSONObject j = new JSONObject ( data );
//		} );
	}

	double Timestamp ()
	{
		return ( DateTime.UtcNow - origin ).TotalSeconds;
	}
}
