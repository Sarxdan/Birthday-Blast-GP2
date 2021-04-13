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
    Camera camera;
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
        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponent<CameraController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        jetPack = GetComponentInChildren<JetPack>();
        camera = Camera.main;
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
            
            playerMovement.enabled = false;
            cameraController.enabled = false;
            thirdPersonController.enabled = false;
            jetPack.gameObject.SetActive(true);
            camera.gameObject.SetActive(true);
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            break;

            case PlayerStates.OnLand:
            playerMovement.enabled = true;
            cameraController.enabled = true;
            thirdPersonController.enabled = true;
            jetPack.gameObject.SetActive(false);
            camera.gameObject.SetActive(false);
            break;

        }
    }
}
