using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelRandomizer : MonoBehaviour
{
    [Header("The ID to transition to at the end of THIS level")]
    public int nextLevelToLoad;
    
    public LevelSegment StartSegmentPrefab;
    public LevelSegment EndSegmentPrefab;

    private LevelSegment spawnedStartSegment, spawnedEndSegment;

    [Space]
    
    [Header("Amount of segments per difficulty to generate")]
    public int easySegmentsCount;
    public int mediumSegmentsCount;
    public int hardSegmentsCount;
    
    [Space]
    
    private int currentSegmentIndex = -1;
    private LevelSegment latestSegmentSpawned;


    private Transform playerTransform;

    //Tracking progress
    [Header("Percentage of level completed")]
    public float levelProgressionPercentage = 0.0f;
    private float levelProgression = 0.0f;
    private float deltaZ;
    private float distFromStartToEnd;


    private void Awake()
    {
        GenerateLevel(easySegmentsCount, mediumSegmentsCount, hardSegmentsCount);
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        FindObjectOfType<LevelTransition>().nextLevel = nextLevelToLoad;
    }

    private void Update()
    {
        #region LevelProgress


        if (playerTransform.position.z > levelProgression)
        {
            levelProgression = playerTransform.position.z;
            levelProgressionPercentage = (levelProgression / distFromStartToEnd) * 100;
            levelProgressionPercentage = Mathf.Clamp(levelProgressionPercentage, 0, 100);
        }
        
        


        deltaZ = playerTransform.position.z;

        #endregion
    }


    private void GenerateLevel(int easyCount, int mediumCount, int hardCount)
    {
        //Reset index, first SpawnSegment call will be index 0
        currentSegmentIndex = -1;
        
        //Spawn StartSegment
        SpawnSegment(StartSegmentPrefab);


        var shuffledEasyArray = ShuffledArray(FetchSegmentInResources(SegmentDifficulty.Easy));
        //Spawn Easy Segments
        for (var i = 0; i < easyCount; i++)
        {
            if (i < shuffledEasyArray.Length)
            {
                SpawnSegment(shuffledEasyArray[i]);
            }
            else
            {
                SpawnSegment(shuffledEasyArray[Random.Range(0,shuffledEasyArray.Length)]);
            }
        }
        
        var shuffledMediumArray = ShuffledArray(FetchSegmentInResources(SegmentDifficulty.Medium));
        //Spawn Medium Segments
        for (var i = 0; i < mediumCount; i++)
        {
            if (i < shuffledMediumArray.Length)
            {
                SpawnSegment(shuffledMediumArray[i]);
            }
            else
            {
                SpawnSegment(shuffledMediumArray[Random.Range(0,shuffledMediumArray.Length)]);
            }
        }
        
        var shuffledHardArray = ShuffledArray(FetchSegmentInResources(SegmentDifficulty.Hard));
        //Spawn Medium Segments
        for (var i = 0; i < hardCount; i++)
        {
            if (i < shuffledHardArray.Length)
            {
                SpawnSegment(shuffledHardArray[i]);
            }
            else
            {
                Debug.Log("Higher count than database");
                SpawnSegment(shuffledHardArray[Random.Range(0,shuffledHardArray.Length)]);
            }
        }
        //Spawn EndSegment
        SpawnSegment(EndSegmentPrefab);
        
        
        distFromStartToEnd = Vector3.Distance(spawnedStartSegment.StartPoint.position, spawnedEndSegment.EndPoint.position);
    }

    private void SpawnSegment(LevelSegment segment)
    {

        var spawnPoint = transform.position +
                         transform.forward * Vector3.Distance(segment.StartPoint.localPosition, segment.transform.localPosition);
        
        if (latestSegmentSpawned != null)
        {
            spawnPoint = latestSegmentSpawned.EndPoint.position +
                         transform.forward * Vector3.Distance(segment.transform.localPosition, segment.StartPoint.localPosition);
        }

        
        
        latestSegmentSpawned = Instantiate(segment, spawnPoint, Quaternion.identity);
        
        
        
        if (segment == StartSegmentPrefab)
        {
            spawnedStartSegment = latestSegmentSpawned;
        }
        else if (segment == EndSegmentPrefab)
        {
            spawnedEndSegment = latestSegmentSpawned;
        }
        
    }


    private LevelSegment[] FetchSegmentInResources(SegmentDifficulty difficulty)
    {
        LevelSegment[] levelSegmentArray;
        
        switch (difficulty)
        {
            case SegmentDifficulty.Easy:
                levelSegmentArray = Resources.LoadAll<LevelSegment>("JetpackSegments/Easy");
                break;
            case SegmentDifficulty.Medium:
                levelSegmentArray = Resources.LoadAll<LevelSegment>("JetpackSegments/Medium");
                break;
            case SegmentDifficulty.Hard:
                levelSegmentArray = Resources.LoadAll<LevelSegment>("JetpackSegments/Hard");
                break;
            default:
                //Default is medium
                levelSegmentArray = Resources.LoadAll<LevelSegment>("JetpackSegments/Medium");
                break;
        }

        
        return levelSegmentArray;
    }


    private LevelSegment[] ShuffledArray(LevelSegment[] array)
    {
        for (var i = 0; i < array.Length; i++) {
            var temp = array[i];
            var randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        return array;
    }
}
