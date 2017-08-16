using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class BuildScript : MonoBehaviour
{
	[MenuItem ("Udacity/Build Windows", false, 20)]
	static void BuildWindows ()
	{
		Build ( BuildTarget.StandaloneWindows64 );
	}

	[MenuItem ("Udacity/Build Mac", false, 21)]
	static void BuildMac ()
	{
		Build ( BuildTarget.StandaloneOSXIntel64 );
	}

	[MenuItem ("Udacity/Build Linux", false, 22)]
	static void BuildLinux ()
	{
		Build ( BuildTarget.StandaloneLinux64 );
	}

	[MenuItem ("Udacity/Build All", false, 23)]
	static void BuildAll ()
	{
		Build ( BuildTarget.StandaloneWindows64, false );
		Build ( BuildTarget.StandaloneOSXIntel64, false );
		Build ( BuildTarget.StandaloneLinux64 );
	}


	static void Build (BuildTarget target, bool showBuilt = true)
	{
		string[] scenes = new string[] {
			"Assets/Scenes/loader.unity",
			"Assets/Scenes/quad_indoor.unity",
			"Assets/Scenes/proto4.unity"
		};

		string basePath = "Builds/DroneSim/";
		string settingFile = Application.dataPath + "/ros_settings.txt";
		string settingPath = "DroneSim_Win_Data";

		string extension = "DroneSim_Win.exe";
		if ( target == BuildTarget.StandaloneOSXIntel64 )
		{
			extension = "DroneSim_OSX.app";
			settingPath = extension + "/Contents";
		} else
		if ( target == BuildTarget.StandaloneLinux64 )
		{
			extension = "DroneSim_Lin.x86_64";
			settingPath = "DroneSim_Lin_Data";
		}
		
		BuildPipeline.BuildPlayer ( scenes, basePath + extension, target, showBuilt ? BuildOptions.ShowBuiltPlayer : BuildOptions.None );

		// go up one from Assets
		string targetFile = Application.dataPath + "/../" + basePath + settingPath + "/ros_settings.txt";
//		Debug.Log ( "source file: " + settingFile + "\ntarget file: " + targetFile );
		System.IO.File.Copy ( settingFile, targetFile );
	}
}