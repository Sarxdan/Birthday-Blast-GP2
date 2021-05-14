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
    

    public virtual void PickUpResource()
    {
        if (NewResource() && popupWindow != null)
        {
            UIManager.instance.EnablePopUp(popupWindow);
        }
        
        doubleResourceChance = Inventory.instance.gardenSpadeLuck;
        
        PutIntoInventory();
        if (DoubleResource())
        {
            PutIntoInventory();
        }
        
        

        if (DestroyOnPickUp)
        {
            DestroyObject();
        }
    }


    private void PutIntoInventory()
    {
        Inventory.instance.PickUpResource(this);
        if (resourceType == ResourceTypes.MagicRoot)
        {
            PlayerManager.instance.playerMaxHealth++;
            PlayerManager.instance.ResetPlayerHealth();
            
            FindObjectOfType<InGameUI>().UpdateHealthText(PlayerManager.instance.playerHealth);
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
