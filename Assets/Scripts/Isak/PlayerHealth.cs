using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public static Events.DamagePlayerEvent onPlayerHealthChange;
    public static Events.EmptyEvent onPlayerDeath;

    // Start is called before the first frame update
    public override void TakeDamage(int damage)
    {       
        base.TakeDamage(damage);
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(health);
        }
    }
    protected override void Death()
    {
        base.Death();
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
    }

    private void OnDisable() {
        DamagePlayer.onPlayerCollision -= TakeDamage;
    }
}
