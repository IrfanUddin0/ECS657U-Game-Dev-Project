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

    private void FixedUpdate()
    {
        if(player.GetComponentInChildren<PlaceableEquipItem>() != null)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
