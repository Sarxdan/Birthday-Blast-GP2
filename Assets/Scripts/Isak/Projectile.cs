using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public static Events.DamagePlayerEvent onPlayerHit;

    public int damage;
    public float speed;
    public Rigidbody rb;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Transform target;


    public bool isHoming;
    public float homingAccuracy;


    private void Start()
    {
        Destroy(gameObject,12.5f);
    }

    private void Update()
    {
        
        
        if (isHoming)
        {
            moveDirection = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * homingAccuracy);
            var rotation = Quaternion.LookRotation(moveDirection, Vector3.forward);
            transform.rotation = rotation;
        }

        rb.MovePosition(transform.position + (moveDirection * (speed * Time.fixedDeltaTime)));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile hit Player");
            ProjectileHit();
        }

        if (!other.CompareTag("Projectile"))
        {
            Debug.Log("Projectile hit something");
            Destroy(gameObject);
        }
    }


    private void ProjectileHit()
    {
        //Add damage to player here
        if(onPlayerHit != null)
        {          
            onPlayerHit(damage);
        }
        Destroy(gameObject);
    }
}
