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

    [Header("Sketchy movement settings")]

    [SerializeField][Range(1, 10)] float timeBetweenSketchyMovements = 1;

    #endregion

    void Awake()
    {
        health = startingHealth;
        camera = Camera.main;
        player = GetComponent<Player>();
        body = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if(!isJetpacking) return;
        Move();
        if(Input.GetButtonDown("Jump"))
        {
            TakeDamage(startingHealth);
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
        if(isAutoBoosting) return;

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

    IEnumerator addSketchyMovements()
    {   
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenSketchyMovements);
            Vector3 sketchyMovements = new Vector3();
            switch(healthState)
            {
                case HealthStates.Healthy: //add no movement
                break;

                case HealthStates.Mild: //add slight movement to one axis
                break;

                case HealthStates.Moderate: //add moderate movement to two axes
                break;

                case HealthStates.Severe: //add severe moveent to three axes
                sketchyMovements.x = Random.Range(1, 10);
                sketchyMovements.y = Random.Range(1, 10);
                sketchyMovements.z = Random.Range(1, 10);
                break;

                default:             
                break;
            }
            transform.Translate(sketchyMovements);
        }        
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
            //StartCoroutine(addSketchyMovements());
            SetCameraPosition();
            StartCoroutine(DamageOverTime());
        }      
    }
}
