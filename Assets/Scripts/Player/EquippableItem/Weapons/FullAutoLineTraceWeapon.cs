using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAutoLineTraceWeapon : LineTraceWeapon
{
    [Header("Weapon Details")]
    public float minShootTime;
    private float lastShotTime = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        lastShotTime = Time.time;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    protected override void OnFireHeld()
    {
        base.OnFireHeld();
        ShootEvent();
    }

    protected override void ShootEvent()
    {
        base.ShootEvent();
        if(Time.time - lastShotTime >= minShootTime)
        {
            lastShotTime = Time.time;
            OnLineTraceShoot();
        }
            
    }
}
