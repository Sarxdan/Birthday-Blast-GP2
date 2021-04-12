using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class InteractEvent : UnityEvent {}

public class Interactable : MonoBehaviour
{
    public KeyCode interactKeyBind;
    public float interactRange = 5f;

    [Header("Player can interact with this object once every X seconds")]
    public float timeBetweenInteractions = 1;

    private float timeSinceLastInteraction = 0;
    

    [Header("Call function when player interacts with object")]
    public InteractEvent OnInteractEvent;


    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GetPlayer();
    }

    private void Update()
    {
        timeSinceLastInteraction += Time.deltaTime;
        
        if(Input.GetKeyDown(interactKeyBind))
            TryToInteract();
        
    }

    private void TryToInteract()
    {
        if(CanInteract() == false) return;

        if (playerTransform == false)
            playerTransform = GetPlayer();
        
        Interact();
    }

    private void Interact()
    {
        timeSinceLastInteraction = 0;

        OnInteractEvent?.Invoke();
    }

    private bool CanInteract()
    {
        return InRange(Vector3.Distance(transform.position, playerTransform.position)) 
               &&
               timeSinceLastInteraction >= timeBetweenInteractions;

    }
    
    private bool InRange(float distance)
    {
        return distance <= interactRange;
    }

    private Transform GetPlayer()
    {
        return FindObjectOfType<ThirdPersonController>().transform;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}