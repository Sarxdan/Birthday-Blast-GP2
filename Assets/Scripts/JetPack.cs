using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    #region variables

    Player player;
    Rigidbody body;  
    Camera camera;

    [Header("Jetpack settings")]
    [SerializeField] float moveSpeed = 1;
    [SerializeField][Range (3, 10)] float health = 3;
    [SerializeField][Range(0.1f, 2)] float flightBoost = 1;
    [SerializeField] float autoMoveSpeed = 1;
    [SerializeField] bool useGravity = false;
    [SerializeField] bool isJetpacking = false;
    [Header("Camera settings")]
    [SerializeField] Vector3 cameraRotation = new Vector3(0,0,0);
    [SerializeField] Vector3 cameraOffsetFromPlayer = new Vector3(0,0,0);
   
    [Header("Detoriate settings")]
    [SerializeField][Range(1, 10)] float minTime = 1;
    [SerializeField][Range(1, 10)] float maxTime = 1;
    [Tooltip("In percent")][SerializeField][Range(0.01f, 0.2f)] float healthLostPerTick = 0.1f;
    [Tooltip("Minimum health after detorioration")][SerializeField][Range(0.1f, 10)] float healthDetoriorateLimit = 1;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        camera = Camera.main;
        player = GetComponent<Player>();
        body = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isJetpacking) return;
        Move();
    }

    void Move()
    {
        Vector3 movement = new Vector3();
        movement.z = autoMoveSpeed;
        movement.x = Input.GetAxis("Horizontal") * moveSpeed; 
        movement.y = Input.GetAxis("Jump") * flightBoost;      
        if(useGravity)
        {
            movement.y += body.velocity.y;       
        }
        else
        {
            movement.y *= -Physics.gravity.y;
            print(movement.y);
        }                 
            body.velocity = movement;
    }

    public void StartJetpacking() // onödig funktion?
    {
        isJetpacking = true;
        body.useGravity = useGravity;
        SetCameraPosition();
        //StartCoroutine(SketchyMovements());
        //StartCoroutine(DamageOverTime());
    }

    IEnumerator SketchyMovements()
    {
        while(true)
        {
        float chance = health/10;
        if(Random.value > chance)
        {
            float addedMovement = Random.Range(-20, 20);
            Vector3 addedMovements = new Vector3();
            addedMovements.x = addedMovement;
            body.velocity += addedMovements;
        }
        yield return new WaitForSeconds(1);
        }       
    }
    IEnumerator DamageOverTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            if(health > healthDetoriorateLimit)
            {
                health -= health * healthLostPerTick; 
            }
                      
        }
    }

    void StopJetpacking() //onödig funktion?
    {
        StopAllCoroutines();
        body.useGravity = true;
        isJetpacking = false;
    }

    public void TakeDamage()
    {
        health --;
    }

    void OnsegmentEvent()
    {
        if(isJetpacking)
        {
            StopJetpacking();
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
        SegmentChanger.OnsegmentEvent += OnsegmentEvent; // behövs en segmentchanger?
    }

    private void OnDisable() {
        SegmentChanger.OnsegmentEvent -= OnsegmentEvent;
    }
}
