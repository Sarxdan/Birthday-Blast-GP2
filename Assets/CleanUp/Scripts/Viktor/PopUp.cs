using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public bool enableOnStart = false;
    public GameObject popUp;


    private void Start()
    {
        if (enableOnStart)
        {
            InteractWithPopUp();
        }
    }

    public void InteractWithPopUp()
    {
        UIManager.instance.EnablePopUp(popUp);
    }
}
