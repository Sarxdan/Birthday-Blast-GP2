using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossPhase[] bossPhases;

    private int currentSphase = 0;
    
    
    private float currPhaseTimer = 0.0f;
    private bool inPhase;

    [HideInInspector] public Transform playerTarget;
    public Transform projectileSpawnPoint;

    public GameObject projectilePrefab;

    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
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

    public void ShootProjectile(Vector3 direction)
    {
        var newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        var projectileScript = newProjectile.GetComponent<Projectile>();
        
        projectileScript.speed = bossPhases[currentSphase].projectileSpeed;
    }

    #endregion
}
