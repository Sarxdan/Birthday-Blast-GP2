using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class BossPhase
{
    public string name;

    public float phaseDuration = 10f;
    
    [Header("Time between this phase and next phase")]
    public float phaseDowntime;

    [Header("Basic Projectiles")]
    public bool fireProjectiles;
    [Header("Projectiles per second")] 
    public float fireRate;
    public float projectileSpeed;
    
    //FirerateTimer
    private float nextTimeToFire = 0.0f;

    [Space] 
    
    [Header("Modify Projectiles")] 
    public bool explosiveProjectiles;
    public bool homingProjectiles;


    [Header("Movement Modifiers")] 
    public bool bossMovement;
    public float bossMovementSpeed;
    
    private Boss boss;

    public void DoPhaseMechanics(Boss _boss)
    {
        //Get reference to boss
        boss = _boss;
        Debug.Log("Phase : " + name + " is going on right now");

        if (fireProjectiles)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + (1 / fireRate);
                FireProjectiles(_boss.playerTarget);
            }
            Debug.Log("During this phase, boss will shoot projectiles");
        }
        
    }


    public void FireProjectiles(Transform target)
    {
        boss.projectileSpawnPoint.LookAt(target);
        var dirToPlayer = target.position - boss.projectileSpawnPoint.position;

        boss.ShootProjectile(dirToPlayer);
        
        if (explosiveProjectiles)
        {
            Debug.Log("Explosion on impact");
        }

        if (homingProjectiles)
        {
            Debug.Log("Homing projectiles");
        }
    }

    
}
