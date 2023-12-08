using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public int treeCount;
    public float waterLevel;

    public GameObject TreePrefab;

    private Terrain terrain;
    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        SpawnTreesOnTerrain();
    }

    private void SpawnTreesOnTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        int currentTreeCount = 0;
        while(currentTreeCount < treeCount)
        {
            // Generate random positions on the terrain
            float randomX = Random.Range(0f, 1f);
            float randomZ = Random.Range(0f, 1f);

            // Calculate the world position based on the terrain's size
            Vector3 spawnPosition = new Vector3(randomX * terrainData.size.x, 0f, randomZ * terrainData.size.z);

            // Use SampleHeight to get the terrain height at that position
            float terrainHeight = terrain.SampleHeight(spawnPosition);

            if (terrainHeight <= waterLevel)
                continue;

            // Adjust the Y position to place the tree on the terrain surface
            spawnPosition.y = terrainHeight;

            // Instantiate your tree prefab at the calculated position
            // Replace "TreePrefab" with the actual tree prefab you want to spawn
            Instantiate(TreePrefab, spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            currentTreeCount++;
        }
    }
}
