using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //testing

public class Transition : MonoBehaviour
{
    public static Events.LoadSceneEvent onTransitionEvent;
    enum PlayerState
    {
        Adventure,
        Jetpack
    }
    [SerializeField][Tooltip("current state of the player")] PlayerState playerState = PlayerState.Adventure;
    [SerializeField] KeyCode interactKey = KeyCode.E;
    [SerializeField] Text transitionText;
    [SerializeField] float sceneTransitionTime = 1;
    [SerializeField][Tooltip("name of scene to load after transition, case sensitive")] string sceneToLoad = string.Empty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player")
        {
            GetInput(other);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            transitionText.text = string.Empty;
        }
    }
    
    void GetInput(Collider other) //print info to ui
    {
        transitionText.text = "Press " + interactKey + " to continue";
        if(Input.GetKeyDown(interactKey))
        {
            transitionText.text = string.Empty;
            StartTransit(other);
        }
    }

    void StartTransit(Collider other)
    {
        switch(playerState)
        {
            case PlayerState.Adventure:
            print(playerState);
            TransitionToJetpack(other);
            break;
            case PlayerState.Jetpack:
            print(playerState);
            TransitionToIsland(other);
            break;
            default:
            Debug.LogError("Something went wrong with the transition code");
            break;
        }
    }

    void TransitionToJetpack(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        Vector3 movement = new Vector3();
        StartCoroutine(TransitionToNewScene());
    }

    void TransitionToIsland(Collider other)
    {
        JetPack jetPack = other.GetComponent<JetPack>();
        jetPack.AllowMovement = false;
        StartCoroutine(TransitionToNewScene());
    }

    IEnumerator TransitionToNewScene() //dont load scene here, change to sending information to gamemanager when created
    {
        yield return new WaitForSeconds(sceneTransitionTime);
        if(onTransitionEvent != null)
        {
            onTransitionEvent(sceneToLoad);
        }
    }
    
}

