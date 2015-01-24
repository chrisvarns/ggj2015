using UnityEngine;
using System.Collections;

public class Ship {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static Ship Create(HullDef[] pHullDefs,
            GeneratorDef[] pGeneratorDefs,
            ShieldDef[] pShieldDefs,
            EngineDef[] pEngineDefs,
            WeaponDef[] pWeaponDefs)
    {
        Ship ship = new Ship();
        
        // Hull
        HullDef hullDef = pHullDefs[Random.Range(0, pHullDefs.Length)];
        ship.m_hullDefinition = hullDef;
        ship.m_hullHealth = hullDef.m_maxHealth;
        // Oxygen
        ship.m_oxygenLevel = Constants.kMaxOxygen;
        ship.m_oxygenSystemStatus = Constants.Status.HEALTHY;
        // Shields
        ShieldDef shieldDef = pShieldDefs[Random.Range(0, pShieldDefs.Length)];
        ship.m_shieldDefinition = shieldDef;
		ship.m_shieldPower = (int)(Random.Range(0.2f, 0.5f) * shieldDef.m_powerCapacity);
		ship.m_shieldSystemStatus = Constants.Status.HEALTHY;
        // Engines
        EngineDef engineDef = pEngineDefs[Random.Range(0, pEngineDefs.Length)];
        ship.m_engineDefinition = engineDef;
        ship.m_enginePower = (int)(Random.Range(0.2f, 0.5f) * engineDef.m_powerCapacity);
        ship.m_engineSystemStatus = Constants.Status.HEALTHY;
        // Generator
        GeneratorDef generatorDef = pGeneratorDefs[Random.Range(0, pGeneratorDefs.Length)];
        ship.m_generatorDefinition = generatorDef;
        ship.m_generatorSystemStatus = Constants.Status.HEALTHY;
        // HyperDrive
        ship.m_hyperdrivePower = 0; // (int)((Random.NextDouble() * 0.3f + 0.2f) * Constants.kHDTargetPower);
        ship.m_hyperdriveSystemStatus = Constants.Status.HEALTHY;

        // Weapons 1-4
        int numWeapons = 2 + Random.Range(0, 3);
        for (int i = 0; i < 4; ++i)
        {
            if (i < numWeapons)
            {
                WeaponDef weaponDef = pWeaponDefs[Random.Range(0, pWeaponDefs.Length)];
                while (i == 0 && weaponDef.m_hullDamage == 0)
                {
                    weaponDef = pWeaponDefs[Random.Range(0, pWeaponDefs.Length)];
                }
                ship.m_weapons[i].m_definition = weaponDef;
                ship.m_weapons[i].m_status = Constants.Status.HEALTHY;
                ship.m_weapons[i].m_power = (int)(Random.Range(0.2f, 0.5f) * weaponDef.m_powerCapacity);
            }
            else
            {
                ship.m_weapons[i] = null;
            }
        }

        // Crew 1-4
        int numCrew = 1 + Random.Range(0, Constants.kMaxCrew);
		List<string> existingNames;
		for (int i=0; i < Constants.kMaxCrew; i++)
		{
			ship.m_crew[i] = (i < numCrew) ? Crew.Create(existingNames) : ship.m_crew[i] = null;
		}

        return ship;
    }

    private Ship() { }
	public static Ship Create()
	{
		return Create (HullDef.s_hullDefs, GeneratorDef.s_generatorDefs,
		              ShieldDef.s_shieldDefs, EngineDef.s_engineDefs, WeaponDef.s_weaponDefs);
	}

    public HullDef m_hullDefinition;
    public int m_hullHealth;
    public GeneratorDef m_generatorDefinition;
    public Constants.Status m_generatorSystemStatus;
    public int m_oxygenLevel;
    public Constants.Status m_oxygenSystemStatus;
    public ShieldDef m_shieldDefinition;
    public int m_shieldPower;
    public Constants.Status m_shieldSystemStatus;
    public EngineDef m_engineDefinition;
    public int m_enginePower;
    public Constants.Status m_engineSystemStatus;
    public int m_hyperdrivePower;
    public Constants.Status m_hyperdriveSystemStatus;
    public Weapon[] m_weapons = new Weapon[4];
    public Crew[] m_crew = new Crew[4];
}
