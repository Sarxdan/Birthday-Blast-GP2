using System;
using System.Collections;
using System.Collections.Generic;
using MiscUtil.Collections.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public static Events.DialogueEvent onNPCDialogue;
    public static Events.DamagePlayerEvent onPlayerHealthChange;
    public static Events.FuelEvent onFuelUse;
    public static Events.FuelEvent onJetpackAwake;
    public static Events.EmptyEvent onPlayerLeavingConversation;
    InGameUI inGameUI;
    PauseMenu pauseMenu;
    public ShopUI shopUI;
    public GameObject journalPanel;

    public GameObject[] popupTabs;


    private Controls controls;
    
    

    public void EnablePopUp(GameObject popup)
    {
        if (popup == null)
        {
            Debug.LogWarning("PopUp window not set as a reference");
            return;
        }
        
        var goPopup = popup;
        
        foreach (var popupTab in popupTabs)
        {
            if (popup.name == popupTab.name)
            {
                goPopup = popupTab;
            }
        }
        goPopup.SetActive(true);
        FindObjectOfType<ThirdPersonController>().ToggleControls(false);
        ToggleMouse(true);
    }

    public void EnablePopUp(GameObject popup, int value, Sprite img)
    {
        if (popup == null)
        {
            Debug.LogWarning("PopUp window not set as a reference");
            return;
        }

        var goPopup = popup;
        
        foreach (var popupTab in popupTabs)
        {
            if (popup.name == popupTab.name)
            {
                goPopup = popupTab;
            }
        }
        StopCoroutine("DisablePopUpAfterX");
        goPopup.SetActive(true);
        goPopup.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "+" + value;
        goPopup.transform.GetChild(0).GetComponentInChildren<Image>().sprite = img;
        StartCoroutine(DisablePopUpAfterX(goPopup));
    }

    private IEnumerator DisablePopUpAfterX(GameObject popUp)
    {
        yield return new WaitForSeconds(3);
        popUp.SetActive(false);
    }

    public void ClosePopUp(GameObject popup)
    {
        FindObjectOfType<ThirdPersonController>().ToggleControls(true);
        popup.SetActive(false);

        ToggleMouse(false);
    }
    
    protected override void Awake()
    {
        base.Awake();
        controls = new Controls();
        inGameUI = GetComponentInChildren<InGameUI>();
        pauseMenu = GetComponentInChildren<PauseMenu>();
        shopUI = GetComponentInChildren<ShopUI>();
    }
    

    private void Start() {
        ToggleInventoryUI();
        TogglePauseMenu();
        ToggleShopUI();
    }

    void TogglePauseMenu()
    {
        if(FindObjectOfType<Gamemanager>() == null) return;
        bool toggle = !pauseMenu.gameObject.activeSelf;
        pauseMenu.gameObject.SetActive(toggle);
        ToggleMouse(toggle);
        if(toggle)
        {
            Gamemanager.instance.UpdateGameState(Gamemanager.GameState.Paused);
        }
        else
        {
            Gamemanager.instance.UpdateGameState(Gamemanager.GameState.Playing);
        }
        if (FindObjectOfType<ThirdPersonController>() == null || FindObjectOfType<Level>() == null) return;
        if (FindObjectOfType<Level>().levelType == LevelType.Island)
        {           
            if (!toggle && FindObjectOfType<ThirdPersonController>().disableCameraController)
            {
                ToggleMouse(true);
            }
        }
    }

    public void ToggleShopUI() //change to event later?
    {
        bool toggle = !shopUI.gameObject.activeSelf;        
        shopUI.gameObject.SetActive(toggle);
        ToggleMouse(toggle);
        
        if(toggle)
        {
            Gamemanager.instance.UpdateGameState(Gamemanager.GameState.Paused); //toggle player movement instead
        }
        else
        {
            Gamemanager.instance.UpdateGameState(Gamemanager.GameState.Playing);
        }
    }

    public void ToggleInventoryUI()
    {
        var toggle = !journalPanel.gameObject.activeSelf;
        
        journalPanel.gameObject.SetActive(toggle);
        
        ToggleMouse(toggle);
        
        if (FindObjectOfType<ThirdPersonController>() == null || FindObjectOfType<Level>() == null) return;
        if (FindObjectOfType<Level>().levelType == LevelType.Jetpack) return;
        FindObjectOfType<ThirdPersonController>().ToggleControls(!toggle);
    }
    
    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePauseMenu();
        }
    }
    
    void OnNPCDialogue(string dialogue, string name, Sprite npcSprite)
    {
        if(onNPCDialogue != null)
        {
            onNPCDialogue(dialogue, name, npcSprite);
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

    void OnPlayerLeavingConversation()
    {
        if(onPlayerLeavingConversation != null)
        {
            onPlayerLeavingConversation();
        }
    }

    void OnPlayerDeath()
    {
        inGameUI.gameObject.SetActive(false);
    }

    void OnGameRestart()
    {
        inGameUI.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnEnable() {
        
        controls.Enable();
        GameOver.onGameRestart += OnGameRestart;
        PauseMenu.onResumeClicked += TogglePauseMenu;
        PlayerHealth.onPlayerHealthChange += OnPlayerHealthChange;
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
        JetPack.onFuelUse += OnFuelUse;
        JetPack.onJetpackAwake += OnJetpackAwake;
        JetpackBase.onFuelUse += OnFuelUse;
        JetpackBase.onJetpackAwake += OnJetpackAwake;
        ShopKeeper.onShopKeeperInteraction += ToggleShopUI;
        DialogueTrigger.onNPCDialogue += OnNPCDialogue;
        DialogueTrigger.onPlayerLeavingConversation += OnPlayerLeavingConversation;
    }

    private void OnDisable() {
        
        controls.Disable();
        
        GameOver.onGameRestart -= OnGameRestart;
        PauseMenu.onResumeClicked -= TogglePauseMenu;
        PlayerHealth.onPlayerHealthChange -= OnPlayerHealthChange;
        PlayerHealth.onPlayerDeath -= OnPlayerDeath;
        JetPack.onFuelUse -= OnFuelUse;
        JetPack.onJetpackAwake -= OnJetpackAwake;
        JetpackBase.onFuelUse -= OnFuelUse;
        JetpackBase.onJetpackAwake -= OnJetpackAwake;
        ShopKeeper.onShopKeeperInteraction -= ToggleShopUI;
        DialogueTrigger.onNPCDialogue -= OnNPCDialogue;
        DialogueTrigger.onPlayerLeavingConversation -= OnPlayerLeavingConversation;
    }


    public void ToggleMouse(bool state)
    {
#if UNITY_STANDALONE
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
#endif
        
    }
}
