using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Vector3 movementDirection = new Vector3(0, 0, 0);
    [SerializeField][Range(1, 10)] float returnTime = 1;
    Rigidbody body;
    float timeUntilReturn;

    private void Awake() {
        body = GetComponent<Rigidbody>();
        timeUntilReturn = returnTime;
    }
    private void Update() {
        MoveObject();       
    }

    void MoveObject()
    {
        body.velocity = movementDirection;
        timeUntilReturn -= Time.deltaTime;
        if(timeUntilReturn <= 0)
        {
            timeUntilReturn = returnTime;
            movementDirection = -movementDirection;
        }
    }

}
