using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : PickUp
{
    public RewardSO rewardStats;
    bool rewarded = false; // if reward has already been given to player
    

    public void GetReward()
    {
        if(rewardStats != null) return;
        if(rewarded) return;
        if (Inventory.instance.UniqueReward(rewardStats.ID))
        {
            var objectiveReward = new ObjectiveReward(rewardStats.name, rewardStats.icon, rewardStats.ID, rewardStats.skillTree);
            Inventory.instance.objRewardsUnlocked.Add(objectiveReward);
            
            DestroyObject();
        }
    }
    
}
