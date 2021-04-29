using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : PickUp
{
    public RewardSO rewardStats;

    

    public void GetReward()
    {
        if (Inventory.instance.UniqueReward(rewardStats.ID))
        {
            var objectiveReward = new ObjectiveReward(rewardStats.name, rewardStats.icon, rewardStats.ID, rewardStats.skillTree);
            Inventory.instance.objRewardsUnlocked.Add(objectiveReward);
            
            DestroyObject();
        }
    }
    
}
