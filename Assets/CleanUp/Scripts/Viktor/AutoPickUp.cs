using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPickUp : MonoBehaviour
{
    public Resource resource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            resource.PickUpResource();
        }
    }
}
