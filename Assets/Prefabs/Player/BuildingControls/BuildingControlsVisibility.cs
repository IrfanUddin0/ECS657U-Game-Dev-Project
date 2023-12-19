using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControlsVisibility : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        PlaceableEquipItem equip = GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<PlaceableEquipItem>();
        if (equip != null)
            GetComponentInChildren<Canvas>().enabled = true;
        else
            GetComponentInChildren<Canvas>().enabled = false;
    }
}
