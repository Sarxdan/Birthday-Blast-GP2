using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public static Events.DamagePlayerEvent onPlayerHit;

    [SerializeField] private int damage = 1;
    public Rigidbody rb;
    
    [HideInInspector] public float speed; 
    public float lifeTime = 7.5f;

    [HideInInspector] public bool isHoming;
    [HideInInspector] public float homingAccuracy;
    
    [HideInInspector] public Transform target;
    [HideInInspector] public Vector3 moveDir;


    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter()");
        
        if (other.CompareTag("Player"))
        {
            ProjectileHit();
        }

        if (!other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
    

    private void FixedUpdate()
    {
        if (transform.position.z <= target.position.z - 500)
        {
            Debug.Log("Player Z : " + target.position.z);
            Debug.Log("Projectile Z : " + transform.position.z);
            Debug.Log("Max range");
            Destroy(gameObject);
        }

        var finalMoveDirection = moveDir;
        
        if (isHoming)
        {
            if (Vector3.Distance(target.position, transform.position) > 2) //Only allow auto aim to happen when the projectile is some distance away
            {
                var projectileForwardDir = transform.forward;
                var directionToTarget = (target.position - transform.position).normalized;

                finalMoveDirection =
                    Vector3.Lerp(projectileForwardDir, directionToTarget, Time.deltaTime * homingAccuracy);

                transform.rotation = Quaternion.LookRotation(finalMoveDirection, Vector3.forward);
            }

        }
        
        var newPos = transform.position + (finalMoveDirection * (speed * Time.fixedDeltaTime));
        rb.MovePosition(newPos);
    }

    private void ProjectileHit()
    {
        Debug.Log("ProjectileHit()");
        //Add damage to player here
        if(onPlayerHit != null)
        {
            Debug.Log("DamageEvent");
            onPlayerHit(damage);
        }
        Destroy(gameObject);
    }
}
