using System;
using System.Collections;
using System.Collections.Generic;
using MiscUtil.Collections.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SkillUpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Upgrade upgrade;
    public RewardUI rewardUI;
    public int upgradeableOrderInPath = 1;
    public int upgradeCost = 0;
    public bool unlockable;

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
        lockedImage.SetActive(!unlockable);
        unlockedImage.SetActive(upgrade.unlocked);

        unlockable = rewardUI.upgradesUnlockedInPath >= upgradeableOrderInPath - 1;

        if (unlockable == false)
        {
            upgradeCostText.text = "";
        }
        else
        {
            upgradeCostText.text = upgradeCost.ToString();
            var imgColor = unlockedImage.GetComponent<Image>().color;
            
            if (upgrade.unlocked == false)
            {
                imgColor.a = 120;
            }
            else
            {
                imgColor.a = 255;
            }
        }
    }


    public void TryUnlock()
    {
        if (upgrade.unlocked || upgradeCost > Inventory.instance.skillTreePoints || unlockable == false) return;

        Inventory.instance.skillTreePoints -= upgradeCost;
        upgrade.Unlock();
        rewardUI.upgradesUnlockedInPath = upgradeableOrderInPath;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (unlockable == false) return;
        
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
