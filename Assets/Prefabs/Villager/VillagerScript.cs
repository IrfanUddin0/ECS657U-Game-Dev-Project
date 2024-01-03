using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerref;
    public float stopDistance = 3f;

    public AudioClip nearSoound;
    public AudioClip tradeSound;

    private NavMeshAgent agent;
    private Vector3 roamPoint;
    private Animator animator;

    void Start()
    {
        playerref = FindAnyObjectByType<MainPlayerScript>().gameObject;
        agent = GetComponent<NavMeshAgent>();
        roamPoint = transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.isOnNavMesh)
            return;

        if(Vector3.SqrMagnitude(playerref.transform.position - transform.position) >= stopDistance * stopDistance)
        {
            animator.SetBool("close", false);
            agent.speed = 1.5f;
            patrol();
        }
        else
        {
            animator.SetBool("close", true);
            transform.rotation = Quaternion.LookRotation(-playerref.transform.forward);
            agent.speed = 0f;
        }
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
        Vector3 randomPoint = Random.insideUnitSphere * 10f;

        randomPoint += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, 1))
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
