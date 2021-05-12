using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{   
    [HideInInspector]
    public int playerHealth;

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
    }

    public void ResetPlayerHealth()
    {
        playerHealth = playerMaxHealth;
    }

    public void PlayerAwake()
    {
        if (chosenCharacterPrefab == null) return;
        List<string> characterParts = new List<string>();
        var players = FindObjectsOfType<PlayerHealth>();
        foreach(Transform skin in chosenCharacterPrefab.transform)
        {
            if(skin.name == "Root") continue;
            if(skin.gameObject.activeSelf) characterParts.Add(skin.name);
        }
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
                        foreach(string part in characterParts)
                        {
                            if(grandchild.name == part) grandchild.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

    }
}
