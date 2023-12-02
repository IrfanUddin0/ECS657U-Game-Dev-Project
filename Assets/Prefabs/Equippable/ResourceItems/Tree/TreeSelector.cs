using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSelector : MonoBehaviour
{
    public GameObject[] treePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], transform);
        float random_scale = 0.3f + Random.Range(0.0f, 0.3f);
        tree.transform.localScale = new Vector3(random_scale, random_scale, random_scale);
    }
}
