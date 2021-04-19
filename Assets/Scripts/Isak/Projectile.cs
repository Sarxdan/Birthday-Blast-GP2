using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public static Events.DamagePlayerEvent onPlayerHit;

    [SerializeField] int damage = 1;
    public Rigidbody rb;
    
    [HideInInspector] public float speed; 
    public float lifeTime = 7.5f;

    [HideInInspector] public bool isHoming;
    [HideInInspector] public float homingAccuracy;
    
    [HideInInspector] public Transform target;


    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ProjectileHit();
        }
        
        Destroy(gameObject);
    }
    

    private void FixedUpdate()
    {

        var finalMoveDirection = transform.forward;
        
        if (isHoming)
        {
            if (Vector3.Distance(target.position, transform.position) > 3f) //Only allow auto aim to happen when the projectile is some distance away
            {
                var projectileForwardDir = transform.forward;
                var directionToTarget = (target.position - transform.position).normalized;

                finalMoveDirection =
                    Vector3.Lerp(projectileForwardDir, directionToTarget, Time.deltaTime * homingAccuracy);
            }

        }
        
        var newPos = transform.position + (finalMoveDirection * (speed * Time.fixedDeltaTime));
        rb.MovePosition(newPos);
    }

    private void ProjectileHit()
    {
        //Add damage to player here
        if(onPlayerHit != null)
        {
            onPlayerHit(damage);
        }
        Debug.Log("Projectile hit player");
        Destroy(gameObject);
    }
}
