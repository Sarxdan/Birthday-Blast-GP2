using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memorabilia : PickUp
{
    public MemorabiliaSO memorabiliaStats;
    
    public void PickUpMemorabilia()
    {
        Inventory.instance.memorabiliasUnlocked.Add(memorabiliaStats);
        if (DestroyOnPickUp)
        {
            DestroyObject();
        }
    }
}
