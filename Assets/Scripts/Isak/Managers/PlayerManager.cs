using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{   
    [HideInInspector]
    public int playerHealth;
    
    public List<string> chosenCharacterMeshesNames;

    [Header("Player health settings")]
    [Range(1, 10)]public int playerMaxHealth;

    [Header("Player fuel Settings")]
    [Tooltip("time until fuel recharges")] public float fuelRechargeTime = 1;
    [Tooltip("fuel used when doing stuff that uses fuel")] public int fuelUsage = 1; 
    [Tooltip("How fast fuel recharges")] public int fuelRechargePerTick = 1;
    [Range(1, 10)] public int maxFuel = 10;

    public GameObject chosenCharacterPrefab;
    

    GameObject player;

    protected override void Awake() {
        base.Awake();
        playerHealth = playerMaxHealth;
        chosenCharacterMeshesNames = new List<string>();
    }

    public void ResetPlayerHealth()
    {
        playerHealth = playerMaxHealth;
    }

    public void PlayerAwake()
    {
        if (chosenCharacterPrefab == null) return;
        var players = FindObjectsOfType<PlayerHealth>();
        foreach (var _player in players)
        {
            foreach (Transform child in _player.transform)
            {
                if (child.name == "MeshBase")
                {
                    foreach (Transform grandchild in child)
                    {

                        if (grandchild.name == "Root") continue;
                        grandchild.gameObject.SetActive(false);
                        if(grandchild.name == chosenCharacterPrefab.name)
                        {
                            grandchild.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

    }
}
