using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SkillUpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Upgrade upgrade;
    public int upgradeCost = 0;

    public GameObject descriptionWindow;
    public GameObject unlockedImage;
    public GameObject lockedImage;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeDescText;


    private void Start()
    {
        upgradeNameText.text = UpgradeName();
        upgradeDescText.text = UpgradeDesc();
        upgradeCostText.text = upgradeCost.ToString();
    }

    private void Update()
    {
        lockedImage.SetActive(!upgrade.unlocked);
        unlockedImage.SetActive(upgrade.unlocked);
    }


    public void TryUnlock()
    {
        if (upgrade.unlocked || upgradeCost > Inventory.instance.skillTreePoints) return;

        Inventory.instance.skillTreePoints -= upgradeCost;
        upgrade.Unlock();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionWindow.SetActive(true);
        upgradeNameText.text = UpgradeName();
        upgradeDescText.text = UpgradeDesc();
        upgradeCostText.text = upgradeCost.ToString();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionWindow.SetActive(false);
    }

    private string UpgradeName()
    {
        return upgrade.upgradeStats.upgradeName;
    }

    private string UpgradeDesc()
    {
        return upgrade.upgradeStats.upgradeDesc;
    }
}
