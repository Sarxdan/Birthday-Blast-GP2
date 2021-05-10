using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemPickup : PickUp
{
    public static Events.UnlockKeyItemEvent onKeyItemPickup;
    [SerializeField] KeyItems.Items itemUnlocked;
    public void UnlockItem()
    {
        if(onKeyItemPickup != null)
        {
            onKeyItemPickup(itemUnlocked);
        }
        DestroyObject();
    }
}
