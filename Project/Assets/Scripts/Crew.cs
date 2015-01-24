﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum AssignedSystem : int
{
    ENGINES = 0,
    GENERATOR,
    HULL,
    SHIELD,
    WEAPONS,
    OXYGEN,
    HYPERDRIVE,
    NUMSYSTEMS
}


public class Crew 
{
    public string m_name;
	public Status m_status;
    public CrewAbilityDef m_definition;
    public AssignedSystem m_assignedSystem;

	public static string[] s_potentialNames =
	{
		"Scotty",
		"Kirk",
		"Riker",
		"McCoy",
		"Uhura",
		"Sulu",
        "Worf",
        "Picard"
	};


	public static Crew Create(List<string> pExistingNames, int system_index)
	{
		bool foundUniqueName;
		string newName;
		do
		{
			foundUniqueName = true;
			newName = s_potentialNames[Random.Range(0, s_potentialNames.Length)];
			foreach(string existingName in pExistingNames)
			{
				if(existingName == newName)
				{
					foundUniqueName = false;
					break;
				}
			}
		} while(foundUniqueName == false);

		pExistingNames.Add(newName);
        Crew newCrew = new Crew();
		newCrew.m_name = newName;
		newCrew.m_definition = null;
		newCrew.m_status = Status.HEALTHY;
		newCrew.m_assignedSystem = (AssignedSystem)system_index;
		return newCrew;
	}
}
