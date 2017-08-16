using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
	public GameObject[] fullBody;
	public GameObject[] overs;
	public GameObject[] tops;
	public GameObject[] bottoms;
	public GameObject[] shoes;
	public GameObject[] underBottom;
	public GameObject[] underTop;

	public Renderer body;
	public Renderer hair;

	public bool female;

	// Use this for initialization
	void Awake ()
	{
		if ( fullBody == null )
			fullBody = new GameObject[0];
		if ( underTop == null )
			underTop = new GameObject[0];
		if ( overs == null )
			overs = new GameObject[0];

		bool useFullBody = false;

		if ( fullBody != null && fullBody.Length > 0 )
			useFullBody = Random.value >= 0.75f;

		// start by turning EVERYTHING off
		fullBody.ForEach ( x => x.SetActive ( false ) );
		tops.ForEach ( x => x.SetActive ( false ) );
		bottoms.ForEach ( x => x.SetActive ( false ) );
		underTop.ForEach ( x => x.SetActive ( false ) );
		underBottom.ForEach ( x => x.SetActive ( false ) );
		shoes.ForEach ( x => x.SetActive ( false ) );

		int index;
		if ( useFullBody )
		{
			index = Random.Range ( 0, fullBody.Length );
			fullBody [ index ].SetActive ( true );

		} else
		{
			index = Random.Range ( 0, tops.Length );
			tops [ index ].SetActive ( true );
			index = Random.Range ( 0, bottoms.Length );
			bottoms [ index ].SetActive ( true );
			if ( overs != null && overs.Length > 0 && Random.value > 0.75f )
			{
				index = Random.Range ( 0, overs.Length );
				overs [ index ].SetActive ( true );
			}
		}

		index = Random.Range ( 0, shoes.Length );
		shoes [ index ].SetActive ( true );

		hair.material.color = Random.ColorHSV ();
		body.material.color = Color.white * Random.Range ( 0.15f, 1f );
	}
}