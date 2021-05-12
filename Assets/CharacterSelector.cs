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
    [SerializeField] Transform circleCenter;
    [SerializeField] Transform circleEnd;
    [SerializeField] Transform cameraFollowTarget;
    [SerializeField] float cameraDistance;
    
    CinemachineVirtualCamera CinemachineVirtualCamera;
    private void Awake() {
        Object[] characterPrefabs = Resources.LoadAll("CharacterPrefabs", typeof(GameObject));
        characters = FillFromObjectArray(characterPrefabs);
        CinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        CinemachineVirtualCamera.Follow = cameraFollowTarget;
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
            cameraFollowTarget.position = chosenCharacter.transform.position + chosenCharacter.transform.forward * cameraDistance;
            cameraFollowTarget.transform.forward = chosenCharacter.transform.forward;
            CinemachineVirtualCamera.LookAt = chosenCharacter.transform;
            break;

            case Direction.Right:
            charactersIndex--;
            if(charactersIndex < 0)
            {
                charactersIndex = characters.Length - 1;
            }
            chosenCharacter = characters[charactersIndex];
            cameraFollowTarget.position = chosenCharacter.transform.position + chosenCharacter.transform.forward * cameraDistance;
            cameraFollowTarget.transform.forward = chosenCharacter.transform.forward;
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
                    PlayerManager.instance.chosenCharacterPrefab = (GameObject)Resources.Load("CharacterPrefabs/" + chosenCharacter.name);
                }
            }

            Gamemanager.instance.LoadLevel(3);
    }

    void PopulateScene()
    {
        float angle = 360/(float)characters.Length;
        float radius = Vector3.Distance(circleCenter.position, circleEnd.position);
        GameObject parent = new GameObject();
        parent.name = "characters";
            for(int i = 0; i < characters.Length; i++)
            {
                Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
                Vector3 direction = rotation * Vector3.forward;
                Vector3 position = circleCenter.position + (direction * radius);
                GameObject character = Instantiate(characters[i], position, rotation);
                character.transform.parent = parent.transform;
                character.name = characters[i].name;
                characters[i] = character;
                
            }
        charactersSpawned = true;
        chosenCharacter = characters[0]; // set a default chosen character to look at
        cameraFollowTarget.position = chosenCharacter.transform.position + chosenCharacter.transform.forward * cameraDistance;
        cameraFollowTarget.transform.forward = chosenCharacter.transform.forward;
        CinemachineVirtualCamera.LookAt = chosenCharacter.transform;
        charactersIndex = 0;
    }
}
