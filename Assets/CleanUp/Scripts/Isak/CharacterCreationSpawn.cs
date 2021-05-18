using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationSpawn : MonoBehaviour
{
    Canvas canvas;
    CharacterSelector selector;
    Light spotLight;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        selector = FindObjectOfType<CharacterSelector>();
        spotLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selector.chosenCharacter == gameObject)
        {
            canvas.gameObject.SetActive(true);
            spotLight.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
            spotLight.gameObject.SetActive(false);
        }
    }
}
