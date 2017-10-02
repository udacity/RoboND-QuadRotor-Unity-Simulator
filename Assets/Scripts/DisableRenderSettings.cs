using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to a camera to disable some render settings for it

public class DisableRenderSettings : MonoBehaviour
{
	public bool active = true;
	public bool disableShadows = true;
	public bool disableFog = true;


	ShadowQuality storedShadows;
	bool fogOn;

	void Awake ()
	{
		enabled = active;
	}

	void OnPreRender ()
	{
		if ( disableShadows )
		{
			storedShadows = QualitySettings.shadows;
			QualitySettings.shadows = ShadowQuality.Disable;
		}
		if ( disableFog )
		{
			fogOn = RenderSettings.fog;
			RenderSettings.fog = false;
		}
	}

	void OnPostRender ()
	{
		if ( disableShadows )
			QualitySettings.shadows = storedShadows;
		if ( disableFog )
			RenderSettings.fog = fogOn;
	}
}