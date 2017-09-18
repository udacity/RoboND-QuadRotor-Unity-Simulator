using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to a camera to disable shadows for it

public class DisableShadows : MonoBehaviour
{
	public bool disable = true;
	ShadowQuality storedShadows;

	void Awake ()
	{
		enabled = disable;
	}

	void OnPreRender ()
	{
		storedShadows = QualitySettings.shadows;
		QualitySettings.shadows = ShadowQuality.Disable;
	}

	void OnPostRender ()
	{
		QualitySettings.shadows = storedShadows;
	}
}