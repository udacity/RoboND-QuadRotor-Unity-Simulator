using UnityEngine;
using System.Collections;

public class FV_Wind : MonoBehaviour {

	[Header("Global Wind Parameters")]
	[Space(10)]
	[Tooltip("0 = No Shaking, 1 = Normal Shaking, 3 = Normal Shaking X 3")]
	[Range(0,3f)]
	public float Wind_Intensity = 0.73f;
	[Space(10)]
	[Tooltip("How much sway do you need in the branches? 1 = Normal, 3 = 3 times Normal")]
	[Range(1f,3f)]
	public float Angle_Multipler = 1.45f;
	[Space(10)]
	[Tooltip("How fast do the branches sway? 0.01 = Way too Fast, 1 = Normal")]
	[Range(0.01f,1f)]
	public float Shake_Distance = 0.8f;




}
