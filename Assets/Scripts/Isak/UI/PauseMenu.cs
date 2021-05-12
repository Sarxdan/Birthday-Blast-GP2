using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static Events.EmptyEvent onResumeClicked;

    public void Resume ()
    {
        if(onResumeClicked != null)
        {
            onResumeClicked();
        }
    }

    public void QuitGame()
    {
        print("quitting game");
        Application.Quit();
    }
    
   
}
