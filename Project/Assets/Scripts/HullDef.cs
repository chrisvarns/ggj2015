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

	public static HullDef[] s_hullDefs = 
	{
		new HullDef ("Cast Iron", 15),
		new HullDef ("Adamantium", 25),
		new HullDef ("Mythril", 40),
	};
}
