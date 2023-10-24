using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquippableItemEvents
{
    public float baseDamage;
    public float cameraShakeDuration;
    public float cameraShakeAmmount;
    public float cameraShakeDecreaseFactor;
    public float cameraShakeSmoothness;
    public float cameraShootJumpAmmount;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update() 
    {
        base.Update();
    }

    //todo: ammo management, reloading
}
