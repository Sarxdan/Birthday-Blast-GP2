using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopResourceItem : ShopItem
{
    [SerializeField] ResourceTypes resource;
    [SerializeField] int amountBought;
    // Start is called before the first frame update
    protected override void OnPurchaseSuccess()
    {
        switch(resource)
        {
            case ResourceTypes.Scrap:
            Inventory.instance.scrapCount += amountBought;
            break;

            case ResourceTypes.Currency:
            Inventory.instance.currency += amountBought;
            break;

            case ResourceTypes.MagicRock:
            Inventory.instance.magicRocks += amountBought;
            break;

            case ResourceTypes.MagicRoot:
            Inventory.instance.magicRootCount += amountBought;
            break;

            default:
            Debug.LogError("error with purchasing resources, item does not exist");
            break;
        }
    }
}
