using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerScript : MonoBehaviour
{
    public float spawnDiameter = 50f;
    public int maxInstances = 100;
    public float minY = 13f;

    public float replaceDistance = 100f;

    public GameObject npcPrefab;

    private Terrain terrain;
    private List<GameObject> npcs;
    private GameObject player;
    void Start()
    {
        terrain = GetComponent<Terrain>();
        npcs = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            // remove any null instances
            if (npcs[i] == null)
            {
                npcs.RemoveAt(i);
                continue;
            }

            // if enemy is far from player replace
            if(Vector3.SqrMagnitude(player.transform.position - npcs[i].transform.position) >= replaceDistance * replaceDistance)
            {
                npcs[i].transform.position = samplePosition();
                print("replaced enemy pos:" + npcs[i].transform.position);
            }
        }
        SpawnInRegion();
    }

    private void SpawnInRegion()
    {
        
        if(npcs.Count< maxInstances)
        {
            npcs.Add(Instantiate(npcPrefab, samplePosition(), Quaternion.Euler(0, Random.Range(0f, 360f), 0)));
        }
    }

    private Vector3 samplePosition()
    {
        int trycount = 5;
        for(int i = 0; i < trycount; i++)
        {
            Vector3 playerpos = player.transform.position;
            Vector2 spawnp = Random.insideUnitCircle * spawnDiameter;

            // Calculate the world position based on the terrain's size
            Vector3 spawnPosition = new Vector3(playerpos.x + spawnp.x, 0, playerpos.z + spawnp.y);

            // Use SampleHeight to get the terrain height at that position
            float terrainHeight = terrain.SampleHeight(spawnPosition);

            // if not in then retry
            if (terrainHeight <= minY)
                continue;

            spawnPosition.y = terrainHeight;

            return spawnPosition;
        }
        return Vector3.zero;
    }
}
