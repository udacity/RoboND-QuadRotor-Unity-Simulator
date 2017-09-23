using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	Slider slider;
	Text text;
	float lastTimeScale;

	void Awake ()
	{
		slider = GetComponent<Slider> ();
		text = GetComponentInChildren<Text> ();
		slider.value = 1;
		SetText ( 1f );
	}

	void Update ()
	{
		if ( Time.timeScale != lastTimeScale )
		{
			lastTimeScale = Time.timeScale;
			SetText ( Time.timeScale );
		}
	}

	public void OnSliderValueChanged ()
	{
		Time.timeScale = lastTimeScale = slider.value;
		SetText ( slider.value );
	}

	void SetText (float value)
	{
		text.text = "Time scale: " + value.ToString ("F1");
	}
}