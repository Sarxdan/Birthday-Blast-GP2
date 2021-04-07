using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{


    Player player;
    public Rigidbody body;
    [SerializeField] bool isJetpacking = false;
    public Camera camera;
    [SerializeField] Vector3 cameraRotation = new Vector3(0,0,0);
    [SerializeField] float moveSpeed = 1;
    [SerializeField][Range (3, 10)] int health = 3;

    [SerializeField] Vector3 cameraOffsetFromPlayer = new Vector3(0,0,0);
    
    [SerializeField] float autoMoveSpeed = 1;
    [SerializeField] float detoriorateTime = 1;

    [SerializeField] bool useGravity = false;
    [SerializeField] float flightBoost = 1;

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
        if(useGravity)
        {
            Vector3 jumpForce = new Vector3();
            movement.y = body.velocity.y;       
        }
            movement.y += Input.GetAxis("Jump") * flightBoost;      
            body.velocity = movement;
    }

    public void StartJetpacking()
    {
        isJetpacking = true;
        body.useGravity = useGravity;
        camera.transform.rotation = Quaternion.Euler(cameraRotation);
        camera.transform.position = transform.position + cameraOffsetFromPlayer;
        //StartCoroutine(SketchyMovements());
        //StartCoroutine(DamageOverTime());
    }

    IEnumerator SketchyMovements()
    {
        while(true)
        {
        int chance = health/10;
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
            health--;
            yield return new WaitForSeconds(detoriorateTime);
        }
    }

    public void StopJetpacking()
    {
        StopAllCoroutines();
        body.useGravity = true;
        isJetpacking = false;
    }

    public void TakeDamage()
    {
        health --;
    }
}
