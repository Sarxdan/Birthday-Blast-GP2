using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNote : MonoBehaviour
{
    public GameObject popup;

    public void PopUpText()
    {
        UIManager.instance.EnablePopUp(popup);
    }
}
