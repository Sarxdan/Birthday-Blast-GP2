using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{   
    [Range(1, 10)]public int playerMaxHealth;
    [HideInInspector]
    public int playerHealth;
    [HideInInspector]
    public List<string> chosenCharacterMeshesNames;

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
        Transform player = FindObjectOfType<PlayerHealth>().transform;
        if(chosenCharacterMeshesNames.Count != 0 && player != null)
        {
            foreach(Transform child in player)
            {
                if(child.name == "MeshBase")
                {
                    foreach(Transform grandchild in child)
                    {
                        
                        if(grandchild.name == "Root") continue;
                        grandchild.gameObject.SetActive(false);
                        foreach(string name in chosenCharacterMeshesNames)
                        {
                            
                            if(grandchild.name == name) grandchild.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
