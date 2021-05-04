using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour // improve to use reward system
{
    [SerializeField] NPCDialogue dialogues; // variable for all the dialogues (scriptable object)
    public static Events.DialogueEvent onNPCDialogue; // event used for printing out the dialogues sentence and name to screen
    public static Events.EmptyEvent onPlayerLeavingConversation;
    NPCDialogue.AlternativeDialogue dialogueToCheck; 
    Dialogue dialogueToUse; 
    Dialogue lastDialogueUsed; 
    int timesSpokenWith = 0;
    bool rewarded = false;
    bool playerIsInteracting = false;
    Transform player;
    Interactable interactable;

    private void Awake() {
        player = FindObjectOfType<PlayerHealth>().transform;
        interactable = GetComponent<Interactable>();
    }

    private void Update() {
        if(!playerIsInteracting) return;
        print(transform.position);
        print(player.position);
        print(Vector3.Distance(player.position, transform.position));
        if(Vector3.Distance(player.position, transform.position) > interactable.interactRadius)
        {
            playerIsInteracting = false;
            if(onPlayerLeavingConversation != null)
            {
                onPlayerLeavingConversation();
            }
        }
    }
    string GetNextDialogue() 
    {
        string nextDialogue = dialogueToUse.Sentences()[timesSpokenWith];
        if(timesSpokenWith < dialogueToUse.Sentences().Length - 1) // if not at final dialogue sentence, increase times spoken with
        {
            timesSpokenWith++;
        }
        else if(dialogueToUse.RepeatDialogue) // if dialogue is set to repeat, set times spoken with back to 0 after final dialogue sentence
        {
            timesSpokenWith = 0;
        }   
        else if(timesSpokenWith == dialogueToUse.Sentences().Length - 1) // end of dialogue if not set to repeating
        {
            EndOfDialogue();
        }
        return nextDialogue;
    }

    void EndOfDialogue()
    {
        ShopKeeper shopKeeper = GetComponent<ShopKeeper>();
        if(shopKeeper != null) shopKeeper.OpenShop();
    }

    void CheckIfNewDialogue() 
    {        
        if(dialogueToUse != lastDialogueUsed && lastDialogueUsed != null) //if there is a difference between the dialogue to use and the last used dialogue
        {
            timesSpokenWith = 0;
        }
        lastDialogueUsed = dialogueToUse; // change which dialogue was last used
    }

    void RewardPlayer() //currently only rewards player once
    {
        if(rewarded) return;
        if(dialogueToUse.Reward == null) return;
        if(timesSpokenWith != dialogueToUse.Sentences().Length - 1) return;
        print("rewarded with " + dialogueToUse.Reward.name);
        GameObject reward = Instantiate(dialogueToUse.Reward, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        rewarded = true;
    }

    void SetPlayerMovement()
    {
        ThirdPersonController thirdPersonController = FindObjectOfType<ThirdPersonController>();
        if(timesSpokenWith != dialogueToUse.Sentences().Length - 1)
        {            
            thirdPersonController.disablePlayerMovement = true;
        }
        else
        {
            thirdPersonController.disablePlayerMovement = false;
        }
        
    }
    public void TriggerDialogue()
    {   
        if(dialogues.DefaultDialogues.Sentences().Length == 0 && dialogues.Alternativedialogues.Length == 0) return;
        playerIsInteracting = true;
        dialogueToUse = ChooseDialogue();
        CheckIfNewDialogue();
        RewardPlayer();
        SetPlayerMovement();
        if(dialogueToUse == null)
        {
            Debug.LogError("requirements for dialogue is not met");
            return;
        }
        if(onNPCDialogue != null)
        {
            onNPCDialogue(GetNextDialogue(), dialogues.Name, dialogues.NPCSprite);
        }
    }
    Dialogue ChooseDialogue()
    {
            foreach(NPCDialogue.AlternativeDialogue dialogue in dialogues.Alternativedialogues) //loop through all dialogues
            {
                dialogueToCheck = dialogue;
                if(CheckCurrentDialogue())
                {
                    return dialogue.Dialogue; // found a dialogue that fulfills requirement
                }
            }  
            return dialogues.DefaultDialogues;    // found no dialogues that fulfills requirement
    }

    bool CheckCurrentDialogue() // this will look like shit, improve over iterations
    {   
        bool meetsReqiurements = true;

        if(dialogueToCheck.DialogueRequirements.jetpack != Gamemanager.instance.UnlockedItems.jetpack) 
        {       
            meetsReqiurements = false;
        }
        if(dialogueToCheck.DialogueRequirements.pewpew != Gamemanager.instance.UnlockedItems.pewpew)
        {
            meetsReqiurements = false;
        }
        if(dialogueToCheck.DialogueRequirements.shovel != Gamemanager.instance.UnlockedItems.shovel)
        {
            meetsReqiurements = false;
        }
        
        return meetsReqiurements;
    }
}

