using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : PlayerHittable
{
    public float attackDamage;
    public float attackTime;

    public AudioClip deathSound;
    public float deathSoundVolume = 1f;

    private float lastAttackTime;

    private NavMeshAgent agent;
    private Transform target;

    private Vector3 roamPoint;
    private PlayerSurvival playerdamage;

    private Animator animator;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerdamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        animator = GetComponent<Animator>();
        roamPoint = transform.position;

        lastAttackTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Util.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
            Destroy(gameObject);
        }
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= 2f
            && Time.timeSinceLevelLoad - lastAttackTime >= attackTime)
        {
            lastAttackTime = Time.timeSinceLevelLoad;
            animator.SetBool("isAttacking", true);

            playerdamage.decreasePlayerHealth(attackDamage);
        }
        else if (distanceToTarget <= 5f
            && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking", false);
            print(animator.GetBool("isChasing"));
        }
        else
        {
            patrol();
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
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

