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
    public Collider player;
    bool isInteracted = false;
    bool cantTriggerAgain = false;

    [SerializeField] PlayerStates transitionTo = PlayerStates.Jetpack;
    [SerializeField] Text transitionText;
    [SerializeField] float sceneTransitionTime = 1;
    [SerializeField][Tooltip("name of scene to load after transition, case sensitive")] string sceneToLoad = string.Empty;
    [SerializeField] bool transmit = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        if(cantTriggerAgain) return;
        if(other.tag == "Player")
        {
            player = other.GetComponent<Collider>();
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
        StateSwitcher switcher = player.GetComponent<StateSwitcher>();
        switcher.SwitchStates = true;
        StartCoroutine(TransitionToNewScene());
    }

    void TransitionToIsland()
    {
        //StateSwitcher switcher = player.GetComponent<StateSwitcher>();
        //switcher.SwitchStates = true;
        JetPack jetPack = player.GetComponentInChildren<JetPack>();
        jetPack.StartLanding();
        StartCoroutine(TransitionToNewScene());
    }

    IEnumerator TransitionToNewScene() 
    {
        yield return new WaitForSeconds(sceneTransitionTime);
        if(onTransitionEvent != null)
        {
            onTransitionEvent(sceneToLoad);
        }
    }
    
}

