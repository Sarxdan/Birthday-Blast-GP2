using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item item;
    public int itemAmount = 1;

    public bool DestroyOnPickUp = true;

    public ParticleSystem particleEffectOnPickUp;

    public void PickUpItem()
    {
        for (var i = 0; i < itemAmount; i++)
        {
            Inventory.instance.EquipItem(item);
        }

        if (particleEffectOnPickUp != null)
        {
            var particle = Instantiate(particleEffectOnPickUp, transform.position, Quaternion.identity);
            Destroy(particle, particle.time);
        }

        if (DestroyOnPickUp)
        {
            Destroy(gameObject);
        }
    }
}
