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

    //todo: ammo management, reloading
}
