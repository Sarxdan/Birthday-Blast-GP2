using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : Singleton<Gamemanager>
{
    public static Events.GameStateEvent onGameStateChange;
    public static Events.EmptyEvent onSceneLoaded;
    public enum GameState
    {
        Pregame,
        Playing,
        Paused
    }
    public KeyItems.Items UnlockedItems
    {
        get{return unlockedItems;}
    }
    public GameState CurrentGameState
    {
        get{return currentGameState;}
    }
    [SerializeField] KeyItems.Items unlockedItems;
    GameState currentGameState = GameState.Pregame;

    void LoadLevel(int levelIndex)
    {       
        SceneManager.LoadScene(levelIndex);
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
        if(onGameStateChange != null)
        {
            onGameStateChange(currentGameState);
        }
    }

    void OnPlayerDeath()
    {
        StartCoroutine(PlayerDeath());       
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForEndOfFrame();
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {      
        if(onSceneLoaded != null)
        {
            onSceneLoaded();
        }
    }

    void OnKeyItemPickUp(KeyItems.Items item)
    {
        if(item.jetpack) 
        {       
            unlockedItems.jetpack = true;
        }
        if(item.pewpew) 
        {       
            unlockedItems.pewpew = true;
        }
        if(item.shovel) 
        {       
            unlockedItems.shovel = true;
        }
    }

    private void OnEnable() {
        UIManager.onGamePaused += UpdateGameState;
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
        SceneManager.sceneLoaded += SceneLoaded;
        KeyItemPickup.onKeyItemPickup += OnKeyItemPickUp;
    }
   
    protected override void OnDestroy() {
        base.OnDestroy();
        UIManager.onGamePaused -= UpdateGameState;
        PlayerHealth.onPlayerDeath -= OnPlayerDeath;
        SceneManager.sceneLoaded -= SceneLoaded;
        KeyItemPickup.onKeyItemPickup -= OnKeyItemPickUp;
    }
}
