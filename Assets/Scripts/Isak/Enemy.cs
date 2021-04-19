using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle
{
    [SerializeField] int health = 1;
    [SerializeField] float despawnTimer = 1;
    bool isRunningAway = false;

    protected override void Update()
    {
        if(isRunningAway) return;
        base.Update();
    }
    public void FleeFromPlayer(Vector3 playerPosition)
    {
        body.velocity = playerPosition;
        isRunningAway = true;
        StartCoroutine(RunAway());
    }
    IEnumerator RunAway()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject); // use objectpooling instead?
    }
}
