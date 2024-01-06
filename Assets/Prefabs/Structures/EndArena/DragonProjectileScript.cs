using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonProjectileScript : MonoBehaviour
{
    public float projectileSpeed;
    void Update()
    {
        transform.position = transform.position + transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerSurvival>().decreasePlayerHealth(5f);
            Destroy(gameObject);
        }
            
    }
}
