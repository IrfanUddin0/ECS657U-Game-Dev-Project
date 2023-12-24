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
    public bool PlayAttackingAnim = false;

    public float RecoilXAmmountMax = 0.0f;
    public float RecoilXAmmountMin = 0.0f;

    public float RecoilYAmmountMax = 0.0f;
    public float RecoilYAmmountMin = 0.0f;

    public AudioClip FireSound;
    public float FireVolume = 0.5f;
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

    protected void PlayCameraShake()
    {
        Camera activecam = GameObject.FindGameObjectsWithTag("CameraArm")[0].GetComponentInChildren<Camera>();
        CameraScript camscript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraScript>();
        camscript.AddCameraShakeToPlay(
            new CameraShakeRandom(
                activecam.transform,
                cameraShakeDuration,
                cameraShakeAmmount,
                cameraShakeDecreaseFactor,
                cameraShakeSmoothness));
        camscript.AddCameraShakeToPlay(
            new CameraShake(
                activecam.transform,
                0.1f,
                cameraShootJumpAmmount,
                1f,
                1f
                ));
    }

    protected void AddRecoil()
    {
        CameraScript cam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraScript>();
        cam.rotationX += Random.Range(RecoilXAmmountMin, RecoilXAmmountMax);
        cam.rotationY += Random.Range(RecoilYAmmountMin, RecoilYAmmountMax);
    }

    protected void PlayShootSound()
    {
        Util.PlayClipAtPoint(FireSound, transform.position, FireVolume);
    }

    protected void DamageEntity(PlayerHittable EntityHit)
    {
        print(EntityHit);
        EntityHit.OnPlayerHit(baseDamage);
    }

    //todo: ammo management, reloading
}
