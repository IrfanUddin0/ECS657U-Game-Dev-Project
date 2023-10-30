using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractScript : PlayerInteractable
{
    public float thirstReplenishAmmount;
    public override void OnInteract()
    {
        PlayerSurvival ps = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        ps.ReplenishThirst(thirstReplenishAmmount);
    }
}
