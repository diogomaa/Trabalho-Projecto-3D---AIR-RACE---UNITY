using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "PickUp")
            {
                Controller c = other.gameObject.GetComponent<Controller>();
                c.ChangeArmor(50);
                Destroy(gameObject);
            }
            
            if (gameObject.tag == "Bullet")
            {
                DealDamage(other);
                Destroy(gameObject);
            }

        }

        if (other.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "Bullet")
            {
                //Debug.Log("GANDA TIRO !!");
                DealDamageE(other);
                Destroy(gameObject);
            }
        }
    }

    void DealDamage(Collider other)
    {
        //============================================================
        //  PLAYER
        //==========================================================
        int damage = 10;

        Controller player = other.gameObject.GetComponent<Controller>();
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

    void DealDamageE(Collider other)
    {
        int damage = 5;
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        int armor = enemy.getArmor();
        int health = enemy.getHealth();

        if (enemy.armorActive)
        {
            // ver se ao total tem mais do que damage
            if (armor + health <= damage)
            {
                enemy.isAlive = false;
            }
            else
            {
                // ver se tem armadura suficiente
                if (armor >= damage)
                {
                    enemy.ChangeArmor(-damage);
                }
                else
                {
                    //Se a armor nao for 0
                    if (armor != 0)
                    {
                        int r = damage - armor;
                        enemy.ChangeArmor(-armor);
                        enemy.ChangeHealth(-r);
                    }
                    else
                    {
                        enemy.ChangeHealth(-damage);
                    }
                }
            }
            // Se o enimgio tiver armadura desativada
        }
        else
        {
            // Ver damage
            if (health - damage <= 0)
            {
                enemy.isAlive = false;
            }
            else
            {
                enemy.ChangeHealth(-damage);
            }
        }
    }

}
