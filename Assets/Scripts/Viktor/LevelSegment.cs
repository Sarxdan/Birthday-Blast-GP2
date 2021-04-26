using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{

    public float speedDuringSegment = 5.0f;

    public Transform StartPoint;
    public Transform EndPoint;

    public SegmentDifficulty segmentDifficulty;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            other.gameObject.GetComponentInChildren<JetPack>().autoMoveSpeed = speedDuringSegment;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(StartPoint.position, 1f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(EndPoint.position, 1f);
    }
}

public enum SegmentDifficulty
{
    Easy,
    Medium,
    Hard
}
