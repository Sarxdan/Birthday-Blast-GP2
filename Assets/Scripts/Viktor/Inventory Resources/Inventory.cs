using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    
    public static Inventory instance;

    public int scrapCount;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
        
        
        DontDestroyOnLoad(gameObject);
    }

    public void EquipItem(Item item)
    {
        items.Add(item);
    }
    
    public void IncreaseScrap(int amount)
    {
        scrapCount += amount;
    }
    

    
}
