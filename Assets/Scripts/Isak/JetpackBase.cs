using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected bool useFuel = false; //is the player currently using fuel?
    protected float fuel = 0;
    protected bool invulnerable = false;
    protected Vector3 movement; //added
    protected bool overCharged = false; //did the jetpack overheat?
    protected bool dashOnCooldown = false;
    protected bool rightAxisPushed = false; //check if player wants to dash right
    protected bool leftAxisPushed = false; //check if player wants to dash left
    protected bool forwardAxisPushed = false; //check if player wants to dash forward
    protected float lastKeyPressTime = 0; //last time player pushed a direction key, counted in time.time  
    protected float cooldown = 0; //cooldown of dash ability, resets when dashing
    protected float dashTimeLeft = 0; //remaining dash length
    protected IEnumerator dashEnumerator;

    [Header("Fuel settings")]
    [SerializeField][Tooltip("time until fuel recharges")] protected float fuelRechargeTime = 1;
    [SerializeField][Tooltip("fuel used when doing stuff that uses fuel")] protected float fuelUsage = 1; 
    [SerializeField][Tooltip("How fast fuel recharges")] protected float fuelRechargePerTick = 1;
    [SerializeField] protected float maxFuel = 100;

    [Header("Ability settings")]
    [SerializeField]protected float dashSpeed = 10;
    [SerializeField]protected float dashTime = 0.2f;
    [SerializeField]protected float dashCooldown = 1;  
    [SerializeField][Tooltip("timer for using the dash ability")]protected float doubleTapTimer = 1;

    [Header("bool settings")]
    [SerializeField] protected bool dashUnlocked = false;

    // Start is called before the first frame update
    protected virtual void Awake() {
        fuel = maxFuel;
        body = GetComponentInParent<Rigidbody>();
        controller = GetComponentInParent<CharacterController>();
        StartCoroutine(FuelRecharger());       
    }
    protected virtual void Start() {
        if(onJetpackAwake != null)
        {
            onJetpackAwake(fuel);
        }
    }

    protected virtual void Update() {
        GetDashInput();
    }

    protected virtual void ResetAxisBools()
    {
        rightAxisPushed = false;
        leftAxisPushed = false;
        forwardAxisPushed = false;
    }

    protected virtual void GetDashInput()
    {        
        //if(overCharged) return;
        //if(dashOnCooldown) return; //funkar ej med inheritance????
        //if(!dashUnlocked) return;
        if(Time.time - lastKeyPressTime > doubleTapTimer)
        {
            ResetAxisBools();
        } 
        if(Input.GetButtonDown("Vertical"))
        {
            if(Input.GetAxis("Vertical") > 0)
            {
                if(!forwardAxisPushed)
                {
                    lastKeyPressTime = Time.time;
                    forwardAxisPushed = true;              
                }   
                else
                {
                    dashDirections = DashDirections.Forward;    
                    ResetAxisBools();
                    dashEnumerator = DashInDirection(dashDirections);
                    StartCoroutine(dashEnumerator);
                } 
            }
        }
    }

    protected virtual IEnumerator DashInDirection(DashDirections directions)
    {
        UseFuel(fuelUsage);
        dashTimeLeft = dashTime;
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

    protected void UseFuel(float amount)
    {
        useFuel = true;
        fuel -= amount;
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
