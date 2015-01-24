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
        name = pName;
        powerPerCharge = pPowerPerCharge;
        powerCapacity = pPowerCapacity;
        chanceToDodge = pChanceToDodge;
    }
    public string name;
    public int powerPerCharge;
    public int powerCapacity;
    public float chanceToDodge;
}
