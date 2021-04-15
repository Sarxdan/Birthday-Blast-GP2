using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] bool repeatDialogue = false;
    [SerializeField][TextArea(3, 10)] string[] sentences;

    public string Name
    {
        get{return name;}
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
