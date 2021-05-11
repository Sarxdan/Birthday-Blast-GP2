using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{

    public float speedDuringSegment = 5.0f;

    public Transform StartPoint;
    public Transform EndPoint;

    [Header("Level width on randomizer will change to this width while player is flying on this segment")]
    [Range(1,100)]
    public float segmentWidth = 45f;

    public SegmentDifficulty segmentDifficulty;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            other.gameObject.GetComponentInChildren<JetPack>().autoMoveSpeed = speedDuringSegment;
            FindObjectOfType<LevelRandomizer>().SetCurrentSegment(this);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(StartPoint.position, 1f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(EndPoint.position, 1f);
        
        
        Gizmos.color = Color.white;
        Gizmos.DrawLine(StartPoint.position + Vector3.right * segmentWidth * 0.5f, StartPoint.position + Vector3.right * segmentWidth * 0.5f + Vector3.forward * DistFromStartToEnd());
        Gizmos.DrawLine(StartPoint.position + Vector3.left * segmentWidth * 0.5f, StartPoint.position + Vector3.left * segmentWidth * 0.5f + Vector3.forward * DistFromStartToEnd());
        
    }

    private float DistFromStartToEnd()
    {
        return Vector3.Distance(StartPoint.position, EndPoint.position);
    }
}

public enum SegmentDifficulty
{
    Easy,
    Medium,
    Hard
}
