using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    Queue<string> sentences;
    [SerializeField] Text npcText; //change to sending event to a UI manager
    [SerializeField] float textExistTimer = 1;

    private void Start() {
        npcText.text = string.Empty;
        sentences = new Queue<string>();
    }
    public void StartDialogue(string dialogue)
    {
        StopAllCoroutines();
        npcText.text = dialogue;
        StartCoroutine(RemoveText());
    }

    IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(textExistTimer);
        npcText.text = string.Empty;
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

    private void OnEnable() {
        DialogueTrigger.onNPCDialogue += StartDialogue;
    }

    private void OnDisable() {
        DialogueTrigger.onNPCDialogue -= StartDialogue;
    }
}
