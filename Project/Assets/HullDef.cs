using UnityEngine;
using System.Collections;

public class HullDef {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public HullDef(string pName, int pMaxHealth)
    {
        name = pName;
        maxHealth = pMaxHealth;
    }
    public string name;
    public int maxHealth;
}
