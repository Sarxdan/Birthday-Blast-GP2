using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public static Events.BoolEvent onPurchase; 
    [SerializeField] int cost;
    // Start is called before the first frame update
    public void BuyItem()
    {
        if(Inventory.instance.currency >= cost)
        {
            Inventory.instance.currency -= cost;
            if(onPurchase != null)
            {
                onPurchase(true);
            }
        }
        else
        {
            if(onPurchase != null)
            {
                onPurchase(false);
            }
        }
    }
}
