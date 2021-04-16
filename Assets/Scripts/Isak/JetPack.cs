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
        Right,
        Forward
    }

    #region variables

    public bool Invulnerable
    {
        get{return invulnerable;}
    }

    Rigidbody body; 
    Camera camera;
    bool isAutoBoosting = false;
    bool gameOver = false;
    Animator animator;  
    DashDirections dashDirections = DashDirections.None;
    bool dashOnCooldown = false;
    bool forwardDashOnCooldown = false;
    bool rightAxisPushed = false;
    bool leftAxisPushed = false;
    bool forwardAxisPushed = false;
    bool invulnerable = false;
    float lastKeyPressTime = 0;
    Pewpew pewpew;
    int health;
    float autoMoveSpeed;

    [Header("movement settings")]
    [SerializeField] float moveSpeed = 1;   
    [SerializeField]float flightBoost = 1;
    [SerializeField] float startingAutoMoveSpeed = 1;
    [SerializeField] float autoMoveSpeedIncreaseOverTime = 1;
    [SerializeField] float maxAutoMoveSpeed = 1;  

    [Header("Camera settings")]
    [SerializeField] Vector3 cameraRotation = new Vector3(0,0,0);
    [SerializeField] Vector3 cameraOffsetFromPlayer = new Vector3(0,0,0);

    [Header("Ability settings")]
    [SerializeField]float dashSpeed = 10;
    [SerializeField]float dashLength = 0.2f;
    [SerializeField]float dashCooldown = 1;  
    [SerializeField]float forwardDashSpeed = 10;
    [SerializeField][Tooltip("timer for using the dash ability")] float doubleTapTimer = 1;

    [Header("bool settings")]
    [SerializeField] bool dashUnlocked = false;
    [SerializeField] bool forwardDashUnlocked = false;
    [SerializeField] bool pewpewUnlocked = false;

    [Header("health settings")]
    [SerializeField] int startingHealth = 10;

    [Header("other settings")]
    [SerializeField][Tooltip("string reference, case sensitive")] string sceneToLoadOnDeath = string.Empty;
    [SerializeField][Tooltip("time until loading scene after death")] float sceneTransitionTime = 1;
    
    
    //Input values

    private float horizontalSteerInput;
    private float verticalSteerInput;

    #endregion

    void Awake()
    {       
        autoMoveSpeed = startingAutoMoveSpeed;
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
        float moveSpeedIncreasePerFrame = Time.deltaTime * autoMoveSpeedIncreaseOverTime;
        IncreaseAutoMoveSpeed(moveSpeedIncreasePerFrame);      
    }

    void IncreaseAutoMoveSpeed(float amount)
    {
        autoMoveSpeed += amount;
        if(autoMoveSpeed >= maxAutoMoveSpeed)
        {
            autoMoveSpeed = maxAutoMoveSpeed;
        }
    }

    void looseHealth(int amount)
    {
        if(invulnerable) return;
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
        if(dashOnCooldown) return;

        if(Input.GetButtonDown("Horizontal"))
        {

            if(!dashUnlocked) return;
            
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
                    ResetAxisBools();
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
                    print("dash left");
                    dashDirections = DashDirections.Right;                   
                    dashOnCooldown = true; 
                    ResetAxisBools();
                    StartCoroutine(DashInDirection(dashDirections));
                }            
            }       
        }
        if(Input.GetButtonDown("Vertical"))
        {

            if(!forwardDashUnlocked) return;

            if(Input.GetAxis("Vertical") > 0)
            {
                if(!forwardAxisPushed)
                {
                    lastKeyPressTime = Time.time;
                    forwardAxisPushed = true;   
                    
                }   
                else
                {
                    print("dash forward");
                    dashDirections = DashDirections.Forward;   
                    forwardDashUnlocked = true; 
                    ResetAxisBools();
                    StartCoroutine(DashInDirection(dashDirections));
                } 
            }
        }
                     
        if(Time.time - lastKeyPressTime > doubleTapTimer)
        {
            ResetAxisBools();
        }             
    }

    void ResetAxisBools()
    {
        rightAxisPushed = false;
        leftAxisPushed = false;
        forwardAxisPushed = false;
    }

    IEnumerator DashInDirection(DashDirections directions)
    {
        float dashlengthLeft = dashLength;
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

                case DashDirections.Forward:
                movement = Vector3.forward * forwardDashSpeed;
                movement.z += body.velocity.z;
                body.velocity = movement;
                invulnerable = true;
                break;

                default:
                break;
            }
            dashlengthLeft -= Time.deltaTime;
            cooldown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            invulnerable = false;
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
        if(horizontalSteerInput != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    } 
       void Move()
    {

        var movement = new Vector3();
        movement.z = autoMoveSpeed;
        movement.x = horizontalSteerInput * moveSpeed; 
     
        body.velocity = movement;
    }

    void SetCameraPosition()
    {
        camera.transform.rotation = Quaternion.Euler(cameraRotation);
        camera.transform.position = transform.position + cameraOffsetFromPlayer;
    }

    private void OnEnable() {
        DamagePlayer.onPlayerCollision += looseHealth;
    }

    private void OnDisable() {
        StopAllCoroutines();
        DamagePlayer.onPlayerCollision -= looseHealth;
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
