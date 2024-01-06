using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurvival : MonoBehaviour
{
    public float health;
    public float maxHealth;

    // percentages, max is 1.0f
    public float hunger;
    public float thirst;

    public float hungerDecreaseRate = 0.01f;
    public float thirstDecreaseRate = 0.015f;
    // Rate at which health decreases when hunger or thirst reach zero
    public float healthDecreaseRate = 0.02f;

    public bool dead = false;

    public GameObject DeathScreenUIPrefab;
    public GameObject defaultWeapon;

    private float timeSinceSpawn;
    public float invulnerableTime = 5f;

    public AudioClip onHitSound;
    public float onHitSoundVolume = 1f;

    public AudioClip deathSound;
    public float deathSoundVolume = 1f;

    // new changes made by Sulaiman
    public ScoreManager ScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        OnRespawn();
    }

    // Update is called once per frame
    void Update()
    {
        // if invul then return
        if (Time.timeSinceLevelLoad - timeSinceSpawn < invulnerableTime && health!=0)
            return;

        // Decrease hunger and thirst over time
        hunger -= hungerDecreaseRate * Time.deltaTime;
        thirst -= thirstDecreaseRate * Time.deltaTime;

        // Ensure hunger and thirst don't go below 0
        hunger = Mathf.Clamp(hunger, 0.0f, 1.0f);
        thirst = Mathf.Clamp(thirst, 0.0f, 1.0f);

        // If hunger or thirst reach zero, reduce health over time
        if (hunger <= 0)
        {
            health -= healthDecreaseRate * Time.deltaTime;
        }
        if (thirst <= 0)
        {
            health -= healthDecreaseRate * Time.deltaTime;
        }
        // keep health within its limited values
        health = Mathf.Clamp(health, 0.0f, maxHealth);

        if (health <= 0)
        {
            if (!dead)
                OnDeath();
            dead = true;
        }
    }
    public void ReplenishHunger(float ammount)
    {
        hunger = Mathf.Clamp(Mathf.Max(ammount, ammount+hunger), 0.0f, 1.0f);
    }
    public void ReplenishThirst(float ammount)
    {
        thirst = Mathf.Clamp(Mathf.Max(ammount, ammount + thirst), 0.0f, 1.0f);
    }

    public void ReplenishHealth(float ammount)
    {
        health = Mathf.Clamp(health+ammount, 0.0f, maxHealth);
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

    public void decreasePlayerHealth(float h)
    {
        // if invul then return
        if (Time.timeSinceLevelLoad - timeSinceSpawn < invulnerableTime)
            return;

        health -= h;

        // do effects
        Util.PlayClipAtPoint(onHitSound, transform.position, onHitSoundVolume);
    }

    public void OnDeath()
    {
        // drop all items
        Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        inven.RemoveEveryItem();

        // display death screen
        Instantiate(DeathScreenUIPrefab, GetComponentInChildren<Canvas>().transform);
        if(ScoreManager != null)
            ScoreManager.onPlayerDeath();

        // play sound
        Util.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
    }

    public void OnRespawn()
    {
        health = maxHealth;
        hunger = 1.0f;
        thirst = 1.0f;
        giveDefaultWeapon();
        timeSinceSpawn = Time.timeSinceLevelLoad;
        dead = false;
        if(ScoreManager!=null)
            ScoreManager.onPlayerRespawn();
    }

    private void giveDefaultWeapon()
    {
        GameObject tempWepaon = Instantiate(defaultWeapon, transform);
        tempWepaon.GetComponent<PickupItem>().OnInteract();
    }
}
