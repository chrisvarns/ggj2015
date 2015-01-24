using UnityEngine;
using System.Collections;


public class ShieldDef 
{
    public ShieldDef(string pName, int pPowerPerLayer, int pPowerCapacity)
    {
        m_name = pName;
        m_powerPerLayer = pPowerPerLayer;
        m_powerCapacity = pPowerCapacity;
    }
    public string m_name;
    public int m_powerPerLayer;
    public int m_powerCapacity;

	public static ShieldDef[] s_shieldDefs =
	{
		new ShieldDef("Magnetic Deflector", 3, 6),
		new ShieldDef("Ion Repulsor", 4, 12),
		new ShieldDef("Large", 5, 25)
	};
}
