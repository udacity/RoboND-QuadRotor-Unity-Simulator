using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class AssignMaterial : ScriptableWizard 
{
	public Material newMaterial;
	String strHelp = "Select Game Objects";
	GameObject[] gos;
	
	void OnWizardUpdate ()
	{
		helpString = strHelp;
		isValid = (newMaterial != null);
	}
	
	void OnWizardCreate ()
		
	{
		gos = Selection.gameObjects;
		foreach (GameObject go in gos)
		{
			go.GetComponent<Renderer>().material = newMaterial;
		}
	}

	[MenuItem ("Custom/Assign Material", false, 4)]
	static void assignMaterial()
	{
		ScriptableWizard.DisplayWizard ("Assign Material", typeof(AssignMaterial), "Assign");
	}
}