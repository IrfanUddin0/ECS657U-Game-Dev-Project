using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyMode
{
    None,
    Normal,
    Chasing,
    Attacking,
    Dead
}

public class Enemy : PlayerHittable
{
    public float attackDamage;
    public float attackTime;

    public float walkSpeed = 1.5f;
    public float chashingSpeed = 3.5f;

    public AudioClip onChaseSound;
    public float OnChaseSoundVolume = 1f;

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

    public float chaseDistance = 6f;
    public float attackDistance = 2f;
    private EnemyMode currentMode = EnemyMode.None;
    private float playerLastHit;
    public float chaseDistance2 = 3f;
    public float viewAngle = 150f;

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
        playerLastHit = Time.timeSinceLevelLoad - 10f;
        setMode(EnemyMode.Normal);


        //change parameters based on difficulty level
        int difficulty = PlayerPrefs.HasKey("Difficulty") ? PlayerPrefs.GetInt("Difficulty") : 0;
        maxHealth = maxHealth + 100f * difficulty;
        health = maxHealth;
        attackDamage = attackDamage / 3f * +3f * (difficulty + 1f);
        chaseDistance = chaseDistance + difficulty;
        chaseDistance2 = chaseDistance2 + difficulty;
        viewAngle = viewAngle + (difficulty * 10);
    }

    void FixedUpdate()
    {
        if (!agent.isOnNavMesh || isDead)
            return;

        // redordered these for optimization
        // use square for optimization
        float distanceToTargetSqr = Vector3.SqrMagnitude(target.position - transform.position);
        //most common condition placed first for optimization
        if (distanceToTargetSqr > chaseDistance * chaseDistance
            && (Time.timeSinceLevelLoad - playerLastHit > 5f) || playerdamage.dead)
        {
            setMode(EnemyMode.Normal);
            patrol();
            return;
        }
        else if (distanceToTargetSqr <= attackDistance * attackDistance
         && (Time.timeSinceLevelLoad - lastAttackTime >= attackTime))
        {

            setMode(EnemyMode.Attacking);
            lastAttackTime = Time.timeSinceLevelLoad;
            playerdamage.decreasePlayerHealth(attackDamage);
            Util.PlayClipAtPoint(AttackSound, transform.position, AttackSoundVolume);
        }
        else if ((playerSeen(distanceToTargetSqr)) || (Time.timeSinceLevelLoad - playerLastHit <= 5f))
        {
            chase();
        }
    }
    private bool playerSeen(float distanceToTargetSqr)
    {
        Vector3 directionToPlayer = target.position - transform.position;
        //get angle between forward diretion and direction to player 
        float angle = Vector3.Angle(directionToPlayer, transform.forward);
        // check if this angle is less than half of field of view
        if ((angle < viewAngle / 2) && (distanceToTargetSqr <= chaseDistance * chaseDistance))
        {
            return true;
        }
        //needed to check if player's footsteps can be heard i.e from behind
        else if (distanceToTargetSqr < chaseDistance2 * chaseDistance2)
        {
            return true;
        }
        return false;
    }

    private void chase()
    {
        setMode(EnemyMode.Chasing);
        agent.SetDestination(target.position);
    }

    public override void OnPlayerHit(float dmg)
    {
        base.OnPlayerHit(dmg);
        //to detect when player is hit and space out attacks
        playerLastHit = Time.timeSinceLevelLoad;

        if (health <= 0 && !isDead)
        {
            Util.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
            isDead = true;
            StartCoroutine(Die());
            FindAnyObjectByType<PlayerObjectives>().addDataEntry("EnemyDefeated", "true");
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
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
    void patrol()
    {
        // use square magnitude instead of distance for performance
        if (Vector3.SqrMagnitude(roamPoint - transform.position) < 9f)
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
                agent.speed = walkSpeed;
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", false);
                break;

            case EnemyMode.Chasing:
                Util.PlayClipAtPoint(onChaseSound, transform.position, OnChaseSoundVolume);
                agent.speed = chashingSpeed;
                animator.SetBool("isChasing", true);
                animator.SetBool("isAttacking", false);
                break;

            case EnemyMode.Attacking:
                agent.speed = chashingSpeed;
                animator.SetBool("isAttacking", true);
                break;

            case EnemyMode.Dead: break;
        }

        currentMode = mode;
    }

}

