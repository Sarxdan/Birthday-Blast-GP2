using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public static Events.EmptyEvent onPlayerDamaged;

    // Start is called before the first frame update
    public override void TakeDamage(int damage)
    {
        if(onPlayerDamaged != null)
        {
            onPlayerDamaged();
        }
        base.TakeDamage(damage);
        print(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        DamagePlayer.onPlayerCollision += TakeDamage;
    }

    private void OnDisable() {
        DamagePlayer.onPlayerCollision -= TakeDamage;
    }
}
