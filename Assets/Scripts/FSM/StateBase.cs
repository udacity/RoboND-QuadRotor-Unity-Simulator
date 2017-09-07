using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
	public string stateName;

	public virtual void OnEnter () {}
	public virtual void OnExit () {}
	public virtual void OnUpdate () {}
	public virtual void OnLateUpdate () {}
}