using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadUI : MonoBehaviour
{
	static QuadUI instance;
	public static bool InputActive { get { return instance.inputActiveText.enabled; } set { instance.inputActiveText.enabled = value; } }
	public Text recordButtonText;
	public Image connectionStatus;
	public Text connectionStatusText;
	public Text inputActiveText;

	Color orange = new Color ( 1, 0.5f, 0, 1 );
	ROSStatus lastStatus;
	ROSStatus status;


	void Awake ()
	{
		instance = this;
		lastStatus = ROSStatus.Unknown;
	}

	void Update ()
	{
		status = ROSController.Status;
		if ( status != lastStatus )
		{
			if ( status == ROSStatus.Connected )
			{
				connectionStatus.color = Color.green;
				connectionStatusText.text = "Connected to ROS";
			} else
			if ( status == ROSStatus.Connecting )
			{
				connectionStatus.color = orange;
				connectionStatusText.text = "Connecting to ROS";
			} else
			{
				connectionStatus.color = Color.red;
				connectionStatusText.text = "Disconnected from ROS";
			}
			lastStatus = status;
		}
	}

	public void OnRecordButton ()
	{
		if (QuadController.ActiveController.isRecordingPath)
		{
			recordButtonText.text = "Record Path";
			QuadController.ActiveController.EndRecordPath ();
		} else
		{
			recordButtonText.text = "Stop Recording";
			QuadController.ActiveController.BeginRecordPath ();
		}
	}
}