using UnityEngine;
using System.Collections;


public class WeaponDef 
{
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

	public static WeaponDef[] s_weaponDefs =
	{
		new WeaponDef("Lazer", 0.25f, 1, 1, 2, 10),
		new WeaponDef("Torpedo", 1.0f, 2, 0, 7, 7),
		new WeaponDef("Ion Cannon", 0.0f, 0, 3, 10, 10),
		new WeaponDef("Nuke", 1.0f, 50, 0, 60, 60)
	};
}
