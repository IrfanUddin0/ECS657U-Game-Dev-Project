using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChargedProjectileWeapon : ProjectileLaunchWeapon
{
    [Header("Weapon Details")]
    public float fullChargeTime;
    private float chargeStartTime;

    public AudioClip chargeClip;
    public float chargeClipVolume;
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

    public override void OnFireClicked()
    {
        base.OnFireClicked();

        Util.PlayClipAtPoint(chargeClip, transform.position, chargeClipVolume);
        chargeStartTime = Time.time;
        PlayAttackingAnim = true;
    }

    public override void OnFireReleased()
    {
        base.OnFireReleased();
        float chargeTime = Time.time - chargeStartTime;
        projectileLaunchScale = Mathf.Clamp(chargeTime / fullChargeTime, 0f, 1f);
        ShootEvent();
        PlayAttackingAnim = false;
    }

    protected override void ShootEvent()
    {
        base.ShootEvent();
        OnProjectileLaunch();
    }

    public float getChargeStartTime()
    {
        if (!PlayAttackingAnim) return 0.0f;
        float chargeTime = Time.time - chargeStartTime;
        float scale = Mathf.Clamp(chargeTime / fullChargeTime, 0f, 1f);
        return scale;
    }
}
