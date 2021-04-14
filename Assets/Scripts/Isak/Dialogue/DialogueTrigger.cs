using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    protected class MultipleDialogues
    {
        public Dialogue dialogue;
        public Gamemanager.KeyItems dialogueRequirements;
    }

    [SerializeField][Tooltip("fill if optional dialogues, such as fetch quest dialogues, are needed, else keep empty and just use default dialogue")] MultipleDialogues[] dialogues;
    public static Events.DialogueEvent onNPCDialogue;
    [SerializeField] Dialogue defaultDialogue;
    MultipleDialogues dialogueToCheck;
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
        if(dialogues.Length != 0)
        {
            bool useDialogue = false;
            foreach(MultipleDialogues dialogue in dialogues)
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
        if(dialogueToCheck.dialogueRequirements.jetpack == Gamemanager.instance.unlockedItems.jetpack && dialogueToCheck.dialogueRequirements.pewpew == Gamemanager.instance.unlockedItems.pewpew)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
