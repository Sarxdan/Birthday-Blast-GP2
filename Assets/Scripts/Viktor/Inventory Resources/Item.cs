using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "InventorySystem/New Item")]
public class Item : ScriptableObject
{
    public string itemName = "Default Item";
    public ItemType itemType = ItemType.Default;
    public ItemFindDifficulty itemDifficulty = ItemFindDifficulty.Normal;

    public AreaRestriction itemFoundIn = AreaRestriction.Global;
}

public enum ItemType
{
    Default,
    Resource,
    Upgrade,
    Currency
}

public enum ItemFindDifficulty
{
    Easy,
    Normal,
    Hard,
    EndGame
}


public enum AreaRestriction
{
    Global,
    Land,
    Jetpack
}