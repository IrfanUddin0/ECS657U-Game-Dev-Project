using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ProjectileLaunchWeapon : Weapon
{
    [Header("Weapon Details")]
    public GameObject projectilePrefab; // The projectile to be launched
    public Transform firePoint; // The point from which the projectile is fired
    public float damageDropoffCoefficient;
    protected float projectileLaunchScale = 1.0f;

    private float max_damage;

    public override void Start()
    {
        base.Start();
        max_damage = baseDamage;
    }
    protected void OnProjectileLaunch()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Projectile prefab or fire point not set.");
            return;
        }

        Transform fireDirection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;
        // Instantiate a new projectile at the fire point position and set its rotation to match the fire point
        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, fireDirection.rotation);

        Action<Vector3, Collider> call = (hit_pos, hit_col) => ProjectileReturnsHit(hit_pos, hit_col);
        projectileInstance.GetComponent<Projectile>().Launch(projectileLaunchScale, call);
        baseDamage = projectileLaunchScale * max_damage;

        PlayShootSound();
        PlayCameraShake();
        AddRecoil();
        PlayAttackingAnim = true;
    }

    protected void ProjectileReturnsHit(Vector3 hit_pos, Collider hit_col)
    {
        PlayerHittable hittable = hit_col.GetComponentInParent<PlayerHittable>();
        if (hittable == null)
            hittable = hit_col.GetComponentInChildren<PlayerHittable>();

        if (hittable != null)
        {
            baseDamage = baseDamage * damageDropoffCoefficient / (Vector3.SqrMagnitude(transform.position - hit_pos));
            DamageEntity(hittable);
        }
            

        baseDamage = max_damage;
    }
}