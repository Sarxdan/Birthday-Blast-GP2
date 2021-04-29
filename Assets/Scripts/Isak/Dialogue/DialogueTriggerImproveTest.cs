using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerImproveTest : MonoBehaviour //add errorhandling if default dialogue is empty
{
    [SerializeField] DialogueImproveTest dialogues;

    public static Events.DialogueEvent onNPCDialogue;
    DialogueImproveTest.DialogueTest dialogueToCheck;
    DialogueImproveTest.DialogueTest dialogueToUse;
    int timesSpokenWith = 0;
    string GetNextDialogue() //change to using the improved dialogue scriptableobject
    {
        if(timesSpokenWith > dialogueToUse.Sentences.Length - 1) // if the chosen dialogue gets switched due to, for example, unlocking stuff, make sure array wont be oob
        {
            timesSpokenWith = dialogueToUse.Sentences.Length - 1;
        }
        string nextDialogue = dialogueToUse.Sentences[timesSpokenWith];
        print(timesSpokenWith);
        if(timesSpokenWith < dialogueToUse.Sentences.Length - 1)
        {
            timesSpokenWith++;
        }
        else if(dialogueToUse.RepeatDialogue)
        {
            timesSpokenWith = 0;
        }   
        return nextDialogue;
    }
    public void TriggerDialogue()
    {   
        dialogueToUse = ChooseDialogue();
        if(dialogueToUse == null)
        {
            Debug.LogError("requirements for dialogue is not met");
            return;
        }
        if(onNPCDialogue != null)
        {
            print(dialogueToUse.Sentences[0]);
            onNPCDialogue(GetNextDialogue(), dialogues.Name);
        }
    }
    DialogueImproveTest.DialogueTest ChooseDialogue()
    {
            foreach(DialogueImproveTest.DialogueTest dialogue in dialogues.Dialogues)
            {
                dialogueToCheck = dialogue;
                if(CheckCurrentDialogue())
                {
                    return dialogue;
                }
            }  
            return null;    
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

