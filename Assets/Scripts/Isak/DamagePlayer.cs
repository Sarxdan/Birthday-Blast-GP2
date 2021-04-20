using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DamagePlayer : MonoBehaviour
{  
    public static Events.DamagePlayerEvent onPlayerCollision;
    [SerializeField] int damageToPlayerOnCollision = 1;
    
    // Start is called before the first frame update
    protected virtual void OnCollisionEnter(Collision other) {
        JetPack jetPack = other.gameObject.GetComponentInChildren<JetPack>();
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if( jetPack != null || playerHealth != null)
        {
            if(onPlayerCollision != null && !jetPack.Invulnerable)
            {           
                onPlayerCollision(damageToPlayerOnCollision);
            }           
        }
    }
    
    protected virtual void OnTriggerEnter(Collider other) {
        JetPack jetPack = other.gameObject.GetComponentInChildren<JetPack>();
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if( jetPack != null || playerHealth != null)
        {
            if(onPlayerCollision != null && !jetPack.Invulnerable)
            {           
                onPlayerCollision(damageToPlayerOnCollision);
            }           
        }
    }

}
