using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectiveReward
{
    public string name;
    public Sprite rewardIcon;
    public int rewardID;
    public SkillTree attachedSkillTree;

    public ObjectiveReward(string _name, Sprite _icon, int _ID, SkillTree _skillTree)
    {
        name = _name;
        rewardIcon = _icon;
        rewardID = _ID;
        attachedSkillTree = _skillTree;
    }
}