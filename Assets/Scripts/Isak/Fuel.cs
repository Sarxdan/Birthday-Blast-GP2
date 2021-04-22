using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public static Events.FuelEvent onFuelUse;
    public static Events.FuelEvent onJetpackAwake;
    protected bool useFuel = false;
    protected float fuel = 0;
    protected bool overCharged = false;

    [Header("Fuel settings")]
    [SerializeField][Tooltip("time until fuel recharges")] protected float fuelRechargeTime = 1;
    [SerializeField][Tooltip("fuel used when doing stuff that uses fuel")] protected float fuelUsage = 1; 
    [SerializeField][Tooltip("How fast fuel recharges")] protected float fuelRechargePerTick = 1;
    [SerializeField] protected float maxFuel = 100;
    // Start is called before the first frame update
    protected virtual void Awake() {
        fuel = maxFuel;
        StartCoroutine(FuelRecharger());
    }
    protected virtual void Start() {
        if(onJetpackAwake != null)
        {
            onJetpackAwake(fuel);
        }
    }
    IEnumerator FuelRecharger() 
    {
        bool recharging = false;
        float rechargeTimeLeft = 0;
        while(true)
        {
            if(useFuel) //if player just dashed, then reset recharge timer
            {
                rechargeTimeLeft = fuelRechargeTime;
                yield return new WaitForEndOfFrame();
                useFuel = false;
                recharging = false;              
            }
            else if(rechargeTimeLeft > 0) //if player did not dash again, then start counting down the recharge time
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
