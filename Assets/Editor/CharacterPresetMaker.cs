using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterPresetMaker : MonoBehaviour
{
	[MenuItem ("People/Spawn Person", false, 1)]
	static void SpawnPerson ()
	{
		GameObject prefab = Selection.activeGameObject;

		GameObject instance = Instantiate ( prefab );
		instance.SetActive ( true );
		instance.GetComponent<CharacterCustomization> ().SendMessage ( "Awake" );

		Camera[] sceneCams = SceneView.GetAllSceneCameras ();
		sceneCams [ 0 ].transform.position = instance.transform.position + instance.transform.forward * 5;
		sceneCams [ 0 ].transform.LookAt ( instance.transform.position );
	}

	[MenuItem ("People/Assign Preset", false, 2)]
	static void AssignPreset ()
	{
		GameObject person = Selection.activeGameObject;
		CharacterCustomization appearance = person.GetComponent<CharacterCustomization> ();

		GameObject spawner = GameObject.Find ( "PeopleSpawner" );
		if ( spawner == null )
		{
			EditorUtility.DisplayDialog ( "Spawner inactive", "Set PeopleSpawner active and try again", "OK" );
			return;
		}

		AppearancePreset preset = new AppearancePreset ( appearance );

		PeopleSpawner script = spawner.GetComponent<PeopleSpawner> ();
		script.heroPreset = preset;

		DestroyImmediate ( person );
	}
}