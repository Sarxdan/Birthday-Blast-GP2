using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject popUp;


    public void InteractWithPopUp()
    {
        UIManager.instance.EnablePopUp(popUp);
    }
}
