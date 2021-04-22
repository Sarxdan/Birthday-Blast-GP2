using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : Singleton<Gamemanager>
{
    public static Events.EmptyEvent onSceneLoaded;
    public enum GameState
    {
        Pregame,
        Playing,
        Paused
    }
    public enum PlayerStates
    {
        OnJetpack,
        Onland
    }
    public PlayerStates CurrentPlayerState
    {
        get{return currentPlayerState;}
    }
    public KeyItems.Items unlockedItems;
    [SerializeField] bool DebugMode = false;
    GameState currentGameState = GameState.Pregame;
    [SerializeField]PlayerStates currentPlayerState = PlayerStates.OnJetpack;
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

    void UpdatePlayerState(PlayerStates newState) // send events to ui so correct ui for flight/land is used
    {
        currentPlayerState = newState;
        switch(currentPlayerState)
        {
            case PlayerStates.OnJetpack:
            break;
            case PlayerStates.Onland:
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
        StartCoroutine(PlayerDeath());       
    }

    IEnumerator PlayerDeath()
    {
        switch(currentPlayerState)
        {
            case PlayerStates.OnJetpack:
            yield return new WaitForSeconds(3);
            break;
            case PlayerStates.Onland:
            yield return new WaitForEndOfFrame();
            break;
        }
        
        LoadLevel(SceneManager.GetActiveScene().name);
    }

    void OnTransition(string level, PlayerStates newState)
    {
        UpdatePlayerState(newState);
        LoadLevel(level);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(onSceneLoaded != null)
        {
            onSceneLoaded();
        }
    }

    private void OnEnable() {
        Transition.onTransition += OnTransition;
        UIManager.onGamePaused += UpdateGameState;
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
        SceneManager.sceneLoaded += SceneLoaded;
    }
   
    protected override void OnDestroy() {
        base.OnDestroy();
        Transition.onTransition -= OnTransition;
        UIManager.onGamePaused -= UpdateGameState;
        PlayerHealth.onPlayerDeath -= OnPlayerDeath;
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}
