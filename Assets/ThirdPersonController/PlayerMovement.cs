using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private CharacterController controller;
    private CameraController camController;

    //Movement fields
    public float movementSpeed;
    public float jumpHeight;
    public float playerRotationSpeed = 5f;
    
    
    //Ground check
    public bool isGrounded;
    public float gravity = -9.81f;
    
    
    //Variables used in Physics.CheckSphere()
    [SerializeField] private Transform groundCheckPosition;
    public float groundDistanceCheck;
    public LayerMask groundMask;

    //velocity used to simulate gravity.
    private Vector3 velocity;
    //Forward direction relative to the camera.
    private Vector3 forwardLookDir;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camController = GetComponent<CameraController>();
    }
    
    public void Movement()
    {
        //Check if player is currently in contact with objects from certain layers
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundDistanceCheck, groundMask);
        
        //Reset velocity if grounded
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;
        
        
        velocity.y += gravity * Time.deltaTime;
        
        //Get input and store into values
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        
        //Get the forward direction from the camera
        forwardLookDir = camController.forwardLookDir;

        //Direction of movement, (normalized)/unit vector
        var movementDirection =
            (forwardLookDir * verticalInput + camController.cameraLookDirectionTransform.right * horizontalInput)
            .normalized;

        //Move in direction * movementSpeed
        controller.Move(movementDirection * (Time.deltaTime * movementSpeed));
        
        
        if(Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        //Gravity
        controller.Move(velocity * Time.deltaTime);
        
        //Rotate player towards movement
        RotatePlayerTowardsDirection(movementDirection);
    }

    private void RotatePlayerTowardsDirection(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        //Slerp : smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * playerRotationSpeed);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

}
