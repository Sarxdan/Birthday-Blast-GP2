using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static Events.EmptyEvent onResumeClicked;

    public void Resume ()
    {
        ResetPauseMenu();
        if(onResumeClicked != null)
        {
            onResumeClicked();
        }
    }

    public void ResetPauseMenu()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            if(child.name == "Menu") child.gameObject.SetActive(true);
        }
    }

    public void QuitGame()
    {
        print("quitting game");
        Application.Quit();
    }
    
    public void SetLocale(int i)
    {
        Events.ChangeLanguage(i);
    }

   
   
}
