using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{   
    public int playerMaxHealth;
    public int playerHealth;

    private void Awake() {
        playerHealth = playerMaxHealth;
    }
}
