using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Obstacle : DamagePlayer
{
    bool isPlayingAudio;
    [SerializeField] bool canBeDestroyedByPlayer = true;
    [SerializeField] Vector3 movementDirection = new Vector3(0, 0, 0);
    [SerializeField][Range(1, 10)] float returnTime = 1;
    [SerializeField] LayerMask playerLayer;
    protected Rigidbody body;
    float timeUntilReturn;

    private void Awake() {
        body = GetComponent<Rigidbody>();   
        body.useGravity = false;
        timeUntilReturn = returnTime;
    }
    protected virtual void Update() {       
        MoveObject();   
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, 10, playerLayer);
        if(colliders.Length > 0 && !isPlayingAudio)
        {
            isPlayingAudio = true; // needs a better fix
            AudioManager.instance.PlayClipAtPoint("Obstacle", transform.position);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 10);
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
