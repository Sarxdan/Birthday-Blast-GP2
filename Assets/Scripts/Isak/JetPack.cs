using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetPack : MonoBehaviour
{   

    public static Events.FuelEvent onFuelUse;
    public static Events.FuelEvent onJetpackAwake;

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
    public bool GameOver
    {
        get{return gameOver;}
    }
    public float AutoMoveSpeed
    {
        get{return autoMoveSpeed;} //added
    }

    Rigidbody body; 
    Camera camera;
    bool isAutoBoosting = false;
     bool gameOver = false;
    DashDirections dashDirections = DashDirections.None;
    bool dashOnCooldown = false;
    bool forwardDashOnCooldown = false;
    bool rightAxisPushed = false;
    bool leftAxisPushed = false;
    bool forwardAxisPushed = false;
    bool invulnerable = false;
    float lastKeyPressTime = 0;
    Pewpew pewpew;
    float autoMoveSpeed;

    bool fuelEmpty = false;
    bool useFuel = false;
    float fuel;

    public Vector3 movement; //added

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

    [Header("Fuel settings")]
    [SerializeField][Tooltip("time until fuel recharges")] float fuelRechargeTime = 1;
    [SerializeField] float fuelUsageWhenDashing = 1; 
    [SerializeField][Tooltip("How fast fuel recharges")] float fuelRechargePerTick = 1;
    [SerializeField] float maxFuel = 100;
       
    //Input values

    private float horizontalSteerInput;
    private float verticalSteerInput;

    #endregion

    void Awake()
    {       
        fuel = maxFuel;
        autoMoveSpeed = startingAutoMoveSpeed;
        camera = Camera.main;
        body = GetComponentInParent<Rigidbody>();
        pewpew = GetComponentInChildren<Pewpew>();  
        StartCoroutine(FuelRecharger());       
    }
    private void Start() {
        if(onJetpackAwake != null)
        {
            onJetpackAwake(maxFuel);
        }
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
        SetCameraPosition();
        GetDashInput();
        float moveSpeedIncreasePerFrame = Time.deltaTime * autoMoveSpeedIncreaseOverTime;
        IncreaseAutoMoveSpeed(moveSpeedIncreasePerFrame);      
    }

    IEnumerator FuelRecharger()
    {
        bool recharging = false;
        while(true)
        {
            if(useFuel)
            {
                recharging = false;
                yield return new WaitForSeconds(fuelRechargeTime);
                recharging = true;
                useFuel = false;
            }
            if(recharging)
            {
                yield return new WaitForEndOfFrame();
                fuel += fuelRechargePerTick * Time.deltaTime;
                if(fuel >= maxFuel)
                {
                    fuel = maxFuel;
                    recharging = false;
                    fuelEmpty = false;
                }
                if(onFuelUse != null)
                {
                    onFuelUse(fuel);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void IncreaseAutoMoveSpeed(float amount)
    {
        autoMoveSpeed += amount;
        if(autoMoveSpeed >= maxAutoMoveSpeed)
        {
            autoMoveSpeed = maxAutoMoveSpeed;
        }
    }

    void GetDashInput()
    {   
        if(fuelEmpty) return;
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

    void UseFuel(float amount)
    {
        fuel -= amount;
        if(fuel <= 0)
        {
            fuel = 0;
            fuelEmpty = true;
        }
        if(onFuelUse != null)
        {
            onFuelUse(fuel);
        }
    }

    IEnumerator DashInDirection(DashDirections directions)
    {
        UseFuel(fuelUsageWhenDashing);
        useFuel = true;
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

       void Move()
    {

        movement.z = autoMoveSpeed;
        movement.x = horizontalSteerInput * moveSpeed; 
     
        body.velocity = movement;
    }

    void SetCameraPosition()
    {
        camera.transform.rotation = Quaternion.Euler(cameraRotation);
        camera.transform.position = transform.position + cameraOffsetFromPlayer;
    }

    void OnPlayerDeath()
    {
        gameOver = true;
        body.useGravity = true;
        body.freezeRotation = false;
        StopAllCoroutines();
    }

    private void OnEnable() {
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable() {
        StopAllCoroutines();
        PlayerHealth.onPlayerDeath -= OnPlayerDeath;
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
