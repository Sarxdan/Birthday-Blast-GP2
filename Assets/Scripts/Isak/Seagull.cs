using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    // thoughts:
    // add movement direction? (expose to inspector?)
    // add ability to hunt player? ("Difficulty")

    Rigidbody body;
    [Header("movement settings")]
    [SerializeField][Range(1, 10)] float movementSpeed = 1;

    [Header("Combat settings")]
    [SerializeField][Range(1, 10)] float damagePlayerOnHit = 1;

    private void Awake() {
        body = GetComponent<Rigidbody>();
    }
    private void Update() {
        Move();
    }

    void Move()
    {
        Vector3 movement = new Vector3();
        movement.z = movementSpeed;
        body.velocity = movement;
    }
}
