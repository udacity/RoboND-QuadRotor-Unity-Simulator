using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "post effect" that removes the need for separate depth cam. outputs color cam's depth buffer to the depth RenderTexture
// attach to color cam, assign depth RT and DepthMat material (or any that uses Custom/DepthGrayscale shader)

public class RGBToDepth : MonoBehaviour
{
	public RenderTexture destTex;
	public Material material;
	public GameObject bwCamObj;

	Camera cam;

	void Awake ()
	{
		cam = GetComponent<Camera> ();
//		Camera.onPostRender += DoPostRender;
	}

	void Start ()
	{
		if ( bwCamObj != null && bwCamObj.activeSelf )
			enabled = false;
	}

//	void DoPostRender (Camera _cam)
//	{
//		if ( _cam == cam )
//			Graphics.Blit ( _cam.targetTexture, destTex, material );
//	}

	void OnPostRender ()
	{
		Graphics.Blit ( cam.targetTexture, destTex, material );
	}
}