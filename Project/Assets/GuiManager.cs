using UnityEngine;
using System.Collections;


public enum GameState
{
    PlayerEntry,
    PlayerMain,
    PlayerExit,

    AIEntry,
    AIMain,
    AIExit,
    __SIZE__
}

public enum MainState
{
    Power,
    Crew,
    System,
    Weapons,
    __SIZE__
}


[RequireComponent(typeof(Camera))]
public class GuiManager : MonoBehaviour 
{
    public Camera cam;
    public TextMesh[] gui_lines;
    public Ship active_ship = null;
    public Ship other_ship = null;
    public Cam game_cam = null;

    public GameState state = GameState.PlayerMain;
    public MainState main_state = MainState.Power;
    public int line_index = 0;
    public int active_crew_index = 0;
    public int pwn_remaining;


    // we want to have some kind of state.

	void Start() 
    {
        cam = GetComponent<Camera>();
        Clear();
        EnableGUI();

        pwn_remaining = active_ship.CalculatePowerToSpend();
	}
	
	void Update() 
    {
        switch (state)
        {
            case GameState.PlayerEntry:
                PlayerEntry();
                break;

            case GameState.PlayerMain:
                PlayerMain();
                break;

            case GameState.PlayerExit:
                PlayerExit();
                break;

            case GameState.AIEntry:
                AIEntry();
                break;

            case GameState.AIMain:
                AIMain();
                break;

            case GameState.AIExit:
                AIExit();
                break;
        }
	}

    public void PlayerEntry()
    {

    }

    public void PlayerMain()
    {
        EnableGUI();
        Clear();

        switch (main_state)
        {
            #region power_state
            case MainState.Power:
                {
                    gui_lines[0].text = "Power  " + pwn_remaining;
                    ShipSystemsStat[] ship_stat = active_ship.GetSystemStates();

                    gui_lines[2].text = ship_stat[0].name;
                    gui_lines[2].color = ship_stat[0].asignable ? Color.white : Color.gray;

                    gui_lines[3].text = ship_stat[1].name;
                    gui_lines[3].color = ship_stat[1].asignable ? Color.white : Color.gray;

                    gui_lines[4].text = ship_stat[2].name;
                    gui_lines[4].color = ship_stat[2].asignable ? Color.white : Color.gray;

                    gui_lines[5].text = ship_stat[3].name;
                    gui_lines[5].color = ship_stat[3].asignable ? Color.white : Color.gray;

                    int gui_index = 6;
                    for (int i = 4; i < ship_stat.Length; i++)
                    {
                        gui_lines[gui_index].text = ship_stat[i].name;
                        gui_lines[gui_index].color = ship_stat[i].asignable ? Color.white : Color.gray;
                        gui_index++;
                    }

                    gui_lines[11].text = "Done";


                    // check if we are on a valid item and move down
                    if (line_index < ship_stat.Length)
                    {
                        if (!ship_stat[line_index].asignable)
                        {
                            DownHelper(ship_stat);
                        }
                    }


                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        line_index++;
                        if (line_index > 9)
                        {
                            line_index = 0;
                        }

                        if (line_index >= ship_stat.Length)
                        {
                            line_index = 9;
                        }

                        while (line_index < ship_stat.Length && !ship_stat[line_index].asignable)
                        {
                            line_index++;
                        }

                        if (line_index > ship_stat.Length - 1)
                        {
                            line_index = 9;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        line_index--;
                        if (line_index >= ship_stat.Length)
                        {
                            line_index = ship_stat.Length - 1;
                        }

                        while (line_index >= 0 && !ship_stat[line_index].asignable)
                        {
                            line_index--;
                        }

                        if (line_index < 0)
                        {
                            line_index = 9;     // catch the end
                        }
                    }

                    if (pwn_remaining == 0)
                    {
                        line_index = 9;
                    }

                    if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return))
                    {
                        if (line_index == 9)        // dont, we want to change state
                        {
                            main_state = MainState.Crew;
                            line_index = 0;
                        }
                        else
                        {
                            switch (line_index)
                            {
                                case 0:
                                    active_ship.m_enginePower++;
                                    pwn_remaining--;
                                    break;

                                case 1:
                                    active_ship.m_shieldPower++;
                                    pwn_remaining--;
                                    break;

                                case 2:
                                    active_ship.AddOxygen();
                                    pwn_remaining--;
                                    break;

                                case 3:
                                    active_ship.m_hyperdrivePower++;
                                    pwn_remaining--;
                                    break;

                                default:
                                    active_ship.AddPowerToWeapon(ship_stat[line_index].weapon_id);
                                    pwn_remaining--;
                                    break;
                            }
                        }
                    }

                    gui_lines[line_index + 2].color = Color.yellow;
                }
                break;
            #endregion

            #region crew_state
            case MainState.Crew:
                {
                    gui_lines[0].text = "Crew";
                    gui_lines[11].text = "Done";

                    int current_line = 2;
                    Ship.CrewPosition[] crew_list = active_ship.GetCrewPositions().ToArray();
                    foreach (Ship.CrewPosition pos in crew_list)
                    {
                        gui_lines[current_line].text = pos.m_name + "  :  " + pos.m_system;
                        current_line++;
                    }


                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        if (line_index == 9)
                        {
                            line_index = 0;
                            main_state = MainState.Weapons;
                            // move to firing weapons...
                        }
                        else
                        {
                            active_crew_index = crew_list[line_index].index;
                            line_index = 0;
                            main_state = MainState.System;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        line_index++;
                        if (line_index > 9)
                        {
                            line_index = 0;
                        }
                        if (line_index >= crew_list.Length)
                        {
                            line_index = 9;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        line_index--;
                        if (line_index > crew_list.Length - 1)
                        {
                            line_index = crew_list.Length - 1;
                        }

                        if (line_index < 0)
                        {
                            line_index = 9;
                        }
                    }

                    // TODO : need to account for the exit button

                    gui_lines[line_index + 2].color = Color.yellow;
                    // set tge active crew index...
                }
                break;
            #endregion

            #region system_state
            case MainState.System:
                {
                    gui_lines[0].text = "Systems  :  " + active_ship.m_crew[active_crew_index].m_name;
                    gui_lines[11].text = "Back";

                    int current_line = 2;
                    Ship.SystemState[] sys_stat = active_ship.GetUnassignedSystems().ToArray();
                    foreach (Ship.SystemState sys in sys_stat)
                    {
                        gui_lines[current_line].text = sys.m_name;
                        if (sys.m_status == Status.DAMAGED)
                        {
                            gui_lines[current_line].text += "[DMG]";
                        }
                        if (sys.m_status == Status.BROKEN)
                        {
                            gui_lines[current_line].text += "[BKN]";
                        }
                        current_line++;
                    }

                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        if (line_index == 9)
                        {
                            line_index = 0;
                            main_state = MainState.Crew;
                        }
                        else
                        {
                            // reasign the crew member
                            active_ship.AssignCrew(active_crew_index, (AssignedSystem)sys_stat[line_index].m_idx);
                            line_index = 0;
                            main_state = MainState.Crew;
                            active_ship.PositionCrew();
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        line_index++;
                        if (line_index > 9)
                        {
                            line_index = 0;
                        }
                        if (line_index >= sys_stat.Length)
                        {
                            line_index = 9;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        line_index--;
                        if (line_index >= sys_stat.Length)
                        {
                            line_index = sys_stat.Length - 1;
                        }
                        if (line_index < 0)
                        {
                            line_index = 9;
                        }
                    }

                    gui_lines[line_index + 2].color = Color.yellow;
                }
                break;
            #endregion

            #region weaoin_state
            case MainState.Weapons:
                {
                    gui_lines[0].text = "Weapons";
                    gui_lines[11].text = "Done";

                    int current_line = 2;
                    Ship.WeaponStatus[] weps = active_ship.GetWeaponStatus();
                    foreach (Ship.WeaponStatus wep in weps)
                    {
                        gui_lines[current_line].text = wep.m_string;
                        gui_lines[current_line].color = wep.m_isFireable ? Color.white : Color.gray;
                        current_line++;
                    }


                    // move down to valid ****************
                    if (line_index > 9)
                    {
                        line_index = 0;
                    }
                    while (line_index < weps.Length && !weps[line_index].m_isFireable)
                    {
                        line_index++;
                    }
                    if (line_index >= weps.Length)
                    {
                        line_index = 9;
                    }
                    // move down to valid ****************


                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        if (line_index == 9)
                        {
                            // we are done, change state
                            line_index = 0;
                            state = GameState.PlayerExit;
                        }
                        else
                        {
                            // fire the weapon
                            active_ship.FireWeapon(weps[line_index].m_index);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        line_index++;
                        if (line_index > 9)
                        {
                            line_index = 0;
                        }
                        while (line_index < weps.Length && !weps[line_index].m_isFireable)
                        {
                            line_index++;
                        }
                        if (line_index >= weps.Length)
                        {
                            line_index = 9;
                        }
                    }
                    if (Input.GetKeyUp(KeyCode.UpArrow))
                    {
                        line_index--;
                        if (line_index >= weps.Length)
                        {
                            line_index = weps.Length - 1;
                        }
                        while (line_index >= 0 && !weps[line_index].m_isFireable)
                        {
                            line_index--;
                        }
                        if (line_index < 0)
                        {
                            line_index = 9;
                        }
                    }

                    gui_lines[line_index + 2].color = Color.yellow;
                }
                break;
            #endregion
        }
    }

    void DownHelper(ShipSystemsStat[] ship_stat)
    {
        line_index++;
        if (line_index > 9)
        {
            line_index = 0;
        }

        if (line_index >= ship_stat.Length)
        {
            line_index = 9;
        }

        while (line_index < ship_stat.Length && !ship_stat[line_index].asignable)
        {
            line_index++;
        }

        if (line_index > ship_stat.Length - 1)
        {
            line_index = 9;
        }
    }

    public void PlayerExit()
    {
        Clear();
        DisableGUI();
        game_cam.FlyRight();
    }

    public void AIEntry()
    {

    }

    public void AIMain()
    {

    }

    public void AIExit()
    {

    }


    public void Clear()
    {
        foreach (TextMesh text in gui_lines)
        {
            text.text = "";
            text.color = Color.white;
        }
    }

    public void EnableGUI()
    {
        cam.enabled = true;
    }

    public void DisableGUI()
    {
        cam.enabled = false;
    }
}
