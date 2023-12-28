using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildlife : PlayerHittable
{

    public AudioClip deathSound;

    public float deathSoundVolume = 1f;
    public AudioClip AttackSound;
    public float AttackSoundVolume = 1f;

    private Transform target;

    private Vector3 roamPoint;
    private PlayerSurvival playerdamage;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerdamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        roamPoint = transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Util.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
            Destroy(gameObject);
        }
        else
        {
            patrol();
            animator.SetBool("Run Forward", true);
        }

    }


    void patrol()
    {
        if (Vector3.Distance(roamPoint, transform.position) < 3f)
        {

            roamPoint = findRandomPoint();

        }
        if (agent.isOnNavMesh)
            agent.SetDestination(roamPoint);

    }

    Vector3 findRandomPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 50;

        randomPoint += transform.position;
        UnityEngine.AI.NavMeshHit hit;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 50, 1))
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
