using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackBase : MonoBehaviour
{
    public static Events.FuelEvent onFuelUse;
    public static Events.FuelEvent onJetpackAwake;

    protected enum DashDirections
    {
        None,  
        Left,
        Right,
        Forward
    }
    protected DashDirections dashDirections = DashDirections.None;
    protected Rigidbody body;
    protected CharacterController controller;
    protected Material material;
    protected bool useFuel = false; //is the player currently using fuel?
    protected float fuel = 0;
    protected float fuelPercentage; //how much fuel is left in %
    protected bool invulnerable = false;
    protected Vector3 movement; 
    protected bool overCharged = false; //did the jetpack overheat?
    protected bool dashOnCooldown = false;
    protected bool rightAxisPushed = false; //check if player wants to dash right
    protected bool leftAxisPushed = false; //check if player wants to dash left
    protected bool forwardAxisPushed = false; //check if player wants to dash forward
    protected float lastKeyPressTime = 0; //last time player pushed a direction key, counted in time.time  
    protected float cooldown = 0; //cooldown of dash ability, resets when dashing
    protected float dashTimeLeft = 0; //remaining dash length
    protected float fuelRechargeTime = 1;
    protected int fuelUsage = 1; 
    protected int fuelRechargePerTick = 1;
    protected int maxFuel = 10;

    [Header("Ability settings")]
    [SerializeField]protected float dashSpeed = 10;
    [SerializeField]protected float dashTime = 0.2f;
    [SerializeField]protected float dashCooldown = 1;  
    [SerializeField][Tooltip("timer for using the dash ability")]protected float doubleTapTimer = 1;

    [Header("bool settings")]
    [SerializeField] protected bool dashUnlocked = false;

    [Header("Particle effects")]
    [SerializeField]protected ParticleSystem[] smokeFX;
    [SerializeField]protected ParticleSystem[] fireStreams;
    [SerializeField]protected ParticleSystem dashEffect;

    
    protected virtual void Awake() {
        
        body = GetComponentInParent<Rigidbody>();
        controller = GetComponentInParent<CharacterController>();
        material = GetComponentInChildren<Renderer>().material;
        StartCoroutine(FuelRecharger());

        Inventory.instance.onUpgradeApplied += ApplyUpgradeStats;
    }
    protected virtual void Start()
    {
        FuelSetup();
        if (onJetpackAwake != null)
        {
            onJetpackAwake(fuel);
        }

        ApplyUpgradeStats();
    }

    private void FuelSetup()
    {
        maxFuel = PlayerManager.instance.maxFuel;
        fuelRechargePerTick = PlayerManager.instance.fuelRechargePerTick;
        fuelRechargeTime = PlayerManager.instance.fuelRechargeTime;
        fuelUsage = PlayerManager.instance.fuelUsage;
        fuel = maxFuel;
    }

    protected void ToggleDashAnimation(bool toggle)
    {
        if(dashEffect == null) return;
        if(toggle) dashEffect.Play();        
        else
        {
            dashEffect.Stop();
        }           
    }

    protected void ToggleBoosterAnimation(bool toggle)
    {
        if(fireStreams == null) return;
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

    protected virtual void Update() {
        GetDashInput();
    }

    protected void ApplyUpgradeStats()
    {
        fuelRechargeTime = Inventory.instance.fuelRechargeTime;
    }

    protected virtual void ResetAxisBools()
    {
        rightAxisPushed = false;
        leftAxisPushed = false;
        forwardAxisPushed = false;
        forwDashInputAmount = 0;
    }

    protected virtual void GetDashInput()
    {        
        if(Time.time - lastKeyPressTime > doubleTapTimer)
        {
            ResetAxisBools();
        } 
        if(forwDashInputAmount >= 2)
        {
            dashDirections = DashDirections.Forward;    
            ResetAxisBools();
            StartCoroutine(DashInDirection(dashDirections));
        }
    }

    public int forwDashInputAmount = 0;
    public void OnForwardDashInput()
    {
        forwDashInputAmount++;
        lastKeyPressTime = Time.time;
    }

    protected virtual IEnumerator DashInDirection(DashDirections directions)
    {
        UseFuel(fuelUsage);
        dashTimeLeft = dashTime;
        AudioManager.instance.Play("JetpackDash");
        //-------------------------------create temporary variables
        StartCoroutine(DashCooldown());
        //---------------------------------start the dash ability
        yield return new WaitForEndOfFrame();
    }

    IEnumerator DashCooldown()
    {
        dashOnCooldown = true;
        cooldown = dashCooldown;
        while(cooldown > 0)
        {
            yield return new WaitForEndOfFrame();
            cooldown -= Time.deltaTime;
        }
        dashOnCooldown = false;
    }

    IEnumerator FuelRecharger() 
    {
        bool recharging = false;
        float rechargeTimeLeft = 0;
        while(true)
        {
            if(useFuel) //if player just used fuel, then reset recharge timer
            {
                rechargeTimeLeft = fuelRechargeTime;
                yield return new WaitForEndOfFrame();
                useFuel = false;
                recharging = false;              
            }
            else if(rechargeTimeLeft > 0) //if player did not use fuel again, then start counting down the recharge time
            {
                yield return new WaitForEndOfFrame();
                rechargeTimeLeft -= Time.deltaTime;          
            }
            else if(rechargeTimeLeft <= 0)
            {
                recharging = true;
            }
            if(recharging)
            {
                yield return new WaitForEndOfFrame();
                fuel += fuelRechargePerTick * Time.deltaTime;
                
                // Fuel overheating
                fuelPercentage = 1 - (fuel / maxFuel);
                material.SetFloat("_overheating", fuelPercentage);
                if (fuelPercentage < .65f && smokeFX[0].isPlaying)
                {
                    foreach (ParticleSystem smoke in smokeFX)
                    {
                        smoke.Stop();
                    }
                }
                
                if(fuel >= maxFuel)
                {
                    fuel = maxFuel;
                    recharging = false;
                    overCharged = false;
                }
                if(onFuelUse != null)
                {
                    onFuelUse(fuel);
                }
                
                
            }
            yield return new WaitForEndOfFrame();
        }
    }

    protected void UseFuel(int amount)
    {
        useFuel = true;
        fuel -= amount;
        
        // Fuel overheating
        fuelPercentage = 1 - (fuel / maxFuel);
        material.SetFloat("_overheating", fuelPercentage);
        if (fuelPercentage >= .65f && !smokeFX[0].isPlaying)
        {
            foreach (ParticleSystem smoke in smokeFX)
            {
                smoke.Play();
            }
        }
        
        if(fuel <= 0)
        {
            fuel = 0;
            overCharged = true;
        }
        if(onFuelUse != null)
        {
            onFuelUse(fuel);
        }
    }
}
