using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    [System.Serializable]
    public class Dialogue
    {
        [SerializeField][Tooltip("Used for sorting and to more easily find dialogues")] string name;
        [SerializeField] bool repeatDialogue = false;  
        [SerializeField][Tooltip("rewarded at end of dialogue if not empty")] Reward reward;
        [SerializeField] KeyItems.Items dialogueRequirements;
        [SerializeField][TextArea(3, 10)] string[] sentences;    

        public bool RepeatDialogue
        {
            get{return repeatDialogue;}
        }
        public Reward Reward
        {
            get{return reward;}
        }
        public KeyItems.Items DialogueRequirements
        {
            get{return dialogueRequirements;}
        }
        public string[] Sentences
        {
            get{return sentences;}
        }
         
    }
    [SerializeField] string name;     
    [SerializeField] Dialogue[] dialogues;
    

    public string Name
    {
        get{return name;}
    }

    public Dialogue[] Dialogues
    {
        get{return dialogues;}
    }
}
