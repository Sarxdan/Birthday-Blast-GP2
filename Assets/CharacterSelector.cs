using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{

    public enum Direction
    {
        Right,
        Left
    }

    public GameObject[] characters;
    public int charactersIndex;
    public GameObject chosenCharacter;
    bool charactersSpawned = false;
    
    CinemachineVirtualCamera CinemachineVirtualCamera;
    private void Awake() {
        Object[] characterPrefabs = Resources.LoadAll("CharacterPrefabs", typeof(GameObject));
        characters = FillFromObjectArray(characterPrefabs);
        CinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        PopulateScene();
    }

    GameObject[] FillFromObjectArray(object[] arrayToFillFrom) //helper function so we wont get spagethi code
    {
        GameObject[] arrayToFill;
        arrayToFill = new GameObject[arrayToFillFrom.Length];
        for(int i = 0; i < arrayToFillFrom.Length; i++)
        {
            arrayToFill[i] = (GameObject)arrayToFillFrom[i];
        }
        return arrayToFill;
    }

    public void SwitchCharacter(Direction direction)
    {
        switch(direction)
        {
            case Direction.Left:
            charactersIndex++;
            if(charactersIndex >= characters.Length)
            {
                charactersIndex = 0;
            }
            chosenCharacter = characters[charactersIndex];
            CinemachineVirtualCamera.LookAt = chosenCharacter.transform;
            break;

            case Direction.Right:
            charactersIndex--;
            if(charactersIndex < 0)
            {
                charactersIndex = characters.Length - 1;
            }
            chosenCharacter = characters[charactersIndex];
            CinemachineVirtualCamera.LookAt = chosenCharacter.transform;
            break;
        }
    }

    public void SelectionDone()
    {
        if(!charactersSpawned) return;

            foreach(Transform child in chosenCharacter.transform)
            {
                if(child.name == "Root") continue;
                if(child.gameObject.activeSelf)
                {
                    PlayerManager.instance.chosenCharacterMeshesNames.Add(child.GetComponent<SkinnedMeshRenderer>().name);
                    PlayerManager.instance.chosenCharacterPrefab = (GameObject)Resources.Load("CharacterPrefabs/" + chosenCharacter.name);
                }
            }

            Gamemanager.instance.LoadLevel(3);
    }

    void PopulateScene()
    {
        Vector3 instantiatedPosition = new Vector3(0,0.5f,0);
        GameObject parent = new GameObject();
        parent.name = "characters";
            for(int i = 0; i < characters.Length; i++)
            {
                GameObject character = Instantiate(characters[i], instantiatedPosition, Quaternion.identity);
                character.transform.parent = parent.transform;
                character.name = characters[i].name;
                characters[i] = character;
                instantiatedPosition.x += 2;
                
            }
        charactersSpawned = true;
        chosenCharacter = characters[0];
        CinemachineVirtualCamera.LookAt = chosenCharacter.transform;
        charactersIndex = 0;
    }
}
