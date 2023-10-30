using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittable : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private void Start()
    {
        health = maxHealth;
    }

    public virtual void OnPlayerHit(float dmg)
    {
        print("Player Hit");
        health -= dmg;
    }
}
