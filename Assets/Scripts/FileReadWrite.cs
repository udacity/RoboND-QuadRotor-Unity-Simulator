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

    // Change ///////////////////////////////////////////////
    void LateUpdate ()
    {

        if ( Input.GetKeyDown ( KeyCode.Y ) )
        {
            BrowseSaveFile ();
        }

        if ( Input.GetKeyDown ( KeyCode.F ) )
        {
            BrowseLoadFile ();
        }

    }


    public void BrowseLoadFile ()
    {
        SimpleFileBrowser.ShowLoadDialog ( LoadFile, null, false, null, "Select Input File", "Open" );
    }


    public void BrowseSaveFile ()
    {
        SimpleFileBrowser.ShowSaveDialog ( SaveFile, null, true, null, "Select Output File", "Select" );
    }

    void LoadFile (string location)
    {
        Debug.Log ( "location " + location );

        // Load the path-hero locations from the .json files ...
        // back into the appropiate managers
        string _svText = File.ReadAllText(location);
        PathSampleCompound _svCompound = JsonUtility.FromJson<PathSampleCompound>( _svText );

        PatrolPathManager.FromSamples( _svCompound.patrol );
        HeroPathManager.FromSamples( _svCompound.hero );
        SpawnPointManager.FromSamples( _svCompound.spawns );
    }

    void SaveFile (string location)
    {
        Debug.Log ( "location " + location );

        PathSample[] patrol = PatrolPathManager.GetPath ();
        PathSample[] hero = HeroPathManager.GetPath ();
        PathSample[] spawns = SpawnPointManager.GetPath ();

        PathSampleCompound _compound = new PathSampleCompound( patrol, hero, spawns );
        
        string _date = "_" + 
                       System.DateTime.Now.Day.ToString() +
                       System.DateTime.Now.Month.ToString() +
                       System.DateTime.Now.Hour.ToString() +
                       System.DateTime.Now.Minute.ToString() +
                       System.DateTime.Now.Second.ToString() + "_";
                       
        string _json = JsonUtility.ToJson( _compound );
        string _svFileName = "sv_points" + _date + ".json";
        File.WriteAllText( System.IO.Path.Combine( location, _svFileName ), _json );
    }
    /////////////////////////////////////////////////////////

}
