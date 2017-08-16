using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest : MonoBehaviour
{
	void Awake ()
	{
		Vector3 v1 = Vector3.right;
		Debug.Log ( "v1: " + v1 + " After double flip: " + v1.ToRos ().ToUnity () );

		Vector3 v2 = Vector3.forward;
		Debug.Log ( "v2: " + v2 + " After double flip: " + v2.ToRos ().ToUnity () );

		Vector3 v3 = Vector3.up;
		Debug.Log ( "v3: " + v3 + " After double flip: " + v3.ToRos ().ToUnity () );

		Quaternion q1 = Quaternion.LookRotation ( v1 );
		Debug.Log ( "q1: " + q1 + " After double flip: " + q1.ToRos ().ToUnity () );

		Quaternion q2 = Quaternion.LookRotation ( v2 );
		Debug.Log ( "q2: " + q2 + " After double flip: " + q2.ToRos ().ToUnity () );

		Quaternion q3 = Quaternion.LookRotation ( v3 );
		Debug.Log ( "q3: " + q3 + " After double flip: " + q3.ToRos ().ToUnity () );

		Quaternion q4 = Quaternion.LookRotation ( ( v1 + v2 + v3 ).normalized );
		Debug.Log ( "q4: " + q4 + " After double flip: " + q4.ToUnity ().ToRos () );

		Quaternion q5 = new Quaternion ( 0.2f, -0.8f, 0.53f, -0.09f );
		Debug.Log ( "q5: " + q5 + " After double flip: " + q5.ToUnity ().ToRos () );
	}
}