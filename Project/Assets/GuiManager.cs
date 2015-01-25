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
        Clear();

        switch (main_state)
        {
            case MainState.Power:
                {
                    gui_lines[0].text = "PWR  " + pwn_remaining;
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
                            // handle the oxygen change power_for_ox;
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

            case MainState.Crew:
                {

                }
                break;

            case MainState.Weapons:
                {

                }
                break;
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
