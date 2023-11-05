using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public int treeCount;

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

        for (int i = 0; i < treeCount; i++)
        {
            // Generate random positions on the terrain
            float randomX = Random.Range(0f, 1f);
            float randomZ = Random.Range(0f, 1f);

            // Calculate the world position based on the terrain's size
            Vector3 spawnPosition = new Vector3(randomX * terrainData.size.x, 0f, randomZ * terrainData.size.z);

            // Use SampleHeight to get the terrain height at that position
            float terrainHeight = terrain.SampleHeight(spawnPosition);

            // Adjust the Y position to place the tree on the terrain surface
            spawnPosition.y = terrainHeight;

            // Instantiate your tree prefab at the calculated position
            // Replace "TreePrefab" with the actual tree prefab you want to spawn
            Instantiate(TreePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
