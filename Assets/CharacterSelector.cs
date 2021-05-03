using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : Singleton<CharacterSelector>
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
    [SerializeField] GameObject characterPrefab;
    [SerializeField] CharacterParts[] femaleCharacters;
    [SerializeField] CharacterParts[] maleCharacters;
    [SerializeField] CharacterParts[] otherCharacters;
    CharacterGenders gender;
    GameObject[] characters;
    public int charactersIndex;
    public GameObject chosenCharacter;
    bool charactersSpawned = false;
    
    public void ChangeGender(CharacterGenders newGender)
    {
        gender = newGender;
        PopulateScene();
    }
    private void Update() {
        if(!charactersSpawned) return;
        if(Input.GetButtonDown("Horizontal"))
        {
            if(Input.GetAxis("Horizontal") > 0)
        {
            charactersIndex++;
            if(charactersIndex >= characters.Length)
            {
                charactersIndex = 0;
            }
            chosenCharacter = characters[charactersIndex];
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            charactersIndex--;
            if(charactersIndex < 0)
            {
                charactersIndex = characters.Length - 1;
            }
            chosenCharacter = characters[charactersIndex];
        }
        }     
        if(Input.GetButtonDown("Select"))
        {
            print("chose character " + chosenCharacter.name);
        }  
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
                GameObject character = Instantiate(characterPrefab, instantiatedPosition, Quaternion.identity);
                character.name = maleCharacters[i].name;
                characters[i] = character;
                Transform[] meshes = character.GetComponentsInChildren<Transform>();
                foreach(Transform mesh in meshes)
                {
                    if(mesh.name == "MeshBase")
                    {
                        meshBase = mesh;
                    }
                }
                if(meshBase == null) return;
                foreach(Transform child in meshBase)
                {
                    if(child.name != "Root")
                    {
                        child.gameObject.SetActive(false);
                    }
                    if(child.name == maleCharacters[i].eyebrows.name || child.name == maleCharacters[i].eyes.name || child.name == maleCharacters[i].body.name)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                instantiatedPosition.x += 2;
                
            }
            break;
            case CharacterGenders.Female:
            characters = new GameObject[femaleCharacters.Length];
            for(int i = 0; i < femaleCharacters.Length; i++)
            {
                GameObject character = Instantiate(characterPrefab, instantiatedPosition, Quaternion.identity);
                character.name = femaleCharacters[i].name;
                characters[i] = character;
                Transform[] meshes = character.GetComponentsInChildren<Transform>();
                foreach(Transform mesh in meshes)
                {
                    if(mesh.name == "MeshBase")
                    {
                        meshBase = mesh;
                    }
                }
                if(meshBase == null) return;
                foreach(Transform child in meshBase)
                {
                    if(child.name != "Root")
                    {
                        child.gameObject.SetActive(false);
                    }
                    if(child.name == femaleCharacters[i].eyebrows.name || child.name == femaleCharacters[i].eyes.name || child.name == femaleCharacters[i].body.name)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                instantiatedPosition.x += 2;
                
            }
            break;
            case CharacterGenders.Neither:
            characters = new GameObject[otherCharacters.Length];
            for(int i = 0; i < otherCharacters.Length; i++)
            {
                GameObject character = Instantiate(characterPrefab, instantiatedPosition, Quaternion.identity);
                character.name = otherCharacters[i].name;
                characters[i] = character;
                Transform[] meshes = character.GetComponentsInChildren<Transform>();
                foreach(Transform mesh in meshes)
                {
                    if(mesh.name == "MeshBase")
                    {
                        meshBase = mesh;
                    }
                }
                if(meshBase == null) return;
                foreach(Transform child in meshBase)
                {
                    if(child.name != "Root")
                    {
                        child.gameObject.SetActive(false);
                    }
                    if(child.name == otherCharacters[i].eyebrows.name || child.name == otherCharacters[i].eyes.name || child.name == otherCharacters[i].body.name)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                instantiatedPosition.x += 2;
                
            }
            break;
        }
        charactersSpawned = true;
        chosenCharacter = characters[0];
        charactersIndex = 0;
    }
}
