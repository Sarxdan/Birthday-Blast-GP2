using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public void UnlockShovel()
    {
        Gamemanager.instance.unlockedItems.shovel = true;
        Destroy(gameObject);
    }
}
