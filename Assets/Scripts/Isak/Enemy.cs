using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle
{
    AudioSource audio;
    enum EnemyTypes
    {
        Bee,
        Bird
    }
    [SerializeField] float despawnTimer = 1;
    [SerializeField] EnemyTypes enemyType;
    bool isRunningAway = false;
    bool isPlayingAudio;

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
    void CheckForPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, 10, LayerMask.GetMask("Player"));
        if(colliders.Length > 0 && !isPlayingAudio)
        {
            isPlayingAudio = true; // needs a better fix
            switch(enemyType)
            {
                case EnemyTypes.Bee:
                audio = AudioManager.instance.PlayClipAtPoint("Bee", transform.position);
                break;

                case EnemyTypes.Bird:
                audio = AudioManager.instance.PlayClipAtPoint("Bird", transform.position);
                break;

                default:
                break;
            }
            
        }
        else if(colliders.Length == 0 && isPlayingAudio)
        {
            isPlayingAudio = false;
            if(audio != null)
            {
                audio.Stop();
            }            
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 10);
    }
    IEnumerator RunAway()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject); // use objectpooling instead?
    }
}
