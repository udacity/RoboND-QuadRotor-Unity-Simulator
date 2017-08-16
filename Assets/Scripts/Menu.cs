using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public Canvas canvas;

	public GameObject rosObject;
	public GameObject quadObject;
	public GameObject quadCamObject;
	public GameObject quadCanvasObject;

	public GameObject peopleSpawnerObject;
	public GameObject peopleCamObject;
	public GameObject recordingObject;


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
			rosObject.SetActive ( true );
			quadObject.SetActive ( true );
			quadCamObject.SetActive ( true );
			quadCanvasObject.SetActive ( true );
			peopleSpawnerObject.SetActive ( false );
			peopleCamObject.SetActive ( false );
			recordingObject.SetActive ( false );
		} else

		// deep learning
		if ( mode == 1 )
		{
			rosObject.SetActive ( false );
			quadObject.SetActive ( false );
			quadCamObject.SetActive ( false );
			quadCanvasObject.SetActive ( false );
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