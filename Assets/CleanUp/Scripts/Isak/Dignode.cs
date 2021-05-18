using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dignode : Resource
{
    [SerializeField] GameObject emptyDignode;
    bool isInteracted = false;
    public override void PickUpResource()
    {
        if(isInteracted) return;
        isInteracted = true;
        base.PickUpResource();
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        emptyDignode.gameObject.SetActive(true);
        GetComponent<Interactable>().enabled = false;
    }
}
