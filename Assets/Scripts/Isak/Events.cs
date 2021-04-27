using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public delegate void EmptyEvent();
    public delegate void BoolEvent(bool check);
    public delegate void LoadSceneEvent(string levelName);
    public delegate void DialogueEvent(string dialogue, string name);
    public delegate void DamagePlayerEvent(int amount);
    public delegate void FuelEvent(float amount);
    public delegate void GameStateEvent(Gamemanager.GameState state);
    public delegate void PositionEvent(Vector3 position);
    public delegate void TransitionEvent(int levelIndex);
    public delegate void UnlockKeyItemEvent(KeyItems.Items item);
}
