using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    List<GameObject> radarObjects;
    public GameObject radarPrefab;
    List<GameObject> borderObjects;
    public float switchDistance;
    public Transform helpTransform;

	// Use this for initialization
	void Start () {
        createRadarObjects();
	}
	
	// Update is called once per frame
	void Update () {
		
        for (int i=0; i < radarObjects.Count; i++)
        {
            if(Vector3.Distance(radarObjects[i].transform.position, transform.position) > switchDistance)
            {
                // Coloca nas bordas
                helpTransform.LookAt(radarObjects[i].transform);
                borderObjects[i].transform.position = transform.position + switchDistance * helpTransform.forward;
                borderObjects[i].layer = LayerMask.NameToLayer("Radar");
                radarObjects[i].layer = LayerMask.NameToLayer("Invisible");
            } else
            {
                // Volta para os objetos do radar
                borderObjects[i].layer = LayerMask.NameToLayer("Invisible");
                radarObjects[i].layer = LayerMask.NameToLayer("Radar");
            }
        }
	}

    void createRadarObjects()
    {
        radarObjects = new List<GameObject>();
        borderObjects = new List<GameObject>();
        foreach (GameObject o in Game.Instance.trackedObjects)
        {
            GameObject k = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            k.transform.parent = o.transform;
            radarObjects.Add(k);
            GameObject j = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            j.transform.parent = o.transform;
            radarObjects.Add(j);

            //Debug.Log("DEBUG 2 " + o.transform.name);
        }
    }

}
