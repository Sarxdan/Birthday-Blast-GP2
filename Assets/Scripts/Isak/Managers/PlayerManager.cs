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

    void OnSceneLoaded()
    {
        Transform player = FindObjectOfType<PlayerHealth>().transform;
        if(chosenCharacterMeshesNames != null && player != null)
        {
            foreach(Transform child in player)
            {
                if(child.name == "MeshBase")
                {

                }
            }
        }
    }

    private void OnEnable() {
        Gamemanager.onSceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        Gamemanager.onSceneLoaded -= OnSceneLoaded;
    }
}
