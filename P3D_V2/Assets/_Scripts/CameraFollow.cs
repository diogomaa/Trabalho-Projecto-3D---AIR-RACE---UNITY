using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float distancia_X = 10.0f;
    public float distancia_Y = 5.0f;

    public float bias = 0.96f;
    public float lookat = 30.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 moveCamTo = transform.position - transform.forward * distancia_X + Vector3.up * distancia_Y;

        Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f - bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * lookat);

    }
}
