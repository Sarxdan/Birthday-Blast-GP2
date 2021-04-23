using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureJetpack : Fuel //make script check if jetpack is unlocked
{
    [SerializeField] float jumpHeight = 1;
    PlayerMovement playerMovement;
    CharacterController controller;
    Renderer[] jetpack;

    // Start is called before the first frame update
    protected override void Awake() {
        base.Awake();
        playerMovement = GetComponentInParent<PlayerMovement>();
        controller = GetComponentInParent<CharacterController>();
        jetpack = GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in jetpack) 
        {
            renderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        foreach(Renderer renderer in jetpack) 
        {
            renderer.enabled = Gamemanager.instance.UnlockedItems.jetpack;
        }
        if(!Gamemanager.instance.UnlockedItems.jetpack) return;
        if(playerMovement == null) return;
        if(Input.GetButtonDown("Jump") && !playerMovement.isGrounded && !overCharged) //player is jumping in air
        {            
            playerMovement.velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerMovement.gravity);
            UseFuel(fuelUsage);
        }
    }
}
