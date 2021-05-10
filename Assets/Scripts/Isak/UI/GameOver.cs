using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] float loadTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(loadTime);
        Gamemanager.instance.LoadLevel(0);
    }
}
