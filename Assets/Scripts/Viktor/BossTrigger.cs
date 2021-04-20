using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Boss bossPrefab;
    public Transform bossSpawnPoint;
    
    private bool bossTriggered; //Turn true once boss has been triggered

    private void Start()
    {
        bossTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || bossTriggered != false) return;
        
        SpawnBoss();
    }


    private void SpawnBoss()
    {
        var spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position + new Vector3(0,-50,0), bossSpawnPoint.rotation);
        spawnedBoss.destinationSpawnPoint = bossSpawnPoint.position;
        spawnedBoss.StartBoss();
        bossTriggered = true;
    }
}
