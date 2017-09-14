using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// grabs the depth buffer from the color cam's RenderTexture to use while rendering mask cams
// the color cam will now render the other cameras manually using its own depth buffer with a shader that doesn't write to z-buffer

public class ReplaceDepthBuffer : MonoBehaviour
{
	public Camera bwCam1;
	public Camera bwCam2;

	Camera cam;
	RenderTexture ownTex;
	RenderTexture tt1;
	RenderTexture tt2;

	void Awake ()
	{
		cam = GetComponent<Camera> ();
		ownTex = cam.targetTexture;
		tt1 = bwCam1.targetTexture;
		tt2 = bwCam2.targetTexture;
//		cam.depthTextureMode = DepthTextureMode.Depth;
//		Debug.Log ( "cam " + name + " depth texture: " + cam.depthTextureMode );
		bwCam1.enabled = bwCam2.enabled = false;
	}

	void OnPostRender ()
	{
//		bwCam1.targetTexture = tt1;
//		bwCam2.targetTexture = tt2;
		bwCam1.SetTargetBuffers ( tt1.colorBuffer, ownTex.depthBuffer );
		bwCam2.SetTargetBuffers ( tt2.colorBuffer, ownTex.depthBuffer );
		bwCam1.Render ();
		bwCam2.Render ();
		bwCam1.targetTexture = tt1;
		bwCam2.targetTexture = tt2;
		bwCam1.SetTargetBuffers ( tt1.colorBuffer, tt1.depthBuffer );
		bwCam2.SetTargetBuffers ( tt2.colorBuffer, tt2.depthBuffer );
//		bwCam1.targetTexture = tt1;
//		bwCam2.targetTexture = tt2;
	}
}