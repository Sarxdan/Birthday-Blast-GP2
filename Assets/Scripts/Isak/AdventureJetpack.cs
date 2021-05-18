using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureJetpack : JetpackBase //make script check if jetpack is unlocked
{
    
    PlayerMovement playerMovement;
    Renderer[] jetpack;  
    public int currentJumpCount;
    bool jetpackUnlocked = false;
    ThirdPersonController player;
    bool isDashing = false;

    [Header("Jetpack boost settings")]
    [SerializeField] float jumpHeight = 1;
    [SerializeField] int maxJumpsInAir;
    [SerializeField] float coyoteTime = 1;

    // Start is called before the first frame update
    protected override void Awake() {
        base.Awake();
        player = GetComponentInParent<ThirdPersonController>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        controller = GetComponentInParent<CharacterController>();
        jetpack = GetComponentsInChildren<Renderer>();
        currentJumpCount = maxJumpsInAir;
    }

    // Update is called once per frame
    protected override void Update()
    {         
        base.Update();
        foreach(Renderer renderer in jetpack) 
        {
            renderer.enabled = Gamemanager.instance.UnlockedItems.jetpack;
        }   

        jetpackUnlocked = Gamemanager.instance.UnlockedItems.jetpack;
        if(!jetpackUnlocked) return;
        
        if (playerMovement.isGrounded && !isDashing)
        {
            currentJumpCount = maxJumpsInAir;
            ToggleBoosterAnimation(false);
        }
    }

    protected override void GetDashInput()
    {
        if(!jetpackUnlocked) return;
        if(overCharged) return;
        if(dashOnCooldown) return;
        if(!dashUnlocked) return;
        base.GetDashInput();
    }

    protected override IEnumerator DashInDirection(DashDirections directions)
    {
        isDashing = true;
        bool groundedStart = playerMovement.isGrounded;
        player.disablePlayerMovement = true;
        
        yield return base.DashInDirection(directions);
        ToggleBoosterAnimation(true);
        ToggleDashAnimation(true);
        while(dashTimeLeft > 0)
        {   
            StopDashingOnJump();       
            switch(directions)
            {
                case DashDirections.Forward:
                if(isDashing)
                {
                    var forwardDirection = FindObjectOfType<CameraController>().forwardLookDir;
                    movement = forwardDirection * dashSpeed;
                    transform.root.rotation = Quaternion.LookRotation(forwardDirection, Vector3.up);
                    controller.Move(movement * Time.deltaTime);
                }
                
                invulnerable = true;
                break;

                default:
                break;
            }
            dashTimeLeft -= Time.deltaTime;     
            yield return new WaitForEndOfFrame();           
        }
        
        ToggleDashAnimation(false);
        invulnerable = false;
        if(!playerMovement.isGrounded && groundedStart)
        {
            
            float coyoteTimeLeft = coyoteTime;
            while(coyoteTimeLeft > 0)
            {
                
                coyoteTimeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            
        }
        ToggleBoosterAnimation(false);
        player.disablePlayerMovement = false;
    }

    
    
    

    void StopDashingOnJump() //function checks if player wants to stop dash early
    {
        if(Input.GetAxis("Jump") > 0) 
        {
            isDashing = false;
            player.disablePlayerMovement = false;
            ToggleDashAnimation(false); //make sure dash effect is stopped with input
            ToggleBoosterAnimation(false); //make sure hover effect is stopped with input
        }       
    }


    public void OnJump()
    {
        if (!playerMovement.isGrounded && !overCharged && Gamemanager.instance.UnlockedItems.jetpack && currentJumpCount > 0 && !player.disablePlayerMovement)
        {
            currentJumpCount--;
            playerMovement.velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerMovement.gravity);
            UseFuel(fuelUsage);
            AudioManager.instance.Play("JetpackJump");
            ToggleBoosterAnimation(true);
        }
        isDashing = false;
        player.disablePlayerMovement = false;
        ToggleDashAnimation(false); //make sure dash effect is stopped with input
        
    }
}
