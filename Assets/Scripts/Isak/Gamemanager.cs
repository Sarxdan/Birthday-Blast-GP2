using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : Singleton<Gamemanager>
{
    [SerializeField] bool DebugMode = false;

    void LoadLevel(string levelName)
    {
        if(DebugMode)
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        }        
    }

    private void OnEnable() {
        Transition.onTransitionEvent += LoadLevel;
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        Transition.onTransitionEvent -= LoadLevel;
    }
}
