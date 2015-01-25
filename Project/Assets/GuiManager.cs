using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour 
{
    public TextMesh[] gui_lines;
    public Ship active_ship = null;


    // we want to have some kind of state.

	void Start() 
    {
        Clear();
	}
	
	void Update() 
    {
	    // Input.GetKeyDown(Key) is frame specific key press.

        // jump to the correct state.
        // active line in the state.
	}


    public void Clear()
    {
        foreach (TextMesh text in gui_lines)
        {
            text.text = "";
        }
    }
}
