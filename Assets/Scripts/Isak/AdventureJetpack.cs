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

    [Header("Particle effects")]
    [SerializeField] ParticleSystem[] fireStreams;
    [SerializeField] ParticleSystem dashEffect;
    [Header("Audio Settings")]
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip jumpSound;
    

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
        
        if (playerMovement.isGrounded)
        {
            currentJumpCount = maxJumpsInAir;
        }
    }

    protected override void GetDashInput()
    {
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
        if(dashSound != null)
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(dashSound);
        }
        ToggleDashAnimation(true);
        while(dashTimeLeft > 0)
        {   
            StopDashingOnJump();       
            switch(directions)
            {
                case DashDirections.Forward:
                if(isDashing)
                {
                    movement = gameObject.transform.parent.transform.forward * dashSpeed;
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
            ToggleHoverAnimation(true);
            float coyoteTimeLeft = coyoteTime;
            while(coyoteTimeLeft > 0)
            {
                
                coyoteTimeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            ToggleHoverAnimation(false);
        }
        player.disablePlayerMovement = false;
        
        //-------------------------------------------count the remaining cooldown after dashing
    }

    

    void ToggleHoverAnimation(bool toggle)
    {
        if(toggle)
        {
            foreach(ParticleSystem fireStream in fireStreams)
            {
                fireStream.Play();
            }
        }
        else
        {
            foreach(ParticleSystem fireStream in fireStreams)
            {
                fireStream.Stop();
            }
        }               
    }

    void ToggleDashAnimation(bool toggle)
    {
        if(toggle)
        {
        dashEffect.Play();
        }
        else
        {
        dashEffect.Stop();
        }               
    }

    void StopDashingOnJump() //function that runs with dash enumerator, checking if player wants to stop dash early
    {
        if(Input.GetAxis("Jump") > 0) // checking for input, add to viktors new input system
        {
            isDashing = false;
            player.disablePlayerMovement = false;
            ToggleDashAnimation(false); //make sure dash effect is stopped with input
            ToggleHoverAnimation(false); //make sure hover effect is stopped with input
        }       
    }


    public void OnJump()
    {
        if (!playerMovement.isGrounded && !overCharged && Gamemanager.instance.UnlockedItems.jetpack && currentJumpCount > 0 && !player.disablePlayerMovement)
        {
            currentJumpCount--;
            playerMovement.velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerMovement.gravity);
            UseFuel(fuelUsage);
            if(jumpSound != null)
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(jumpSound);
        }
        }
    }
}
