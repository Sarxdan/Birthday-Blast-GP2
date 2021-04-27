using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTabButton : MonoBehaviour
{
    [SerializeField] Canvas connectedTab;
    ShopTabButton[] shopTabButtons;
    GameObject tabs;
    private void Awake() {
        shopTabButtons = FindObjectsOfType<ShopTabButton>();
        tabs = GameObject.Find("Tabs");
        CheckIfActive();
    }
    public void OnButtonPressed()
    {
        ToggleButtons();

        ToggleTabs();
    }

    void CheckIfActive()
    {
        if(gameObject.GetComponentInChildren<TextMeshProUGUI>().color == Color.white)
        {
            ToggleTabs();
        }
    }

    private void ToggleButtons()
    {
        foreach (ShopTabButton button in shopTabButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }

    private void ToggleTabs()
    {
        foreach (Transform child in tabs.transform)
        {
            child.gameObject.SetActive(false);
        }
        connectedTab.gameObject.SetActive(true);
    }
}
