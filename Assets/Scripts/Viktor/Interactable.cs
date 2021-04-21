using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Serialization;


public class Interactable : MonoBehaviour
{

    [SerializeField] KeyItems.Items requiredItems;
    public string interactText = "*interact*";
    public string unableToInteracteText = "Can't interact!";
    
    public float interactRadius = 5f;

    public float minimumTimeBetweenInteractions = 2f;
    private float timeSinceLastInteraction = 0f;
    
    [Header("Call function when player interacts with object")]
    public InteractEvent OnInteractEvent;

    public void TryToInteract(PlayerInteraction player)
    {
        var distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if (distanceFromPlayer <= interactRadius && timeSinceLastInteraction >= minimumTimeBetweenInteractions)
        {
            Interact();
        }
    }

    private void Update()
    {
        timeSinceLastInteraction += Time.deltaTime;
    }

    private void Interact()
    {
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

        if(requiredItems.jetpack != Gamemanager.instance.unlockedItems.jetpack) 
        {       
            meetsReqiurements = false;
        }
        if(requiredItems.pewpew != Gamemanager.instance.unlockedItems.pewpew)
        {
            meetsReqiurements = false;
        }
        if(requiredItems.shovel != Gamemanager.instance.unlockedItems.shovel)
        {
            meetsReqiurements = false;
        }
        
        return meetsReqiurements;
    }
}
