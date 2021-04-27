using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillTree Upgrade", menuName = "Inventory/Rewards/SkillTree/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public UpgradeChanges[] changes;

    public string upgradeName;
    [TextArea(5,10)]
    public string upgradeDesc;
}

