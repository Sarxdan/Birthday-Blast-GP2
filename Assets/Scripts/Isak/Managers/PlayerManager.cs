using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{   
    public int playerMaxHealth;
    public int playerHealth;

    public List<string> chosenCharacterMeshesNames;

    public GameObject player;

    protected override void Awake() {
        base.Awake();
        playerHealth = playerMaxHealth;
        chosenCharacterMeshesNames = new List<string>();
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
