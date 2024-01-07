using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public int treeCount;
    public float waterLevel;

    public GameObject TreePrefab;

    public bool regen;
    public float regenTime;

    private Terrain terrain;
    private float lastSpawn;
    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        lastSpawn = Time.timeSinceLevelLoad;

        // if using saver loader and save exists, then dont spawn anything in
        if(!SaverLoader.SaveExists()
            && FindAnyObjectByType<SaverLoader>()!= null)
            SpawnTreesOnTerrain();
    }

    private void Update()
    {
        if (regen && Time.timeSinceLevelLoad - lastSpawn > regenTime)
        {
            float randomX = Random.Range(0f, 1f);
            float randomZ = Random.Range(0f, 1f);
            SpawnTreeAt(new Vector2(randomX, randomZ));
            lastSpawn = Time.timeSinceLevelLoad;
            print("Spawned tree");
        }
    }

    private void SpawnTreesOnTerrain()
    {

        int currentTreeCount = 0;
        while(currentTreeCount < treeCount)
        {
            // Generate random positions on the terrain
            float randomX = Random.Range(0f, 1f);
            float randomZ = Random.Range(0f, 1f);

            if(SpawnTreeAt(new Vector2(randomX,randomZ)))
                currentTreeCount++;
        }
    }

    private bool SpawnTreeAt(Vector2 pos)
    {
        TerrainData terrainData = terrain.terrainData;
        // Calculate the world position based on the terrain's size
        Vector3 spawnPosition = new Vector3(pos.x * terrainData.size.x, 0f, pos.y * terrainData.size.z);

        // Use SampleHeight to get the terrain height at that position
        float terrainHeight = terrain.SampleHeight(spawnPosition);

        if (terrainHeight <= waterLevel)
            return false;

        // Adjust the Y position to place the tree on the terrain surface
        spawnPosition.y = terrainHeight;

        // Instantiate your tree prefab at the calculated position
        // Replace "TreePrefab" with the actual tree prefab you want to spawn
        Instantiate(TreePrefab, spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        return true;
    }
}
