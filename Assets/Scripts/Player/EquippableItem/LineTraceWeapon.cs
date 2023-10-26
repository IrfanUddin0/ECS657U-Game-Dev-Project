using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class LineTraceWeapon : Weapon
{
    [Header("Weapon Details")]
    public float range;
    public bool debugDraw;
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

    protected void OnLineTraceShoot()
    {
        GameObject cam = GameObject.FindGameObjectsWithTag("CameraArm")[0];

        RaycastHit hit;
        Physics.Linecast(cam.transform.position + 0.01f * cam.transform.forward, range * cam.transform.forward + cam.transform.position, out hit);
        Debug.DrawLine(cam.transform.position + 0.01f * cam.transform.forward, range * cam.transform.forward + cam.transform.position,
            debugDraw?Color.red : Color.clear,15);

        PlayCameraShake();
        PlayAttackingAnim = true;

        if (hit.transform == null)
            return;
        else
        {
            //print("Hit");
            //TODO: take damage, bullethole, smoke, sound
        }
    }
}
