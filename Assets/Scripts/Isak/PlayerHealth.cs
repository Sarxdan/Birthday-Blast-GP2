using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public static Events.DamagePlayerEvent onPlayerHealthChange;
    public static Events.EmptyEvent onPlayerDeath;
    [SerializeField][Tooltip("How long the player is invulnerable after taking damage")] float invulnerableTime = 1;
    public bool invulnerable = false;

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
        float timer = invulnerableTime;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();      
        invulnerable = true;
        while(timer > 0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            foreach(Renderer renderer in renderers) // make this work
            {
                float timerInPercent = timer/invulnerableTime;
                Color newColor = renderer.material.color; 
                newColor.a = timerInPercent;
                renderer.material.color = newColor;
            }
        }    
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
