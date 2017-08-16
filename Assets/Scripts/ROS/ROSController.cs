using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using Ros_CSharp;
using XmlRpc_Wrapper;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ROSStatus
{
	Unknown,
	Disconnected,
	Connecting,
	Connected
}

public class ROSController : MonoBehaviour
{
	static object instanceLock = new object ();
	public static ROSController instance;
	public static ROSStatus Status
	{
		get {
			if ( instance == null )
				return ROSStatus.Disconnected;
			return instance.status;
		}
	}
	static object callbackLock = new object ();
	static Queue<Action> callbacks = new Queue<Action> ();
	static Queue<NodeHandle> nodes = new Queue<NodeHandle> ();
	public static bool delayedStart;

	public string rosMasterURI = "http://localhost:11311";
	public string nodePrefix = "";
	public bool overrideURI;
	public string rosIP = "0.0.0.0";
	public bool overrideIP;

	ROSStatus status;
	bool initComplete;
	bool starting;
	bool stopping;
	bool delete;
	bool connectedToMaster;

	void Awake ()
	{
		if ( instance != null && instance != this )
		{
			Debug.LogError ( "Too many ROSControllers! Only one must exist." );
			Destroy ( gameObject );
			return;
		}

		GetConfigFile ();

		Debug.LogWarning ( "Main thread ID " + System.Threading.Thread.CurrentThread.ManagedThreadId );

		status = ROSStatus.Disconnected;

//		Application.targetFrameRate = 0;
		if ( QualitySettings.vSyncCount == 2 )
			Application.targetFrameRate = 30;
		else
			Application.targetFrameRate = 60;

//		Debug.Log ( "ros master is " + ROS.ROS_MASTER_URI );
		if ( ( string.IsNullOrEmpty ( Environment.GetEnvironmentVariable ( "ROS_MASTER_URI", EnvironmentVariableTarget.User ) ) &&
		     string.IsNullOrEmpty ( Environment.GetEnvironmentVariable ( "ROS_MASTER_URI", EnvironmentVariableTarget.Machine ) ) ) || overrideURI )
			ROS.ROS_MASTER_URI = rosMasterURI;

		if ( overrideIP )
		{
			if ( rosIP == "0.0.0.0" )
				Debug.LogError ( "host IP is set to override but address is set to 0.0.0.0" );
			else
				ROS.ROS_IP = rosIP;
		}
		
//			delayedStart = true;
		instance = this;
		initComplete = false;
		StartROS ();
		new Thread ( new ThreadStart ( UpdateMasterConnection ) ).Start ();
	}

	void Update ()
	{
		if ( ROS.isStarted () && ROS.ok && connectedToMaster )
			status = ROSStatus.Connected;
		else
			if ( ROS.shutting_down || !ROS.isStarted () || !ROS.ok )
			status = ROSStatus.Disconnected;
		else
			status = ROSStatus.Connecting;
	}

	void OnDestroy ()
	{
		StopROS ();
	}

	void OnApplicationQuit ()
	{
		StopROS ();
	}

	void GetConfigFile ()
	{
		string filename = Application.dataPath + "/ros_settings.txt";

		if ( File.Exists ( filename ) )
		{
//			Debug.Log ( "exists" );
			using ( var fs = new FileStream ( filename, FileMode.Open, FileAccess.Read ) )
			{
				byte[] bytes = new byte[fs.Length]; 
				fs.Read ( bytes, 0, bytes.Length );
				string json = System.Text.Encoding.UTF8.GetString ( bytes );
//				Debug.Log ( "json: " + json );
				JSONObject jo = new JSONObject ( json );
				if ( jo.HasField ( "vm-override" ) && jo.GetField ( "vm-override" ).b )
				{
					if ( jo.HasField ( "vm-ip" ) && jo.GetField ( "vm-ip" ).IsString )
						rosMasterURI = "http://" + jo.GetField ( "vm-ip" ).str;
					if ( jo.HasField ( "vm-port" ) && jo.GetField ( "vm-port" ).IsNumber )
						rosMasterURI += ":" + ( (int) ( jo.GetField ( "vm-port" ).n ) ).ToString ();
					else
						rosMasterURI += ":11311";
//					ROS.ROS_HOSTNAME = "udacity";
					Debug.Log ( "setting ip to " + rosMasterURI );
					overrideURI = true;
				}
				if ( jo.HasField ( "host-override" ) && jo.GetField ( "host-override" ).b )
				{
					if ( jo.HasField ( "host-ip" ) && jo.GetField ( "host-ip" ).IsString )
						rosIP = jo.GetField ( "host-ip" ).str;
					overrideIP = true;
				}
			}
		} else
		{
//			Debug.Log ( "not exists" );
		}
	}

	void UpdateMasterConnection ()
	{
		while ( !ROS.shutting_down )
		{
			connectedToMaster = master.check ();
			Thread.Sleep ( 500 );
		}
		connectedToMaster = false;
		Thread.CurrentThread.Join ( 10 );
	}

/*	void OnGUI ()
	{
		float width = 150;
		float y = 5;
		float height = 20;
		GUI.Box ( new Rect ( 5, y, width + 5, 100 ), "" );
		GUI.Label ( new Rect ( 10, y, width, height ), "ROS started: " + ROS.isStarted () );
		y += height;
		GUI.Label ( new Rect ( 10, y, width, height ), "ROS OK: " + ROS.ok );
		y += height;
		GUI.Label ( new Rect ( 10, y, width, height ), "ROS stopping: " + ROS.shutting_down );
		y += height * 2;

		if ( ROS.isStarted () )
		{
			if ( GUI.Button ( new Rect ( 5, y, width + 5, height ), "Stop ROS" ) )
			{
				ROSController.StopROS ();
			}
		} else
		{
			if ( GUI.Button ( new Rect ( 5, y, width + 5, height ), "Start ROS" ) )
			{
				ROSController.StartROS ();
			}
		}
	}*/

	public static void StartROS (Action callback = null)
	{
		#if UNITY_EDITOR
		if (!EditorApplication.isPlaying)
			return;
		#endif

		lock ( instanceLock )
		{
			if ( instance == null )
			{
				lock ( callbackLock )
				{
					if ( callback != null )
						callbacks.Enqueue ( callback );
				}
				GameObject go = new GameObject ( "ROSController" );
				go.AddComponent<ROSController> ();
				return;
			}
		}

		lock ( instanceLock )
		{
			if ( instance.initComplete )
	//		if ( ROS.isStarted () && ROS.ok )
			{
				if ( callback != null )
				{
					new Thread ( new ThreadStart ( callback ) ).Start ();
	//				callback ();
				}
				return;
			}
		}

		lock ( callbackLock )
		{
			if ( callback != null )
				callbacks.Enqueue ( callback );
		}

		lock ( instanceLock )
		{
			if ( instance.starting )
				return;
		}

		// this gets set when the environment variable ROS_MASTER_URI isn't set
		if ( delayedStart )
			return;

//		string timeString = DateTime.UtcNow.ToString ( "MM_dd_yy_HH_MM_ss" );
//		Debug.Log ( timeString );
		lock ( instanceLock )
		{
			if ( instance.starting )
				return;
			
			instance.starting = true;
			instance.stopping = false;
			Debug.Log ( "ROS is starting" );
			if ( instance.nodePrefix == null )
				instance.nodePrefix = "";
//			instance.status = ROSStatus.Connecting;
			new System.Threading.Thread ( () =>
			{
				ROS.Init ( new string[0], instance.nodePrefix );
			} ).Start ();
			//		ROS.Init ( new string[0], instance.nodePrefix );
			instance.StartCoroutine ( instance.WaitForInit () );
		}
	}

	public static void StopROS (Action callback = null)
	{
		if ( ROS.isStarted () && !ROS.shutting_down && !instance.stopping )
		{
//			instance.status = ROSStatus.Disconnected;
			instance.starting = false;
			instance.stopping = true;
			while ( nodes.Count > 0 )
			{
				NodeHandle node = nodes.Dequeue ();
				node.shutdown ();
				node.Dispose ();
			}
			Debug.Log ( "stopping ROS" );
			ROS.shutdown ();
			ROS.waitForShutdown ();

		}
		if ( callback != null )
			callback ();
	}

	public static void AddNode (NodeHandle nh)
	{
		nodes.Enqueue ( nh );
	}

	IEnumerator WaitForInit ()
	{
		while ( !ROS.isStarted () && !ROS.ok && !stopping && ROS.GlobalNodeHandle == null )
			yield return null;

		#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_LINUX_API
		yield return new WaitForSeconds (0.1f);
		if ( ROS.GlobalNodeHandle == null )
			Debug.LogError ("Why is this null!?");
		#endif

//		XmlRpcUtil.SetLogLevel(XmlRpcUtil.XMLRPC_LOG_LEVEL.ERROR);
		if ( ROS.ok && !stopping )
		{
			lock ( instanceLock )
			{
				starting = false;
				initComplete = true;
			}
//			status = ROSStatus.Connected;
			Debug.Log ( "ROS Init successful" );
			lock ( callbackLock )
			{
				while ( callbacks != null && callbacks.Count > 0 )
				{
					Action action = callbacks.Dequeue ();
					new Thread ( new ThreadStart ( action ) ).Start ();
//					callbacks.Dequeue () ();
				}
			}
		}
	}
}