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

    //coroutine for death animation
    private IEnumerator Die()
    {

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        //configure animations 
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

            StartCoroutine(Sit()); //named sit but also used for sleeping
            roamPoint = findRandomPoint();  // finds new poitn to go to after coroutine is done
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
        int animation = Random.Range(1, 3);  //choose randomly to sit or sleep
        if (animation == 1)
        {
            animator.SetBool("Sit", true);
        }
        else
        {
            animator.SetBool("Sleep", true);
        }

        yield return new WaitForSeconds(Random.Range(5f, 30f)); // random time between 5 and 30 seconds
        animator.SetBool("Sit", false);
        animator.SetBool("Sleep", false);
        isSitting = false;
        agent.isStopped = false;
    }

    //find random point on the navMesh
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
