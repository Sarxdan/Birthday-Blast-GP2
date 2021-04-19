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
        var spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        spawnedBoss.StartBoss();
        bossTriggered = true;
    }
}
