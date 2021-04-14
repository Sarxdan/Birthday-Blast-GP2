using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public static Events.DialogueEvent onNPCDialogue;
    Queue<string> sentences;

    private void Start() {
        sentences = new Queue<string>();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        print(sentence);
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
    void OnNPCDialogue(string dialogue, string name)
    {
        if(onNPCDialogue != null)
        {
            onNPCDialogue(dialogue, name);
        }
    }

    private void OnEnable() {
            DialogueTrigger.onNPCDialogue += OnNPCDialogue;       
    }

    private void OnDisable() {
            DialogueTrigger.onNPCDialogue -= OnNPCDialogue;
    }
}
