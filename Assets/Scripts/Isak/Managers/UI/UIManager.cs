using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public static Events.GameStateEvent onGamePaused;
    public static Events.DialogueEvent onNPCDialogue;
    InGameUI inGameUI;
    PauseMenu pauseMenu;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
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
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            if(onGamePaused != null)
            {
                onGamePaused(Gamemanager.GameState.Playing);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            print(Time.timeScale);
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

    private void OnEnable() {
        DialogueManager.onNPCDialogue += OnNPCDialogue;
        PauseMenu.onResumeClicked += TogglePauseMenu;
    }
    private void OnDisable() {
        DialogueManager.onNPCDialogue -= OnNPCDialogue; 
        PauseMenu.onResumeClicked += TogglePauseMenu;
    }
}
