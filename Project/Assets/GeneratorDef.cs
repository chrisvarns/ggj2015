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
        name = pname;
        powerOutputPerTurn = ppowerOutputPerTurn;
    }
    public string name;
    public int powerOutputPerTurn;
}
