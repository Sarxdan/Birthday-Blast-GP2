using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float spawnDuration;
    public float endDuration;
    public Transform projectileShootPosition;
    
    
    
    public bool inPhase;
    public bool inDowntime;
    private float phaseTime;
    private float phaseTimer;
    private float nextTimeToFire;


    public float distFromPlayer = 75.0f;
    private Transform playerTransform;
    private Vector3 bossPosition;
    
    //Phases array
    public BossPhase[] phases;
    private int bossPhaseIndex = -1;
    
    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartBoss();
    }

    private void Update()
    {
        if (inDowntime)
        {
            //Time between phases
        }
        

        //vvv In phase code vvv
        if (inPhase == false) return;

        phaseTimer += Time.deltaTime;
        
        if (phaseTimer >= phaseTime)
        {
            EndPhase();
            return;
        }


        MoveHorizontal(CurrentPhase().moveAmount, CurrentPhase().moveSpeed);
        
        if (Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1 / CurrentPhase().projectileFireRate);
            ShootProjectile(CurrentPhase().projectilePrefab, CurrentPhase().projectileSpeed);
        }
    }

    private void LateUpdate()
    {
        bossPosition.z = playerTransform.position.z + distFromPlayer;
        transform.position = bossPosition;
    }

    #region Phase Management
    
    private void StartBoss()
    {
        Debug.Log("Boss Started");
        //Play Animation?

        StartCoroutine(BossSpawn());
    }
    
    
    private void StartPhase()
    {
        //Increment current phase
        bossPhaseIndex++;
        //Reset phase timer
        ResetPhaseTimers(CurrentPhase());
        inPhase = true;
        
        
        Debug.Log("Started Phase " + bossPhaseIndex);
    }
    
    private void EndPhase()
    {
        inPhase = false;
        ResetPhaseTimers(CurrentPhase());
        Debug.Log("Ended Phase " + bossPhaseIndex);
        
        if (bossPhaseIndex == phases.Length - 1)
        {
            //Just ended last phase
            StartCoroutine(EndOfBoss());
        }
        else
        {
            StartCoroutine(StartPhaseDowntime(CurrentPhase().phaseDowntime));
        }
        
    }
    
    private void EndBoss()
    {
        Debug.Log("End Of Boss");
        gameObject.SetActive(false);
    }


    private void ResetPhaseTimers(BossPhase bossPhase)
    {
        phaseTime = bossPhase.phaseDuration;
        phaseTimer = 0.0f;

        bossPosition.x = 0;
    }


    private IEnumerator StartPhaseDowntime(float time)
    {
        inDowntime = true;
        yield return new WaitForSeconds(time);
        inDowntime = false;
        StartPhase();
    }

    private IEnumerator BossSpawn()
    {
        yield return new WaitForSeconds(spawnDuration);
        StartPhase();
    }

    private IEnumerator EndOfBoss()
    {
        yield return new WaitForSeconds(endDuration);
        EndBoss();
    }

    private BossPhase CurrentPhase()
    {
        return phases[bossPhaseIndex];
    }
    
    #endregion

    #region Boss Mechanics

    private void ShootProjectile(GameObject projectilePrefab, float projectileSpeed)
    {
        var playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        var predictedPlayerPos = playerTarget.position +
                                 playerTarget.GetComponentInChildren<Rigidbody>().velocity;
        
        projectileShootPosition.transform.LookAt(predictedPlayerPos);
        
        var newProjectile = Instantiate(projectilePrefab, projectileShootPosition.position, projectileShootPosition.localRotation);

        var projectileScript = newProjectile.GetComponent<Projectile>();
        
        projectileScript.projectileSpeed = projectileSpeed;
        projectileScript.moveDirection =
            (predictedPlayerPos - projectileShootPosition.position).normalized;
        projectileScript.projectileDamage = CurrentPhase().projectileDamage;
    }


    private void MoveHorizontal(float amount, float speed)
    {
        bossPosition.x += Mathf.Sin(Time.time * speed) * amount;
    }

    #endregion
    
}
