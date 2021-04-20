using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //Phases
    public BossPhase[] bossPhases;

    private int currentSphase = 0;
    private float currPhaseTimer = 0.0f;
    private bool inPhase;

    //Player
    [HideInInspector] public Transform playerTarget;
    
    //Projectiles
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public GameObject tripleProjectilePrefab;

    //Horizontal Movement
    [HideInInspector] public Vector3 originalSpawnPoint;
    private Vector3 farLeftPos;
    private Vector3 farRightPos;

    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        originalSpawnPoint = transform.position;
        farLeftPos = originalSpawnPoint + -transform.right * 7.5f;
        farRightPos = originalSpawnPoint + transform.right * 7.5f;

        StartBoss();
    }

    public void StartBoss() 
    {
        Debug.Log("Boss has begun!");
        inPhase = true;
    }

    public void EndBoss()
    {
        Debug.Log("Boss is over");
        Destroy(gameObject);
    }

    private void NextPhase()
    {
        currentSphase++;
        inPhase = true;
        Debug.Log("Started next phase!");
    }

    private IEnumerator EndPhase()
    {
        Debug.Log("End of phase : " + bossPhases[currentSphase]);
        inPhase = false;
        currPhaseTimer = 0.0f;

        Debug.Log("Time until next phase : " + bossPhases[currentSphase].phaseDowntime);
        yield return new WaitForSeconds(bossPhases[currentSphase].phaseDowntime);
        if (currentSphase == bossPhases.Length - 1)
        {
            EndBoss();
        }
        else
        {
            NextPhase();
        }
    }
    
    private void Update()
    {
        if (inPhase)
        { 
            bossPhases[currentSphase].DoPhaseMechanics(this);
            currPhaseTimer += Time.deltaTime;
            if (currPhaseTimer >= bossPhases[currentSphase].phaseDuration)
            {
                StartCoroutine(EndPhase());
            }
        }   
    }



    #region BossMechanics

    public void ShootProjectile()
    {
        var prefab = projectilePrefab;
        if (bossPhases[currentSphase].tripleProjectile)
        {
            prefab = tripleProjectilePrefab;
        }

        var newProjectile = Instantiate(prefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        var projectileScripts = newProjectile.GetComponentsInChildren<Projectile>();
        foreach (var projectileScript in projectileScripts)
        {
            projectileScript.speed = bossPhases[currentSphase].projectileSpeed;

            projectileScript.target = playerTarget;
            projectileScript.isHoming = bossPhases[currentSphase].homingProjectiles;
            projectileScript.homingAccuracy = bossPhases[currentSphase].homingAccuracy;
        }
    }
    
    
    public void MoveHorizontally(float speed)
    {
        Vector3 pos = originalSpawnPoint;
        pos.x += bossPhases[currentSphase].moveAmount *
                         Mathf.Sin(Time.time * speed);

        transform.position = pos;

    }

    #endregion
}
