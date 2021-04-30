using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    [System.Serializable]
    public class DefaultDialogue //turn into part of Dialogue class?
    {
        [SerializeField] bool repeatDialogue = false;
        [SerializeField][TextArea(3, 10)] string[] sentences;
        public bool RepeatDialogue
        {
            get{return repeatDialogue;}
        }
        public string[] Sentences
        {
            get{return sentences;}
        }
    }
    [System.Serializable]
    public class Dialogue 
    {
        [SerializeField][Tooltip("Used for sorting and to more easily find dialogues")] string name; 
        [SerializeField][Tooltip("rewarded at end of dialogue if not empty")] Reward reward;
        [SerializeField] KeyItems.Items dialogueRequirements;  
        [SerializeField] DefaultDialogue dialogue;
        public Reward Reward
        {
            get{return reward;}
        }
        public KeyItems.Items DialogueRequirements
        {
            get{return dialogueRequirements;}
        }
        public DefaultDialogue Dialogues
        {
            get{return dialogue;}
        }
         
    }

    [SerializeField] string name;  
    [SerializeField][Tooltip("the dialogue to use if requirements are not met")] DefaultDialogue defaultDialogue;   
    [SerializeField][Tooltip("alternative dialogues with requirements")] Dialogue[] alternativeDialogues;
    

    public string Name
    {
        get{return name;}
    }

    public Dialogue[] Alternativedialogues
    {
        get{return alternativeDialogues;}
    }
    public DefaultDialogue DefaultDialogues
    {
        get{return defaultDialogue;}
    }
}
