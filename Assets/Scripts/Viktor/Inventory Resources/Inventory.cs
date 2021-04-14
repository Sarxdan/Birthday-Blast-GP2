using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public int resources;
    public int upgrades;
    public int currency;
    public int other;
    
    public void EquipItem(Item item)
    {
        items.Add(item);
        IncreaseAmount(item.itemType);
    }
    
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    public int ItemSpecificCount(Item item) //Returns how many of the same item specified the inventory has
    {
        return items.Count(_item => _item.name == item.name);
    }

    private void IncreaseAmount(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Default:
                other++;
                break;
            case ItemType.Resource:
                resources++;
                break;
            case  ItemType.Upgrade:
                upgrades++;
                break;
            case ItemType.Currency:
                currency++;
                break;
        }
    }
    
}
