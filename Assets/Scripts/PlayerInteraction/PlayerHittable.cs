using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittable : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public AudioClip onHitSound;
    public virtual void Start()
    {
        health = maxHealth;
    }

    public virtual void OnPlayerHit(float dmg)
    {
        if(health > 0)
            Util.PlayClipAtPoint(onHitSound, transform.position, 1f);

        print("Player Hit");
        health -= dmg;

        var des = GetComponent<DestroyableObject>();
        if (des != null)
            des.check();
    }
}
