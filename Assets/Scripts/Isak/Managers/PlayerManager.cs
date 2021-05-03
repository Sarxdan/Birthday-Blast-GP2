using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{   
    public int playerMaxHealth;
    public int playerHealth;

    public List<string> chosenCharacterMeshesNames;

    public GameObject player;

    private void Awake() {
        playerHealth = playerMaxHealth;
        chosenCharacterMeshesNames = new List<string>();
    }
}
