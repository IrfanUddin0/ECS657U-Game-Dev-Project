using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittable : MonoBehaviour
{
    public float health;

    public virtual void OnPlayerHit(float dmg)
    {
        print("Player Hit");
        health -= dmg;
    }
}
