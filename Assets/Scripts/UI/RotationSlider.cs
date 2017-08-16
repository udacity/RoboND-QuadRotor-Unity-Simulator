using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RotationAxis { X, Y, Z }

public class RotationSlider : MonoBehaviour
{
	public QuadController quad;
	public RotationAxis axis;

	public void OnValueChanged (float value)
	{
		float adjustedValue = ( value - 0.5f ) * 2;
		Vector3 rotAxis = Vector3.zero;
		if ( axis == RotationAxis.X )
			rotAxis.x = adjustedValue;
		if ( axis == RotationAxis.Y )
			rotAxis.y = adjustedValue;
		if ( axis == RotationAxis.Z )
			rotAxis.z = adjustedValue;
		quad.ApplyMotorTorque ( rotAxis, true );
//		quad.ApplyMotorTorque ( rotAxis.x, rotAxis.y, rotAxis.z, true, true );
	}

	public void OnText ()
	{
		GetComponent<Slider> ().value = 0.5f;
		OnValueChanged ( 0.5f );
	}
}