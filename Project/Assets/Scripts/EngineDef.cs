using UnityEngine;
using System.Collections;

public class EngineDef {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
}
