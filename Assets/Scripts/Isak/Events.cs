using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Events
{
    public delegate void LanguageChanged(int i);

    public static LanguageChanged onLanguageChanged;

    public static void ChangeLanguage(int i)
    {
        onLanguageChanged?.Invoke(i);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
    }
    public delegate void EmptyEvent();
    public delegate void IntEvent(int value);
    public delegate void BoolEvent(bool check);
    public delegate void LoadSceneEvent(string levelName);
    public delegate void DialogueEvent(string dialogue, string name, Sprite NPCSprite);
    public delegate void DamagePlayerEvent(int amount);
    public delegate void FuelEvent(float amount);
    public delegate void GameStateEvent(Gamemanager.GameState newState);
    public delegate void PositionEvent(Vector3 position);
    public delegate void TransitionEvent(int levelIndex);
    public delegate void UnlockKeyItemEvent(KeyItems.Items item);
}
