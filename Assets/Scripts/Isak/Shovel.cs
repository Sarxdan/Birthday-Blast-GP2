using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public static Events.UnlockKeyItemEvent onShovelPickup;
    [SerializeField] KeyItems.Items itemUnlocked;
    public void UnlockShovel()
    {
        if(onShovelPickup != null)
        {
            onShovelPickup(itemUnlocked);
        }
        Destroy(gameObject);
    }
}
