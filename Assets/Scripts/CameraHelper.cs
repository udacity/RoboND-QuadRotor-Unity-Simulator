using UnityEngine;

public static class CameraHelper
{
  public static byte[] CaptureFrame(Camera camera)
  {
		if ( camera == null )
		{
			Debug.Log ( "null camera" );
			return new byte[0];
		}
//		System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch ();
    RenderTexture targetTexture = camera.targetTexture;
    RenderTexture.active = targetTexture;
	Texture2D texture2D = new Texture2D(targetTexture.width, targetTexture.height, TextureFormat.RGB24, false);
	texture2D.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
    texture2D.Apply();
//		w.Start ();
	byte[] image = texture2D.EncodeToJPG();
//		w.Stop ();
//		Debug.Log ("capture2 took " + w.ElapsedMilliseconds + "ms");
	Object.DestroyImmediate(texture2D); // Required to prevent leaking the texture
    return image;
  }
  
  public static byte[] CaptureDepthFrame(Camera camera)
  {
		if ( camera == null )
		{
			Debug.Log ( "null camera" );
			return new byte[0];
		}
    RenderTexture targetTexture = camera.targetTexture;
    RenderTexture.active = targetTexture;
	  Texture2D texture2D = new Texture2D(targetTexture.width, targetTexture.height, TextureFormat.ARGB32, false);
    texture2D.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
    texture2D.Apply();
//		System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch ();
//		w.Start ();
    // Debug
    //Debug.Log("Values at 10,10: " + texture2D.GetPixel(10,10));

	  byte[] image = texture2D.EncodeToPNG();
//		w.Stop ();
//		Debug.Log ("capture4 took " + w.ElapsedMilliseconds + "ms");
	  Object.DestroyImmediate(texture2D); // Required to prevent leaking the texture
    return image;
  }
}
