using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeChanges
{
    public string name;
    public UpgradeVariables change;
        
    [Header("Value changed in %")]
    public int inPercentage;
}
public enum UpgradeVariables
{
    FuelRechargeTime,
    BaseFuelRechargeTime,
    GardenSpadeLuck
}