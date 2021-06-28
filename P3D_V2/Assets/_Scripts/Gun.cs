using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bullet;
    public Transform gun1; //Right
    public Transform gun2; //Left
    public Transform plane;
    public float shootRate = 0f;
    public float shootForce = 0f;
    private float shootRateTimeStamp = 0f;

    //AMMO
    public bool infiniteAmmo = false;
    public int maxAmmo = 10;
    public int currentAmmo;

    // TRUE = RIGHT  FALSE = LEFT
    private bool switchGun = true;
    
	// Use this for initialization
	void Start () {
        currentAmmo = maxAmmo;
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (!infiniteAmmo)
        {
            if (currentAmmo <= 0)
            {
                Destroy(gameObject);
                WeaponSystem.change = true;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
            if (Time.time > shootRateTimeStamp)
            {
                if (switchGun)
                {
                    // RIGHT
                    GameObject go = (GameObject)Instantiate(
                    bullet, gun1.position, plane.rotation);
                    go.GetComponent<Rigidbody>().AddForce(plane.forward * shootForce);
                    switchGun = false;
                    if (!infiniteAmmo) { currentAmmo--; }
                    shootRateTimeStamp = Time.time + shootRate;
                }
                else
                {
                    // LEFT
                    GameObject go = (GameObject)Instantiate(
                    bullet, gun2.position, plane.rotation);
                    go.GetComponent<Rigidbody>().AddForce(plane.forward * shootForce);
                    switchGun = true;
                    if (!infiniteAmmo) { currentAmmo--; }
                    shootRateTimeStamp = Time.time + shootRate;
                }


            }
        }
    }
