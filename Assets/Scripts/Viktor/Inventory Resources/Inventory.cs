using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int baseResourceCount;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    public void EquipResource()
    {
        baseResourceCount++;
    }
    

    
}
