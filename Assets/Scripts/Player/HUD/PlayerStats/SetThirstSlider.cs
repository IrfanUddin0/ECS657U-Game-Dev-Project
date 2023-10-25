using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetThirstSlider : MonoBehaviour
{
    PlayerSurvival survival;
    // Start is called before the first frame update
    void Start()
    {
        survival =  GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
    }

    // Update is called once per frame
    void Update()
    {
        if(survival != null)
        {
            GetComponent<Slider>().value = survival.GetThirst();
        }
    }
}
