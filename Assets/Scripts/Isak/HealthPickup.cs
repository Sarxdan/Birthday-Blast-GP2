using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthRecovered;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if(player != null)
        {
            if(player.canHeal())
            {
                player.Heal(healthRecovered);
                Destroy(gameObject); // use objectpooling?
            } 
        }
    }
}
