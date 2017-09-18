using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReplacementShader : MonoBehaviour
{
	public Shader shader;

	Camera cam;

	void Awake ()
	{
		cam = GetComponent<Camera> ();
		cam.depthTextureMode = DepthTextureMode.Depth;
//		cam.SetReplacementShader ( shader, "" );
	}
}