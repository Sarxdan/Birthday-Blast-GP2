using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject thirdPersonController;
    public GameObject JetpackController;

    private GameObject controllerToSpawn;
    public bool manualSpawn;

    private void Awake()
    {
        //Spawn ThirdPersonController on island, Jetpack otherwise
        controllerToSpawn = transform.parent.GetComponent<Level>().levelType == LevelType.Island ? thirdPersonController : JetpackController;
        
        if (manualSpawn == false)
        {
            SpawnPlayer(controllerToSpawn);
        }
    }

    private void SpawnPlayer(GameObject player)
    {
        Instantiate(player, transform.position, quaternion.identity);
    }


    public void ManualSpawnPlayer()
    {
        SpawnPlayer(controllerToSpawn);
    }
}
