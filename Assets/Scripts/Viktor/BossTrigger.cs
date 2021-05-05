using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (FindObjectOfType<BossBehaviour>().gameObject.activeInHierarchy) return;
            boss.SetActive(true);
        }
    }
}
