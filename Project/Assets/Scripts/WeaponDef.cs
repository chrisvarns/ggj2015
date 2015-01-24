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
        m_name = pName;
        m_shieldPenChancePct = pShieldPenChangePct;
        m_hullDamage = pHullDamage;
        m_shieldDamage = pShieldDamage;
        m_powerPerShot = pPowerPerShot;
        m_powerCapacity = pPowerCapacity;
    }
    public string m_name;
    public float m_shieldPenChancePct;
    public int m_hullDamage;
    public int m_shieldDamage;
    public int m_powerPerShot;
    public int m_powerCapacity;
}
