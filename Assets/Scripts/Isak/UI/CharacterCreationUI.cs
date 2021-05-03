using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameObject.SetActive(false);
    }

    public void Female()
    {
        selector.StartSelection(CharacterSelector.CharacterGenders.Female);
        gameObject.SetActive(false);
    }

    public void Neither()
    {
        selector.StartSelection(CharacterSelector.CharacterGenders.Neither);
        gameObject.SetActive(false);
    }
}
