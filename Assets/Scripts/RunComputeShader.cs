using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunComputeShader : MonoBehaviour
{
	public ComputeShader shader;

	void Awake ()
	{
		if ( shader != null )
			RunShader ();
	}

	void RunShader ()
	{
		int kernelHandle = shader.FindKernel ( "CSMain" );

		RenderTexture tex = new RenderTexture ( 256, 256, 24 );
		tex.enableRandomWrite = true;
		tex.Create ();

		shader.SetTexture ( kernelHandle, "Result", tex );
		shader.Dispatch ( kernelHandle, 256 / 8, 256 / 8, 1 );
	}
}