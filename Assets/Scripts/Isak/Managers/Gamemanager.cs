using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : Singleton<Gamemanager>
{
    [System.Serializable]
    public struct KeyItems // make this struct not public?
    {
        public bool jetpack;
        public bool pewpew;
    }

    enum GameState
    {
        Pregame,
        Playing,
        Paused
    }

    public KeyItems unlockedItems;
    [SerializeField] bool DebugMode = false;
    GameState currentGameState = GameState.Pregame;
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

    void UpdateGameState(GameState newState)
    {
        currentGameState = newState;
        switch(currentGameState)
        {
            case GameState.Pregame:
            Time.timeScale = 1;
            break;
            case GameState.Playing:
            Time.timeScale = 1;
            break;
            case GameState.Paused:
            Time.timeScale = 0;
            break;
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
