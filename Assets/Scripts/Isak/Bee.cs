using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy //hunt player when found one
{
    Transform player;
    Animator animator;
    bool isAttackingPlayer = false;
    [SerializeField] float chaseSpeed;

    // Start is called before the first frame update
    override protected void Awake() {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }
    protected override void CheckForPlayer()
    {
        base.CheckForPlayer();
        if(checkedColliders.Length > 0)
        {
            player = checkedColliders[0].GetComponent<Transform>();
            audio = AudioManager.instance.PlayClipAtPoint("Bee", transform.position);
            isAttackingPlayer = true;
            animator.SetBool("Fly Forward", true);
        }
        else if(checkedColliders.Length == 0)
        {
            isAttackingPlayer = false;
            animator.SetBool("Fly Forward", false);
            print(audio);
            if(audio != null)
            {
                audio.Stop();
            }          
        }
    }

    protected override void Update()
    {
        if(isRunningAway) return;        
            base.Update();
            AttackPlayer();
                   
    }
    protected override void MoveObject()
    {
        if(!isAttackingPlayer)
        base.MoveObject();
    }

    void AttackPlayer()
    {
        if(!isAttackingPlayer) return;
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) >= 1)
        { 
            transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        }
 
    }
}
