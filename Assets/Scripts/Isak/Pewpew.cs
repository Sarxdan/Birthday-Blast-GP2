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
    [SerializeField][Range(1, 100)] float projectileSpeed = 1;
    [SerializeField][Range(10, 100)] int maxAmmo = 1;
    [SerializeField] GameObject barrelFront;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileMaxRange = 1;
    [SerializeField][Range(0.01f, 1)] float offsetBetweenBarrelAndProjectile = 1; // any better names available?
    [SerializeField] ObjectPool projectilePool;

    private void Awake() {
        ammo = maxAmmo;
    }

    private void Update() {
        Shoot();
    }

    void Shoot()
    {
        if(!canShoot || ammo <= 0) return;
        if(Input.GetButtonDown("Pewpew"))
        {
            SetUpProjectile();
            canShoot = false;
            ammo--;
            StartCoroutine(WeaponCoolingDown());
        }

        IEnumerator WeaponCoolingDown()
        {
            yield return new WaitForSeconds(fireRate);
            canShoot = true;
        }
    }

    private void SetUpProjectile()
    {
        Vector3 projectileSpawn = barrelFront.transform.position;
        projectileSpawn.z += offsetBetweenBarrelAndProjectile;
        Projectile projectile = projectilePool.usePooledObject().GetComponent<Projectile>();
        projectile.ProjectileSpeed = projectileSpeed + jetpackSpeed;
        projectile.MaxBulletRange = projectileMaxRange;
        projectile.gameObject.transform.position = projectileSpawn;
        projectile.gameObject.SetActive(true);
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
