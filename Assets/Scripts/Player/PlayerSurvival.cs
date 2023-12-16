using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurvival : MonoBehaviour
{
    public float health;
    public float maxHealth;

    // percentages, max is 1.0f
    private float hunger;
    private float thirst;

    public float hungerDecreaseRate = 0.01f;
    public float thirstDecreaseRate = 0.015f;
    // Rate at which health decreases when hunger or thirst reach zero
    public float healthDecreaseRate = 0.02f;

    public bool dead = false;

    public GameObject DeathScreenUIPrefab;
    public GameObject defaultWeapon;

    private float timeSinceSpawn;
    public float invulnerableTime = 5f;

    // new changes made by Sulaiman
    // main light for disabling/enabling animation
    public GameObject MainLight;

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
        if (Time.timeSinceLevelLoad - timeSinceSpawn < invulnerableTime)
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
    }

    public void OnDeath()
    {
        // drop all items
        Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        inven.RemoveEveryItem();

        // display death screen
        Instantiate(DeathScreenUIPrefab, GetComponentInChildren<Canvas>().transform);

        // new changes made by Sulaiman
        // disable the main light animation
        MainLight.GetComponent<Animator>().enabled = false;
        // values rounded to compare with main light rotation value simpler
        ScoreManager.lastDeathXLightPosition = Mathf.Round(ScoreManager.MainLight.eulerAngles.x);
        ScoreManager.lastDeathYLightPosition = Mathf.Round(ScoreManager.MainLight.eulerAngles.y);
    }

    public void OnRespawn()
    {
        health = maxHealth;
        hunger = 1.0f;
        thirst = 1.0f;
        giveDefaultWeapon();
        timeSinceSpawn = Time.timeSinceLevelLoad;
        dead = false;

        // new changes made by Sulaiman
        // enable the main light animation
        MainLight.GetComponent<Animator>().enabled = true;
        // set days survived score to 0
        ScoreManager.scoreCount = 0;
        ScoreManager.scoreText.text = "Day " + ScoreManager.scoreCount.ToString();
    }

    private void giveDefaultWeapon()
    {
        GameObject tempWepaon = Instantiate(defaultWeapon, transform);
        tempWepaon.GetComponent<PickupItem>().OnInteract();
    }
}
