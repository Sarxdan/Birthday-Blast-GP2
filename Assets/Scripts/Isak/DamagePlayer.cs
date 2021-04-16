using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DamagePlayer : MonoBehaviour
{  
    public static Events.DamagePlayerEvent onPlayerCollision;
    [SerializeField] int damageOnCollision = 1;
    [SerializeField] bool canBeDestroyedByPlayer = true;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) {
        JetPack jetPack = other.gameObject.GetComponentInChildren<JetPack>();
        if( jetPack != null || other.gameObject.GetComponent<ThirdPersonController>() != null)
        {
            if(canBeDestroyedByPlayer && jetPack.Invulnerable)
            {
                Destroy(gameObject);
            } 
            else if(onPlayerCollision != null)
            {
                onPlayerCollision(damageOnCollision);
            }
            
        }
    }

}
