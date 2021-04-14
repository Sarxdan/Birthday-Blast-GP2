using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public delegate void EmptyEvent();
    public delegate void LoadSceneEvent(string levelName);
    public delegate void DialogueEvent(string dialogue, string name);
}
