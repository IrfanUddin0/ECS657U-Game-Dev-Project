using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    public float healAmmount;
    public float healRate;

    public float effectDistance;

    private float lastHealTime;
    // Start is called before the first frame update
    void Start()
    {
        lastHealTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is close heal overtime
        Vector3 playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Vector3.Distance(playerpos, transform.position) <= effectDistance
            && Time.timeSinceLevelLoad - lastHealTime > healRate)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>().ReplenishHealth(healAmmount);
        }

        // if food is close, replace it with cooked version
        RaycastHit hit;
        Physics.SphereCast(transform.position, effectDistance, transform.forward, out hit);
        CookFoodScript[] foods = GameObject.FindObjectsByType<CookFoodScript>(FindObjectsSortMode.None);

        foreach (var food in foods)
        {
            if(Vector3.SqrMagnitude(food.transform.position - transform.position) <= effectDistance * effectDistance)
            {
                food.OnCook();
            }
        }
    }
}
