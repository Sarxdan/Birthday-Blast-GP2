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

    #region variables

    Player player;
    Rigidbody body;  
    Camera camera;
    public float health;
    bool isAutoBoosting = false;
    float fuel = 0;
    Animator animator;
    [SerializeField] HealthStates healthState = HealthStates.Healthy;

    [Header("Jetpack settings")]
    [SerializeField] float moveSpeed = 1;   
    [SerializeField][Range(0.1f, 2)] float flightBoost = 1;
    [SerializeField] float autoMoveSpeed = 1;
    [SerializeField] bool useGravity = false;
    [SerializeField] bool isJetpacking = false;
    [SerializeField][Range(20, 40)] float autoBoost = 20;
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


    #endregion

    void Awake()
    {
        health = startingHealth;
        camera = Camera.main;
        fuel = startingFuel;
        player = GetComponent<Player>();
        body = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!isJetpacking) return;
        Move();
        Animate();
        if(useFuel)
        {
        UseFuel();
        }
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
        if(isAutoBoosting || fuel <= 0) return;
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

    void OnEnable() {
        if(isJetpacking)
        {
            SetCameraPosition();
            StartCoroutine(DamageOverTime());
        }      
    }
}
