using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action = System.Action;

public class RecordingController : MonoBehaviour
{
    public static RecordingController INSTANCE;
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
        RecordingController.INSTANCE = this;

		SaveLocation = "";
		OnCancelRecord ();
	}

	void LateUpdate ()
	{
		if ( Input.GetButtonDown ( "Record" ) )
        {
            if ( DataExtractionManager.INSTANCE.isExtractionRunning() )
            {
                Debug.Log( "THERE IS AN EXTRACTION IN PROCESS" );
                return;
            }
            
			ToggleRecording ();
        }
		
	}

	public void ToggleRecording ()
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

    public void forceSaveLocation( string newSaveLocation )
    {
        RecordingController.SaveLocation = newSaveLocation;
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

	public void OnBeginRecord ()
	{
		recording = true;
		recordingStatus.text = "Recording!";
		recordingStatus.color = Color.green;
		if ( BeginRecordCallback != null )
			BeginRecordCallback ();
        if ( DataExtractionManager.INSTANCE.isExtractionRunning() )
        {
            if ( !DataExtractionManager.INSTANCE.isExtractionTypeBatch() )
            {
                DataExtractionManager.INSTANCE.onBeginRecordingSingle();
            }
        }
	}

	public void OnCancelRecord ()
	{
		recordingStatus.text = "Not Recording";
		recordingStatus.color = Color.red;
        if ( DataExtractionManager.INSTANCE.isExtractionRunning() &&
             !DataExtractionManager.INSTANCE.isExtractionTypeBatch() )
        {
            DataExtractionManager.INSTANCE.onCancelRecordingSingle();
        }
	}

	public void OnEndRecord ()
	{
		recording = false;
		recordingStatus.text = "Not Recording";
		recordingStatus.color = Color.red;
		if ( EndRecordCallback != null )
			EndRecordCallback ();
        if ( DataExtractionManager.INSTANCE.isExtractionRunning() )
        {
            if ( !DataExtractionManager.INSTANCE.isExtractionTypeBatch() )
            {
                DataExtractionManager.INSTANCE.onEndRecordingSingle();
            }
        }
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