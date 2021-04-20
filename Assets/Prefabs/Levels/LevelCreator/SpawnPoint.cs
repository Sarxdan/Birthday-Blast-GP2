using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject thirdPersonController;
    public GameObject JetpackController;

    private GameObject controllerToSpawn;

    private void Awake()
    {
        //Spawn ThirdPersonController on island, Jetpack otherwise
        controllerToSpawn = transform.parent.GetComponent<Level>().levelType == LevelType.Island ? thirdPersonController : JetpackController;
        
        SpawnPlayer(controllerToSpawn);
    }

    private void SpawnPlayer(GameObject player)
    {
        //Spawn player and apply the rotation of the SpawnPoint onto the player
        Instantiate(player, transform.position, transform.rotation);
    }
}
