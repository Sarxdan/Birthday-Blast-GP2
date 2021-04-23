using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureJetpack : Fuel //make script check if jetpack is unlocked
{
    [SerializeField] float jumpHeight = 1;
    PlayerMovement playerMovement;
    CharacterController controller;
    Renderer[] jetpack;

    public int maxJumpsInAir;
    private int currentJumpCount;

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

        currentJumpCount = maxJumpsInAir;
    }

    // Update is called once per frame
    void Update()
    {        
        foreach(Renderer renderer in jetpack) 
        {
            renderer.enabled = Gamemanager.instance.UnlockedItems.jetpack;
        }

        if (playerMovement.isGrounded)
        {
            currentJumpCount = maxJumpsInAir;
        }
    }


    public void OnJump()
    {
        if (!playerMovement.isGrounded && !overCharged && Gamemanager.instance.UnlockedItems.jetpack && currentJumpCount > 0)
        {
            currentJumpCount--;
            playerMovement.velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerMovement.gravity);
            UseFuel(fuelUsage);
        }
    }
}
