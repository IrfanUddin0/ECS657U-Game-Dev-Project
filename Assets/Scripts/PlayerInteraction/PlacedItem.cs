using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class PlacedItem : PlayerHittable
{
    public override void Start()
    {
        base.Start();
    }
}
