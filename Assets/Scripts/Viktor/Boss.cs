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
    
    
    //Center position
    public Vector3 bossCenterPos;
    public Vector3 newBossPosition;

    public float distanceFromPlayer = 90f;
    

    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartBoss()
    {
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

        newBossPosition.z = playerTarget.position.z + distanceFromPlayer;
        transform.position = newBossPosition;
    }



    #region BossMechanics

    public void ShootProjectile()
    {
        #region Select Projectile
        
        var prefab = projectilePrefab;
        if (bossPhases[currentSphase].tripleProjectile)
        {
            prefab = tripleProjectilePrefab;
        }
        
        #endregion

        #region Spawn Projectile(s)

        var newProjectile = Instantiate(prefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        var projectileScripts = newProjectile.GetComponentsInChildren<Projectile>();

        #endregion

        #region Populate Projectile Variables/Fields
        
        foreach (var projectileScript in projectileScripts)
        {
            //Set speed of projectile
            projectileScript.speed = bossPhases[currentSphase].projectileSpeed;

            //Projectile target
            var jetpackVelocityVector = playerTarget.GetComponent<Rigidbody>().velocity;
            var rawDirToPlayer = (playerTarget.position - projectileSpawnPoint.position) *
                                     Vector3.Distance(playerTarget.position, projectileSpawnPoint.position);
            
            var predictedPlayerDirection = rawDirToPlayer + jetpackVelocityVector;
            
            projectileScript.target = playerTarget;
            projectileScript.moveDir = predictedPlayerDirection;
            
            //Set homing variables
            projectileScript.isHoming = bossPhases[currentSphase].homingProjectiles;
            projectileScript.homingAccuracy = bossPhases[currentSphase].homingAccuracy;
        }

        #endregion
    }
    
    
    
    
    public void MoveHorizontally(float speed)
    {
        newBossPosition = bossCenterPos;
        newBossPosition.x += bossPhases[currentSphase].moveAmount *
                         Mathf.Sin(Time.time * speed);

        transform.position = newBossPosition;

    }

    #endregion
}
