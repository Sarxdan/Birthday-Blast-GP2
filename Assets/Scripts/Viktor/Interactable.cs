using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Serialization;
using UnityEngine.Localization.Settings;


public class Interactable : MonoBehaviour
{

    [SerializeField] KeyItems.Items requiredItems;
    public bool oneTimeInteraction = false;
    private bool interactedWith;
    public string interactText;
    public string interactTextEN = "*interact*";
    public string interactTextSE = "*interagera*";
    public string unableToInteracteText;
    public string unableToInteracteTextEN = "Can't interact!";
    public string unableToInteracteTextSE = "Kan inte interagera!";
    
    public float interactRadius = 5f;

    public float minimumTimeBetweenInteractions = 2f;
    private float timeSinceLastInteraction = 0f;

    [Header("Call function when player interacts with object")]
    public InteractEvent OnInteractEvent;

    public void Awake()
    {
        //Listen for language change event
        Events.onLanguageChanged += AdaptInteractTextToLanguage;
        
        //Start off by changing language to the selected one
        AdaptInteractTextToLanguage(LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.Instance.GetSelectedLocale()));
    }

    private void AdaptInteractTextToLanguage(int i)
    {
        switch (i)
        {
            case 0:
                interactText = interactTextEN;
                unableToInteracteText = unableToInteracteTextEN;
                break;
            case 1:
                interactText = interactTextSE;
                unableToInteracteText = unableToInteracteTextSE;
                break;
        }
    }

    public void TryToInteract(PlayerInteraction player)
    {
        Interact();
    }

    private void Update()
    {
        timeSinceLastInteraction += Time.deltaTime;
    }

    private void Interact()
    {
        interactedWith = true;
        timeSinceLastInteraction = 0;
        OnInteractEvent?.Invoke();
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);

    }

    //added functions

    public bool CheckRequiredItems() // this will look like shit, improve over iterations
    {

        bool meetsReqiurements = true;

        if(requiredItems.jetpack)
        {
            if(!Gamemanager.instance.UnlockedItems.jetpack) 
            {       
                meetsReqiurements = false;
            }
        }
        if(requiredItems.shovel)
        {
            if(!Gamemanager.instance.UnlockedItems.shovel) 
            {       
                meetsReqiurements = false;
            }
        }
        if(requiredItems.pewpew)
        {
            if(!Gamemanager.instance.UnlockedItems.pewpew) 
            {       
                meetsReqiurements = false;
            }
        }

        return meetsReqiurements;
    }


    public bool CanInteract()
    {
        if (oneTimeInteraction && interactedWith)
        {
            return false;
        }

        return timeSinceLastInteraction >= minimumTimeBetweenInteractions;

    }
}
