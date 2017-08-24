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

	public AppearancePreset () {}
	public AppearancePreset (CharacterCustomization c)
	{
		fullBody = -1;
		for ( int i = 0; i < c.fullBody.Length; i++ )
			if ( c.fullBody [ i ].activeSelf )
			{
				fullBody = i;
				break;
			}

		for ( int i = 0; i < c.overs.Length; i++ )
			if ( c.overs [ i ].activeSelf )
			{
				over = i;
				break;
			}

		for (int i = 0; i < c.tops.Length; i++)
			if ( c.tops[i].activeSelf )
			{
				top = i;
				break;
			}

		for ( int i = 0; i < c.bottoms.Length; i++ )
			if ( c.bottoms [ i ].activeSelf )
			{
				bottom = i;
				break;
			}

		for ( int i = 0; i < c.shoes.Length; i++ )
			if ( c.shoes[i].activeSelf )
			{
				shoes = i;
				break;
			}

		for ( int i = 0; i < c.underTop.Length; i++ )
			if ( c.underTop[i].activeSelf )
			{
				underTop = i;
				break;
			}

		for ( int i = 0; i < c.underBottom.Length; i++ )
			if ( c.underBottom[i].activeSelf )
			{
				underBottom = i;
				break;
			}

		hairColor = c.hair.material.color;
		bodyColor = c.body.material.color;

		female = c.name.ToLower ().Contains ( "female" );
	}
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
		if ( fullBody == null )
			fullBody = new GameObject[0];
		if ( underTop == null )
			underTop = new GameObject[0];
		if ( overs == null )
			overs = new GameObject[0];
	}

	void Start ()
	{
		if ( !spawned )
		{
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
			if ( preset.top != -1 && tops.Length > preset.top )
				tops [ preset.top ].SetActive ( true );
			if ( preset.bottom != -1 && bottoms.Length > preset.bottom )
				bottoms [ preset.bottom ].SetActive ( true );
			if ( preset.over != -1 && overs.Length > preset.over )
				overs [ preset.over ].SetActive ( true );
		}

		if ( preset.shoes != -1 && shoes.Length > preset.shoes )
			shoes [ preset.shoes ].SetActive ( true );

		body.material.color = preset.bodyColor;
		hair.material.color = preset.hairColor;
		spawned = true;
	}
}