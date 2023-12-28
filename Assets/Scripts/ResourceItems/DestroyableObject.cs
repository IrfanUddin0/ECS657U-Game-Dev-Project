using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHittable))]
public class DestroyableObject : MonoBehaviour
{
    PlayerHittable health;
    public GameObject PickupableToSpawn;
    void Start()
    {
        health = GetComponent<PlayerHittable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0)
        {
            //Instantiate(PickupableToSpawn, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
