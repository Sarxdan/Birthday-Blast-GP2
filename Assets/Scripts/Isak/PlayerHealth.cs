using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public static Events.DamagePlayerEvent onPlayerHealthChange;
    public static Events.EmptyEvent onPlayerDeath;
    [SerializeField][Tooltip("How long the player is invulnerable after taking damage")] float invulnerableTime = 1;
    bool invulnerable = false;

    public Animator damageAnimator;

    private void OnCollisionStay(Collision other)
    {
        damageAnimator = GameObject.FindGameObjectWithTag("CharModel").GetComponent<Animator>();
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        PlayerManager.instance.playerHealth = health;
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(health);
        }
    }
    public override void TakeDamage(int damage)
    {       
        if(invulnerable) return;
        PlayerManager.instance.playerHealth -= damage;
        base.TakeDamage(damage);
        if(health > 0)
        {
            AudioManager.instance.Play("PlayerDamaged");
        }        
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(health);
        }
        StartCoroutine(Invulnerable());
    }
    
    IEnumerator Invulnerable()
    {       
        
        float timer = invulnerableTime;   
        invulnerable = true;
        if (damageAnimator != null)
        {
            damageAnimator.SetBool("IsDamaged", true);
        }

        while(timer > 0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }

        if (damageAnimator != null)
        {
            damageAnimator.SetBool("IsDamaged", false);
        }

        invulnerable = false;
    }
    protected override IEnumerator Death()
    {
        yield return base.Death();
        AudioManager.instance.Play("PlayerDeath");
        yield return new WaitForEndOfFrame(); //death animation?
        PlayerManager.instance.ResetPlayerHealth();
        if(onPlayerDeath != null)
        {
            onPlayerDeath();
        }
        yield return new WaitForEndOfFrame();
    }

    private void Start() {
        PlayerManager.instance.PutOnModel();
        if(FindObjectOfType<CheckpointManager>() != null) CheckpointManager.instance.Setup();    
        OutOfBounds[] bounds = FindObjectsOfType<OutOfBounds>();   
        foreach(OutOfBounds bound in bounds)
        {
            bound.PlayerAwake();
        } 
        maxHealth = PlayerManager.instance.playerMaxHealth;        
        health = PlayerManager.instance.playerHealth;
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(health);
        }
    }

    private void OnEnable() {
        DamagePlayer.onPlayerCollision += TakeDamage;
        Projectile.onPlayerHit += TakeDamage;
    }

    private void OnDisable() {
        DamagePlayer.onPlayerCollision -= TakeDamage;
        Projectile.onPlayerHit -= TakeDamage;
    }
}
