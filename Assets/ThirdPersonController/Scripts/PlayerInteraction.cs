using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable interactableObject;
    public float maxInteractRange = 5f;
    public LayerMask interactableMask;
    
    public TextMeshProUGUI interactTextUI;

    private void Update()
    {
        if (ClosestInteractable() && InInteractDistance(ClosestInteractable()))
        {
            if (ClosestInteractable().CanInteract())
            {
                if (ClosestInteractable().CheckRequiredItems() == false)
                {
                    //Cant interact because of unlocked items/Abilites
                    interactTextUI.text = ClosestInteractable().unableToInteracteText;
                }
                else
                {
                    //Can interact
                    interactTextUI.text = ClosestInteractable().interactText;
                }
            }
            else
            {
                interactTextUI.text = "";
            }
        }
        else
        {
            interactTextUI.text = "";
        }
    }

    public void OnInteract()
    {
        if (ClosestInteractable() == null)
        {
            return;
        }

        interactableObject = ClosestInteractable();

        if (interactableObject.CheckRequiredItems())
        {
            interactableObject.TryToInteract(this);
        }
    }
    
    private Interactable ClosestInteractable()
    {
        var interactablesNearby = Physics.OverlapSphere(transform.position, maxInteractRange, interactableMask);

        if (interactablesNearby.Length <= 0) return null;
        
        var closestInteractable = interactablesNearby[0].GetComponent<Interactable>();
        var closestDistance = Vector3.Distance(transform.position, interactablesNearby[0].transform.position);
        
        foreach (var collider in interactablesNearby)
        {
            var interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                var localDistance = Vector3.Distance(transform.position, interactable.transform.position);
                if (localDistance < closestDistance)
                {
                    closestDistance = localDistance;
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }


    private bool InInteractDistance(Interactable closestInteractable)
    {
        var interactDistance = closestInteractable.interactRadius;
        return Vector3.Distance(closestInteractable.transform.position, transform.position) <= interactDistance;
    }
}
