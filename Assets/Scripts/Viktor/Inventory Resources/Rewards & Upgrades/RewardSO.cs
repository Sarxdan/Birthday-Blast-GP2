using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reward", menuName = "Inventory/Reward")]
public class RewardSO : ScriptableObject
{
    public string name = "Reward Name";
    [TextArea(5, 15)] 
    public string description = "Reward Description";
    public Sprite icon;
    public int ID;
    public SkillTree skillTree;
}
