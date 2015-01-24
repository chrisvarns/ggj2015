using UnityEngine;
using System.Collections;

public class GeneratorDef {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GeneratorDef(string pname, int ppowerOutputPerTurn)
    {
        m_name = pname;
        m_powerOutputPerTurn = ppowerOutputPerTurn;
    }
    public string m_name;
    public int m_powerOutputPerTurn;
}
