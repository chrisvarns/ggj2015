﻿using UnityEngine;
using System.Collections;


public class GeneratorDef 
{
    public GeneratorDef(string pname, int ppowerOutputPerTurn)
    {
        m_name = pname;
        m_powerOutputPerTurn = ppowerOutputPerTurn;
    }
    public string m_name;
    public int m_powerOutputPerTurn;

	public static GeneratorDef[] s_generatorDefs =
	{
		new GeneratorDef("Ole Smokey", 4),
		new GeneratorDef("Medium", 7),
		new GeneratorDef("Large", 10)
	};
}
