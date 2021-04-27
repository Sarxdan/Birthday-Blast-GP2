using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    TextMeshProUGUI purchaseText;
    [SerializeField] float purchaseTextDissappearTime = 1;
    // Start is called before the first frame update

    private void Awake() {
        purchaseText = GameObject.Find("PurchaseText").GetComponent<TextMeshProUGUI>();
        purchaseText.text = string.Empty;
    }
    public void ExitShop()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
