using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Settings;

[System.Serializable]
public class Dialogue 
{
    [SerializeField][Tooltip("rewarded at end of dialogue if not empty")] GameObject reward;
    [SerializeField] bool repeatDialogue = false;
    [SerializeField] int locale;
    [SerializeField][TextArea(3, 10)] string[] english;
    [SerializeField][TextArea(3, 10)] string[] swedish;
    public GameObject Reward
    {
        get{return reward;}
    }
    public bool RepeatDialogue
    {
        get{return repeatDialogue;}
    }

    public string[] Sentences()
    {
        // Send sentence according to current locale
        switch(LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.Instance
            .GetSelectedLocale()))
        {
            case 0:
                return english;
            case 1:
                return swedish;
        }

        return english;
    }

}


[System.Serializable]
[CreateAssetMenu(fileName = "NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    
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

    [SerializeField] new string name;  
    [SerializeField] Sprite npcSprite;
    [SerializeField][Tooltip("the dialogue to use if requirements are not met")] Dialogue defaultDialogue;   
    [SerializeField][Tooltip("alternative dialogues with requirements")] AlternativeDialogue[] alternativeDialogues;
    

    public string Name
    {
        get{return name;}
    }
    public Sprite NPCSprite
    {
        get{return npcSprite;}
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
