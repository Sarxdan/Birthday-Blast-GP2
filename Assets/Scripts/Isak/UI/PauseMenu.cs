using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static Events.EmptyEvent onResumeClicked;
    public GameObject pcControlRoot;
    public GameObject mobileControlRoot;
    private GameObject rootControl;
    public GameObject[] pcControls;
    public GameObject[] mobileControls;

    private GameObject[] controlArray;
    private int currControlIndex = 0;

    private void Awake()
    {
#if UNITY_STANDALONE
        controlArray = pcControls;
        rootControl = pcControlRoot;
#else
        controlArray = mobileControls;
        rootControl = mobileControlRoot;
#endif
        
        
        ToggleObjects(controlArray, false);
        ToggleObject(controlArray[currControlIndex],true);
    }

    public void Resume ()
    {
        if(onResumeClicked != null)
        {
            onResumeClicked();
        }
    }

    public void QuitGame()
    {
        print("quitting game");
        Application.Quit();
    }

    public void ToggleControlGuide(bool toggle)
    {
        rootControl.SetActive(toggle);
        EnableControl(currControlIndex,toggle);
    }
    
    public void ChangeControlPage(int amount)
    {
        if (currControlIndex == controlArray.Length - 1 && amount == 1)
        {
            //Max index
            currControlIndex = 0;
        }
        else if (currControlIndex == 0 && amount == -1)
        {
            currControlIndex = controlArray.Length - 1;
        }
        else
        {
            currControlIndex += amount;
        }
        
        EnableControl(currControlIndex,true);
    }

    private void ToggleObjects(GameObject[] objArr, bool toggle)
    {
        foreach (var o in objArr)
        {
            o.SetActive(toggle);
        }
    }

    private void ToggleObject(GameObject obj, bool toggle)
    {
        obj.SetActive(toggle);
    }

    private void EnableControl(int index, bool toggle)
    {
        ToggleObjects(controlArray,!toggle);
        ToggleObject(controlArray[index],toggle);
    }
   
}
