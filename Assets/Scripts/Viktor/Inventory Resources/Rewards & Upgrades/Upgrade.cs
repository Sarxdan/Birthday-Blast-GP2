using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public bool unlocked;
    public UpgradeSO upgradeStats;


    public void Unlock()
    {
        if(unlocked) return;
        
        unlocked = true;
        
        Inventory.instance.ApplySkillTreeUpgrades();
    }

    public void Lock()
    {
        unlocked = false;
        Inventory.instance.ApplyBaseStats();
        Inventory.instance.ApplySkillTreeUpgrades();
    }
}
