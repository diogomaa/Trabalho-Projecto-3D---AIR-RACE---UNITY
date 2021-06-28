using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamage : MonoBehaviour {

    public GameObject player;
    private string playername;

    public GameObject _BRS_Mechanics;
    public Controller playerHealth;

    public float TickRate = 3.0f;
    public int TickDamage = 1;

    private bool inZone = true;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        playername = player.transform.name;

        playerHealth = _BRS_Mechanics.GetComponent<Controller>();

        //Setup the DamagePlayer to run every X seconds
        InvokeRepeating("DamagePlayer", 0.0f, TickRate);
    }

    void OnTriggerExit(Collider col)
    {

        Debug.Log("Saiu 0001");
        if (col.transform.name == playername)
        {
            Debug.Log("Saiu 0002");
            inZone = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Entrou 0001");
        if (col.transform.name == playername)
        {
            Debug.Log("Entrou 0002");
            inZone = true;
        }
    }

    void DamagePlayer()
    {
        if (!inZone)
        {
            //Damage the player [TickDamage] amount
            playerHealth.ChangeHealth(-TickDamage);
            Debug.Log("OUTSIDE");
        }
        else
        {
            playerHealth.ChangeHealth(100);
            Debug.Log("IN");
        }
    }
}
