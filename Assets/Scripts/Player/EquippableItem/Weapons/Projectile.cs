using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 10f;
    public int damage = 10;

    private float currentLifetime = 0f;
    Action<Vector3, Collider> callback;

    public void Launch(float scale, Action<Vector3, Collider> callback)
    {
        currentLifetime = 0f;
        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        rb.AddForce(scale * speed * transform.forward, ForceMode.Impulse);
        this.callback = callback;
    }

    void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime) { Destroy(gameObject); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (callback != null && collision.collider.GetType()!=typeof(CapsuleCollider))
        {
            callback.Invoke(transform.position, collision.collider);
            print(collision.collider);
            Destroy(gameObject);
        }
    }
}