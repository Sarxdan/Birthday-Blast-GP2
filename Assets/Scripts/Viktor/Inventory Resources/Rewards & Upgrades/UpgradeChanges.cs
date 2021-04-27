using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeChanges
{
    public string name;
    public UpgradeVariables change;
        
    [Header("ex: increase FuelCapacity by 25 %")]
    public int inPercentage;
}
public enum UpgradeVariables
{
    FuelCapacity,
    FuelConsumption,
    GunCooldown,
    GunBlastRadius
}