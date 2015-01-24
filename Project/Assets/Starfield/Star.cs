using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SpriteRenderer))]
public class Star : MonoBehaviour 
{
    public Vector3 position;
    public float speed;

	void Start() 
    {
	
	}
	
	void Update() 
    {
        position.x -= speed * Time.deltaTime;
        transform.localPosition = new Vector3((int)position.x, (int)position.y, (int)position.z);

        if (position.x < -100f)
        {
            position.x = 100f;
        }
	}
}
