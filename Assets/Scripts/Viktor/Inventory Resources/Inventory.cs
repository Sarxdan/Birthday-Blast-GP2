using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public int scrapCount;
    public int magicRocks;
    public int currency;
    public int magicRootCount;

    public int gems;

    public int skillTreePoints = 999;

    [Header("Upgradeable stats")] 
    [Header("Base Stats")]
    public float baseFuelRechargeTime = 3.0f;
    public float baseGardenSpadeLuck = 25.0f;

    [Header("Effective Stat")] 
    public float changedBaseFuelRechargeTime;
    public float fuelRechargeTime = 0.0f;
    public float gardenSpadeLuck = 0.0f;
    
    public List<ObjectiveReward> objRewardsUnlocked = new List<ObjectiveReward>();
    public List<MemorabiliaSO> memorabiliasUnlocked = new List<MemorabiliaSO>();

    public Action onUpgradeApplied;
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        
        ApplyBaseStats();
        
    }
    
    public void ApplyBaseStats()
    {
        changedBaseFuelRechargeTime = baseFuelRechargeTime;
        gardenSpadeLuck = baseGardenSpadeLuck;
        fuelRechargeTime = changedBaseFuelRechargeTime;
        
        onUpgradeApplied?.Invoke();
    }

    public void PickUpResource(Resource resource)
    {
        var resourceType = resource.resourceType;
        switch (resourceType)
        {
            case ResourceTypes.Scrap:
                scrapCount += resource.value;
                break;
            case ResourceTypes.MagicRock:
                magicRocks += resource.value;
                break;
            case ResourceTypes.Currency:
                currency += resource.value;
                break;
            case ResourceTypes.MagicRoot:
                magicRootCount += resource.value;
                break;
        }
    }


    public bool UniqueReward(int rewardID)
    {
        return objRewardsUnlocked.All(objReward => objReward.rewardID != rewardID);
    }


    public void ApplySkillTreeUpgrades()
    {

        //For every ObjectiveReward
        for (var i = 0; i < objRewardsUnlocked.Count; i++)
        {
            //All upgrades in skillTree
            var paths =
                objRewardsUnlocked[i].attachedSkillTree.paths;
            
            for (var j = 0; j < paths.Length; j++)
            {
                var upgradesInPath = paths[j].upgrades;

                for (var k = 0; k < upgradesInPath.Count; k++)
                {
                    var upgrade = upgradesInPath[k];

                    if (upgrade.unlocked)
                    {

                        var changesInUpgrade = upgrade.upgradeStats.changes;
                        
                        for (var l = 0; l < changesInUpgrade.Length; l++)
                        {
                            var upgradeType = changesInUpgrade[l].change;
                            var change = changesInUpgrade[l];
                            
                            
                            switch (upgradeType)
                            {
                                //Apply change to correct variable
                                case UpgradeVariables.BaseFuelRechargeTime:
                                    
                                    changedBaseFuelRechargeTime =
                                        ApplyChange(baseFuelRechargeTime, change.inPercentage);
                                    break;
                                
                                case UpgradeVariables.FuelRechargeTime:

                                    fuelRechargeTime = ApplyChange(changedBaseFuelRechargeTime, change.inPercentage);

                                    break;
                                case UpgradeVariables.GardenSpadeLuck:

                                    gardenSpadeLuck = ApplyChange(baseGardenSpadeLuck, change.inPercentage);

                                    break;
                                
                            }
                        }
                    }
                }
            }
        }
        
        
        onUpgradeApplied?.Invoke();
    }

    private float ApplyChange(float baseValue, float percentage)
    {
        return baseValue + (float) (baseValue * (percentage * 0.01));
    }
    
}


public enum ResourceTypes
{
    Scrap,
    MagicRock,
    Currency,
    MagicRoot
}
