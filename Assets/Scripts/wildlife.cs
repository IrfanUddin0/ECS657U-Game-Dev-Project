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
    private bool isDead = false;
    private bool isSitting = false;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerdamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        roamPoint = transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Util.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
            StartCoroutine(Die());
        }
        else if (!isDead)
        {
            patrol();
        }

    }
    private IEnumerator Die()
    {

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        animator.SetBool("Run Forward", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sleep", false);
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    void patrol()
    {
        // use square magnitude instead of distance for performance
        if ((Vector3.SqrMagnitude(roamPoint - transform.position) < 9f) && !isSitting)
        {

            StartCoroutine(Sit());
            roamPoint = findRandomPoint();


        }
        if (agent.isOnNavMesh && !isSitting)
            animator.SetBool("Run Forward", true);
        agent.SetDestination(roamPoint);

    }
    private IEnumerator Sit()
    {
        agent.isStopped = true;
        isSitting = true;
        animator.SetBool("Run Forward", false);
        int animation = Random.Range(1, 3);
        if (animation == 1)
        {
            animator.SetBool("Sit", true);
        }
        else
        {
            animator.SetBool("Sleep", true);
        }


        yield return new WaitForSeconds(Random.Range(5f, 30f));
        animator.SetBool("Sit", false);
        animator.SetBool("Sleep", false);
        isSitting = false;
        agent.isStopped = false;



    }
    Vector3 findRandomPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 50;

        randomPoint += transform.position;
        UnityEngine.AI.NavMeshHit hit;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 50, 1))
        {
            roamPoint = hit.position;
            Debug.Log("Yes");
            return roamPoint;
        }
        else
        {
            Debug.Log("NO");
            return transform.position;
        }
    }

}
