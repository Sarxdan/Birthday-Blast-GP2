using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //testing

public class Transition : MonoBehaviour
{
    public static Events.TransitionEvent onTransition;

    bool isInteracted = false;
    bool cantTriggerAgain = false;
    [SerializeField] Gamemanager.PlayerStates transitionTo = Gamemanager.PlayerStates.OnJetpack;
    [SerializeField][Tooltip("name of scene to load after transition, case sensitive")] string sceneToLoad = string.Empty;

    private void OnTriggerEnter(Collider other) {
        if(cantTriggerAgain) return;
        if(other.tag == "Player")
        {
            if(transitionTo == Gamemanager.PlayerStates.Onland)
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
        if(onTransition != null)
        {
            onTransition(sceneToLoad, transitionTo);
        }
    }

    void TransitionToIsland()
    {
        if(onTransition != null)
        {
            onTransition(sceneToLoad, transitionTo);
        }
    }
}

