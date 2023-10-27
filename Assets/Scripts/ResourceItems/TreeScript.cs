using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    PlayerHittable health;
    public GameObject woodToSpawn;
    void Start()
    {
        health = GetComponent<PlayerHittable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0)
        {
            Instantiate(woodToSpawn, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
