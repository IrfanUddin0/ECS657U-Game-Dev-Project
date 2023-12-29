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

    public AudioClip AttackSound;
    public float AttackSoundVolume = 1f;

    private float lastAttackTime;

    private NavMeshAgent agent;
    private Transform target;

    private Vector3 roamPoint;
    private PlayerSurvival playerdamage;

    private Animator animator;
    private bool isDead = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerdamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        animator = GetComponentInChildren<Animator>();
        roamPoint = transform.position;

        lastAttackTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isDead)
        {
            Util.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
            isDead = true;
            StartCoroutine(Die());

        }
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= 2f
            && (Time.timeSinceLevelLoad - lastAttackTime >= attackTime)
            && !isDead)
        {
            agent.speed = 3.5f;
            lastAttackTime = Time.timeSinceLevelLoad;
            animator.SetBool("isAttacking", true);

            playerdamage.decreasePlayerHealth(attackDamage);
            Util.PlayClipAtPoint(AttackSound, transform.position, AttackSoundVolume);
        }
        else if (distanceToTarget <= 5f
            && agent.isOnNavMesh)
        {
            agent.speed = 3.5f;
            agent.SetDestination(target.position);
            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            agent.speed = 1.7f;
            patrol();
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
        }
    }
    private IEnumerator Die()
    {

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
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

