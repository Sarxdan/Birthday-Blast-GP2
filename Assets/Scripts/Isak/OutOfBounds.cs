using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class OutOfBounds : MonoBehaviour
{       

    float enableMovementTimer = 0.1f;
    new Collider collider;
    Vector3[] lastPlayerPositionsOnLand = new Vector3[3];
    ThirdPersonController thirdPersonController;
    PlayerMovement playerMovement;
    float startTimer = 0.5f;
    float timer;
    int index = 0;

    private void Awake() {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        timer = startTimer;
        
    }

    public void PlayerAwake()
    {
        thirdPersonController = FindObjectOfType<ThirdPersonController>();        
        playerMovement = FindObjectOfType<PlayerMovement>();        
        lastPlayerPositionsOnLand[0] = thirdPersonController.transform.position; // in case the player INSTANTLY jumps off the island
    }
    private void Update() {
        if(playerMovement == null) return;
        if(playerMovement.isGrounded)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                lastPlayerPositionsOnLand[index] = thirdPersonController.gameObject.transform.position;
                timer = startTimer;
                index++;
                if(index >= lastPlayerPositionsOnLand.Length)
                {
                    index = 0;
                }
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
            int lastIndex = lastPlayerPositionsOnLand.Length - 1;
            StartCoroutine(EnablePlayerAfterSeconds());
            thirdPersonController.disablePlayerMovement = true;
            other.transform.position = lastPlayerPositionsOnLand[lastIndex];           
        }       
    }

}
