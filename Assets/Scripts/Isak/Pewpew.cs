using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pewpew : MonoBehaviour
{
    //TODO
    //add effects to pewpew?
    //make script use pool instead of constant instantiation
    float jetpackSpeed = 0; //variable to keep track of how fast the jetpack is moving

    public float JetpackSpeed
    {
        set {jetpackSpeed = value;}
    }
    bool canShoot = true;
    int ammo;
    [SerializeField][Range(0.1f, 1)] float fireRate = 1;
    [SerializeField][Range(10, 100)] int maxAmmo = 1;
    [SerializeField] float projectileRadius = 1;
    ParticleSystem effect;

    private void Awake() {
        ammo = maxAmmo;
        effect = GetComponentInChildren<ParticleSystem>();
    }

    public void OnShootInput()
    {
        if (CanShoot())
        {
            GameObject.FindGameObjectWithTag("CharModel").GetComponent<Animator>().SetTrigger("Shoot");
            Shoot();
        }
    }

    bool CanShoot()
    {
        if (Gamemanager.instance.UnlockedItems.pewpew == false) return false;
        
        if(canShoot) return true;
        if(ammo > 0) return true;
        else
        {
            return false;
        }        
    }

    void Shoot()
    {
        ammo --;
        canShoot = false;
        effect.Play();
        AudioManager.instance.Play("Pewpew");
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, projectileRadius, LayerMask.GetMask("Enemy"));
        foreach(Collider collider in colliders)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.FleeFromPlayer(transform.position);
            }
        }
        StartCoroutine(WeaponCoolingDown());
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projectileRadius);       
    }

    IEnumerator WeaponCoolingDown()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void RefillAmmo(int amount) // add restriction so the player wont use resources in vain
    {
        ammo += amount;
        if(ammo >= maxAmmo)
        {
            ammo = maxAmmo;
        }
    }
}
