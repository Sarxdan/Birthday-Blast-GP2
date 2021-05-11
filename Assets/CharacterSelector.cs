using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    [System.Serializable]
    public class CharacterParts
    {
        public string name;
        public Mesh eyebrows;
        public Mesh eyes;
        public Mesh body;
    }
    public enum CharacterGenders
    {
        Male,
        Female,
        Neither
    }

    public enum Direction
    {
        Right,
        Left
    }
    [SerializeField] GameObject characterPrefab;
    [SerializeField] GameObject[] femaleCharacters;
    [SerializeField] GameObject[] maleCharacters;
    [SerializeField] GameObject[] otherCharacters;
    CharacterGenders gender;
    GameObject[] characters;
    public int charactersIndex;
    public GameObject chosenCharacter;
    bool charactersSpawned = false;
    
    CinemachineVirtualCamera CinemachineVirtualCamera;
    private void Awake() {
        CinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        StartSelection(CharacterGenders.Neither);
    }

    public void SwitchCharacter(Direction direction)
    {
        switch(direction)
        {
            case Direction.Right:
            charactersIndex++;
            if(charactersIndex >= characters.Length)
            {
                charactersIndex = 0;
            }
            chosenCharacter = characters[charactersIndex];
            CinemachineVirtualCamera.LookAt = chosenCharacter.transform;
            break;

            case Direction.Left:
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
                }
            }

            Gamemanager.instance.LoadLevel(3);
    }

    public void StartSelection(CharacterGenders newGender)
    {
        if(characters != null)
        {
            foreach(GameObject character in characters)
        {
            Destroy(character);
        }
        }       
        gender = newGender;
        PopulateScene();
    }

    void PopulateScene()
    {
        Vector3 instantiatedPosition = new Vector3(0,0.5f,0);
        Transform meshBase = transform;
        SkinnedMeshRenderer renderer;
        switch(gender)
        {
            case CharacterGenders.Male:
            characters = new GameObject[maleCharacters.Length];
            for(int i = 0; i < maleCharacters.Length; i++)
            {
                GameObject character = Instantiate(maleCharacters[i], instantiatedPosition, Quaternion.identity);
                character.name = maleCharacters[i].name;
                characters[i] = character;
                instantiatedPosition.x += 2;
                
            }
            break;
            case CharacterGenders.Female:
            characters = new GameObject[femaleCharacters.Length];
            for(int i = 0; i < femaleCharacters.Length; i++)
            {
                GameObject character = Instantiate(femaleCharacters[i], instantiatedPosition, Quaternion.identity);
                character.name = femaleCharacters[i].name;
                characters[i] = character;
                instantiatedPosition.x += 2;
                
            }
            break;
            case CharacterGenders.Neither:
            characters = new GameObject[otherCharacters.Length];
            for(int i = 0; i < otherCharacters.Length; i++)
            {
                GameObject character = Instantiate(otherCharacters[i], instantiatedPosition, Quaternion.identity);
                character.name = otherCharacters[i].name;
                characters[i] = character;
                instantiatedPosition.x += 2;
                
            }
            break;
        }
        charactersSpawned = true;
        chosenCharacter = characters[0];
        CinemachineVirtualCamera.LookAt = chosenCharacter.transform;
        charactersIndex = 0;
    }
}
