using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Memorabilia : PickUp
{
    public MemorabiliaSO memorabiliaStats;
    
    public void PickUpMemorabilia()
    {
        UIManager.instance.EnablePopUp(memorabiliaStats.popupCard);
        
        Inventory.instance.memorabiliasUnlocked.Add(memorabiliaStats);
        UIManager.instance.EnablePopUp(Inventory.instance.pickUpPopUp,1,memorabiliaStats.popupCard.transform.GetChild(0).GetComponent<Image>().sprite);
        if (DestroyOnPickUp)
        {
            DestroyObject();
        }
    }
}
