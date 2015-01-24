using UnityEngine;
using System.Collections;


public class EngineDef 
{
    public EngineDef(string pName, int pPowerPerCharge, int pPowerCapacity, float pChanceToDodge)
    {
        m_name = pName;
        m_powerPerCharge = pPowerPerCharge;
        m_powerCapacity = pPowerCapacity;
        m_chanceToDodge = pChanceToDodge;
    }
    public string m_name;
    public int m_powerPerCharge;
    public int m_powerCapacity;
    public float m_chanceToDodge;

	public static EngineDef[] s_engineDefs = 
	{
		new EngineDef("v8", 3, 12, 0.4f),
		new EngineDef("2 stroke", 2, 12, 0.2f),
		new EngineDef("Ion Thruster", 4, 8, 0.7f)
	};
}
