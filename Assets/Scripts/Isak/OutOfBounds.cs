using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class OutOfBounds : MonoBehaviour
{       

    [SerializeField] float enableMovementTimer = 1;
    Collider collider;
    public Vector3 lastPlayerPositionOnLand;
    ThirdPersonController thirdPersonController;
    PlayerMovement playerMovement;
    public float startTimer = 2;
    public float timer;

    private void Awake() {
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        timer = startTimer;
        lastPlayerPositionOnLand = thirdPersonController.transform.position;
    }
    private void Update() {
        if(playerMovement.isGrounded)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                lastPlayerPositionOnLand = thirdPersonController.gameObject.transform.position;
                timer = startTimer;
                print(lastPlayerPositionOnLand);
            }
            
        }
    }

    IEnumerator EnablePlayerAfterSeconds()
    {
        yield return new WaitForSeconds(enableMovementTimer);
        thirdPersonController.disablePlayerMovement = false;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            print("test");
            StartCoroutine(EnablePlayerAfterSeconds());
            thirdPersonController.disablePlayerMovement = true;
            other.transform.position = lastPlayerPositionOnLand;           
        }       
    }

}
