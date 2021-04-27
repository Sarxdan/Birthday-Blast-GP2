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
        unlocked = true;
    }

    public void Lock()
    {
        unlocked = false;
    }
}
