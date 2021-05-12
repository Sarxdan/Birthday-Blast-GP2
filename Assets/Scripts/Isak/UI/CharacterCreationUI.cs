using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreationUI : MonoBehaviour
{
    CharacterSelector selector;
    private void Awake() {
        selector = FindObjectOfType<CharacterSelector>();
    }
    // Start is called before the first frame update
    public void RightArrow()
    {
        selector.SwitchCharacter(CharacterSelector.Direction.Right);
    }

    public void LeftArrow()
    {
        selector.SwitchCharacter(CharacterSelector.Direction.Left);
    }

    public void BackArrow()
    {
        SceneManager.LoadScene(0);
    }

    public void SelectionDone()
    {
        PlayerPrefs.DeleteAll();
        Gamemanager.instance.UpdateGameState(Gamemanager.GameState.Playing);
        selector.SelectionDone();       
    }
}
