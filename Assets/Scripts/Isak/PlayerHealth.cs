using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public static Events.DamagePlayerEvent onPlayerHealthChange;
    public static Events.EmptyEvent onPlayerDeath;
    [SerializeField][Tooltip("How long the player is invulnerable after taking damage")] float invulnerableTime = 1;
    bool invulnerable = false;

    public override void TakeDamage(int damage)
    {       
        if(invulnerable) return;
        base.TakeDamage(damage);
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(health);
        }
        StartCoroutine(Invulnerable());
    }
    
    IEnumerator Invulnerable()
    {       
        Animator animator = GetComponent<Animator>();
        float timer = invulnerableTime;   
        invulnerable = true;
        animator.SetBool("IsDamaged", true);
        while(timer > 0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }   
        animator.SetBool("IsDamaged", false);
        invulnerable = false;
    }
    protected override void Death()
    {
        if(onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }

    private void Start() {
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
