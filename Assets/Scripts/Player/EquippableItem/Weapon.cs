using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquippableItemEvents
{
    [Header("Weapon Details")]
    public float baseDamage;
    [Header("CameraShake")]
    public float cameraShakeDuration=0.1f;
    public float cameraShakeAmmount=50f;
    public float cameraShakeDecreaseFactor=3f;
    public float cameraShakeSmoothness=1f;
    public float cameraShootJumpAmmount=1f;
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
