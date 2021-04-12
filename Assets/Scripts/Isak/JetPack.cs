using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{   
    enum DashDirections
    {
        None,
        Left,
        Right
    }

    #region variables

    Rigidbody body;  
    Camera camera;
    bool isAutoBoosting = false;
    bool gameOver = false;
    public float currentFuel = 0;
    Animator animator;  
    DashDirections dashDirections = DashDirections.None;
    bool dashOnCooldown = false;
    bool rightAxisPushed = false;
    bool leftAxisPushed = false;
    float lastKeyPressTime = 0;
    Pewpew pewpew;

    [Header("movement settings")]
    [SerializeField] float moveSpeed = 1;   
    [SerializeField][Range(0.1f, 2)] float flightBoost = 1;
    [SerializeField] float autoMoveSpeed = 1;
    [SerializeField][Range(20, 40)] float autoBoost = 20;  
    [SerializeField] ParticleSystem[] thrusters;   

    [Header("Camera settings")]
    [SerializeField] Vector3 cameraRotation = new Vector3(0,0,0);
    [SerializeField] Vector3 cameraOffsetFromPlayer = new Vector3(0,0,0);

    [Header("Fuel settings")]
    [SerializeField][Range(100, 1000)] float startingFuel = 100;
    [SerializeField][Range(0.1f, 2)] float fuelUsageOnMovement = 0.1f;
    [SerializeField][Range(2, 5)] float fuelUsageOnAbilities = 2;    

    [Header("Ability settings")]
    [SerializeField][Range(1, 100)] float dashSpeed = 10;
    [SerializeField][Range(0.1f, 1)] float DashLength = 0.2f;
    [SerializeField][Range(0.1f, 10)] float dashCooldown = 1;   
    [SerializeField][Range(0.1f, 1)][Tooltip("timer for using the dash ability")] float doubleTapTimer = 1;

    [Header("bool settings")]
    [SerializeField] bool dashUnlocked = false;
    [SerializeField] bool useFuel = false;
    [SerializeField] bool allowMovement = true;
    [SerializeField] bool useGravity = false;
    [SerializeField] bool pewpewUnlocked = false;

    #endregion

    void Awake()
    {       
        camera = Camera.main;
        currentFuel = startingFuel;
        body = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
        pewpew = transform.parent.GetComponentInChildren<Pewpew>();        
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
        UseFuel();
        SetCameraPosition();
        GetDashInput();
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
        if(Input.GetAxis("Jump") > 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void UseFuel()
    {
        if(!useFuel) return;
        if(Input.GetAxis("Jump") > 0 || Input.GetAxis("Horizontal") != 0)
        {
            currentFuel -= fuelUsageOnMovement * Time.deltaTime;
            checkFuel();
        }
        else if(dashOnCooldown)
        {
            currentFuel -= fuelUsageOnAbilities * Time.deltaTime;
            checkFuel();
        }
    }

    public void LooseFuel(float amount)
    {
        if(!useFuel) return;
        currentFuel -= amount;
        checkFuel();
    }

    void checkFuel() //helping function to check if fuel is zero
    {
        if(currentFuel <= 0)
        {
            gameOver = true;
        }
    }

    void RefillFuel(float amount) //--------------------needs a way to stop refuelling when at max fuel so the player wont waste resources
    {
        currentFuel += amount;
        if(currentFuel >= startingFuel)
        {
            currentFuel = startingFuel;
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
        Vector3 movement = new Vector3();
        movement.z = autoMoveSpeed;
        movement.x = Input.GetAxis("Horizontal") * moveSpeed; 
        movement.y = Input.GetAxis("Jump") * flightBoost;

        //----------------------------------------------------activate the thrusters
        if(Input.GetAxis("Jump") > 0) 
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
}
