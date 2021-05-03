using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Obstacle
{
    protected AudioSource audio;
    [SerializeField] float despawnTimer = 1;
    protected bool isRunningAway = false;
    protected Collider[] checkedColliders;
    [SerializeField] protected float checkRange;

    protected override void Update()
    {
        if(isRunningAway) return;
        base.Update();
        CheckForPlayer();
    }
    public void FleeFromPlayer(Vector3 playerPosition)
    {
        body.velocity = playerPosition;
        isRunningAway = true;
        StartCoroutine(RunAway());
        
    }
    protected virtual void CheckForPlayer()
    {
        checkedColliders = Physics.OverlapSphere(transform.position, checkRange, LayerMask.GetMask("Player"));        
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, checkRange);
    }
    IEnumerator RunAway()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject); // use objectpooling instead?
    }
}
