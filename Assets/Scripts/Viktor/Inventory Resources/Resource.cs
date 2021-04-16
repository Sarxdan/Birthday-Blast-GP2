using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : PickUp
{
    public ResourceTypes resourceType = ResourceTypes.Scrap;
    public int value = 1;

    public void PickUpResource()
    {
        Inventory.instance.PickUpResource(this);
        
        if (DestroyOnPickUp)
        {
            DestroyObject();
        }
    }
}
