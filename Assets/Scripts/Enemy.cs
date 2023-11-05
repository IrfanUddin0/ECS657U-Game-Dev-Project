using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : PlayerHittable
{
    private bool isChasing = false;
    private bool reached = false;
    private NavMeshAgent agent;
    public Transform target;
    public float damageAmount;

    private Vector3 roamPoint;
    private PlayerSurvival playerdamage;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerdamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        roamPoint = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= 2f)
        {
            playerdamage.health -= damageAmount;
        }
        else if (distanceToTarget <= 5f)
        {
            isChasing = true;
            agent.SetDestination(target.position);
        }
        else
        {
            patrol();
        }

    }
    void patrol()
    {
        if (Vector3.Distance(roamPoint, transform.position) < 3f)
        {

            roamPoint = findRandomPoint();

        }

        agent.SetDestination(roamPoint);

    }

    Vector3 findRandomPoint()
    {

        Vector3 randomPoint = Random.insideUnitSphere * 50;

        randomPoint += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 50, 1))
        {
            roamPoint = hit.position;
            return roamPoint;
        }
        else
        {
            return transform.position;
        }


    }

}

