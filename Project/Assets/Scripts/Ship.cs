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
	private List<WeaponDef> m_weaponsFiredThisTurn = new List<WeaponDef>();


    void Start()
    {
        Generate();  // test if the ship is working.
    }

    void Update()
    {

    }

	public int CalculatePowerToSpend()
	{
		return (m_generatorSystemStatus == Status.HEALTHY) ? m_generatorDefinition.m_powerOutputPerTurn
			: 0;
	}
	
	public int CalculateOxygenUsed()
	{
		int oxygenUsed = 0;
		foreach (Crew crewMem in m_crew)
		{
			if(crewMem != null)
			{
				oxygenUsed += (crewMem.m_race != Race.Android) ? 1 : 0;
			}
		}
		return oxygenUsed;
	}

	public void DamageNoOxygen()
	{
		foreach (Crew crewMem in m_crew)
		{
			if(crewMem != null)
			{
				crewMem.m_status -= (crewMem.m_status != Status.BROKEN) ? 1 : 0;
			}
		}
		PositionCrew ();
	}

	public int ComputeOxygenChange(int powerAdded)
	{
		int oxygenUsed = CalculateOxygenUsed ();
		int powerToOxygenRatio = (int)m_oxygenSystemStatus;
		int oxygenGenerated = powerAdded * powerToOxygenRatio;
		m_oxygenLevel = Mathf.Clamp (oxygenGenerated - oxygenUsed, 0, Constants.kMaxOxygen);
		if (m_oxygenLevel == 0)
		{
			DamageNoOxygen();
		}
		return m_oxygenLevel;
	}

	public bool isAnyCrewAlive()
	{
		foreach (Crew crewMem in m_crew) {
			if (crewMem != null && crewMem.m_status != Status.BROKEN)
			{
				return true;
			}
		}
		return false;
	}

	public bool isShipAlive()
	{
		return m_hullHealth > 0;
	}

	public class WeaponStatus
	{
		public string m_string;
		public int m_index;
		public bool m_isFireable;
		public int m_power;
		public bool m_isPowerAddable;
	}

	public WeaponStatus[] GetWeaponStatus()
	{
		List<WeaponStatus> wepStats = new List<WeaponStatus>();
		for(int i = 0; i < m_weapons.Length; i++)
		{
			if(m_weapons[i] == null) continue;
			WeaponStatus wepstat = new WeaponStatus();
			wepstat.m_index = i;
			wepstat.m_power = m_weapons[i].m_power;
			wepstat.m_isPowerAddable = (wepstat.m_power < m_weapons[i].m_definition.m_powerCapacity);
			string statusString = (m_weapons[i].m_status != Status.BROKEN) ?
					(m_weapons[i].m_power + "/" + m_weapons[i].m_definition.m_powerCapacity
					+ "[" + m_weapons[i].m_definition.m_powerPerShot + "]")
					: ("[BRK]") ;
			wepstat.m_string = m_weapons[i].m_definition.m_name + " " + statusString;
			wepstat.m_isFireable = (m_weapons[i].m_status != Status.BROKEN)
				&& !m_weapons[i].m_firedThisTurn
				&& (m_weapons[i].m_power > m_weapons[i].m_definition.m_powerPerShot);
			wepStats.Add(wepstat);
		}
		return wepStats.ToArray();
	}

	public bool FireWeapon(int index)
	{
		Weapon wepToFire = m_weapons [index];
		if (wepToFire == null || wepToFire.m_firedThisTurn)
		{
			return false;
		}
		wepToFire.m_power -= wepToFire.m_definition.m_powerPerShot;
		wepToFire.m_firedThisTurn = true;
		m_weaponsFiredThisTurn.Add(wepToFire.m_definition);
		return true;
	}

	public void ResetWeaponFiredStatus()
	{
		foreach (Weapon wep in m_weapons) {
			if(wep != null)
			{
				wep.m_firedThisTurn = false;
			}
		}
		m_weaponsFiredThisTurn.Clear();
	}

	public bool AddPowerToWeapon(int index)
	{
		Weapon wep = m_weapons [index];
		if (wep == null || wep.m_power == wep.m_definition.m_powerCapacity) {
			return false;
		}
		wep.m_power++;
		return true;
	}

	public class CrewPosition
	{
		public string m_name;
		public AssignedSystem m_system;
	}

	public List<CrewPosition> GetCrewPositions()
	{
		List<CrewPosition> crewPositions = new List<CrewPosition>();
		foreach (Crew crewMem in m_crew)
		{
			if(crewMem == null || crewMem.m_status == Status.BROKEN)
			{
				continue;
			}
			CrewPosition crewPos = new CrewPosition();
			crewPos.m_name = crewMem.m_name;
			crewPos.m_system = crewMem.m_assignedSystem;
			crewPositions.Add(crewPos);
		}
		return crewPositions;
	}

	public List<int> GetUnassignedSystems()
	{
		List<int> unassignedSystems = new List<int>();
		unassignedSystems.Add((int)AssignedSystem.ENGINES);
		unassignedSystems.Add((int)AssignedSystem.GENERATOR);
		unassignedSystems.Add((int)AssignedSystem.HULL);
		unassignedSystems.Add((int)AssignedSystem.HYPERDRIVE);
		unassignedSystems.Add((int)AssignedSystem.OXYGEN);
		unassignedSystems.Add((int)AssignedSystem.SHIELD);
		unassignedSystems.Add((int)AssignedSystem.WEAPONS);

		foreach(Crew crewMem in m_crew)
		{
			if(crewMem != null && crewMem.m_status != Status.BROKEN)
			{
				unassignedSystems.Remove((int)crewMem.m_assignedSystem);
			}
		}
		return unassignedSystems;
	}

	public void AssignCrew(int crewID, AssignedSystem system)
	{
		m_crew[crewID].m_assignedSystem = system;
	}

    public void Generate()      // genreate a rabdom ship.
    {
        // Hull
		HullDef hullDef = HullDef.s_hullDefs[Random.Range(0, HullDef.s_hullDefs.Length)];
        m_hullDefinition = hullDef;
        m_hullHealth = hullDef.m_maxHealth;
        // Oxygen
        m_oxygenLevel = Constants.kMaxOxygen;
        m_oxygenSystemStatus = Status.HEALTHY;
        // Shields
		ShieldDef shieldDef = ShieldDef.s_shieldDefs[Random.Range(0, ShieldDef.s_shieldDefs.Length)];
        m_shieldDefinition = shieldDef;
		m_shieldPower = (int)(Random.Range(0.2f, 0.5f) * shieldDef.m_powerCapacity);
		m_shieldSystemStatus = Status.HEALTHY;
        // Engines
		EngineDef engineDef = EngineDef.s_engineDefs[Random.Range(0, EngineDef.s_engineDefs.Length)];
        m_engineDefinition = engineDef;
        m_enginePower = (int)(Random.Range(0.2f, 0.5f) * engineDef.m_powerCapacity);
        m_engineSystemStatus = Status.HEALTHY;
        // Generator
		GeneratorDef generatorDef = GeneratorDef.s_generatorDefs[Random.Range(0, GeneratorDef.s_generatorDefs.Length)];
        m_generatorDefinition = generatorDef;
        m_generatorSystemStatus = Status.HEALTHY;
        // HyperDrive
        m_hyperdrivePower = 0;
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
            m_crew[i] = (i < numCrew) ? Crew.Create(existingNames, starting_pos[i]) : null;

            if (m_crew[i] != null)
            {
                m_crew[i].instance = Instantiate(crew_prefab[(int)m_crew[i].m_race]) as GameObject;
                m_crew[i].instance.name = "Crew Member " + i;
                m_crew[i].instance.transform.position = Vector3.zero;
                m_crew[i].instance.transform.rotation = Quaternion.identity;
            }
		}

		m_weaponsFiredThisTurn.Clear();

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

            string[] anim_names = new string[] { "Death", "Hurt", "Idle" };
            int namehash = Animator.StringToHash(anim_names[(int)crew.m_status]);
            if (crew.instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).nameHash != namehash)
            {
                crew.instance.GetComponent<Animator>().Play(namehash);      // if its a differnt anim play it
            }
        }
    }
}
