using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //testing

public class Transition : MonoBehaviour
{
    public static Events.LoadSceneEvent onTransitionEvent;

    enum PlayerStates
    {
        Jetpack, 
        OnFoot
    }
    bool isInteracted = false;
    bool cantTriggerAgain = false;
    [SerializeField] PlayerStates transitionTo = PlayerStates.Jetpack;
    [SerializeField][Tooltip("name of scene to load after transition, case sensitive")] string sceneToLoad = string.Empty;

    private void OnTriggerEnter(Collider other) {
        if(cantTriggerAgain) return;
        if(other.tag == "Player")
        {
            if(transitionTo == PlayerStates.OnFoot)
            {
                TransitionToIsland();
                cantTriggerAgain = true;
            }
        }  
    }

    public void Interacting()
    {
        if(isInteracted) return;
        isInteracted = true;
        TransitionToJetpack();
    }

    void TransitionToJetpack()
    {
        if(onTransitionEvent != null)
        {
            onTransitionEvent(sceneToLoad);
        }
    }

    void TransitionToIsland()
    {
        if(onTransitionEvent != null)
        {
            onTransitionEvent(sceneToLoad);
        }
    }
}

