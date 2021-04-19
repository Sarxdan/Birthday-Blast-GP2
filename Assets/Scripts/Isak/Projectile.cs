using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;
    
    public float speed;
    public float lifeTime = 7.5f;

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
        var newPos = transform.position + (transform.forward * (speed * Time.fixedDeltaTime));
        rb.MovePosition(newPos);
    }

    private void ProjectileHit()
    {
        //Add damage to player here
        
        Debug.Log("Projectile hit player");
        Destroy(gameObject);
    }
}
