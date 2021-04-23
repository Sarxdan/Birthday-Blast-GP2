using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public static Events.GameStateEvent onGamePaused;
    public static Events.DialogueEvent onNPCDialogue;
    public static Events.DamagePlayerEvent onPlayerHealthChange;
    public static Events.FuelEvent onFuelUse;
    public static Events.FuelEvent onJetpackAwake;
    InGameUI inGameUI;
    PauseMenu pauseMenu;
    
    protected override void Start()
    {
        base.Start();
        inGameUI = GetComponentInChildren<InGameUI>();
        pauseMenu = GetComponentInChildren<PauseMenu>();
        TogglePauseMenu();
    }

    void TogglePauseMenu()
    {
        bool toggle = !pauseMenu.gameObject.activeSelf;
        pauseMenu.gameObject.SetActive(toggle);
        if(toggle)
        {
            Cursor.lockState = CursorLockMode.None;
            if(onGamePaused != null)
            {
                onGamePaused(Gamemanager.GameState.Paused);
                Cursor.visible = true;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            if(onGamePaused != null)
            {
                onGamePaused(Gamemanager.GameState.Playing);
                Cursor.visible = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu(); // använd viktors input!!!
        }
    }
    void OnNPCDialogue(string dialogue, string name)
    {
        if(onNPCDialogue != null)
        {
            onNPCDialogue(dialogue, name);
        }
    }

    private void OnPlayerHealthChange(int amount)
    {
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(amount);
        }
    }

    private void OnFuelUse(float amount)
    {
        if(onFuelUse != null)
        {
            onFuelUse(amount);
        }
    }

    private void OnJetpackAwake(float amount)
    {
        if(onJetpackAwake != null)
        {
            onJetpackAwake(amount);
        }
    }

    private void OnEnable() {
        DialogueTrigger.onNPCDialogue += OnNPCDialogue;
        PauseMenu.onResumeClicked += TogglePauseMenu;
        PlayerHealth.onPlayerHealthChange += OnPlayerHealthChange;
        JetPack.onFuelUse += OnFuelUse;
        JetPack.onJetpackAwake += OnJetpackAwake;
        Fuel.onFuelUse += OnFuelUse;
        Fuel.onJetpackAwake += OnJetpackAwake;
    }

    private void OnDisable() {
        DialogueTrigger.onNPCDialogue -= OnNPCDialogue; 
        PauseMenu.onResumeClicked -= TogglePauseMenu;
        PlayerHealth.onPlayerHealthChange -= OnPlayerHealthChange;
        JetPack.onFuelUse -= OnFuelUse;
        JetPack.onJetpackAwake -= OnJetpackAwake;
        Fuel.onFuelUse -= OnFuelUse;
        Fuel.onJetpackAwake -= OnJetpackAwake;
    }  
}
