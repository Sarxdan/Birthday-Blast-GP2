using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Events.DamagePlayerEvent onPlayerHit;
    public ParticleSystem onHitEffect;

    public int projectileDamage = 1;
    public float projectileSpeed;
    public Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        Destroy(gameObject, 25.0f);
        rb = GetComponent<Rigidbody>();
    }

    private void PlayerHit()
    {
        onPlayerHit?.Invoke(projectileDamage);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection.normalized * (Time.fixedDeltaTime * projectileSpeed));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag)) return;

        if (other.CompareTag("Player"))
        {
            PlayerHit();
        }

        if (onHitEffect != null)
        {
            var newEffect = Instantiate(onHitEffect, transform.position, Quaternion.identity);
            Destroy(newEffect.gameObject, newEffect.time);
        }
        
        Destroy(gameObject);
    }
}
