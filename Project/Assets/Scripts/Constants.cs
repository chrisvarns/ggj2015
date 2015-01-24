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
        Broken = 0,
        Damaged = 1,
        FullyFunctional = 2
    };
}
