using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyTest : MonoBehaviour
{   
    public float rotationSpeed;
    public float moveSpeed;
    public float jumpSpeed;
    Rigidbody body;
    public bool isGrounded;
    public float groundDistanceCheck;
    [SerializeField] private Transform groundCheckPosition;
    // Update is called once per frame
    private void Awake() {
        body = GetComponent<Rigidbody>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundDistanceCheck, LayerMask.GetMask("Ground"));
        if(isGrounded)
        {
            body.AddForce((Vector3.up * Input.GetAxis("Jump")) * jumpSpeed, ForceMode.Impulse);
        }
        body.MovePosition(body.position + (transform.forward * (Input.GetAxis("Vertical") * moveSpeed)) * Time.deltaTime);
        Vector3 horizontalInput = new Vector3(0,Input.GetAxis("Horizontal"),0);
        
        Vector3 currentPosition = body.transform.position;
        horizontalInput += currentPosition;
        Quaternion targetRotation = Quaternion.LookRotation(horizontalInput - currentPosition);
        float addedZRotation = 0;
        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        }
        
    }
}
