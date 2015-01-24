using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crew {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string m_name;
	public Constants.Status m_status;
    public CrewAbilityDef m_definition;
    public int m_assignedSystem;

	public static string[] s_potentialNames =
	{
		"Scotty",
		"Kirk",
		"Riker",
		"McCoy",
		"Uhura",
		"Sulu"
	};

	public static Crew Create(List<string> pExistingNames)
	{
		bool foundUniqueName;
		string newName;
		do
		{
			foundUniqueName = true;
			newName = s_potentialNames[Random.Range(0, s_potentialNames.length)];
			foreach(string existingName in pExistingNames)
			{
				if(existingName = newName)
				{
					foundUniqueName = false;
					break;
				}
			}
		} while(foundUniqueName == false);

		pExistingNames.Add(newName);
		Crew newCrew;
		newCrew.m_name = newName;
		newCrew.m_definition = null;
		newCrew.m_status = Constants.Status.HEALTHY;
		newCrew.m_assignedSystem = Constants.AssignedSystem.UNASSIGNED;
		return newCrew;
	}
}
