using System;
using System.Collections;
using System.Collections.Generic;
using MiscUtil.Xml.Linq.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[Serializable] public class OnSteerEvent : UnityEvent<float,float>{}
[Serializable] public class OnDashEvent : UnityEvent{}
[Serializable] public class  OnActionEvent : UnityEvent{}

public class JetpackInputManager : MonoBehaviour
{
    private Controls controls;


    #region Player Input Events

    public OnSteerEvent onSteerEvent;
    public OnDashEvent onDashEvent;
    public OnActionEvent onActionEvent;
    
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
        controls.Jetpack.Steering.performed += OnSteer;
        controls.Jetpack.Steering.canceled += OnSteer;

        controls.Jetpack.Dash.started += OnDash;
        controls.Jetpack.Action.started += OnAction;
    }

    private void UnSubscribeFromEvents()
    {
        controls.Jetpack.Steering.performed -= OnSteer;
        controls.Jetpack.Steering.canceled -= OnSteer;

        controls.Jetpack.Dash.started -= OnDash;
        controls.Jetpack.Action.started -= OnAction;
    }

    #region MovementMethods

    private void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            onDashEvent?.Invoke();
        }
    }

    private void OnSteer(InputAction.CallbackContext context)
    {
        var steerInputValue = context.ReadValue<Vector2>();
        
        onSteerEvent?.Invoke(steerInputValue.x, steerInputValue.y);
    }

    

    #endregion

    #region ActionMethods
    
    private void OnAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            onActionEvent?.Invoke();
        }
    }
    
    #endregion
}
