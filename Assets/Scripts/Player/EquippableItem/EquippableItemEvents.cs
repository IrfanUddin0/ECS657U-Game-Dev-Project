using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableItemEvents : MonoBehaviour
{
    [Header("ADS Transform")]
    public float ads_time = 0.1f;
    public float ads_fov_scale = 0.8f;
    public Vector3 target_ads_transform_const = Vector3.zero;

    private Vector3 current_ads_translation = Vector3.zero;
    private Vector3 transform_velocity;

    bool fire_held = false;
    bool ads_held = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        adsAnimTick();
        if( fire_held ) { OnFireHeld(); }
        if( ads_held ) { OnADSHeld(); }
    }

    public virtual void OnFireClicked()
    {
        fire_held = true;
    }

    protected virtual void OnFireHeld()
    {
    }

    public virtual void OnFireReleased()
    {
        fire_held = false;
    }

    public virtual void OnADSClicked()
    {
        setAdsAnimNew(false);
        ads_held = true;
    }

    protected virtual void OnADSHeld()
    {
    }

    public virtual void OnADSReleased()
    {
        setAdsAnimNew(true);
        ads_held = false;
    }

    public virtual void OnReloadClicked()
    {

    }

    public virtual void OnReloadCanceled()
    {

    }

    protected virtual void ShootEvent()
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
    }

    public bool getFireHeld() {  return fire_held; }
}
