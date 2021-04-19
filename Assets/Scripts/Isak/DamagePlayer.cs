using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DamagePlayer : MonoBehaviour
{  
    public static Events.DamagePlayerEvent onPlayerCollision;
    [SerializeField] int damageToPlayerOnCollision = 1;
    [SerializeField] bool canBeDestroyedByPlayer = true;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) {
        JetPack jetPack = other.gameObject.GetComponentInChildren<JetPack>();
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if( jetPack != null || playerHealth != null)
        {
            if(canBeDestroyedByPlayer && jetPack.Invulnerable)
            {
                Destroy(gameObject);
            } 
            else if(onPlayerCollision != null)
            {           
                onPlayerCollision(damageToPlayerOnCollision);
            }           
        }
    }

}
