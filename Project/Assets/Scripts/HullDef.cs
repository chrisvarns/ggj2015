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
        m_name = pName;
        m_maxHealth = pMaxHealth;
    }
    public string m_name;
    public int m_maxHealth;
}
