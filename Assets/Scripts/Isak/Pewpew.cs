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
    [SerializeField][Range(0.1f, 1)] float fireRate = 1;
    [SerializeField][Range(1, 100)] float projectileSpeed = 1;
    [SerializeField][Range(10, 100)] int ammo = 1;
    [SerializeField] GameObject barrelFront;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField][Range(0.01f, 1)] float offsetBetweenBarrelAndProjectile = 1; // any better names available?

    private void Update() {
        Shoot();
    }

    void Shoot()
    {
        if(!canShoot || ammo <= 0) return;
        if(Input.GetButtonDown("Pewpew"))
        {
            Vector3 offset = new Vector3();
            offset.z = offsetBetweenBarrelAndProjectile;
            Projectile projectile = Instantiate(projectilePrefab, barrelFront.transform.position + offset, Quaternion.identity).GetComponent<Projectile>();
            projectile.ProjectileSpeed = projectileSpeed + jetpackSpeed;
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
}
