using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;



public class LevelCreator : EditorWindow
{

    private string levelName = "New Level";
    private int levelID = 0;
    private LevelType levelType;
    private bool saveToPrefab = true;


    [MenuItem("LevelCreation/Level Creator Window")]
    private static void Init()
    {
        var levelCreator = (LevelCreator) EditorWindow.GetWindow(typeof(LevelCreator));
        levelCreator.Show();
    }


    private void OnGUI()
    {
        GUILayout.Label("Level Information", EditorStyles.boldLabel);
        levelName = EditorGUILayout.TextField("Level Name", levelName);
        levelID = EditorGUILayout.IntField("Unique Level ID", levelID);
        levelType = (LevelType)EditorGUILayout.EnumPopup("Level Type", levelType);
        saveToPrefab = EditorGUILayout.Toggle("Save Level As A Prefab", saveToPrefab);


        if (GUILayout.Button("Generate Level"))
        {
            if (ValidLevel())
            {
                GenerateLevel();
            }
            else
            {
                Debug.LogWarning("Something went wrong\nIs your level ID UNIQUE?");
                Debug.LogWarning("Make sure your Level ID is unique!");
            }
        }
    }

    private bool ValidLevel()
    {
        var savedLevelPrefabs = Resources.LoadAll("LevelPrefabs", typeof(Level));
        foreach (var savedLevelPrefab in savedLevelPrefabs)
        {
            var level = savedLevelPrefab.GetComponent<Level>();
            if (level == null) continue;
            if (level.levelID == levelID)
                return false;
        }

        return true;
    }
    
    private void GenerateLevel()
    {
        var levelObjectName = $"Level:|{levelName}|ID:{levelID}";
        var levelObject = new GameObject(levelObjectName);
        
        AddLevelComponent(levelObject);

        Debug.Log($"Generated Level {levelName} with ID {levelID}");

        if (saveToPrefab)
        {
            SavePrefab(levelObject);
        }
    }

    private void AddLevelComponent(GameObject levelObject)
    {
        var levelComponent= levelObject.AddComponent<Level>();
        PopulateLevelFields(levelComponent);
        GenerateSpawnPoint(levelComponent);
    }

    private void GenerateSpawnPoint(Level level)
    {
        var loadedSpawnPoint = Resources.Load("SpawnPointPrefab/PlayerSpawnPoint") as GameObject;
        
        var newSpawnPoint = Instantiate(loadedSpawnPoint,level.transform.position,Quaternion.identity,level.transform);

        newSpawnPoint.name = "Player SpawnPoint";
    }

    private void PopulateLevelFields(Level level)
    {
        level.levelName = levelName;
        level.levelID = levelID;
        level.levelType = levelType;
    }

    private void SavePrefab(GameObject levelObject)
    {
        //Save level prefab to assets
        string localPath = $"Assets/Levels/Resources/LevelPrefabs/{levelName}.prefab";
        levelObject = PrefabUtility.SaveAsPrefabAsset(levelObject,localPath);

        if (levelObject != null)
        {
            Debug.Log("Saved level prefab to : " + localPath);
        }
    }
 

    
}
