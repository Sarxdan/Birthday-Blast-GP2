using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    Vector3 latestCheckPoint;
    float enableMovementTimer = 0.1f;
    ThirdPersonController thirdPersonController;
    Transform player;
    int playerhealth;
    // Start is called before the first frame update

    public void Setup()
    {   
        if(GameObject.FindGameObjectWithTag("Player") == null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        if(thirdPersonController == null) return;
        
        playerhealth = PlayerManager.instance.playerHealth;
        
        latestCheckPoint = player.localPosition;
    }

    void MovePlayerToCheckpoint()
    {
        if(player != null && thirdPersonController != null)
        {
            thirdPersonController.disablePlayerMovement = true;
            StartCoroutine(EnablePlayerAfterSeconds());        
            player.position = latestCheckPoint;
        }       
    }

    IEnumerator EnablePlayerAfterSeconds()
    {
        yield return new WaitForSeconds(enableMovementTimer);
        thirdPersonController.disablePlayerMovement = false;
    }
    private void UpdateLatestCheckpoint(Vector3 position)
    {
        latestCheckPoint = position;   
    }

    void OnPlayerHealthChange(int amount)
    {
        if(playerhealth < amount) return; //player got healed, dont move them
        if(amount == 0) return; //player death animation?
        MovePlayerToCheckpoint();
        playerhealth = amount;
    }

    private void OnEnable() {
        PlayerMovement.playerStuck += MovePlayerToCheckpoint;
        Checkpoint.onCheckPointTriggered += UpdateLatestCheckpoint;
        PlayerHealth.onPlayerHealthChange += OnPlayerHealthChange;
        Gamemanager.onSceneLoaded += Setup;
    }

    private void OnDisable() {
        PlayerMovement.playerStuck -= MovePlayerToCheckpoint;
        Checkpoint.onCheckPointTriggered -= UpdateLatestCheckpoint;
        PlayerHealth.onPlayerHealthChange -= OnPlayerHealthChange;
        Gamemanager.onSceneLoaded -= Setup;
    }

}
