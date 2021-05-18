using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemPickup : PickUp
{
    public static Events.UnlockKeyItemEvent onKeyItemPickup;
    [SerializeField] KeyItems.Items itemUnlocked;
    public GameObject itemPopUp;
    public void UnlockItem()
    {
        if(onKeyItemPickup != null)
        {
            if (GetComponent<Reward>() != null)
            {
                UIManager.instance.EnablePopUp(itemPopUp, 1, GetComponent<Reward>().rewardStats.icon);
            }

            onKeyItemPickup(itemUnlocked);
        }
        DestroyObject();
    }
}
