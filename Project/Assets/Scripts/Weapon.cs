using UnityEngine;
using System.Collections;


public class Weapon
{
    public WeaponDef m_definition;
    public int m_power;
    public Status m_status;
	public bool m_firedThisTurn;

	public static Weapon Create(bool forceHullDamage)
	{		
		WeaponDef weaponDef;
		do
		{
			weaponDef = WeaponDef.s_weaponDefs[Random.Range (0, WeaponDef.s_weaponDefs.Length)];
		} while (forceHullDamage && weaponDef.m_hullDamage == 0);

		Weapon wep = new Weapon();
		wep.m_definition = weaponDef;
		wep.m_status = Status.HEALTHY;
		wep.m_power = (int)(Random.Range(0.2f, 0.5f) * weaponDef.m_powerCapacity);
		wep.m_firedThisTurn = false;

		return wep;
	}
}
