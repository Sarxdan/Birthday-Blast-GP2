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
    PlayerStates currentState;
    [SerializeField] PlayerStates startingState = PlayerStates.OnJetpack;
    [SerializeField] bool switchStates = false;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CameraController CameraController;
    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] JetPack jetPack;

    private void Awake() {
        SwitchState(startingState);
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
            CameraController.enabled = false;
            thirdPersonController.enabled = false;
            jetPack.gameObject.SetActive(true);
            break;

            case PlayerStates.OnLand:
            playerMovement.enabled = true;
            CameraController.enabled = true;
            thirdPersonController.enabled = true;
            jetPack.gameObject.SetActive(false);
            break;

        }
    }
}
