using UnityEngine;
using System.Collections;

public class Constants {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public const int kMaxOxygen = 20;
    public const int kHDTargetPower = 30;
    public const float kHDDamagedExplosionChance = 0.4f;
    public const int kMaxCrew = 4;

    public enum Status : int
    {
        BROKEN = 0,
        DAMAGED = 1,
        HEALTHY = 2
    };

	public enum AssignedSystem : int
	{
		UNASSIGNED = -1,
		ENGINES,
		GENERATOR,
		HULL,
		SHIELD,
		WEAPONS,
		OXYGEN,
		HYPERDRIVE,
		NUMSYSTEMS
	}
}
