using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pewpew : MonoBehaviour
{
    //TODO
    //add effects to pewpew?
    bool canShoot = true;
    [SerializeField] float fireRate = 1;
    [SerializeField] GameObject barrelFront;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float offsetBetweenBarrelAndProjectile = 1; // any better names available?

    private void Update() {
        Shoot();
    }

    void Shoot()
    {
        if(Input.GetButtonDown("Pewpew"))
        {
            Vector3 offset = new Vector3();
            offset.z = offsetBetweenBarrelAndProjectile;
            Instantiate(projectilePrefab, barrelFront.transform.position + offset, Quaternion.identity);
            canShoot = false;
            StartCoroutine(WeaponCoolingDown());
        }

        IEnumerator WeaponCoolingDown()
        {
            yield return new WaitForSeconds(fireRate);

        }
    }
}
