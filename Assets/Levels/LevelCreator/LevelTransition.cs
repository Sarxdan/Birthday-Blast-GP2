using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelTransition : MonoBehaviour
{
    public static Events.TransitionEvent onLevelTransit;
    [Header("Insert LVL ID of the level you want to transition TO")]
    public int nextLevel;

    public void LoadScene()
    {
        PlayerPrefs.SetInt("TransitionLevelID",nextLevel);
        var scene = SceneManager.GetActiveScene();
        if(onLevelTransit != null)
        {
            onLevelTransit(scene.buildIndex);
        }
        //SceneManager.LoadScene(scene.buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }
}
