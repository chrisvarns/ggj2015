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
        name = pName;
        powerPerLayer = pPowerPerLayer;
        powerCapacity = pPowerCapacity;
    }
    public string name;
    public int powerPerLayer;
    public int powerCapacity;
}
