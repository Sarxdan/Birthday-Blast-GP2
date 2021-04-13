using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable interactableObject;
    public float maxInteractRange = 5f;
    public LayerMask interactableMask;

    public void OnInteract()
    {
        if (ClosestInteractable() == null) return;
        
         interactableObject = ClosestInteractable();
         
         interactableObject.TryToInteract(this);
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
}
