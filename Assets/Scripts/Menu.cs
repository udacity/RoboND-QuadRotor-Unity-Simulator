using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public Canvas canvas;

	// follow me
	public GameObject rosObject;
	public GameObject quadObject;
	public GameObject quadCamObject;
	public GameObject quadCanvasObject;
	public GameObject commandServerObject;
	public GameObject defaultTaskObject;
	public Toggle peopleToggle;

	// training
	public GameObject peopleSpawnerObject;
	public GameObject peopleCamObject;
	public GameObject recordingObject;
	public GameObject fileReadWrite;
	public Toggle heroToggle;
	public Toggle otherToggle;
	public Text heroText;
	public Text otherText;

	void Awake ()
	{
	}

	void Start ()
	{
		EnableCanvas ();
	}

	void LateUpdate ()
	{
		if ( heroToggle.interactable != peopleToggle.isOn )
		{
			heroToggle.interactable = peopleToggle.isOn;
			heroText.color = heroToggle.interactable ? Color.white : Color.gray;
			otherToggle.interactable = peopleToggle.isOn;
			otherText.color = otherToggle.interactable ? Color.white : Color.gray;
		}
	}

	void EnableCanvas ()
	{
		canvas.enabled = true;
	}

	public void OnModeSelect (int mode)
	{
		// controls
		if ( mode == 0 )
		{
//			rosObject.SetActive ( true );
			quadObject.SetActive ( true );
			quadCamObject.SetActive ( true );
			quadCanvasObject.SetActive ( true );
			commandServerObject.SetActive ( true );
			defaultTaskObject.SetActive ( true );
			if ( peopleToggle.isOn )
			{
				peopleSpawnerObject.GetComponent<PeopleSpawner> ().UseHero ( heroToggle.isOn, otherToggle.isOn );
				peopleSpawnerObject.SetActive ( true );
			} else
				peopleSpawnerObject.SetActive ( false );
			peopleCamObject.SetActive ( false );
			recordingObject.SetActive ( false );
			SimpleQuadController.ActiveController.gimbal.SetSecondaryCam ( 0 );
		} else

		// deep learning
		if ( mode == 1 )
		{
			if ( peopleToggle.isOn )
				peopleSpawnerObject.GetComponent<PeopleSpawner> ().UseHero ( heroToggle.isOn, otherToggle.isOn );
			quadObject.SetActive ( true );
			quadCamObject.SetActive ( true );
//			quadCanvasObject.SetActive ( true );
			commandServerObject.SetActive ( true );
//			rosObject.SetActive ( false );
//			quadObject.SetActive ( false );
//			quadCamObject.SetActive ( false );
			quadCanvasObject.SetActive ( false );
//			commandServerObject.SetActive ( false );
			defaultTaskObject.SetActive ( false );
			peopleSpawnerObject.SetActive ( false );
			fileReadWrite.SetActive ( true );

//			peopleCamObject.SetActive ( true );
			recordingObject.SetActive ( true );
			SimpleQuadController.ActiveController.gimbal.SetSecondaryCam ( 1 );
		}

		gameObject.SetActive ( false );
//		canvas.enabled = false;
	}

	public void OnExitButton ()
	{
		#if !UNITY_EDITOR
		ROSController.StopROS (new System.Action ( () => { Application.Quit (); } ));
		#endif
	}
}
