using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public static Events.EmptyEvent onShopKeeperInteraction;
    // Start is called before the first frame update
    public void OpenShop()
    {
        if(onShopKeeperInteraction != null)
        {
            onShopKeeperInteraction();
        }
    }
}
