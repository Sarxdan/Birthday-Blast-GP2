using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        Destroy(gameObject, 15.0f);
        rb = GetComponent<Rigidbody>();
    }

    private void PlayerHit()
    {
        onPlayerHit?.Invoke(projectileDamage);
    }

    Vector3 PredictPosition(Rigidbody targetRigid){
        Vector3 pos = targetRigid.position;
        Vector3 dir = targetRigid.velocity;

        float dist = (pos-transform.position).magnitude;

        return pos + (dist/projectileSpeed)*dir;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * (Time.fixedDeltaTime * projectileSpeed));
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

    public void SetupProjectile(float speed, int damage, Transform target)
    {
        projectileSpeed = speed;
        projectileDamage = damage;
        // Predict the position for the player and calculate direction
        //moveDirection = (PredictPosition(GameObject.FindWithTag("Player").transform.GetComponent<Rigidbody>()) - transform.position).normalized;

        moveDirection = (target.position - transform.position).normalized;
    }
}
