using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTest : MonoBehaviour
{
    public Vector3 posTest;
    [SerializeField] float distanceBetweenObjects = 10;
    public float obj2Speed = 1;
    public GameObject player;
    public GameObject obj2;

    public Vector3 playerPosition;
    public Vector3 obj2Position;

    public float playerSpeed = 3;
    public float timeForPlayerToReachEnemy;
    public float timeForEnemyToReachPlayer;
    public float meetingTime;
    JetPack jetPack;

    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenObjects = player.transform.position.z - obj2.transform.position.z;
        playerPosition = player.gameObject.transform.position;
        obj2Position = obj2.gameObject.transform.position;
        if(distanceBetweenObjects < 0)
        {
            distanceBetweenObjects = -distanceBetweenObjects;
        }
        playerSpeed = player.GetComponent<ProjectileTest>().speed;
        obj2Speed = obj2.GetComponent<ProjectileTest>().speed;
        //jetPack = GetComponent<JetPack>();
        //playerSpeed = GetComponentInParent<PlayerMovement>().movementSpeed;
        timeForPlayerToReachEnemy = distanceBetweenObjects/playerSpeed;
        timeForEnemyToReachPlayer = distanceBetweenObjects/obj2Speed;
        meetingTime =  distanceBetweenObjects /(playerSpeed + obj2Speed);
        Vector3 playerPositionAfterTime = playerPosition;
        float playerSpeedInDeltaTime = playerSpeed * Time.deltaTime; 
        float addedMovementToPlayer = (playerPosition.z * playerSpeedInDeltaTime) * meetingTime;
        playerPositionAfterTime.z +=  addedMovementToPlayer;
        //Vector3 obj2PositionAfterTime = obj2Position * meetingTime * obj2Speed;
        print(playerPositionAfterTime);
        //print(obj2PositionAfterTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
