using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy //hunt player when found one
{
    Transform player;
    bool isAttackingPlayer = false;
    [SerializeField] float chaseSpeed;

    // Start is called before the first frame update
    protected override void CheckForPlayer()
    {
        base.CheckForPlayer();
        if(checkedColliders.Length > 0)
        {
            player = checkedColliders[0].GetComponent<Transform>();
            audio = AudioManager.instance.PlayClipAtPoint("Bee", transform.position);
            isAttackingPlayer = true;
        }
        else if(checkedColliders.Length == 0)
        {
            if(audio != null)
            {
                audio.Stop();
            }            
        }
    }

    protected override void Update()
    {
        if(isRunningAway) return;
        if(!isAttackingPlayer)
        {
            base.Update();
        }
        else
        {
            AttackPlayer();
        }
        
    }

    void AttackPlayer()
    {
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) >= 1)
        { 
            transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        }
 
    }
}
