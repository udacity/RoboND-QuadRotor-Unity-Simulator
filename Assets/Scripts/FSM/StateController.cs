using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
	StateBase[] states;
	StateBase curState;
	StateBase lastState;

	void Awake ()
	{
		states = GetComponentsInChildren<StateBase> ();
	}

	void Update ()
	{
		if ( curState != null )
		{
			curState.OnUpdate ();
		}
	}
	void LateUpdate ()
	{
		if ( curState != null )
		{
			curState.OnLateUpdate ();
		}
	}

	public void SetState (string stateName)
	{
		if ( IsCurrentStateName ( stateName ) )
			return;
		
		if ( curState != null )
			curState.OnExit ();
		lastState = curState;
		curState = states.Find ( x => x.stateName == stateName );
		if ( curState != null )
			curState.OnEnter ();
	}

	public void RevertState ()
	{
		if ( curState != null )
			curState.OnExit ();
		var temp = curState;
		curState = lastState;
		lastState = temp;

		if ( curState != null )
			curState.OnEnter ();
	}

	public bool IsCurrentStateName (string s)
	{
		if ( curState == null )
			return false;
		return curState.stateName == s;
	}
}