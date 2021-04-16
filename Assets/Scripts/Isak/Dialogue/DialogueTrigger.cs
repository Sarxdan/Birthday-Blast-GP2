using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour //add errorhandling if default dialogue is empty
{
    [System.Serializable]
    protected class OptionalDialogues
    {
        public Dialogue dialogue;
        public KeyItems.Items dialogueRequirements;

    }

    [SerializeField][Tooltip("fill if optional dialogues, such as fetch quest dialogues, are needed, else keep empty and just use default dialogue")] OptionalDialogues[] optionalDialogues;
    public static Events.DialogueEvent onNPCDialogue;
    [SerializeField] Dialogue defaultDialogue;
    OptionalDialogues dialogueToCheck;
    Dialogue dialogueToUse;
    int timesSpokenWith = 0;
    string GetNextDialogue()
    {
        string nextDialogue = dialogueToUse.Sentences[timesSpokenWith];
        if(timesSpokenWith != dialogueToUse.Sentences.Length - 1)
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
        if(onNPCDialogue != null)
        {
            onNPCDialogue(GetNextDialogue(), dialogueToUse.Name);
        }
    }
    Dialogue ChooseDialogue()
    {
        if(optionalDialogues.Length != 0)
        {
            bool useDialogue = false;
            foreach(OptionalDialogues dialogue in optionalDialogues)
            {
                dialogueToCheck = dialogue;
                useDialogue = CheckCurrentDialogue();
                if(useDialogue)
                {
                    return dialogue.dialogue;
                }
            }
        }       
        return defaultDialogue; 
    }

    bool CheckCurrentDialogue() // this will look like shit, improve over iterations
    {   
        bool meetsReqiurements = true;

        if(dialogueToCheck.dialogueRequirements.jetpack != Gamemanager.instance.unlockedItems.jetpack) 
        {       
            meetsReqiurements = false;
        }
        if(dialogueToCheck.dialogueRequirements.pewpew != Gamemanager.instance.unlockedItems.pewpew)
        {
            meetsReqiurements = false;
        }
        
        return meetsReqiurements;
    }
}
