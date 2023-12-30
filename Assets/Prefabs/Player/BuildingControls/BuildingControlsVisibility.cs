using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControlsVisibility : MonoBehaviour
{
    GameObject player;
    Canvas canvas;
    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        canvas = GetComponentInChildren<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        var equip = player.GetComponentInChildren<PlaceableEquipItem>();
        if (equip != null)
            canvas.enabled = true;
        else
            canvas.enabled = false;
    }
}
