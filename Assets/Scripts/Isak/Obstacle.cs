using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Obstacle : DamagePlayer
{
    [SerializeField][Tooltip("Can be destroyed by player dashing/charging forward")] bool canBeDestroyedByPlayer = true;
    [SerializeField] Vector3 movementDirection = new Vector3(0, 0, 0);
    [SerializeField][Range(1, 10)] float returnTime = 1;
    protected Rigidbody body;
    float timeUntilReturn;

    protected virtual void Awake() {
        body = GetComponent<Rigidbody>();   
        body.useGravity = false;
        timeUntilReturn = returnTime;
    }
    protected virtual void Update() {       
        MoveObject();   
    }
   
    void MoveObject()
    {
        body.velocity = movementDirection;
        timeUntilReturn -= Time.deltaTime;
        if(timeUntilReturn <= 0)
        {
            timeUntilReturn = returnTime;
            movementDirection = -movementDirection;
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        JetPack jetPack = other.gameObject.GetComponentInChildren<JetPack>();
        if(jetPack != null)
        {
            if(canBeDestroyedByPlayer && jetPack.Invulnerable)
            {
                Destroy(gameObject);
            } 
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        JetPack jetPack = other.gameObject.GetComponentInChildren<JetPack>();
        if(jetPack != null)
        {
            if(canBeDestroyedByPlayer && jetPack.Invulnerable)
            {
                Destroy(gameObject);
            } 
        }
        
    }

}
