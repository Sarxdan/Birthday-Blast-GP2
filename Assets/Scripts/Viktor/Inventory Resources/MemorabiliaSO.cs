using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Memorabilia", menuName = "Inventory/Memorabilia")]
public class MemorabiliaSO : ScriptableObject
{
    public string name;

    public string uniqueQuote;
    [TextArea(5,10)]
    public string desc;
}
