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
    public void Male()
    {
        selector.StartSelection(CharacterSelector.CharacterGenders.Male);
    }

    public void Female()
    {
        selector.StartSelection(CharacterSelector.CharacterGenders.Female);
    }

    public void Neither()
    {
        selector.StartSelection(CharacterSelector.CharacterGenders.Neither);
    }

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
        selector.SelectionDone();
        
    }
}
