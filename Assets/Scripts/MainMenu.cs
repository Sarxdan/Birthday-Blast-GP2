using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MainMenu : MonoBehaviour
{
    public string FirstGameScene = "S_GameScene";
    public string MenuScene = "Menu";
    public string CreditsScene = "Credits";
    public string charSelectScene = "S_CharacterSelection";

    private void Start()
    {
        //Events.ChangeLanguage(0);
    }

    public void PlayGame () 
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(FirstGameScene);
    }

    public void SetLocale(int i)
    {
        Events.ChangeLanguage(i);
    }

    //osäker på vad som är mest optimalt ang buildindex och hur den borde användas :)  /Mikael
    public void Credits ()
    {
        SceneManager.LoadScene(CreditsScene);
    }

    public void CharSelectButton()
    {
        SceneManager.LoadScene(charSelectScene);
    }

    public void Back()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
