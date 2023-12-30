using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyMode
{
    Normal,
    Chasing,
    Attacking,
    Dead
}

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

    private float chaseDistance = 5f;
    private float attackDistance = 2f;
    private EnemyMode currentMode;

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

        if (!agent.isOnNavMesh || isDead)
            return;

        // redordered these for optimization
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        // check this first as this is the case for most enemies
        if(distanceToTarget > chaseDistance)
        {
            setMode(EnemyMode.Normal);
            patrol();
            return;
        }

        else if (distanceToTarget <= attackDistance
            && (Time.timeSinceLevelLoad - lastAttackTime >= attackTime))
        {
            setMode(EnemyMode.Attacking);
            lastAttackTime = Time.timeSinceLevelLoad;
            playerdamage.decreasePlayerHealth(attackDamage);
            Util.PlayClipAtPoint(AttackSound, transform.position, AttackSoundVolume);
        }
        else
        {
            setMode(EnemyMode.Chasing);
            agent.SetDestination(target.position);
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

    private void setMode(EnemyMode mode)
    {
        if (mode == currentMode)
            return;

        switch (mode)
        {
            case EnemyMode.Normal:
                agent.speed = 1.7f;
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", false); 
                break;

            case EnemyMode.Chasing:
                agent.speed = 3.5f;
                animator.SetBool("isChasing", true);
                animator.SetBool("isAttacking", false);
                break;

            case EnemyMode.Attacking:
                agent.speed = 3.5f;
                animator.SetBool("isAttacking", true);
                break;

            case EnemyMode.Dead: break;
        }

        currentMode = mode;
    }

}

