using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static Events.DialogueEvent onNPCDialogue;
    [SerializeField] Dialogue dialogue;
    int timesSpokenWith = 0;
    string GetNextDialogue()
    {
        string nextDialogue = dialogue.Sentences[timesSpokenWith];
        if(timesSpokenWith != dialogue.Sentences.Length - 1)
        {
            timesSpokenWith++;
        }
        else if(dialogue.RepeatDialogue)
        {
            timesSpokenWith = 0;
        }      
        return nextDialogue;
    }
    public void TriggerDialogue()
    {   
        if(onNPCDialogue != null)
        {
            onNPCDialogue(GetNextDialogue());
        }
    }
}
