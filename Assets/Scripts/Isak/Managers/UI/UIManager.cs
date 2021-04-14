using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public static Events.DialogueEvent onNPCDialogue;
    public InGameUI inGameUI;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        inGameUI = GetComponentInChildren<InGameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    private void OnDisable() {
            DialogueManager.onNPCDialogue -= OnNPCDialogue; 
    }
}
