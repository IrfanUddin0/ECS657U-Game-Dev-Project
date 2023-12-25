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
        Util.PlayClipAtPoint(onHitSound, transform.position, 1f);
        print("Player Hit");
        health -= dmg;
    }
}
