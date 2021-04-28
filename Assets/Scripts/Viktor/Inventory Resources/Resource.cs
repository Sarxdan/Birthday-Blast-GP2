using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;



public class Resource : PickUp
{
    public ResourceTypes resourceType = ResourceTypes.Scrap;
    public int value = 1;
    
    private float doubleResourceChance;

    private void Start()
    {
        doubleResourceChance = Inventory.instance.gardenSpadeLuck;
    }

    public void PickUpResource()
    {
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
}
