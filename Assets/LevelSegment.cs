using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public SegmentDifficulty segmentDifficulty;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player crossed segment divider");
        }
    }
}

public enum SegmentDifficulty
{
    Easy,
    Medium,
    Hard
}
