using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Action = System.Action;
using Pathing;

public class FileReadWrite : MonoBehaviour
{
	
	SimpleFileBrowser browser;
//	string saveLocation = "";

	void Awake ()
	{
		browser = new SimpleFileBrowser();
		browser.AcceptNonExistingFilename = true;
	}

//	void LateUpdate ()
//	{
//
//		if ( Input.GetKeyDown ( KeyCode.Y ) )
//		{
//			BrowseSaveFile ();
//		}
//
//		if ( Input.GetKeyDown ( KeyCode.H ) )
//		{
//			BrowseLoadFile ();
//		}
//
//	}


	public void BrowseLoadFile ()
	{
		SimpleFileBrowser.ShowLoadDialog ( LoadFile, null, false, null, "Select Input File", "Select" );
	}


	public void BrowseSaveFile ()
	{
		SimpleFileBrowser.ShowSaveDialog ( SaveFile, null, true, null, "Select Output File", "Select" );
	}

	void LoadFile (string location)
	{

		Debug.Log ( "location " + location );


	}

	void SaveFile (string location)

	{
		Debug.Log ( "location " + location );

 		PathSample[] patrol = PatrolPathManager.GetPath ();
		PathSample[] hero = HeroPathManager.GetPath ();
		PathSample[] spawns = SpawnPointManager.GetPath ();

		Debug.Log ( "patrol" + patrol );
//
		string patrol_json = JsonUtility.ToJson ( patrol );
		string hero_json = JsonUtility.ToJson ( hero );
		string spawns_json = JsonUtility.ToJson ( spawns );
		
		Debug.Log ( "location " + spawns_json );
		File.WriteAllText( System.IO.Path.Combine(location,"tmp.txt"), spawns_json );


	}

}
