using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ship : MonoBehaviour
{
    public GameObject[] position_tags = new GameObject[(int)AssignedSystem.__SIZE__];       // hold gameobject for the position tags.
    public UnityEngine.Object[] crew_prefab = new UnityEngine.Object[(int)Race.__SIZE__];   // holds references to the crew prefabs


    private HullDef m_hullDefinition;
    private int m_hullHealth;
    private GeneratorDef m_generatorDefinition;
    private Status m_generatorSystemStatus;
    private int m_oxygenLevel;
    private Status m_oxygenSystemStatus;
    private ShieldDef m_shieldDefinition;
    private int m_shieldPower;
    private Status m_shieldSystemStatus;
    private EngineDef m_engineDefinition;
    private int m_enginePower;
    private Status m_engineSystemStatus;
    private int m_hyperdrivePower;
    private Status m_hyperdriveSystemStatus;
    private Weapon[] m_weapons = new Weapon[4];
    private Crew[] m_crew = new Crew[4];


    void Start()
    {
        Generate();  // test if the ship is working.
    }

    void Update()
    {

    }


    public void Generate()      // genreate a rabdom ship.
    {
        HullDef[] pHullDefs = HullDef.s_hullDefs;
        GeneratorDef[] pGeneratorDefs = GeneratorDef.s_generatorDefs;
        ShieldDef[] pShieldDefs = ShieldDef.s_shieldDefs;
        EngineDef[] pEngineDefs = EngineDef.s_engineDefs;
        WeaponDef[] pWeaponDefs = WeaponDef.s_weaponDefs;

        
        // Hull
        HullDef hullDef = pHullDefs[Random.Range(0, pHullDefs.Length)];
        m_hullDefinition = hullDef;
        m_hullHealth = hullDef.m_maxHealth;
        // Oxygen
        m_oxygenLevel = Constants.kMaxOxygen;
        m_oxygenSystemStatus = Status.HEALTHY;
        // Shields
        ShieldDef shieldDef = pShieldDefs[Random.Range(0, pShieldDefs.Length)];
        m_shieldDefinition = shieldDef;
		m_shieldPower = (int)(Random.Range(0.2f, 0.5f) * shieldDef.m_powerCapacity);
		m_shieldSystemStatus = Status.HEALTHY;
        // Engines
        EngineDef engineDef = pEngineDefs[Random.Range(0, pEngineDefs.Length)];
        m_engineDefinition = engineDef;
        m_enginePower = (int)(Random.Range(0.2f, 0.5f) * engineDef.m_powerCapacity);
        m_engineSystemStatus = Status.HEALTHY;
        // Generator
        GeneratorDef generatorDef = pGeneratorDefs[Random.Range(0, pGeneratorDefs.Length)];
        m_generatorDefinition = generatorDef;
        m_generatorSystemStatus = Status.HEALTHY;
        // HyperDrive
        m_hyperdrivePower = 0; // (int)((Random.NextDouble() * 0.3f + 0.2f) * Constants.kHDTargetPower);
        m_hyperdriveSystemStatus = Status.HEALTHY;

        // Weapons 1-4
        int numWeapons = 2 + Random.Range(0, 3);
        for (int i = 0; i < 4; ++i)
        {
			m_weapons[i] = (i < numWeapons) ? Weapon.Create(i==0) : null;
        }

        // Crew 1-4
        int[] starting_pos = new int[(int)AssignedSystem.__SIZE__];
        for (int i = 0; i < starting_pos.Length; i++)
        {
            starting_pos[i] = i;
        }
        Shuffle(starting_pos);

        int numCrew = 1 + Random.Range(0, Constants.kMaxCrew);
        List<string> existingNames = new List<string>();
		for (int i = 0; i < Constants.kMaxCrew; i++)
		{
            m_crew[i] = (i < numCrew) ? Crew.Create(existingNames, starting_pos[i]) : m_crew[i] = null;

            if (m_crew[i] != null)
            {
                m_crew[i].instance = Instantiate(crew_prefab[(int)m_crew[i].m_race]) as GameObject;
                m_crew[i].instance.name = "Crew Member " + i;
                m_crew[i].instance.transform.position = Vector3.zero;
                m_crew[i].instance.transform.rotation = Quaternion.identity;
            }
		}

        PositionCrew();
    }

    static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(UnityEngine.Random.Range(0f, 1f) * (n - i));
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

    public void PositionCrew()
    {
        foreach (Crew crew in m_crew)
        {
            if (crew == null)
            {
                continue;
            }

            crew.instance.transform.parent = position_tags[(int)crew.m_assignedSystem].transform;
            crew.instance.transform.localPosition = Vector3.zero;
            crew.instance.transform.localRotation = Quaternion.identity;
        }
    }
}
