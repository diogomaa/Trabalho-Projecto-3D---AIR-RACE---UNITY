using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour {

    public static float speed = 0.0f;
    public float start_speed = 30.0f;
    public float max_speed = 60.0f;
    public float min_speed = 10.0f;

    public int health = 100;
    public int armor = 100;
    public int max_health = 100;
    public int max_armor = 200;

    public bool armorActive = false;

    public float sphereRadius;
    public float maxDistance;

    RaycastHit hit;

    public GameObject bullet;
    public Transform gun1; //Right
    public Transform gun2; //Left
    public Transform plane;
    public float shootRate = 0f;
    public float shootForce = 0f;
    private float shootRateTimeStamp = 0f;

    private Vector3 targetPoint;
    private bool attack = false;
    private Transform tr;
    private List<Transform> targets;

    private float rotationSpeed = 20f;
    public bool isAlive = false;

    //Gizmos
    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;

    private Transform tg = null;

    // TRUE = RIGHT  FALSE = LEFT
    private bool switchGun = true;

    private Game game;
    public static Enemy Instancexd;
    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        Instancexd = this;
        StartCoroutine(SetTargetLoop());
        isAlive = true;
    }
	
	// Update is called once per frame
	void Update () {

        //tg = targetPoint;
        if (attack)
        {
            if (Vector3.Distance(tr.position, tg.position) < 250)
            {
                Shoot();
            }
        }


        // Look At Target
        var rot = tr.rotation;
        tr.LookAt(tg.position);
        tr.rotation = Quaternion.RotateTowards(rot, tr.rotation, rotationSpeed * Time.deltaTime);

        // Faz o avião andar para a frente
        tr.position += transform.forward * Time.deltaTime * speed;
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

        origin = tr.position;
        direction = transform.forward;

        // RayCast para detetar objetos
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            currentHitDistance = hit.distance;

            if (hit.collider.tag == "PickUp")
            {
                targetPoint = hit.transform.position;
            }

            if (hit.collider.tag == "Enemy")
            {
                targetPoint = hit.transform.position;
                Shoot();
            }

            if (hit.collider.tag == "Player")
            {
                targetPoint = hit.transform.position;
                Shoot();
            }
        } else
        {
            //Se nao detetou nada
            currentHitDistance = maxDistance;
            
        }

        
        if (!isAlive || health<=0)
        {
            Destroy(gameObject);
            
        }

        if (tg.gameObject != null)
        {
            // NAO FAZ NADA
        } else
        {
            Debug.Log("MORREU");
            tg = null;
        }

    }

    void Shoot()
    {
        if (Time.time > shootRateTimeStamp)
        {
                GameObject go = (GameObject)Instantiate(
                bullet, gun1.position, plane.rotation);
                go.GetComponent<Rigidbody>().AddForce(plane.forward * shootForce);
                GameObject go2 = (GameObject)Instantiate(
                bullet, gun2.position, plane.rotation);
                go2.GetComponent<Rigidbody>().AddForce(plane.forward * shootForce);

                shootRateTimeStamp = Time.time + shootRate;
        }
    }

    private IEnumerator SetTargetLoop()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(2);

            if (tg == null) {
                SetTarget();
            }
        }
    }

    private void SetTarget()
    {
        var targets = Game.Instance.playersList.Where(
           a => Vector3.Distance(tr.position, a.position) < 350 && Vector3.Distance(tr.position, a.position) != 0).ToList();

        if (targets.Count == 0) return;
        tg = targets[Random.Range(0, targets.Count - 1)];

        Debug.Log(transform.name + "  --> Enemy: " + tg.transform.name);

        attack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }

    public void ChangeHealth(int changeAmount)
    {
        if (health + changeAmount >= max_health)
        {
            health = max_health;
        }
        else
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
}
