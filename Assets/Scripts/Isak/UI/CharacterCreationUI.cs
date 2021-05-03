using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void Male()
    {
        CharacterSelector.instance.ChangeGender(CharacterSelector.CharacterGenders.Male);
        gameObject.SetActive(false);
    }

    public void Female()
    {
        CharacterSelector.instance.ChangeGender(CharacterSelector.CharacterGenders.Female);
        gameObject.SetActive(false);
    }

    public void Neither()
    {
        CharacterSelector.instance.ChangeGender(CharacterSelector.CharacterGenders.Neither);
        gameObject.SetActive(false);
    }
}
