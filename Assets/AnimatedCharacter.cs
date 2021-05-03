using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPosition;
    public Vector3 randomizedDirection;
    public float playerRotationSpeed = 5f;
    public float vertical;
    public float horizontal;
    public Vector3 forwardLookDir;
    public float jumpHeight;
    public float groundDistanceCheck;
    public float movementSpeed;
    float gravity = -9.81f;
    Vector3 velocity;
    CharacterController controller;
    public bool isGrounded;
    public Animator animator;
    public float timeBetweenRandomization;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenRandomization = Random.Range(4, 10);
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(RandomizeMovement());
    }
    private void Update() {
        Move();       
    }

    IEnumerator RandomizeMovement()
    {
        while(true)
        {
            float dividedTime = Random.Range(0, timeBetweenRandomization);
            float remainingTime = timeBetweenRandomization - dividedTime;
            yield return new WaitForSeconds(dividedTime);
            vertical = Random.Range(0, 2);
            horizontal = Random.Range(0, 2);
            yield return new WaitForSeconds(remainingTime);
            vertical = 0;
            horizontal = 0;
            //randomizedDirection = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360));
            transform.Rotate(randomizedDirection);
            TryJump();
        }
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

    public void Move()
    {
        //Check if player is currently in contact with objects from certain layers
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundDistanceCheck, LayerMask.GetMask("Ground"));
        
        //Reset velocity if grounded

        if (isGrounded && velocity.y < 0)
        velocity.y = 0f;
        
        velocity.y += gravity * Time.deltaTime;
        

        //Get the forward direction from the camera
        forwardLookDir = randomizedDirection;
        

        //Direction of movement, (normalized)/unit vector
        var movementDirection = new Vector3(horizontal, 0, vertical);
        
            //(forwardLookDir * vertical * horizontal)
            //.normalized;

        //Move in direction * movementSpeed
        controller.Move(movementDirection * (Time.deltaTime * movementSpeed));
        print(movementDirection);
        

        //Gravity
        controller.Move(velocity * Time.deltaTime);
        
        //Rotate player towards movement
        RotatePlayerTowardsDirection(movementDirection);
                
        animator.SetFloat("Velocity", velocity.y);
    }

    private void RotatePlayerTowardsDirection(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        //Slerp : smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * playerRotationSpeed);
    }
}
