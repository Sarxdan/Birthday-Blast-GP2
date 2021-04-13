using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwitcher : MonoBehaviour
{
    enum PlayerStates
    {
        OnJetpack,
        OnLand
    }
    public bool SwitchStates
    {
        set{switchStates = value;}
    }

    CapsuleCollider collider;
    CharacterController characterController;
    Camera jetpackCamera; //camera used for jetpack
    Camera playerCamera; //camera used for playercontroller
    PlayerStates currentState;
    [SerializeField] PlayerStates startingState = PlayerStates.OnJetpack;
    [SerializeField] bool switchStates = false;
    PlayerMovement playerMovement;
    CameraController cameraController;
    ThirdPersonController thirdPersonController;
    JetPack jetPack;

    private void Awake() {
        SetUpComponents();
        SwitchState(startingState);
    }

    void SetUpComponents()
    {
        characterController = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponent<CameraController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        jetPack = GetComponentInChildren<JetPack>();
        jetpackCamera = Camera.main;
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCam").GetComponent<Camera>();
        collider = GetComponent<CapsuleCollider>();
    }

    private void Update() {
        if(switchStates)
        {
            switch(currentState)
            {
                case PlayerStates.OnJetpack:
                SwitchState(PlayerStates.OnLand);
                break;
                case PlayerStates.OnLand:
                SwitchState(PlayerStates.OnJetpack);
                break;
            }
            switchStates = false;
        }
    }

    void SwitchState(PlayerStates state)
    {

        currentState = state;

        switch(currentState)
        {

            case PlayerStates.OnJetpack:           
            SetPlayerActive(false);
            SetJetpackActive(true);
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            break;

            case PlayerStates.OnLand:           
            SetPlayerActive(true);
            SetJetpackActive(false);
            break;

        }
    }
    void SetPlayerActive(bool isActive)
    {
        characterController.enabled = isActive;
        playerCamera.gameObject.SetActive(isActive);
        playerMovement.enabled = isActive;
        cameraController.enabled = isActive;
        thirdPersonController.enabled = isActive;
    }
    void SetJetpackActive(bool isActive)
    {
        collider.enabled = isActive;
        jetpackCamera.gameObject.SetActive(isActive);
        jetPack.gameObject.SetActive(isActive);        
    }
}
