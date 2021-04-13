using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement),typeof(CameraController))]
public class ThirdPersonController : MonoBehaviour
{
    #region MovementControls

    [Header("Movement variables")]
    public float movementSpeed = 5f;


    public float jumpHeight = 2f;

    public float playerRotationSpeed = 5f;

    #endregion

    #region CameraControls

    [Header("Camera variables")] 
    [Range(1, 100)]
    public float cameraSensitivityX;
    
    [Range(1,100)]
    public float cameraSensitivityY;

    [Range(-50,50)]
    public float minVerticalRotation, maxVerticalRotation;


    public Vector3 cameraLookOffset;

    #endregion

    #region Gravity

    [Header("Gravity/ GroundCheck")]
    public float gravity = -9.81f;


    public bool isGrounded;

    [Tooltip("Decide what layers the player can jump on")]
    public LayerMask groundMask;

    [Tooltip("The distance to check for nearby ground")]
    public float groundDistanceCheck = 0.2f;

    #endregion

    #region Components

    [HideInInspector] public CameraController camController;
    [HideInInspector] public PlayerMovement playerMovement;

    [Header("Component management")]
    public bool disablePlayerMovement;

    public bool disableCameraController;

    #endregion

    private void Awake()
    {
        camController = GetComponent<CameraController>();
        playerMovement = GetComponent<PlayerMovement>();
        
        UpdateControllerVariables();
    }
    
    private void OnValidate()
    {
        UpdateControllerVariables();
    }

    private void UpdateControllerVariables()
    {
        //Make sure the character components aren't null
        if (playerMovement == null || camController == null) return;

        #region AssignMovementVariables

        playerMovement.movementSpeed = movementSpeed;

        playerMovement.jumpHeight = jumpHeight;

        playerMovement.gravity = gravity;

        playerMovement.isGrounded = isGrounded;

        playerMovement.playerRotationSpeed = playerRotationSpeed;

        playerMovement.groundMask = groundMask;

        playerMovement.groundDistanceCheck = groundDistanceCheck;

        #endregion

        #region AssignCameraVariables

        camController.horizontalSensitivity = cameraSensitivityX;

        camController.verticalSensitivity = cameraSensitivityY;

        camController.cameraOffset = cameraLookOffset;

        camController.minXRotation = minVerticalRotation;

        camController.maxXRotation = maxVerticalRotation;

        #endregion
    }

    private void Update()
    {
        if (disablePlayerMovement != true)
            DoPlayerMovement();

        if (disableCameraController != true)
            DoCameraMovement();
    }

    private void DoPlayerMovement()
    {
        playerMovement.Move();
    }

    private void DoCameraMovement()
    {
        camController.HandleCamera();
    }
}
