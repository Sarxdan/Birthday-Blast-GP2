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
            Debug.Log("Player crossed segment divider");
            var jetpackController =
                other.gameObject.GetComponentInChildren<JetPack>().autoMoveSpeed = speedDuringSegment;
        }
    }
}

public enum SegmentDifficulty
{
    Easy,
    Medium,
    Hard
}
