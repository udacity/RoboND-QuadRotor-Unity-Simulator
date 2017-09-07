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
	public Toggle peopleToggle;

	// training
	public GameObject peopleSpawnerObject;
	public GameObject peopleCamObject;
	public GameObject recordingObject;
	public Toggle heroToggle;
	public Toggle otherToggle;
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
		if ( Input.GetKeyDown ( KeyCode.F1 ) )
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene ( UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name );
		}
		if ( otherToggle.interactable != heroToggle.isOn )
		{
			otherToggle.interactable = heroToggle.isOn;
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
			if ( peopleToggle.isOn )
			{
				peopleSpawnerObject.GetComponent<PeopleSpawner> ().UseHero ( heroToggle.isOn, otherToggle.isOn );
				peopleSpawnerObject.SetActive ( true );
			} else
				peopleSpawnerObject.SetActive ( false );
			peopleCamObject.SetActive ( false );
			recordingObject.SetActive ( false );
		} else

		// deep learning
		if ( mode == 1 )
		{
			peopleSpawnerObject.GetComponent<PeopleSpawner> ().UseHero ( heroToggle.isOn, otherToggle.isOn );
			rosObject.SetActive ( false );
			quadObject.SetActive ( false );
			quadCamObject.SetActive ( false );
			quadCanvasObject.SetActive ( false );
			commandServerObject.SetActive ( false );
			peopleSpawnerObject.SetActive ( true );
			peopleCamObject.SetActive ( true );
			recordingObject.SetActive ( true );
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