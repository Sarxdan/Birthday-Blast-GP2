using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && OtherBossActive() == false)
        {
            boss.SetActive(true);
        }
    }


    private bool OtherBossActive()
    {
        foreach (var bossInLevel in AllBossesInLevel())
        {
            if (bossInLevel == this.boss.GetComponent<BossBehaviour>())
            {
                //This boss, do nothing
            }
            else
            {
                //Found other boss
                return bossInLevel.gameObject.activeSelf;
            }
        }

        return false;
    }
    
    private BossBehaviour[] AllBossesInLevel()
    {
        return FindObjectsOfType<BossBehaviour>();
    }
}
