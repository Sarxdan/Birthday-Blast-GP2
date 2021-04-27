using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public static Events.BoolEvent onGemBought;
    [SerializeField] int amount = 10;
    // Start is called before the first frame update
    public void BuyGems()
    {
        Inventory.instance.gems += amount;
        if(onGemBought != null)
        {
            onGemBought(true);
        }
    }
}
