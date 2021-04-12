using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    float projectileSpeed = 1;
    Rigidbody body;

    public float ProjectileSpeed
    {
        set {projectileSpeed = value;}
    }

    // Start is called before the first frame update
    private void Awake() {
        body = GetComponent<Rigidbody>();
    }
    private void Start() {
        body.AddForce(Vector3.forward * projectileSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
