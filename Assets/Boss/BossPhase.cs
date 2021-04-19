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
    [Header("Projectiles per second")] public float fireRate;
    public float projectileSpeed;

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
            Debug.Log("During this phase, boss will shoot projectiles");

            FireProjectiles(_boss.playerTarget);
        }
        
    }


    public void FireProjectiles(Transform target)
    {
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
