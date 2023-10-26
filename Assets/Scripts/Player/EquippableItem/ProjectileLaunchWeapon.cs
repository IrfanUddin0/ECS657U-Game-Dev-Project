using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunchWeapon : Weapon
{
    [Header("Weapon Details")]
    public GameObject projectilePrefab; // The projectile to be launched
    public Transform firePoint; // The point from which the projectile is fired
    protected float projectileLaunchScale = 1.0f;

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

        PlayCameraShake();
        PlayAttackingAnim = true;
    }

    protected void ProjectileReturnsHit(Vector3 hit_pos, Collider hit_col)
    {
    }
}