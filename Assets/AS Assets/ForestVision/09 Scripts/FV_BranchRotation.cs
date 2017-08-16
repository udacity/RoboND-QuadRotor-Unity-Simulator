using UnityEngine;
using System.Collections;

public class FV_BranchRotation : MonoBehaviour {
	//============================================================================= Exposed Variables
	[Header("General Branch Controls")]
	[Space(10)]

	[Tooltip("Distance from Main Camera to this branch, at which point branch movement will cease")]
	public float stopSwayDistance = 50;
	[Tooltip("Determines how profound branch movements will be during runtime")]
	public float WindStrength = 1;
	[Tooltip("Used to control overall amount of wind applied to the branch: 0 = no wind at all. ")]
	[Range(0,2f)]
	public float WindDampening = 1;
	[Space(10)]

	[Header("Initial Branch Runtime Offset")]
	[Space(10)]
	[Tooltip("At runtime, how much do you want to offset your original branch rotations? Can help to minimize identical trees.")]
	[Range(0,5f)]
	public float Offset_Amount = 3f;
	[Space(10)]
	[Header("Periodic Randomness Update")]
	[Space(10)]
	[Tooltip("Minimum amount of time to wait before updating randomness")]
	[Range(0,3f)]
	public float Random_Range_Amount = 1.34f;
	[Tooltip("Minimum amount of time to wait before updating randomness")]
	public float RandomUpdate_Min = 5f;
	[Tooltip("Minimum amount of time to wait before updating randomness")]
	public float RandomUpdate_Max = 40f;
	[Space(10)]

	[Header("Specific Wind Parameters")]
	[Range(0,0.1f)]
	public float X_Angle = 0.05f;
	[Range(-1f,1f)]
	public float X_Period = 0.854f;
	[Range(0,0.1f)]
	public float Y_Angle = 0.05f;
	[Range(-1f,1f)]
	public float Y_Period = 1.047f;
	[Range(0,0.1f)]
	public float Z_Angle = 0.05f;
	[Range(-1f,1f)]
	public float Z_Period = 0.754f;


	//================================================================================ end
	private float levelWindFrequency;
	private float Angle_Multipler = 1;
	private float Period_Multipler = 1;

	private float GlobalWindX = 1;
	private float GlobalWindY = 1;
	private float GlobalWindZ = 1;

	private float _Time;
	private int RandomSeeder;
	private float RandomRanger;

	private float rand1;
	private float rand2;

	private float WindRandomness;

	private float Xphase;
	private float Yphase;
	private float Zphase;

	private float xSpeedStart, ySpeedStart, zSpeedStart;//used to hold original wind values

	private float originalX, originalY, originalZ; //used to get original rotation values of branch
	private Vector3 originalBranchRotation; //used to apply original values back to the branch

	//================================================================================

	void Start(){
		
		//generate unique seed value from this branch position
		Random.seed = Mathf.RoundToInt(transform.localPosition.x);

		//offset orignal period by a little
		X_Period = X_Period + Random.Range(0.005f, 0.009f); 
		Y_Period = Y_Period + Random.Range(0.002f, 0.007f);
		Z_Period = Z_Period + Random.Range(0.001f, 0.006f);

		//offset original angle by a little
		X_Angle = X_Angle + Random.Range(0.001f, 0.003f); xSpeedStart = X_Angle;
		Y_Angle = Y_Angle + Random.Range(0.001f, 0.003f); ySpeedStart = Y_Angle;
		Z_Angle = Z_Angle + Random.Range(0.001f, 0.003f); zSpeedStart = Z_Angle;

		// random wind movement generation
		PeriodicRandomUpdate();
			//Debug.Log(rand1 + "  and   " + rand2);

		//randomly offset branches from their starting values
		InitialRandomOffset();
		//now store this new offset as base rotation for the branch
		originalX = transform.eulerAngles.x;
		originalY = transform.eulerAngles.y;
		originalZ = transform.eulerAngles.z;
		//set these values into a new Vector3 so we can move back to this offset later
		originalBranchRotation.x = originalX;
		originalBranchRotation.y = originalY;
		originalBranchRotation.z = originalZ;

		//run initial coroutine for periodic updates to random generator
		StartCoroutine("PeriodicRandomUpdate");

		//StartCoroutine("WindGen");
	}

	IEnumerator WindGen(){
		yield return new WaitForSeconds(4);
		StartCoroutine("BlowX");
	}

	IEnumerator BlowX(){
		X_Angle = X_Angle * GlobalWindX;

		yield return new WaitForSeconds(4);
		X_Angle = xSpeedStart;
		StartCoroutine("BlowY");
	}

	IEnumerator BlowY(){
		Y_Angle = Y_Angle * GlobalWindY;

		yield return new WaitForSeconds(4);
		Y_Angle = ySpeedStart;
		StartCoroutine("BlowZ");
	}

	IEnumerator BlowZ(){
		Z_Angle = Z_Angle * GlobalWindZ;

		yield return new WaitForSeconds(4);
		Z_Angle = zSpeedStart;
		StartCoroutine("BlowX");
	}



	IEnumerator PeriodicRandomUpdate() {

		//starting random value
		rand1 = Random.Range(-Random_Range_Amount,Random_Range_Amount);
		//animate to second random value
		rand2 = rand1 + Random.Range(-Random_Range_Amount,Random_Range_Amount) + Random.Range(-Random_Range_Amount,Random_Range_Amount);
			//Debug.Log(rand1 + "  and   " + rand2);

		//Wait a random interval specified by a min and max wait time from user
		yield return new WaitForSeconds(Random.Range(RandomUpdate_Min,RandomUpdate_Max));
		//run again
		StartCoroutine("PeriodicRandomUpdate");

	}


	//use this to offset each branch initially from start to help randomize overall look of the branch arrangement
	void InitialRandomOffset(){
		transform.Rotate( Random.Range(0f, Offset_Amount), Random.Range(0f, Offset_Amount), Random.Range(0f, Offset_Amount), Space.Self);

	}

	// Update is called once per frame
	void Update () {
		
		//get parameters from FV_WindZone if it exists in the scene
		if(GameObject.Find("FV_WindZone") != null){
			levelWindFrequency = GameObject.Find("FV_WindZone").GetComponent<FV_Wind>().Wind_Intensity;
			Angle_Multipler = GameObject.Find("FV_WindZone").GetComponent<FV_Wind>().Angle_Multipler;
			Period_Multipler = GameObject.Find("FV_WindZone").GetComponent<FV_Wind>().Shake_Distance;

			//GlobalWindX = GameObject.Find("FV_WindZone").GetComponent<FV_Wind>().WindX;
			//GlobalWindY = GameObject.Find("FV_WindZone").GetComponent<FV_Wind>().WindY;
			//GlobalWindZ = GameObject.Find("FV_WindZone").GetComponent<FV_Wind>().WindZ;

			//X_Angle = X_Angle * GlobalWindX;
			//Y_Angle = Y_Angle * GlobalWindY;
			//Z_Angle = Z_Angle * GlobalWindZ;
		}

		WindRandomness = Mathf.Sin(_Time / Mathf.Lerp(rand1, rand2, Time.time) );//generate random wind movement

		 
		//get distance to main camera
		float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
		//Debug.Log(dist);

		//caluclate time driven offset engine
		_Time = _Time + Time.deltaTime;
		Xphase = Mathf.Sin(_Time / (X_Period * Period_Multipler));
		Yphase = Mathf.Sin(_Time / (Y_Period * Period_Multipler));
		Zphase = Mathf.Sin(_Time / (Z_Period * Period_Multipler));

		//handle when to run offset engine
		if(dist < stopSwayDistance){ AnimateBranches();}

		//constantly be pulling branches back to their original positions
		PullBranchesBack();





	}




	void PullBranchesBack(){
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(originalBranchRotation), Time.deltaTime);
	}


	void AnimateBranches(){
		


		transform.Rotate((Xphase * (X_Angle * Angle_Multipler))  * ((WindStrength * WindRandomness) * (WindDampening * levelWindFrequency)), (Yphase * (Y_Angle * Angle_Multipler)) * ((WindStrength * WindRandomness) * (WindDampening * levelWindFrequency)), (Zphase * (Z_Angle * Angle_Multipler)) * ((WindStrength * WindRandomness) * (WindDampening * levelWindFrequency)), Space.Self);
	
	}


}
