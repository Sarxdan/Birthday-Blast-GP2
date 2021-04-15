using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string FirstGameScene = "IslandScene";
    public string MenuScene = "Menu";
    public string CreditsScene = "Credits";
    
   public void PlayGame () 
    {
        SceneManager.LoadScene("IslandScene");
    }


    //osäker på vad som är mest optimalt ang buildindex och hur den borde användas :)  /Mikael
    public void Credits ()
    {
        SceneManager.LoadScene(CreditsScene);
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
