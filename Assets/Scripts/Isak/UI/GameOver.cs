using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Gamemanager.instance.UpdateGameState(Gamemanager.GameState.Pregame);
        yield return new WaitForSeconds(loadTime);
        if(onGameRestart != null)
        {
            onGameRestart();
        }
        
        Gamemanager.instance.LoadLevel(0);
    }
}
