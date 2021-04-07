using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody body;
    JetPack jetPack;
    Camera camera;

    [SerializeField] Vector3 cameraRotation = new Vector3(0,0,0);
    [SerializeField] Vector3 cameraOffsetFromPlayer = new Vector3(0,0,0);
    [SerializeField][Range (0.3f, 5)] float moveSpeed = 1;
    [SerializeField] bool onIsland = true; // what to call this variable?
    [SerializeField][Range (0.3f, 2)] float jumpSpeed = 1;
    [SerializeField] LayerMask groundLayers;

    public bool OnIsland
    {
        get
        {
            return onIsland;
        }
    }
    [SerializeField] float segmentChangeTimer = 1;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        body = GetComponent<Rigidbody>();
        jetPack = GetComponentInChildren<JetPack>();
        jetPack.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!onIsland) return;
        Move();
    }

    void Move()
    {
        Vector3 movement = new Vector3();
        movement.z = Input.GetAxis("Vertical") * moveSpeed;
        movement.x = Input.GetAxis("Horizontal") * moveSpeed;
        movement.y = body.velocity.y;
        if(isTouchingGround())
        {
            movement.y += Input.GetAxis("Jump") * jumpSpeed;
        }
        body.velocity = movement;
    }

    bool isTouchingGround()
    {   
        return Physics.Raycast(transform.position, Vector3.down, 1, groundLayers); //detta var ett jävla helvete att få till    
    }

    public void StartFlight(float height)
    {               
        StartCoroutine(ChangeToJetPack(height));
    }

    public void StopFlight()
    {
        jetPack.gameObject.SetActive(false);
        onIsland = true;
        camera.transform.position = transform.position + cameraOffsetFromPlayer;
        camera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    private IEnumerator ChangeToJetPack(float height)
    {
        onIsland = false;
        Vector3 flightHeight = new Vector3();
        flightHeight.y = height;
        body.velocity = flightHeight;
        yield return new WaitForSeconds(segmentChangeTimer); 
        jetPack.gameObject.SetActive(true);
        jetPack.StartJetpacking();
    }
}
