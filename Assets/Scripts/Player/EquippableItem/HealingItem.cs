using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : EquippableItemEvents
{
    public float healthReplenishAmmount;
    public float hungerReplenishAmmount;

    public AudioClip healSound;
    public float healSoundVolume = 1f;
    public override void OnFireClicked()
    {
        base.OnFireClicked();
        PlayerSurvival ps = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        ps.ReplenishHunger(hungerReplenishAmmount);
        ps.ReplenishHealth(healthReplenishAmmount);
        Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        InventoryEquipManager managerr = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>();
        inven.RemoveItem(inven.getItemAtIndex(managerr.currentEquipIndex), false);
        Util.PlayClipAtPoint(healSound, transform.position, healSoundVolume);
    }
}
