using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float expiryTime = 0f;
    public int damage = 0;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, expiryTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            int armor = enemy.getArmor();
            int health = enemy.getHealth();

            if (enemy.armorActive)
            {
                // ver se ao total tem mais do que damage
                if (armor + health <= damage)
                {
                    enemy.isAlive = false;
                } else
                {
                    // ver se tem armadura suficiente
                    if (armor >= damage)
                    {
                        enemy.ChangeArmor(-damage);
                    } else
                    {
                        //Se a armor nao for 0
                        if (armor != 0)
                        {
                            int r = damage - armor;
                            enemy.ChangeArmor(-armor);
                            enemy.ChangeHealth(-r);
                        } else
                        {
                            enemy.ChangeHealth(-damage);
                        }
                    }
                } 
            // Se o enimgio tiver armadura desativada
            } else {
                // Ver damage
                if (health - damage <= 0)
                {
                    enemy.isAlive = false;
                } else
                {
                    enemy.ChangeHealth(-damage);
                }
            }
        }

        //============================================================
        //  PLAYER
        //==========================================================

        if (col.gameObject.tag == "Player")
        {
            Controller player = col.gameObject.GetComponent<Controller>();
            int armor = player.getArmor();
            int health = player.getHealth();

            // ver se ao total tem mais do que damage
            if (armor + health <= damage)
            {
                player.isAlive = false;
            }
            else
            {
                // ver se tem armadura suficiente
                if (armor >= damage)
                {
                    player.ChangeArmor(-damage);
                }
                else
                {
                    //Se a armor nao for 0
                    if (armor != 0)
                    {
                        int r = damage - armor;
                        player.ChangeArmor(-armor);
                        player.ChangeHealth(-r);
                    }
                    else
                    {
                        player.ChangeHealth(-damage);
                    }
                }
            }
        }
    }
}
