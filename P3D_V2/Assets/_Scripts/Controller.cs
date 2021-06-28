using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public static float speed = 0.0f;
    public float start_speed = 30.0f;
    public float max_speed = 60.0f;
    public float min_speed = 10.0f;

    public static int health = 0;
    public static int armor = 0;
    public int max_health = 100;
    public int max_armor = 200;
    public bool isAlive = false;

    private bool inZone = true;

    public static int lifes = 3;
    public static int turbo;
    public int turbomax = 100;


	// Use this for initialization
	void Start () {
        speed = start_speed;
        health = max_health;
        armor = 0;
        turbo = 0;
	}
	
	// Update is called once per frame
	void Update () {

        // Faz o avião andar para a frente
        transform.position += transform.forward * Time.deltaTime * speed;
        // Faz o aviao rodar
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

        // Aumenta/Diminui a velocidade se estiver a subir/descer
        speed -= transform.forward.y * Time.deltaTime * 50.0f;

        // Velocidade Mínima
        if (speed < min_speed)
        {
            speed = min_speed;
        }

        // Velocidade Maxima
        if (speed > max_speed)
        {
            speed = max_speed;
        }

        // Não deixa o aviao passar para baixo do chao
        /*
        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (terrainHeightWhereWeAre > transform.position.y)
        {
            transform.position = new Vector3(
                    transform.position.x,
                    terrainHeightWhereWeAre,
                    transform.position.z
                );
        }
        */
    }

    public void ChangeHealth(int changeAmount)
    {
        if (health + changeAmount >= max_health)
        {
            health = max_health;
        } else
        {
            health += changeAmount;
        }

        health += changeAmount;
    }

    public void ChangeArmor(int changeAmount)
    {
        if (armor + changeAmount >= max_armor)
        {
            armor = max_armor;
        }
        else
        {
            armor += changeAmount;
        }
    }

    public int getArmor()
    {
        return armor;
    }

    public int getHealth()
    {
        return health;
    }

    void OnTriggerEnter(Collision col)
    {
        Debug.Log("HEALTH KIT EHEHEH   sasdssaddssad ");

        if (col.gameObject.tag == "PickUp")
        {
            Destroy(col.gameObject);

        }
        
    }
}
