using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerScript : MonoBehaviour
{
    public float spawnDiameter = 200f;
    public int maxInstances = 100;
    public float minY = 13f;

    public GameObject npcPrefab;

    private Terrain terrain;
    private List<GameObject> npcs;
    void Start()
    {
        terrain = GetComponent<Terrain>();
        npcs = new List<GameObject>();
    }

    private void Update()
    {
        // remove any null instances
        for (int i = 0; i < npcs.Count; i++)
        {
            if (npcs[i] == null)
                npcs.RemoveAt(i);
        }
        SpawnInRegion();
    }

    private void SpawnInRegion()
    {
        Vector3 playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if(npcs.Count< maxInstances)
        {
            Vector2 spawnp = Random.insideUnitCircle * spawnDiameter;

            // Calculate the world position based on the terrain's size
            Vector3 spawnPosition = new Vector3(playerpos.x + spawnp.x, 0, playerpos.z + spawnp.y);

            // Use SampleHeight to get the terrain height at that position
            float terrainHeight = terrain.SampleHeight(spawnPosition);

            if (terrainHeight <= minY)
                return;
            spawnPosition.y = terrainHeight;

            npcs.Add(Instantiate(npcPrefab, spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0)));
        }
    }
}
