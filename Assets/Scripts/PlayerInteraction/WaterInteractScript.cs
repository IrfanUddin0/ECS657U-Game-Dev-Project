using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractScript : PlayerInteractable
{
    public float thirstReplenishAmmount = 1;
    public override void OnInteract()
    {
        base.OnInteract();
        PlayerSurvival ps = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        ps.ReplenishThirst(thirstReplenishAmmount);
    }
}
