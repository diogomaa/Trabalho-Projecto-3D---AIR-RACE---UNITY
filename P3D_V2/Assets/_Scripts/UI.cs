using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField] private Text compass;
    [SerializeField] private Text ammo;

    private GameObject gun1;
    private Gun gun1script;

    public Image healthbar;
    public Image armorbar;
    public Image turbobar;

    public Image life1;
    public Image life2;
    public Image life3;

    public static int alive;


    // Use this for initialization
    void Start () {

        ReloadGuns();

        life1.enabled = false;
        life2.enabled = false;
        life3.enabled = false;

        alive = Game.Instance.enemyNumber + 1;
    }
	
	// Update is called once per frame
	void Update () {

        ammo.text = "ALIVE: " + alive;

        compass.text = Controller.speed.ToString();

        // BARS
        healthbar.fillAmount = Map(Controller.health, 0, 100, 0, 1);
        armorbar.fillAmount = Map(Controller.armor, 0, 200, 0, 1);
        turbobar.fillAmount = Map(Controller.turbo, 0, 100, 0, 1);

        if (Controller.lifes == 3)
        {
            life3.enabled = true;
        } else if (Controller.lifes == 2)
        {
            life2.enabled = true;
            life3.enabled = false;
        } else
        {
            life1.enabled = true;
            life2.enabled = false;
        }

    }

    void ReloadGuns ()
    {
        gun1 = GameObject.Find("missile");
        gun1script = gun1.GetComponent<Gun>();
    }

    private float Map (float value, float inMin, float inMax, float outMin, float outMax)
    {
        // value = valor a calcular
        // inMin = minimo do valor a calcular (ex: minimo de vida possivel)
        // inMin = maximo do valor a calcular (ex: maximo de vida possivel)
        // outMin = 0 (minimo do fillAmount)
        // outMax = 1 (maximo do fillAmount)

        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

        //EXEMPLO
        // VIDA A 65
        // (65 - 0) * (1 - 0) / (100 - 0) + 0
        // 65 / 100 = 0,65
    }
}
