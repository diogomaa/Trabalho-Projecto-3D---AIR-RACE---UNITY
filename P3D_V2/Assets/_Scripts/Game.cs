using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField]
    public List<SpawnPoint> _allSpawnPoints;

    public GameObject player;
    public GameObject enemys;
    public GameObject pk1;

    public int enemyNumber;

    public List<Transform> playersList;
    public GameObject[] trackedObjects;

    public static Game Instance;



    void Awake()
    {
        Instance = this;
        SpawnPlayer();
        SpawnEnemys();
        SpawnPickUps();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void SpawnPlayer()
    {
       GameObject p = (GameObject)Instantiate(
                   player, _allSpawnPoints[0].transform.position, _allSpawnPoints[0].transform.rotation);
        playersList.Add(p.transform);
    }

    void SpawnEnemys()
    {
        trackedObjects = new GameObject[enemyNumber];

        for (int i=1; i <= enemyNumber; i++)
        {
            GameObject p = (GameObject)Instantiate(
                   enemys, _allSpawnPoints[i].transform.position, _allSpawnPoints[i].transform.rotation);
            playersList.Add(p.transform);
            trackedObjects[i-1] = p;
            //Debug.Log("DEBUGGG " + trackedObjects[i - 1].transform.name);
        }
    }

    private IEnumerator StartSpawnPK()
    {
        SpawnPickUps();
        yield return new WaitForSeconds(2);
             
    }

    void SpawnPickUps()
    {
        for (int i = 0; i < enemyNumber+1; i++)
        {
            GameObject p = (GameObject)Instantiate(
                   pk1, _allSpawnPoints[i].transform.position, _allSpawnPoints[i].transform.rotation);
        }
    }
}
