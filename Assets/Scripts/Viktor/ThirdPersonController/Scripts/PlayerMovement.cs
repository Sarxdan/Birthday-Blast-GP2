using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static Events.EmptyEvent playerStuck;
    //Components
    public Animator animator;
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
    public Transform groundCheckPosition;
    public float groundDistanceCheck;
    public LayerMask groundMask;

    //velocity used to simulate gravity.
    public Vector3 velocity;
    //Forward direction relative to the camera.
    private Vector3 forwardLookDir;
    
    public float countedTime = 0; //counter that checks if player gets stuck
    public Vector3 lastCountedPlayerPosition;
    
    
    //Input values
    private float horizontal;
    private float vertical;


    bool isSliding = false;
    Vector3 hitNormal;

    private void Start()
    {
        lastCountedPlayerPosition = transform.position;
        controller = GetComponent<CharacterController>();
        camController = GetComponent<CameraController>();

        animator = GameObject.FindGameObjectWithTag("CharModel").GetComponent<Animator>();
    }

    private void Update() {
        if(!isGrounded)
        {
            countedTime += Time.deltaTime;
            if(countedTime > 5)
            {
            
            if(lastCountedPlayerPosition == transform.position)
            {
                if(playerStuck != null)
                {
                    playerStuck();
                }
            }
            countedTime = 0;
            lastCountedPlayerPosition = transform.position;
            }
        }
        else
        {
            countedTime = 0;
        }
    }
    
    public void FetchMovementInput(float _horizontal, float _vertical)
    {
        
        horizontal = _horizontal;
        vertical = _vertical;
        
        
    }

    public void CheckIfGrounded() //added
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundDistanceCheck, groundMask);
    }

    public void Move()
    {
        //Check if player is currently in contact with objects from certain layers
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundDistanceCheck, groundMask) && !isSliding;
        if (animator != null)
        {
            animator.SetBool("isGrounded", isGrounded);
        }

        //Reset velocity if grounded

        if (isGrounded && velocity.y < 0 && !isSliding)
        velocity.y = 0f;
        
        velocity.y += gravity * Time.deltaTime;
        

        //Get the forward direction from the camera
        forwardLookDir = camController.forwardLookDir;
        //Direction of movement, (normalized)/unit vector
        var movementDirection =
            (forwardLookDir * vertical + camController.cameraLookDirectionTransform.right * horizontal)
            .normalized;
        if(isSliding)
        {
            float slideFriction = 0.5f;
            movementDirection.x = (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
            movementDirection.z = (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);
        }
        else
        {
            //Rotate player towards movement
            RotatePlayerTowardsDirection(movementDirection);
        }
        //Move in direction * movementSpeed
        controller.Move(movementDirection * (Time.deltaTime * movementSpeed));
        

        //Gravity
        controller.Move(velocity * Time.deltaTime);
        
        if(isGrounded && !AudioManager.instance.IsPlaying("PlayerWalk"))
        {
            if(vertical != 0 || horizontal != 0)
            {
                AudioManager.instance.Play("PlayerWalk"); 
            }            
        }

        if (animator == null) return;
        
        animator.SetFloat("Speed",Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        animator.SetBool("isFalling", velocity.y < -0.5);
        animator.SetFloat("Velocity", velocity.y);
    }

    private void RotatePlayerTowardsDirection(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        //Slerp : smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * playerRotationSpeed);
    }

    public void TryJump()
    {
        if(isGrounded)
            Jump();
    }
    
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        AudioManager.instance.Play("PlayerJump");

        animator.SetTrigger("Jump");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        float slopeAngle = Vector3.Angle(Vector3.up, hit.normal);
        if(slopeAngle >= controller.slopeLimit && slopeAngle < 80)
        {      
            isSliding = true;
            hitNormal = hit.normal;
        }
        else
        {
            hitNormal = new Vector3(0,0,0);
            isSliding = false;
        }
    }
}
