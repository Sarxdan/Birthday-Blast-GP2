using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public int scrapCount;
    public int magicRocks;
    public int currency;
    public int magicRootCount;

    public int gems; //used as store value, bought with real money

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void PickUpResource(Resource resource)
    {
        var resourceType = resource.resourceType;
        switch (resourceType)
        {
            case ResourceTypes.Scrap:
                scrapCount += resource.value;
                break;
            case ResourceTypes.MagicRock:
                magicRootCount += resource.value;
                break;
            case ResourceTypes.Currency:
                currency += resource.value;
                break;
            case ResourceTypes.MagicRoot:
                magicRootCount += resource.value;
                break;
        }
    }
}

public enum ResourceTypes
{
    Scrap,
    MagicRock,
    Currency,
    MagicRoot
}
