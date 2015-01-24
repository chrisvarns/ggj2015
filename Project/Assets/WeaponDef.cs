using UnityEngine;
using System.Collections;

public class WeaponDef {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public WeaponDef(string pName, float pShieldPenChangePct, int pHullDamage, int pShieldDamage,
            int pPowerPerShot, int pPowerCapacity)
    {
        name = pName;
        shieldPenChancePct = pShieldPenChangePct;
        hullDamage = pHullDamage;
        shieldDamage = pShieldDamage;
        powerPerShot = pPowerPerShot;
        powerCapacity = pPowerCapacity;
    }
    public string name;
    public float shieldPenChancePct;
    public int hullDamage;
    public int shieldDamage;
    public int powerPerShot;
    public int powerCapacity;
}
