using UnityEngine;
using System.Collections;


// helper to move the camera around.

[RequireComponent(typeof(Animator))]
public class Cam : MonoBehaviour 
{
    private Animator anim;


	void Start() 
    {
        anim = GetComponent<Animator>();
	}
	
	void Update() 
    {
        FlyRight();
	}

    public void FlyRight()
    {
        int hash = Animator.StringToHash("CameraFlyRight");
        anim.Play(hash);
    }

    public void FlyLeft()
    {
        int hash = Animator.StringToHash("CameraFlyLeft");
        anim.Play(hash);
    }

    public void Callback()
    {
        Debug.Log("Should have called back.");
        // callback whoever...
    }
}
