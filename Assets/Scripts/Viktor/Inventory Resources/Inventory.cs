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

    [Header("Player Stats")]
    public float baseFuelCapacity;
    public float baseFuelConsumption;

    public float baseGunCooldown;
    public float baseGunBlastRadius;
    [Space]
    public float fuelCapacity;
    public float fuelConsumption;
    public float gunCooldown;
    public float gunBlastRadius;

    public List<ObjectiveReward> objRewardsUnlocked = new List<ObjectiveReward>();

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            ApplySkillTreeUpgrades();
    }

    public void ApplyBaseStats()
    {
        fuelCapacity = baseFuelCapacity;
        fuelConsumption = baseFuelConsumption;
        gunCooldown = baseGunCooldown;
        gunBlastRadius = baseGunBlastRadius;
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
                magicRootCount += resource.value;
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

                                case UpgradeVariables.FuelCapacity:

                                    fuelCapacity = ApplyChange(baseFuelCapacity, change.inPercentage);

                                    break;
                                case UpgradeVariables.FuelConsumption:

                                    fuelConsumption = ApplyChange(baseFuelConsumption, change.inPercentage);

                                    break;
                                case UpgradeVariables.GunCooldown:
                                    
                                    gunCooldown = ApplyChange(baseGunCooldown, change.inPercentage);

                                    break;
                                case UpgradeVariables.GunBlastRadius:
                                    
                                    gunBlastRadius = ApplyChange(baseGunBlastRadius, change.inPercentage);

                                    break;
                            }
                        }
                    }
                }
            }
        }
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
