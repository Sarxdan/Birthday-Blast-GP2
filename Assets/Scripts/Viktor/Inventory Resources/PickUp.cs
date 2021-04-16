using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool DestroyOnPickUp = true;

    protected void DestroyObject()
    {
        Destroy(gameObject);
    }
}
