using UnityEngine;
using System.Collections;

public class ShieldDef {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public ShieldDef(string pName, int pPowerPerLayer, int pPowerCapacity)
    {
        m_name = pName;
        m_powerPerLayer = pPowerPerLayer;
        m_powerCapacity = pPowerCapacity;
    }
    public string m_name;
    public int m_powerPerLayer;
    public int m_powerCapacity;
}
