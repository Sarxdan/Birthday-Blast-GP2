using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureJetpack : Fuel //make script check if jetpack is unlocked
{
    [SerializeField] float jumpHeight = 1;
    PlayerMovement playerMovement;
    CharacterController controller;
    Renderer[] jetpack;
    bool isBoosting = false;

    // Start is called before the first frame update
    protected override void Awake() {
        base.Awake();
        playerMovement = GetComponentInParent<PlayerMovement>();
        controller = GetComponentInParent<CharacterController>();
        jetpack = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !playerMovement.isGrounded && !overCharged) //player is jumping in air
        {
            isBoosting = true;
            foreach(Renderer renderer in jetpack) 
            {
                renderer.enabled = true;
            }
            playerMovement.velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerMovement.gravity);
            UseFuel(fuelUsage);
        }
        else if(playerMovement.isGrounded) //player touches ground
        {
            isBoosting = false;
                foreach(Renderer renderer in jetpack) 
            {
                renderer.enabled = false;
            }
        }
    }
}
