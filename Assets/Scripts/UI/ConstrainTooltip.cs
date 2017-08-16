using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstrainTooltip : MonoBehaviour
{
	public static ConstrainTooltip instance;

	public Text text;

	RectTransform rt;
	ConstrainAxes lastCA;
	bool lastActive;
	Vector3 offset = new Vector3 ( 10, 10, 0 );

	void Awake ()
	{
		instance = this;
		rt = GetComponent<RectTransform> ();
		Hide ();
	}

	void Update ()
	{
		rt.position = Input.mousePosition - offset;
		if ( lastCA != null && lastCA.active != lastActive )
			Show ( lastCA );
	}


	public void Show (ConstrainAxes ca)
	{
		text.text = ca.tooltip;
		if ( ca.active )
			text.text += ": <color=^0>ON</color>".Replace ( "^0", ColorToHex ( ca.color ) );
		else
			text.text += ": <color=^0>OFF</color>".Replace ( "^0", ColorToHex ( ca.color ) );
		
		lastActive = ca.active;
		lastCA = ca;
		gameObject.SetActive ( true );
	}

	public void Hide ()
	{
		gameObject.SetActive ( false );
	}

	string ColorToHex (Color c)
	{
		Color32 c32 = c;
		return "#" + c32.r.ToString ( "x2" ) + c32.g.ToString ( "x2" ) + c32.b.ToString ( "x2" );
	}
}