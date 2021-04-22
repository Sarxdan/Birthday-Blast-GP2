using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Boss bossPrefab;
    public Transform bossSpawnPoint;
    
    private bool bossTriggered; //Turn true once boss has been triggered


    private Boss spawnedBoss;
    private bool bossIsSpawning;

    public float bossSpawnSpeed = 1f;

    private void Start()
    {
        bossTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || bossTriggered) return;
        Debug.Log("Start boss");
        StartBoss();
    }


    private void Update()
    {
        if (bossIsSpawning)
        {
            var distFromSpawnPoint = Vector3.Distance(spawnedBoss.transform.position, bossSpawnPoint.position);
            spawnedBoss.transform.position = Vector3.Lerp(spawnedBoss.transform.position, bossSpawnPoint.position,
                Time.deltaTime * bossSpawnSpeed);
            
            if (distFromSpawnPoint < 1)
            {
                bossIsSpawning = false;
                StartBoss();
            }
        }
    }


    private void SpawnBoss()
    {
        spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        bossIsSpawning = true;
        
        bossTriggered = true;
    }


    private void StartBoss()
    {
        spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        spawnedBoss.StartBoss();
    }
}
