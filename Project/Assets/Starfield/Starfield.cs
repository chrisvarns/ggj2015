using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public class Starfield : MonoBehaviour 
{
    public Sprite[] start_sprites;
    public int num_stars = 40;
    public UnityEngine.Object star_prefab;

    private Camera cam;

	void Start() 
    {
        cam = GetComponent<Camera>();

        GameObject Starfield = new GameObject();
        Starfield.name = "Starfield";
        Starfield.transform.position = transform.position;
        Starfield.transform.rotation = transform.rotation;
        Starfield.transform.localScale = Vector3.one;
        Starfield.transform.parent = transform;

        for (int i = 0; i < num_stars; i++)
        {
            GameObject star_inst = Instantiate(star_prefab) as GameObject;
            star_inst.name = "Star Instance " + i;
            star_inst.transform.parent = Starfield.transform;

            int star = UnityEngine.Random.Range(0, 10);
            switch (star)
            {
                case 0:
                    star_inst.GetComponent<SpriteRenderer>().sprite = start_sprites[0];
                    break;

                case 1:
                case 2:
                    star_inst.GetComponent<SpriteRenderer>().sprite = start_sprites[1];
                    break;

                case 3:
                case 4:
                case 5:
                case 6:
                    star_inst.GetComponent<SpriteRenderer>().sprite = start_sprites[2];
                    break;

                case 7:
                case 8:
                case 9:
                    star_inst.GetComponent<SpriteRenderer>().sprite = start_sprites[3];
                    break;
            }


            float size = cam.orthographicSize + 5f;
            float x = UnityEngine.Random.Range(-size, size);
            float y = UnityEngine.Random.Range(-size * 2f, size * 2f);

            star_inst.transform.localPosition = new Vector3(x, y, 0f);
            star_inst.GetComponent<Star>().position = new Vector3(x, y, 100f);
            star_inst.GetComponent<Star>().speed = UnityEngine.Random.Range(10f, 50f);
        }
	}

    void Update() 
    {
	
	}
}
