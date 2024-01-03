using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class VillagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerref;
    public float stopDistance = 3f;
    public float dropChance = 0.33f;
    public PrefabCollection mapping;

    public AudioClip nearSoound;
    public AudioClip tradeSound;

    private NavMeshAgent agent;
    private Vector3 roamPoint;
    private Animator animator;

    private bool walkingMode;

    void Start()
    {
        playerref = FindAnyObjectByType<MainPlayerScript>().gameObject;
        agent = GetComponent<NavMeshAgent>();
        roamPoint = transform.position;
        animator = GetComponentInChildren<Animator>();
        walkingMode = false;
        setWalkMode(true);
    }


    void FixedUpdate()
    {
        if (!agent.isOnNavMesh)
            return;

        if(Vector3.SqrMagnitude(playerref.transform.position - transform.position) >= stopDistance * stopDistance)
        {
            setWalkMode(true);
            patrol();
        }
        else
        {
            setWalkMode(false);
            transform.rotation = Quaternion.LookRotation(-playerref.transform.forward);

            RaycastHit hit;
            Physics.SphereCast(transform.position, 3f, transform.forward, out hit);
            PickupItem[] items = GameObject.FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
            foreach (PickupItem item in items)
            {
                if(Vector3.SqrMagnitude(item.transform.position - transform.position) <= stopDistance * stopDistance)
                {
                    Destroy(item.gameObject);
                    spawnRandomItemChance();
                }
            }
        }
    }

    void spawnRandomItemChance()
    {
        if (Util.RngDifficultyScaled(dropChance))
        {
            FindAnyObjectByType<PlayerObjectives>().addDataEntry("VillagerTrade", "true");
            Util.PlayClipAtPoint(tradeSound, transform.position, 1f);

            var p = mapping.prefabMappings[Random.Range(0, mapping.prefabMappings.Count)];
            Item item = new Item();
            item.PickupPrefab = p.keyPrefab;
            item.EquipPrefab = p.valuePrefab;
            item.itemName = p.PrefabMappingName;
            item.description = p.ItemDescription;
            item.image = p.image;
            item.maxInStack = p.maxInStack;
            FindAnyObjectByType<Inventory>().AddItem(item);
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

    private void setWalkMode(bool mode)
    {
        if (mode == walkingMode) // if unchanged return
            return;

        if (mode) // if want to walk
        {
            animator.SetBool("close", false);
            agent.speed = 1.5f;
        }
        else
        {
            Util.PlayClipAtPoint(nearSoound, transform.position, 1f);
            animator.SetBool("close", true);
            agent.speed = 0f;
        }

        walkingMode = mode;
    }
}
