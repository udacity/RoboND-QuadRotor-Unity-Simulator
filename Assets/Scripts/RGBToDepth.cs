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
	public bool asImageEffect;
	public bool useMaterial = true;

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

	void OnPostRender ()
	{
		if ( !asImageEffect )
		{
			if ( useMaterial )
				Graphics.Blit ( cam.targetTexture, destTex, material );
			else
				Graphics.Blit ( cam.targetTexture, destTex );
		}
	}

//	void OnOnRenderImage (RenderTexture src, RenderTexture dst)
//	{
//		if ( asImageEffect )
//			Graphics.Blit ( src, dst, material );
//		else
//			Graphics.Blit ( src, dst );
//	}
}