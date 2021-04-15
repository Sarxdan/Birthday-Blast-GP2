using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickUp : MonoBehaviour
{
    public Item item;
    
    public void PickUp()
    {
        Inventory.instance.EquipItem(item);
        Inventory.instance.IncreaseScrap(item.scrapValue);
        Destroy(gameObject);
    }
}
