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
        [SerializeField][Tooltip("rewarded at end of dialogue if not empty")] Reward reward;
        [SerializeField] bool repeatDialogue = false;
        [SerializeField][TextArea(3, 10)] string[] sentences;
        public Reward Reward
        {
            get{return reward;}
        }
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
    public class AlternativeDialogue 
    {
        [SerializeField][Tooltip("Used for sorting and to more easily find dialogues")] string name; 
        
        [SerializeField] KeyItems.Items dialogueRequirements;  
        [SerializeField] Dialogue dialogue;
        
        public KeyItems.Items DialogueRequirements
        {
            get{return dialogueRequirements;}
        }
        public Dialogue Dialogue
        {
            get{return dialogue;}
        }
         
    }

    [SerializeField] string name;  
    [SerializeField][Tooltip("the dialogue to use if requirements are not met")] Dialogue defaultDialogue;   
    [SerializeField][Tooltip("alternative dialogues with requirements")] AlternativeDialogue[] alternativeDialogues;
    

    public string Name
    {
        get{return name;}
    }

    public AlternativeDialogue[] Alternativedialogues
    {
        get{return alternativeDialogues;}
    }
    public Dialogue DefaultDialogues
    {
        get{return defaultDialogue;}
    }
}
