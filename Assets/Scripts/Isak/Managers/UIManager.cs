using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class UIManager : Singleton<UIManager>
{
    public static Events.GameStateEvent onGamePaused;
    public static Events.DialogueEvent onNPCDialogue;
    public static Events.DamagePlayerEvent onPlayerHealthChange;
    public static Events.FuelEvent onFuelUse;
    public static Events.FuelEvent onJetpackAwake;
    InGameUI inGameUI;
    PauseMenu pauseMenu;
    ShopUI shopUI;
    public GameObject journalPanel;

    public GameObject[] popupTabs;


    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    public void EnablePopUp(GameObject popup)
    {
        var goPopup = popup;
        
        foreach (var popupTab in popupTabs)
        {
            if (popup.name == popupTab.name)
            {
                goPopup = popupTab;
            }
        }
        goPopup.SetActive(true);
        FindObjectOfType<ThirdPersonController>().disableCameraController = true;
        FindObjectOfType<ThirdPersonController>().disablePlayerMovement = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePopUp(GameObject popup)
    {
        FindObjectOfType<ThirdPersonController>().disableCameraController = false;
        FindObjectOfType<ThirdPersonController>().disablePlayerMovement = false;
        popup.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    protected override void Start()
    {
        base.Start();
        inGameUI = GetComponentInChildren<InGameUI>();
        pauseMenu = GetComponentInChildren<PauseMenu>();
        shopUI = GetComponentInChildren<ShopUI>();
        ToggleInventoryUI();
        TogglePauseMenu();
        ToggleShopUI();
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

    public void ToggleShopUI() //change to event later?
    {
        bool toggle = !shopUI.gameObject.activeSelf;        
        shopUI.gameObject.SetActive(toggle);
        if(toggle)
        {
            if(onGamePaused != null)
            {
                onGamePaused(Gamemanager.GameState.Paused);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            if(onGamePaused != null)
            {
                onGamePaused(Gamemanager.GameState.Playing);
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ToggleInventoryUI()
    {
        var toggle = !journalPanel.gameObject.activeSelf;
        
        journalPanel.gameObject.SetActive(toggle);
        if (toggle)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePauseMenu();
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
        
        controls.Enable();
        
        DialogueTrigger.onNPCDialogue += OnNPCDialogue;
        PauseMenu.onResumeClicked += TogglePauseMenu;
        PlayerHealth.onPlayerHealthChange += OnPlayerHealthChange;
        JetPack.onFuelUse += OnFuelUse;
        JetPack.onJetpackAwake += OnJetpackAwake;
        JetpackBase.onFuelUse += OnFuelUse;
        JetpackBase.onJetpackAwake += OnJetpackAwake;
        ShopKeeper.onShopKeeperInteraction += ToggleShopUI;
        DialogueTriggerImproveTest.onNPCDialogue += OnNPCDialogue;
    }

    private void OnDisable() {
        
        controls.Disable();
        
        DialogueTrigger.onNPCDialogue -= OnNPCDialogue; 
        PauseMenu.onResumeClicked -= TogglePauseMenu;
        PlayerHealth.onPlayerHealthChange -= OnPlayerHealthChange;
        JetPack.onFuelUse -= OnFuelUse;
        JetPack.onJetpackAwake -= OnJetpackAwake;
        JetpackBase.onFuelUse -= OnFuelUse;
        JetpackBase.onJetpackAwake -= OnJetpackAwake;
        ShopKeeper.onShopKeeperInteraction -= ToggleShopUI;
        DialogueTriggerImproveTest.onNPCDialogue -= OnNPCDialogue;
    }  
}
