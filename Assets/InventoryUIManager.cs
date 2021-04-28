using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    public TextMeshProUGUI scrapCountText;
    public TextMeshProUGUI magicRockCountText;
    public TextMeshProUGUI magicRootCountText;
    public TextMeshProUGUI currencyCountText;


    private void Update()
    {
        scrapCountText.text = Inventory.instance.scrapCount.ToString();
        magicRockCountText.text = Inventory.instance.magicRocks.ToString();
        magicRootCountText.text = Inventory.instance.magicRootCount.ToString();
        currencyCountText.text = Inventory.instance.currency.ToString();
    }
}
