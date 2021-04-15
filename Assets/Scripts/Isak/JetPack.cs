using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetPack : MonoBehaviour
{   
    public static Events.LoadSceneEvent onPlayerDeath;
    enum DashDirections
    {
        None,
        Left,
        Right
    }

    #region variables

    public bool AllowMovement
    {
        set{allowMovement = value;}
    }

    Rigidbody body; 
    Camera camera;
    bool isAutoBoosting = false;
    bool gameOver = false;
    float currentFuel = 0;
    Animator animator;  
    DashDirections dashDirections = DashDirections.None;
    bool dashOnCooldown = false;
    bool rightAxisPushed = false;
    bool leftAxisPushed = false;
    float lastKeyPressTime = 0;
    Pewpew pewpew;
    int health;

    [Header("movement settings")]
    [SerializeField] float moveSpeed = 1;   
    [SerializeField]float flightBoost = 1;
    [SerializeField] float autoMoveSpeed = 1;
    [SerializeField]float autoBoost = 20;  
    [SerializeField] ParticleSystem[] thrusters;   

    [Header("Camera settings")]
    [SerializeField] Vector3 cameraRotation = new Vector3(0,0,0);
    [SerializeField] Vector3 cameraOffsetFromPlayer = new Vector3(0,0,0);

    [Header("Ability settings")]
    [SerializeField]float dashSpeed = 10;
    [SerializeField]float DashLength = 0.2f;
    [SerializeField]float dashCooldown = 1;   
    [SerializeField][Tooltip("timer for using the dash ability")] float doubleTapTimer = 1;

    [Header("bool settings")]
    [SerializeField] bool dashUnlocked = false;
    [SerializeField] bool useFuel = false;
    [SerializeField] bool allowMovement = true;
    [SerializeField] bool useGravity = false;
    [SerializeField] bool pewpewUnlocked = false;

    [Header("health settings")]
    [SerializeField] int startingHealth;

    [Header("other settings")]
    [SerializeField][Tooltip("string reference, case sensitive")] string sceneToLoadOnDeath = string.Empty;
    [SerializeField][Tooltip("time until loading scene after death")] float sceneTransitionTime = 1;
    
    
    //Input values

    private float horizontalSteerInput;
    private float verticalSteerInput;

    #endregion

    void Awake()
    {       
        health = startingHealth;
        camera = Camera.main;
        body = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
        pewpew = GetComponentInChildren<Pewpew>();     
    }

    void Update() //vad ska hände när man får game over? falla ner en bit? UI uppdateras? 
    {
        if(gameOver) return;
        if(pewpew != null)
        {
            pewpew.gameObject.SetActive(pewpewUnlocked);
            pewpew.JetpackSpeed = autoMoveSpeed;
        }
        Move();
        Animate();    
        SetCameraPosition();
        GetDashInput();
        {
            if(Input.GetButtonDown("Horizontal")) //testing
            looseHealth(startingHealth); 
        }         
    }

    void looseHealth(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            StartCoroutine(Death());          
        }
    }

    IEnumerator Death()
    {
        gameOver = true;
        yield return new WaitForSeconds(sceneTransitionTime);
        if(onPlayerDeath != null)
        {
            onPlayerDeath(sceneToLoadOnDeath);
        }
    }

    void GetDashInput()
    {
        if(!dashUnlocked) return;
        if(dashOnCooldown) return;
        if(Input.GetButtonDown("Horizontal"))
        {
            if(Input.GetAxis("Horizontal") < 0)
            {
                        
                if(!leftAxisPushed)
                {
                    lastKeyPressTime = Time.time;
                    leftAxisPushed = true;                       
                }   
                else
                {
                    print("dash right");
                    dashDirections = DashDirections.Left;
                    dashOnCooldown = true; 
                    rightAxisPushed = false;
                    leftAxisPushed = false;
                    StartCoroutine(DashInDirection(dashDirections));
                }                                      
            }
            else if(Input.GetAxis("Horizontal") > 0)
            {
                if(!rightAxisPushed)
                {
                    lastKeyPressTime = Time.time;
                    rightAxisPushed = true;   
                    
                }   
                else
                {
                    dashDirections = DashDirections.Right;
                    print("dash left");
                    dashOnCooldown = true; 
                    rightAxisPushed = false;
                    leftAxisPushed = false;
                    StartCoroutine(DashInDirection(dashDirections));
                }            
            }       
        }
                     
        if(Time.time - lastKeyPressTime > doubleTapTimer)
        {
            rightAxisPushed = false;
            leftAxisPushed = false;
        }             
    }

    IEnumerator DashInDirection(DashDirections directions)
    {
        float dashlengthLeft = DashLength;
        //-------------------------------create temporary variables
        Vector3 movement = new Vector3();
        float cooldown = dashCooldown;
        //---------------------------------start the dash ability
        while(dashlengthLeft > 0)
        {           
            switch(directions)
            {
                case DashDirections.Left:
                movement = Vector3.left * dashSpeed;
                movement.z = body.velocity.z;
                body.velocity = movement;
                break;

                case DashDirections.Right:
                movement = Vector3.right * dashSpeed;
                movement.z = body.velocity.z;
                body.velocity = movement;
                break;

                default:
                break;
            }
            dashlengthLeft -= Time.deltaTime;
            cooldown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //-------------------------------------------count the remaining cooldown after dashing
        while(cooldown > 0)
        {
            yield return new WaitForEndOfFrame();
            cooldown -= Time.deltaTime;
        }
        dashOnCooldown = false;
    }

    void Animate()
    {
        if(verticalSteerInput > 0 || horizontalSteerInput != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void AutoBoost() //change to use events?
    {
        if(gameOver) return;
        //----------------------------------------------------------makes the player boost upwards, useful to make sure the player does not fall too far
        isAutoBoosting = true;   
        Vector3 boostMovement = new Vector3();
        boostMovement.y = autoBoost * flightBoost;  
        body.velocity = boostMovement;  
        isAutoBoosting = false;
    }

    void Move()
    {
        if(isAutoBoosting || !allowMovement) return;
        //----------------------------------------------------get all movement inputs
        var movement = new Vector3();
        movement.z = autoMoveSpeed;
        movement.x = horizontalSteerInput * moveSpeed; 
        movement.y = verticalSteerInput * flightBoost;

        //----------------------------------------------------activate the thrusters
        if(verticalSteerInput > 0) 
        {                  
            foreach(ParticleSystem thruster in thrusters)
            {
                thruster.Play();
            }
        }    

        //---------------------------------------------------add some extra logics if using gravity or not
        if(useGravity)
            {
                movement.y += body.velocity.y;       
            }
            else
            {
                movement.y *= -Physics.gravity.y;
            }      
        body.velocity = movement;
    }

    void SetCameraPosition()
    {
        camera.transform.rotation = Quaternion.Euler(cameraRotation);
        camera.transform.position = transform.position + cameraOffsetFromPlayer;
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    #region Inputs

    public void OnSteerInput(float horizontal, float vertical)
    {
        horizontalSteerInput = horizontal;
        verticalSteerInput = vertical;
    }

    public void OnDashInput()
    {
        var direction = horizontalSteerInput > 0 ? DashDirections.Right : DashDirections.Left;

        StartCoroutine(DashInDirection(direction));
    }

    public void OnActionInput()
    {
        Debug.Log("Jetpack ACTION");
    }

    #endregion
}
