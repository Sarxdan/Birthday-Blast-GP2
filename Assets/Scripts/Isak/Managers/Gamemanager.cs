using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : Singleton<Gamemanager>
{

    public enum GameState
    {
        Pregame,
        Playing,
        Paused
    }
    enum PlayerState
    {
        OnJetpack,
        Onland
    }
    public KeyItems.Items unlockedItems;
    [SerializeField] bool DebugMode = false;
    GameState currentGameState = GameState.Pregame;
    PlayerState currentPlayerState = PlayerState.OnJetpack;
    void LoadLevel(string levelName)
    {
        
        SceneManager.LoadScene(levelName);
        
        /*  Old code
        if(DebugMode)
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        }        
        */
    }

    void UpdatePlayerState(PlayerState newState) // send events to ui so correct ui for flight/land is used
    {
        currentPlayerState = newState;
        switch(currentPlayerState)
        {
            case PlayerState.OnJetpack:
            break;
            case PlayerState.Onland:
            break;
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

    void OnPlayerDeath()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }

    private void OnEnable() {
        Transition.onTransitionEvent += LoadLevel;
        JetPack.onPlayerDeath += OnPlayerDeath;
        UIManager.onGamePaused += UpdateGameState;
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        Transition.onTransitionEvent -= LoadLevel;
        JetPack.onPlayerDeath += OnPlayerDeath;
        UIManager.onGamePaused -= UpdateGameState;
    }
}
