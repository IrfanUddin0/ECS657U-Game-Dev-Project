using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableItemEvents : MonoBehaviour
{
    public float ads_time = 0.01f;
    public float ads_fov_sacle = 0.8f;
    public Vector3 target_ads_transform_const = Vector3.zero;

    private Vector3 current_ads_translation = Vector3.zero;
    private Vector3 transform_velocity;
    private float fov_transform_velocity;

    bool fire_held = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        adsAnimTick();
        if( fire_held ) { OnFireHeld(); }
    }

    public virtual void OnFireClicked()
    {
        print("FIre");
        fire_held = true;
    }

    protected virtual void OnFireHeld()
    {
        print("fire held");
    }

    public virtual void OnFireReleased()
    {
        print("Fire released");
        fire_held = false;
    }

    public virtual void OnADSClicked()
    {
        setAdsAnimNew(false);
    }

    public virtual void OnADSReleased()
    {
        setAdsAnimNew(true);
    }

    public virtual void OnReloadClicked()
    {

    }

    public virtual void OnReloadCanceled()
    {

    }

    private void setAdsAnimNew(bool reverse)
    {
        current_ads_translation = (reverse? Vector3.zero : target_ads_transform_const);
    }

    private void adsAnimTick()
    {
        transform.localPosition = Vector3.SmoothDamp(
            transform.localPosition,
            current_ads_translation,
            ref transform_velocity,
            ads_time);

        float base_fov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInputScript>().cameraFieldOfView;
        Camera activecam = GameObject.FindGameObjectsWithTag("CameraArm")[0].GetComponentInChildren<Camera>();
        if( activecam != null )
        {
            activecam.fieldOfView = Mathf.SmoothDamp(
                activecam.fieldOfView,
                (current_ads_translation==Vector3.zero)? base_fov :base_fov * ads_fov_sacle,
                ref fov_transform_velocity,
                ads_time);
        }
    }
}
