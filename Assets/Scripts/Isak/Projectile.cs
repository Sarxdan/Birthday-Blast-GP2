using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    float projectileSpeed = 1;
    float maxBulletRange = 1;
    float bulletRangeLeft;
    public float MaxBulletRange
    {
        set{maxBulletRange = value;}
    }
    Rigidbody body;

    public float ProjectileSpeed
    {
        set {projectileSpeed = value;}
    }

    // Start is called before the first frame update
    private void Awake() {
        body = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject); //do something else other than destroying?
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        bulletRangeLeft = maxBulletRange;
        body.AddForce(Vector3.forward * projectileSpeed, ForceMode.Impulse);
        StartCoroutine(BulletRange());
    }

    IEnumerator BulletRange()
    {
        while(gameObject.activeSelf)
        {
            yield return new WaitForEndOfFrame();
            bulletRangeLeft -= Time.deltaTime;
            if(bulletRangeLeft <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
