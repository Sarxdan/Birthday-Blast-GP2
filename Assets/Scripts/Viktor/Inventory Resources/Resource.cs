using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class Resource : PickUp
{
    public ResourceTypes resourceType = ResourceTypes.Scrap;
    public int value = 1;
    public GameObject popupWindow;
    
    private float doubleResourceChance;
    

    public void PickUpResource()
    {
        if (NewResource())
        {
            UIManager.instance.EnablePopUp(popupWindow);
        }
        
        doubleResourceChance = Inventory.instance.gardenSpadeLuck;
        
        Inventory.instance.PickUpResource(this);
        if (DoubleResource())
        {
            Inventory.instance.PickUpResource(this);
        }
        
        

        if (DestroyOnPickUp)
        {
            DestroyObject();
        }
    }


    private bool DoubleResource()
    {
        var rnd = Random.Range(0, 100);

        return (rnd <= doubleResourceChance);
    }


    private bool NewResource()
    {
        var resourceCountInInventory = 0;
        switch (resourceType)
        {
            case ResourceTypes.Currency:
                resourceCountInInventory = Inventory.instance.currency;
                break;
            case ResourceTypes.Scrap:
                resourceCountInInventory = Inventory.instance.scrapCount;
                break;
            case ResourceTypes.MagicRock:
                resourceCountInInventory = Inventory.instance.magicRocks;
                break;
            case ResourceTypes.MagicRoot:
                resourceCountInInventory = Inventory.instance.magicRootCount;
                break;
        }

        return resourceCountInInventory == 0;

    }
}
