using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DamagePlayer : MonoBehaviour
{  
    public static Events.DamagePlayerEvent onPlayerCollision;
    [SerializeField] int damageOnCollision = 1;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponentInChildren<JetPack>() != null || other.gameObject.GetComponent<ThirdPersonController>() != null)
        {
            if(onPlayerCollision != null)
            {
                onPlayerCollision(damageOnCollision);
            }
        }
    }

    private void OnTriggerEnter(Collision other) {
        if(other.gameObject.GetComponentInChildren<JetPack>() != null || other.gameObject.GetComponent<ThirdPersonController>() != null)
        {
            if(onPlayerCollision != null)
            {
                onPlayerCollision(damageOnCollision);
            }
        }
    }
}
