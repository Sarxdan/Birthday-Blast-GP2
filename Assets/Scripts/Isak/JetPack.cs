using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{

    enum HealthStates
    {
        Healthy,
        Mild,
        Moderate, 
        Severe
    }
    enum DashDirections
    {
        None,
        Left,
        Right
    }

    #region variables

    Player player;
    Rigidbody body;  
    Camera camera;
    public float health;
    bool isAutoBoosting = false;
    float fuel = 0;
    public float dashlength;
    Animator animator;
    [SerializeField] HealthStates healthState = HealthStates.Healthy;
    [SerializeField] DashDirections dashDirections = DashDirections.None;
    public bool dashOnCooldown = false;

    [Header("Jetpack settings")]
    [SerializeField] float moveSpeed = 1;   
    [SerializeField][Range(0.1f, 2)] float flightBoost = 1;
    [SerializeField] float autoMoveSpeed = 1;
    [SerializeField] bool useGravity = false;
    [SerializeField] bool isJetpacking = false;
    [SerializeField][Range(20, 40)] float autoBoost = 20;
    [SerializeField] bool allowMovement = true;

    [SerializeField] ParticleSystem[] thrusters;   

    [Header("Health settings")]
    [SerializeField][Range (3, 10)] float startingHealth = 3;

    [Header("Camera settings")]
    [SerializeField] Vector3 cameraRotation = new Vector3(0,0,0);
    [SerializeField] Vector3 cameraOffsetFromPlayer = new Vector3(0,0,0);
   
    [Header("Detoriate settings")]
    [SerializeField][Range(1, 10)] float minTime = 1;
    [SerializeField][Range(1, 10)] float maxTime = 1;
    [Tooltip("In percent")][SerializeField][Range(0.01f, 0.2f)] float healthLostPerTick = 0.1f;
    [Tooltip("Minimum health after detorioration")][SerializeField][Range(0.1f, 10)] float healthDetoriorateLimit = 1;

    [Header("Fuel settings")]
    [SerializeField][Range(100, 1000)] float startingFuel = 100;
    [SerializeField][Range(0.1f, 2)] float fuelUsageOnMovement = 0.1f;
    [SerializeField][Range(2, 5)] float fuelUsageOnAbilities = 2;
    [SerializeField] bool useFuel = false;
    [SerializeField][Range(0.1f, 0.5f)] float passiveFuelUsage = 0.1f;
    [SerializeField][Range(1, 5)] float passiveFuelUsageTimer = 1;

    [Header("Ability settings")]
    [SerializeField][Range(1, 100)] float dashSpeed = 10;
    [SerializeField][Range(0.1f, 1)] float DashLength = 0.2f;
    [SerializeField][Range(1, 10)] float dashCooldown = 1;
    [SerializeField] bool dashUnlocked = false;
    [SerializeField] bool useDashAbility = false;


    #endregion

    void Awake()
    {
        dashlength = DashLength;
        health = startingHealth;
        camera = Camera.main;
        fuel = startingFuel;
        player = GetComponent<Player>();
        body = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("DashAbility"))
        {
            useDashAbility = !useDashAbility;
        }
        if(!isJetpacking) return;
        Move();
        Animate();
        if(useDashAbility)
        {
            GetDashInput();  
        }     
        UseFuel();
        SetCameraPosition();
    }

    void GetDashInput()
    {
        if(!dashUnlocked || dashOnCooldown) return;
        if(Input.GetAxis("Horizontal") < 0)
        {
            dashDirections = DashDirections.Left;
            dashOnCooldown = true;
            StartCoroutine(DashInDirection(dashDirections));
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            dashDirections = DashDirections.Right;
            dashOnCooldown = true;
            StartCoroutine(DashInDirection(dashDirections));
        }
    }
    IEnumerator DashInDirection(DashDirections directions)
    {
        //-------------------------------create temporary variables
        Vector3 movement = new Vector3();
        float cooldown = dashCooldown;
        //---------------------------------start the dash ability
        while(dashlength > 0)
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
            dashlength -= Time.deltaTime;
            cooldown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //-------------------------------------------count the remaining cooldown after dashing
        while(cooldown > 0)
        {
            yield return new WaitForEndOfFrame();
            cooldown -= Time.deltaTime;
        }
        dashlength = DashLength;
        dashOnCooldown = false;
        useDashAbility = false;
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
            fuel -= fuelUsageOnMovement * Time.deltaTime;
        }
    }

    void RefillFuel(float amount) //--------------------needs a way to stop refuelling when at max fuel so the player wont waste resources
    {
        fuel += amount;
        if(fuel >= startingFuel)
        {
            fuel = startingFuel;
        }
    }

    public void AutoBoost() //change to use events?
    {
        //----------------------------------------------------------makes the player boost upwards, useful to make sure the player does not fall too far
        isAutoBoosting = true;   
        Vector3 boostMovement = new Vector3();
        boostMovement.y = autoBoost * flightBoost;  
        body.velocity = boostMovement;  
        isAutoBoosting = false;
    }

    void Move()
    {
        if(isAutoBoosting || fuel <= 0 || !allowMovement) return;
        float fuelPercentage = fuel/startingFuel;
        //----------------------------------------------------get all movement inputs
        Vector3 movement = new Vector3();
        movement.z = autoMoveSpeed;
        movement.x = (Input.GetAxis("Horizontal") * fuelPercentage) * moveSpeed; 
        movement.y = (Input.GetAxis("Jump") * fuelPercentage) * flightBoost;

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

    IEnumerator DamageOverTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            if(health > healthDetoriorateLimit)
            {
                TakeDamage(health * healthLostPerTick); 
            }                     
        }
    }

    void TakeDamage(float damage) // can be better, MUCH better
    {
        health -= damage;
        if(health < startingHealth)
        {
            healthState = HealthStates.Mild;
            if(health < startingHealth/2)
            {
                healthState = HealthStates.Moderate;
                if(health < startingHealth/4)
                {
                    healthState = HealthStates.Severe;
                }
            }
        }
        if(health <= 0)
        {
            JetpackDestroyed();
        }
    }  

    void JetpackDestroyed()
    {
        Destroy(gameObject);
    }
    void RepairJetpack(float amount) //same as TakeDamage()
    {
        health += amount;
        if(health > startingHealth/4)
        {
            healthState = HealthStates.Moderate;
            if(health > startingHealth/2)
            {
                healthState = HealthStates.Moderate;
                if(health < startingHealth)
                {
                    healthState = HealthStates.Mild;
                }
            }
        }
        if(health >= startingHealth)
        {
            health = startingHealth;
            healthState = HealthStates.Healthy;
        }
    }

    void SetCameraPosition()
    {
        camera.transform.rotation = Quaternion.Euler(cameraRotation);
        camera.transform.position = transform.position + cameraOffsetFromPlayer;
    }
    private void OnDisable() {
        StopAllCoroutines();
    }

    void OnEnable() {
        if(isJetpacking)
        {
            StartCoroutine(DamageOverTime());
        }      
    }
}
