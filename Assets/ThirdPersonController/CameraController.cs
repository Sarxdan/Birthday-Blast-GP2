using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraController : MonoBehaviour
{
    public float horizontalSensitivity = 100f;
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


    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        
        if (mainCamera == null)
            mainCamera = Camera.main.transform;
        
        if (TransformToChildren.Length > 0)
        {
            foreach (var obj in TransformToChildren)
            {
               obj.SetParent(null);
            }
        }
    }


    public void HandleCamera()
    {
        cameraLookDirectionTransform.rotation = Quaternion.Euler(0f,mainCamera.eulerAngles.y,0f);

        forwardLookDir = cameraLookDirectionTransform.forward;

        var mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
        
        
        xRotation += mouseY * Time.deltaTime;

        var yRotation = cameraLookAtTarget.localEulerAngles.y;
        yRotation += mouseX * Time.deltaTime;
        
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
        
        cameraLookAtTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        

        //LookAtTarget follows player
        cameraLookAtTarget.position = transform.position + cameraOffset;

    }

}
