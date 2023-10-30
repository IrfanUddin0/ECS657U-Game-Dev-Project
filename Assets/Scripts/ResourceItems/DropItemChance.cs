using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemChance : MonoBehaviour
{
    public GameObject dropItem;
    public float chance;
    public float spawn_up_distance;

    private bool isQuitting = false;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if(!isQuitting)
        {
            float rng = Random.Range(0.0f, 1.0f);
            if (rng <= chance)
            {
                Instantiate(dropItem, transform.position+ spawn_up_distance * Vector3.up, transform.rotation);
            }
                
        }
    }
}
