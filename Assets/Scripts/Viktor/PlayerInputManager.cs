using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable] public class  MoveInputEvent : UnityEvent<float,float>{}
[Serializable] public class JumpInputEvent : UnityEvent{}
[Serializable] public class CameraRotateEvent : UnityEvent<float,float>{}
[Serializable] public class InteractEvent : UnityEvent{}

[Serializable] public class ShootEvent : UnityEvent{}

[Serializable] public class DashEvent : UnityEvent{}

public class PlayerInputManager : MonoBehaviour
{
    private Controls controls;


    #region Player Input Events
    
    public MoveInputEvent moveInputEvent;
    public JumpInputEvent jumpInputEvent;
    public CameraRotateEvent cameraRotateEvent;

    public InteractEvent interactEvent;
    public ShootEvent shootEvent;
    public DashEvent dashEvent;
    
    #endregion
    
    public GameObject[] MobileUIComponents;

    
    private void Awake()
    {
        controls = new Controls();
        
#if UNITY_STANDALONE
        EnableMobileComponents(false);
#else
        EnableMobileComponents(true);
#endif
    }
    
    private void EnableMobileComponents(bool enable)
    {
        foreach (var mobileUIComponent in MobileUIComponents)
        {
            if (mobileUIComponent)
            {
                mobileUIComponent.SetActive(enable);
            }
        }
    }

    private void OnEnable()
    {
        controls.GroundMovement.Enable();
        
        SubscribeToEvents();

#if UNITY_ANDROID || UNITY_IOS
        EnableMobileComponents(true);
#endif
    }

    private void OnDisable()
    {
        controls.GroundMovement.Disable();
        
        UnSubscribeFromEvents();

#if UNITY_ANDROID || UNITY_IOS
        EnableMobileComponents(false);
#endif
    }

    private void SubscribeToEvents()
    {
        //Movement
        controls.GroundMovement.Movement.performed += OnMove;
        controls.GroundMovement.Movement.canceled += OnMove;
        
        //Jump
        controls.GroundMovement.Jump.started += OnJump;

        //Camera
        controls.GroundMovement.CameraRotate.performed += OnCameraRotate;
        controls.GroundMovement.CameraRotate.canceled += OnCameraRotate;
        
        //Actions
        controls.GroundMovement.Interact.performed += OnInteract;
        controls.GroundMovement.Shoot.started += OnShoot;
        controls.GroundMovement.Dash.performed += OnDash;
    }

    private void UnSubscribeFromEvents()
    {
        controls.GroundMovement.Movement.performed -= OnMove;
        controls.GroundMovement.Movement.canceled -= OnMove;
        
        controls.GroundMovement.Jump.started -= OnJump;
        
        controls.GroundMovement.CameraRotate.performed -= OnCameraRotate;
        controls.GroundMovement.CameraRotate.canceled -= OnCameraRotate;

        controls.GroundMovement.Interact.performed -= OnInteract;
        controls.GroundMovement.Shoot.started -= OnShoot;
        controls.GroundMovement.Dash.performed -= OnDash;

    }

    #region MovementMethods

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInputEvent?.Invoke();
        }

    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();

        moveInputEvent?.Invoke(inputVector.x,inputVector.y);
    }

    private void OnCameraRotate(InputAction.CallbackContext context)
    {
        var rotationInput = context.ReadValue<Vector2>();
        
        cameraRotateEvent?.Invoke(rotationInput.x,rotationInput.y);
    }


    private void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("OnDash playerinputmanager");
            dashEvent?.Invoke();
        }
    }

    #endregion

    #region ActionMethods
    
    private void OnInteract(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            interactEvent?.Invoke();
        }
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shootEvent?.Invoke();
        }
    }
    
    #endregion
}
