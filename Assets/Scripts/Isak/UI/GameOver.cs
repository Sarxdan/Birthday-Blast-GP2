using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static Events.EmptyEvent onGameRestart;
    [SerializeField] float loadTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RestartGame());
        Gamemanager.instance.ResetUnlockedItems();
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(loadTime);
        if(onGameRestart != null)
        {
            onGameRestart();
        }

        SceneManager.LoadScene("S_GameScene");
        Gamemanager.instance.UpdateGameState(Gamemanager.GameState.Playing);
        
        //Make sure cursor is locked when returning to the game scene
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
