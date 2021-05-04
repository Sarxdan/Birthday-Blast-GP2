using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossPhase
{
    [Header("Boss Phase Information")] 
    public float phaseDuration;
    public float phaseDowntime;
    
    [Header("Boss Projectiles")] 
    public GameObject projectilePrefab;
    public float projectileFireRate;
    public float projectileSpeed;
    public int projectileDamage;

    [Header("Boss Horizontal Movement")]
    public float moveAmount;
    public float moveSpeed;

}
