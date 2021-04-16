using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int startingLevel = 0;
    
    private void Awake()
    {
        var levelID = PlayerPrefs.GetInt("TransitionLevelID",startingLevel);

        LoadLevel(GetLevel(levelID));
    }
    
    
    private void LoadLevel(Level level)
    {
        if (level == null)
        {
            LevelNotFound();
        }
        else
        {

            var levelGameObject = level.gameObject;
            Instantiate(levelGameObject);
        }
    }

    private void LevelNotFound()
    {
        Debug.LogError("Level to load is not valid, did you insert a non valid Level ID in previous transition?");
    }

    private Level GetLevel(int id)
    {
        var LevelPrefabs = Resources.LoadAll("LevelPrefabs", typeof(Level));
        foreach (var levelPrefab in LevelPrefabs)
        {
            var level = levelPrefab.GetComponent<Level>();

            if (level != null)
            {
                if (level.levelID == id)
                    return level;
            }
        }

        return null;
    }
}
