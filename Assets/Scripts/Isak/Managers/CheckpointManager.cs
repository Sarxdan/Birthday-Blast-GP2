using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public Vector3 latestCheckPoint;
    float enableMovementTimer = 0.1f;
    ThirdPersonController thirdPersonController;
    Transform player;
    // Start is called before the first frame update
    protected override void Awake() {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        latestCheckPoint = player.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MovePlayerToCheckpoint()
    {
        thirdPersonController.disablePlayerMovement = true;
        StartCoroutine(EnablePlayerAfterSeconds());        
        player.position = latestCheckPoint;
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
        MovePlayerToCheckpoint();
    }

    private void OnEnable() {
        Checkpoint.onCheckPointTriggered += UpdateLatestCheckpoint;
        PlayerHealth.onPlayerHealthChange += OnPlayerHealthChange;
    }

    private void OnDisable() {
        Checkpoint.onCheckPointTriggered -= UpdateLatestCheckpoint;
        PlayerHealth.onPlayerHealthChange -= OnPlayerHealthChange;
    }

}
