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
    public Transform projectileTarget;
    private Vector3 moveDirection;

    public bool isHoming;
    public float homingAccuracy;

    private void OnEnable()
    {
        Destroy(gameObject, 10.0f);
    }

    private void ProjectileHit()
    {
        onPlayerHit?.Invoke(projectileDamage);
    }


    private void Update()
    {
        Debug.DrawRay(transform.position, moveDirection);
        
        
        
        if (isHoming)
        {
            moveDirection = Vector3.Lerp(transform.position, projectileTarget.position, homingAccuracy * Time.deltaTime);
        }
        
        MoveInDirection(moveDirection);
    }

    private void MoveInDirection(Vector3 direction)
    {
        
        transform.Translate(direction * (Time.deltaTime * projectileSpeed));
    }


    private void OnTriggerEnter(Collider other)
    {
        GetComponent<MeshRenderer>().enabled = false;
        
        if (other.CompareTag("Player"))
        {
            ProjectileHit();
        }

        var destroyAfterTime = 0.0f;
        if (onHitEffect != null)
        {
            destroyAfterTime = onHitEffect.time;
            var newEffect =Instantiate(onHitEffect, transform.position, quaternion.identity);
            Destroy(newEffect,destroyAfterTime);
        }
        Destroy(gameObject, destroyAfterTime);
    }


    public void InitializeProjectile(float speed, Transform target,Vector3 direction, bool _isHoming, float homingAcc)
    {
        projectileSpeed = speed;
        projectileTarget = target;
        isHoming = _isHoming;
        homingAccuracy = homingAcc;

        moveDirection = direction;
    }
    
}
