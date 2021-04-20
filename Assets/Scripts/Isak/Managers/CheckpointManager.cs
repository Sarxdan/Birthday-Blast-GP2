using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public Vector3 latestCheckPoint;
    float enableMovementTimer = 0.1f;
    public ThirdPersonController thirdPersonController;
    public Transform player;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        print("setting up");
        yield return new WaitForSeconds(1);
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
    void OnSceneLoaded()
    {
        StartCoroutine(Setup());
    }

    private void OnEnable() {
        Checkpoint.onCheckPointTriggered += UpdateLatestCheckpoint;
        PlayerHealth.onPlayerHealthChange += OnPlayerHealthChange;
        Gamemanager.onSceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        Checkpoint.onCheckPointTriggered -= UpdateLatestCheckpoint;
        PlayerHealth.onPlayerHealthChange -= OnPlayerHealthChange;
        Gamemanager.onSceneLoaded -= OnSceneLoaded;
    }

}
