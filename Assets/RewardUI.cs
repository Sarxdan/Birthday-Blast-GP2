using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    public RewardSO reward;

    public int upgradesUnlockedInPath = -1;


    private void OnEnable()
    {
        if (RewardUnlocked() && upgradesUnlockedInPath < 0)
        {
            Debug.Log("Unlocked reward : " + reward.name);
            upgradesUnlockedInPath = 0;
        }
    }


    public bool RewardUnlocked()
    {
        foreach (var _reward in Inventory.instance.objRewardsUnlocked)
        {
            if (_reward.rewardID == reward.ID)
            {
                //Has reward
                return true;
            }
        }

        return false;
    }
}
