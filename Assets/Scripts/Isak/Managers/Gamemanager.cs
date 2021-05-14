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
        Paused,
        CutScene
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
    public GameState currentGameState = GameState.Pregame;

    public void LoadLevel(int levelIndex)
    {       
        SceneManager.LoadScene(levelIndex);       
    }

    public void ResetUnlockedItems()
    {
        unlockedItems.jetpack = false;
        unlockedItems.pewpew = false;
        unlockedItems.shovel = false;
    }

    public void UpdateGameState(GameState newState)
    {
        GameState lastState = currentGameState;
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

            case GameState.CutScene:
            Time.timeScale = 1;
            break;
        }
        if(onGameStateChange != null)
        {
            onGameStateChange(currentGameState, lastState);
        }
    }

    void OnPlayerDeath()
    {
        LoadLevel(6);     
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForEndOfFrame();
        LoadLevel(6);
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
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
        SceneManager.sceneLoaded += SceneLoaded;
        KeyItemPickup.onKeyItemPickup += OnKeyItemPickUp;
    }
   
    protected override void OnDestroy() {
        base.OnDestroy();
        PlayerHealth.onPlayerDeath -= OnPlayerDeath;
        SceneManager.sceneLoaded -= SceneLoaded;
        KeyItemPickup.onKeyItemPickup -= OnKeyItemPickUp;
    }
}
