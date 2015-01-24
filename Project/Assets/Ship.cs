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
            WeaponDef[] pWeaponDefs,
            CrewAbilityDef[] pCrewAbilityDefs)
    {
        Ship ret = new Ship();
        
        // Hull
        HullDef hullDef = pHullDefs[Random.Range(0, pHullDefs.Length)];
        ret.shp_hulladdr = hullDef;
        ret.shp_hullhealth = hullDef.maxHealth;
        // Oxygen
        ret.shp_oxamt = Constants.kMaxOxygen;
        ret.shp_oxstat = Constants.Status.FullyFunctional;
        // Shields
        ShieldDef shieldDef = pShieldDefs[Random.Range(0, pShieldDefs.Length)];
        ret.shp_shldaddr = shieldDef;
        ret.shp_shldpwr = (int)(Random.Range(0.2f, 0.5f) * shieldDef.powerCapacity);
        ret.shp_shldstat = Constants.Status.FullyFunctional;
        // Engines
        EngineDef engineDef = pEngineDefs[Random.Range(0, pEngineDefs.Length)];
        ret.shp_engaddr = engineDef;
        ret.shp_engpwr = (int)(Random.Range(0.2f, 0.5f) * engineDef.powerCapacity);
        ret.shp_engstat = Constants.Status.FullyFunctional;
        // Generator
        GeneratorDef generatorDef = pGeneratorDefs[Random.Range(0, pGeneratorDefs.Length)];
        ret.shp_genaddr = generatorDef;
        ret.shp_genstat = Constants.Status.FullyFunctional;
        // HyperDrive
        ret.shp_hdpwr = 0; // (int)((Random.NextDouble() * 0.3f + 0.2f) * Constants.kHDTargetPower);
        ret.shp_hdstat = Constants.Status.FullyFunctional;

        // Weapons 1-4
        int numWeapons = 2 + Random.Range(0, 3);
        for (int i = 0; i < 4; ++i)
        {
            if (i < numWeapons)
            {
                WeaponDef weaponDef = pWeaponDefs[Random.Range(0, pWeaponDefs.Length)];
                while (i == 0 && weaponDef.hullDamage == 0)
                {
                    weaponDef = pWeaponDefs[Random.Range(0, pWeaponDefs.Length)];
                }
                ret.shp_weapons[i].wep_saddr = weaponDef;
                ret.shp_weapons[i].wep_status = Constants.Status.FullyFunctional;
                ret.shp_weapons[i].wep_power = (int)(Random.Range(0.2f, 0.5f) * weaponDef.powerCapacity);
            }
            else
            {
                ret.shp_weapons[i] = null;
            }
        }

        // Crew 1-4
        int numCrew = 1 + Random.Range(0, Constants.kMaxCrew);

        return ret;
    }

    private Ship() { }
    public HullDef shp_hulladdr;
    public int shp_hullhealth;
    public GeneratorDef shp_genaddr;
    public Constants.Status shp_genstat;
    public int shp_oxamt;
    public Constants.Status shp_oxstat;
    public ShieldDef shp_shldaddr;
    public int shp_shldpwr;
    public Constants.Status shp_shldstat;
    public EngineDef shp_engaddr;
    public int shp_engpwr;
    public Constants.Status shp_engstat;
    public int shp_hdpwr;
    public Constants.Status shp_hdstat;
    public Weapon[] shp_weapons = new Weapon[4];
    public Crew[] shp_crew = new Crew[4];
}
