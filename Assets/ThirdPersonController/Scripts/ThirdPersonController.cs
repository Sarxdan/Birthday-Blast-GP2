using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement),typeof(CameraController))]
public class ThirdPersonController : MonoBehaviour
{
    [HideInInspector] public Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    
    
    public Pewpew pewpew; //added

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

    [Range(-80,80)]
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

    Vector3 hitNormal;

    private void Awake()
    {
        camController = GetComponent<CameraController>();
        playerMovement = GetComponent<PlayerMovement>();
        pewpew = GetComponentInChildren<Pewpew>(); //added
        UpdateControllerVariables();
    }

    private void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        
        
        ToggleRagdoll(false);


        var landJetpack = PlayerPrefs.GetInt("LandJetpack") == 1;
        if (landJetpack)
        {
            StartCoroutine(LandJetpack());
        }
    }

    private IEnumerator LandJetpack()
    {
        ToggleControls(false);
        GetComponentInChildren<Animator>().SetTrigger("LandJetpack");
        FindObjectOfType<CinemachineVirtualCamera>().m_LookAt = GetComponentInChildren<Rigidbody>().transform;
        yield return new WaitForSeconds(2.5f);
        FindObjectOfType<CinemachineVirtualCamera>().m_LookAt = GameObject.FindGameObjectWithTag("CamLookAt").transform;
        ToggleControls(true);
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
        playerMovement.CheckIfGrounded(); //added
        pewpew.gameObject.SetActive(Gamemanager.instance.UnlockedItems.pewpew);
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


    public void ToggleControls(bool state)
    {
        disableCameraController = !state;
        disablePlayerMovement = !state;

        if (state == false)
        {
            //If disabled
            GetComponentInChildren<Animator>().SetFloat("Speed", 0);
        }
    }

    public void ToggleRagdoll(bool state)
    {
        GetComponentInChildren<Animator>().enabled = !state;

        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = state;
        }
        GetComponent<CharacterController>().enabled = !state;


        ToggleControls(!state);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.tag == "Boulder")
        {
            float slideSpeed = 1;
            hitNormal = hit.normal;
            disablePlayerMovement = true;
            Vector3 slideDirection = new Vector3();
            slideDirection.x = ((1f - hit.normal.y) * hit.normal.x) * slideSpeed;
            slideDirection.y = -9.81f;
            slideDirection.z = ((1f - hit.normal.y) * hit.normal.z) * slideSpeed;
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move(slideDirection * Time.deltaTime);
        }
        else
        {
            disablePlayerMovement = false;
        }
        
        
    }
}
