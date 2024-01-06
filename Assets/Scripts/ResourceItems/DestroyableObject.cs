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

    public void check()
    {
        if (health.health <= 0)
        {
            Instantiate(PickupableToSpawn, gameObject.transform.position, gameObject.transform.rotation);
            FindAnyObjectByType<PlayerObjectives>().addDataEntry(gameObject.name.Split("(")[0]+"Destroyed", "true");
            Destroy(gameObject);
        }
    }
}
