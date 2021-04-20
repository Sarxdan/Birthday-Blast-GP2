using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float spawnTime = 5f;
    private bool isSpawning;
    [HideInInspector] public Vector3 destinationSpawnPoint;
    
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
    private Vector3 farLeftPos;
    private Vector3 farRightPos;

    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        farLeftPos = destinationSpawnPoint + -transform.right * 7.5f;
        farRightPos = destinationSpawnPoint + transform.right * 7.5f;

        StartBoss();
    }

    public void StartBoss()
    {
        StartCoroutine(OnBossSpawn());
    }

    private IEnumerator OnBossSpawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnTime);
        isSpawning = false;
        inPhase = true;
    }

    public void EndBoss()
    {
        Destroy(gameObject);
    }

    private void NextPhase()
    {
        currentSphase++;
        inPhase = true;
    }

    private IEnumerator EndPhase()
    {
        inPhase = false;
        currPhaseTimer = 0.0f;

        if (currentSphase == bossPhases.Length - 1)
        {
            //Do something here at end of boss
            //Ex: fly away?
        }
        
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
        else if (isSpawning)
        {
            transform.position = Vector3.Lerp(transform.position, destinationSpawnPoint, Time.deltaTime);
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
            projectileScript.origin = projectileSpawnPoint.position;
            projectileScript.maxRangeAllowed =
                Vector3.Distance(projectileSpawnPoint.position, playerTarget.position);
        }
    }
    
    
    public void MoveHorizontally(float speed)
    {
        Vector3 pos = destinationSpawnPoint;
        pos.x += bossPhases[currentSphase].moveAmount *
                         Mathf.Sin(Time.time * speed);

        transform.position = pos;

    }

    #endregion
}
