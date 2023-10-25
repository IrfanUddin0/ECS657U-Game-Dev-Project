using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurvival : MonoBehaviour
{
    private float health;
    public float maxHealth;

    // percentages, max is 1.0f
    private float hunger;
    private float thirst;

    public float hungerDecreaseRate = 0.01f;
    public float thirstDecreaseRate = 0.015f;
    // Rate at which health decreases when hunger or thirst reach zero
    public float healthDecreaseRate = 0.02f;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hunger = 1.0f;
        thirst = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease hunger and thirst over time
        hunger -= hungerDecreaseRate * Time.deltaTime;
        thirst -= thirstDecreaseRate * Time.deltaTime;

        // Ensure hunger and thirst don't go below 0
        hunger = Mathf.Clamp(hunger, 0.0f, 1.0f);
        thirst = Mathf.Clamp(thirst, 0.0f, 1.0f);

        // If hunger or thirst reach zero, reduce health over time
        if (hunger <= 0 || thirst <= 0)
        {
            health -= healthDecreaseRate * Time.deltaTime;
        }

        if (health <= 0)
        {
            if (!dead)
                OnDeath();
            dead = true;
        }
    }
    public void ReplenishHunger()
    {
        hunger = 1.0f;
    }
    public void ReplenishThirst()
    {
        thirst = 1.0f;
    }

    public float GetHealth()
    {
        return health;
    }

    public float getHunger()
    {
        return hunger;
    }

    public float GetThirst()
    {
        return thirst;
    }

    public void OnDeath()
    {

    }
}
