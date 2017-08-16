using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action = System.Action;

public class RecordingController : MonoBehaviour
{
	public static Action BeginRecordCallback;
	public static Action EndRecordCallback;
	public static string SaveLocation { get; protected set; }
	public Text recordingStatus;

	public string imageDirectory = "IMG";
	public string framePrefix = "img_";

	bool recording;
	bool saveRecording;

//	string saveLocation = "";

	void Awake ()
	{
		SaveLocation = "";
		OnCancelRecord ();
	}

	void LateUpdate ()
	{
		if ( Input.GetButtonDown ( "Record" ) )
			ToggleRecording ();
		
	}

	void ToggleRecording ()
	{
		if ( recording )
		{
			OnEndRecord ();

		} else
		{
			if ( CheckSaveLocation () )
			{
				OnBeginRecord ();
			} else
			{
			}
		}
	}

	public bool CheckSaveLocation ()
	{
		if ( SaveLocation != "" )
		{
			return true;
		} else
		{
			SimpleFileBrowser.ShowSaveDialog ( OpenFolder, OnCancelRecord, true, null, "Select Output Folder", "Select" );
		}
		return false;
	}

	void OnBeginRecord ()
	{
		recording = true;
		recordingStatus.text = "Recording!";
		recordingStatus.color = Color.green;
		if ( BeginRecordCallback != null )
			BeginRecordCallback ();
	}

	void OnCancelRecord ()
	{
		recordingStatus.text = "Not Recording";
		recordingStatus.color = Color.red;
	}

	void OnEndRecord ()
	{
		recording = false;
		recordingStatus.text = "Not Recording";
		recordingStatus.color = Color.red;
		if ( EndRecordCallback != null )
			EndRecordCallback ();
	}

	void OpenFolder (string location)
	{
		Debug.Log ( "location " + location );
		SaveLocation = Path.Combine ( location, imageDirectory );

		try
		{
			Debug.Log ("Deleting any previous IMG folder and CSV file..");
//			if ( File.Exists ( Path.Combine ( SaveLocation, CSVFileName ) ) )
//				File.Delete ( Path.Combine ( SaveLocation, CSVFileName ) );
			if ( Directory.Exists ( SaveLocation ) )
				Directory.Delete ( SaveLocation, true );

		}
		catch (System.Exception e)
		{
			Debug.LogException ( e );
			OnCancelRecord ();
			return;
		}

//		SaveLocation = location;
		Directory.CreateDirectory ( SaveLocation );
//		Directory.CreateDirectory ( Path.Combine ( SaveLocation, imageDirectory ) );
		OnBeginRecord ();
	}
}