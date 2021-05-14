using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public float horizontalSensitivity = 100f;
    [HideInInspector]
    public float verticalSensitivity = 50f;

    //Limit the vertical rotation of the camera
    public float minXRotation = -50f;
    public float maxXRotation = 50f;
    
    //Reference to the camera used for the ThirdPersonController
    private Transform mainCamera;
    
    public Transform cameraLookAtTarget;
    
    //The transform to get the forwardLookDir from
    public Transform cameraLookDirectionTransform;
    
    //Forward direction of the camera
    [HideInInspector] public Vector3 forwardLookDir;

    //Offset from the LookAtTarget
    [HideInInspector] public Vector3 cameraOffset;


    //Turn all the transforms into children OnEnable
    //Easier to use, 1 object, 1 controller. Drag on Drop Controller into scene
    public Transform[] TransformToChildren;

    private float xRotation = 0f;

    //Inputs
    private float mouseXInput;
    private float mouseYInput;

    bool invertMouse = false;

    private void Awake() {
        OnMouseSensitivityChanged();
        OnMouseInverted();
    }

    public void OnMouseSensitivityChanged()
    {
        horizontalSensitivity = PlayerManager.instance.horizontalSensitivity * PlayerManager.instance.mouseSensitivityMultiplier;
        verticalSensitivity = PlayerManager.instance.verticalSensitivity * PlayerManager.instance.mouseSensitivityMultiplier;
    }
    public void OnMouseInverted()
    {
        invertMouse = PlayerManager.instance.invertMouse;
    }

    private void OnEnable()
    {
        
        #if UNITY_STANDALONE
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        #endif

        if (mainCamera == null)
            mainCamera = GameObject.FindGameObjectWithTag("PlayerCam").transform;
        
        if (TransformToChildren.Length > 0)
        {
            foreach (var obj in TransformToChildren)
            {
               obj.SetParent(null);
            }
        }
    }


    public void FetchCameraInput(float mouseX, float mouseY)
    {
        mouseXInput = mouseX * horizontalSensitivity;
        mouseYInput = mouseY * verticalSensitivity;
        
    }

    private bool FingerOnScreen()
    {
        return Input.touchCount > 0;
    }
    
    private bool Finger0OnUI()
    {
        return FingerOnScreen() && EventSystem.current.IsPointerOverGameObject(0);
    }

    private bool Finger1OnUI()
    {
        return FingerOnScreen() && EventSystem.current.IsPointerOverGameObject(1);
    }
    
    private bool CanRotateCameraOnMobile()
    {
        if(FingerOnScreen())
            return !Finger0OnUI() || !Finger1OnUI();


        return false;
    }
    
    public void HandleCamera()
    {

        
#if UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            if (CanRotateCameraOnMobile())
            {
                if (Finger0OnUI() == false)
                {
                    var deltaTouchPos = Input.GetTouch(0).deltaPosition;

                    mouseXInput = deltaTouchPos.x * horizontalSensitivity;
                    mouseYInput = -deltaTouchPos.y * verticalSensitivity;
                }
                else if (Finger1OnUI())
                {
                    var deltaTouchPos = Input.GetTouch(1).deltaPosition;

                    mouseXInput = deltaTouchPos.x * horizontalSensitivity;
                    mouseYInput = -deltaTouchPos.y * verticalSensitivity;
                }
            }
            else
            {
                mouseXInput = 0;
                mouseYInput = 0;
            }
        }
        else
        {
            
            //Reset rotation movement to 0 when not touching the screen
            mouseXInput = 0;
            mouseYInput = 0;
        }
#endif
        
        
        cameraLookDirectionTransform.rotation = Quaternion.Euler(0f,mainCamera.eulerAngles.y,0f);

        forwardLookDir = cameraLookDirectionTransform.forward;
        
        
        
        xRotation += mouseYInput * Time.deltaTime;

        var yRotation = cameraLookAtTarget.localEulerAngles.y;
        yRotation += mouseXInput * Time.deltaTime;
        
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        if(invertMouse)
        {
            cameraLookAtTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
        else
        {
            cameraLookAtTarget.rotation = Quaternion.Euler(-xRotation, yRotation, 0f);
        }
        
        
        

        //LookAtTarget follows player
        cameraLookAtTarget.position = (transform.position + new Vector3(0,0.5f,0)) + cameraOffset;

    }

}
