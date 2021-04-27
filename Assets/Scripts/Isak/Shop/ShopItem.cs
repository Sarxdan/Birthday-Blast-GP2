using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public static Events.BoolEvent onPurchase; 
    [SerializeField] int gemCost;
    [SerializeField] int currencyCost;
    // Start is called before the first frame update
    public void BuyForCurrency()
    {
        if(Inventory.instance.currency >= currencyCost)
        {
            Inventory.instance.currency -= currencyCost;
            if(onPurchase != null)
            {
                onPurchase(true);
            }
            OnPurchaseSuccess();
        }
        else
        {
            if(onPurchase != null)
            {
                onPurchase(false);
            }
        }
    }
    public void BuyForGems()
    {
        if(Inventory.instance.gems >= gemCost)
        {
            Inventory.instance.gems -= gemCost;
            if(onPurchase != null)
            {
                onPurchase(true);
            }
            OnPurchaseSuccess();
        }
        else
        {
            if(onPurchase != null)
            {
                onPurchase(false);
            }
        }
    }

    protected virtual void OnPurchaseSuccess()
    {

    }
}
