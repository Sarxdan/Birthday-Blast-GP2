using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    TextMeshProUGUI purchaseText;
    TextMeshProUGUI playerGems;
    TextMeshProUGUI playerCandy;
    [SerializeField] float purchaseTextDissappearTime = 1;
    // Start is called before the first frame update

    private void Awake() {
        purchaseText = GameObject.Find("PurchaseText").GetComponent<TextMeshProUGUI>();
        playerCandy = GameObject.Find("PlayerCandy").GetComponentInChildren<TextMeshProUGUI>();
        playerGems = GameObject.Find("PlayerGems").GetComponentInChildren<TextMeshProUGUI>();
        purchaseText.text = string.Empty;
        playerCandy.text = string.Empty;
        playerGems.text = string.Empty;
    }
    public void ExitShop()
    {
        UIManager.instance.ToggleShopUI();
    }
    private void Update() {
        playerGems.text = Inventory.instance.gems.ToString();
        playerCandy.text = Inventory.instance.currency.ToString();
    }
    void OnPurchase(bool check)
    {
        StopAllCoroutines();
        if(check)
        {
            purchaseText.text = "Purchase Successful";
        }
        else
        {
            purchaseText.text = "Purchase Failed";
        }
        StartCoroutine(ClearPurchaseText());
    }

    IEnumerator ClearPurchaseText()
    {
        yield return new WaitForSeconds(purchaseTextDissappearTime);
        purchaseText.text = string.Empty;
    }

    private void OnEnable() {
        
        ShopItem.onPurchase += OnPurchase;
        Gem.onGemBought += OnPurchase;
    }

    private void OnDisable() {
        ShopItem.onPurchase -= OnPurchase;
        Gem.onGemBought -= OnPurchase;
    }
}
