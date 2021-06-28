using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collision col)
    {
        Debug.Log("HEALTH KIT EHEHEH   sasdssaddssad ");

        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);

        }

        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            Controller c = col.gameObject.GetComponent<Controller>();

            Debug.Log("HEALTH KIT EHEHEH");
        }
    }

}
