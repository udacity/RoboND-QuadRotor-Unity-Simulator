using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppearancePreset
{
	public int fullBody = -1;
	public int over = -1;
	public int top = -1;
	public int bottom = -1;
	public int shoes = -1;
	public int underBottom = -1;
	public int underTop = -1;

	public Color bodyColor = Color.white;
	public Color hairColor = Color.white;
	public bool female;
}

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
	bool spawned;

	// Use this for initialization
	void Awake ()
	{
		if ( !spawned )
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
			DisableAll ();
			
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

	void DisableAll ()
	{
		fullBody.ForEach ( x => x.SetActive ( false ) );
		tops.ForEach ( x => x.SetActive ( false ) );
		bottoms.ForEach ( x => x.SetActive ( false ) );
		underTop.ForEach ( x => x.SetActive ( false ) );
		underBottom.ForEach ( x => x.SetActive ( false ) );
		shoes.ForEach ( x => x.SetActive ( false ) );
	}

	public void SetAppearance (AppearancePreset preset)
	{
		DisableAll ();

		if ( preset.fullBody != -1 )
		{
			fullBody [ preset.fullBody ].SetActive ( true );

		} else
		{
			if ( preset.top != -1 )
				tops [ preset.top ].SetActive ( true );
			if ( preset.bottom != -1 )
				bottoms [ preset.bottom ].SetActive ( true );
			if ( preset.over != -1 )
				overs [ preset.over ].SetActive ( true );
		}

		if ( preset.shoes != -1 )
			shoes [ preset.shoes ].SetActive ( true );

		body.material.color = preset.bodyColor;
		hair.material.color = preset.hairColor;
		spawned = true;
	}
}