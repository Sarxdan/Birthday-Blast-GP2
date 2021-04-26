using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTest : MonoBehaviour
{
    public Vector3 posTest;
    [SerializeField] float distanceBetweenObjects = 10;
    public float projectileSpeed = 1;
    public GameObject player;
    public GameObject projectile;

    public Vector3 playerPosition;
    public Vector3 projectilePosition;

    public float playerSpeed = 3;
    public float timeForPlayerToReachEnemy;
    public float timeForEnemyToReachPlayer;
    public float meetingTime;
    JetPack jetPack;

    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenObjects = player.transform.position.z - projectile.transform.position.z;
        playerPosition = player.gameObject.transform.position;
        projectilePosition = projectile.gameObject.transform.position;
        if(distanceBetweenObjects < 0)
        {
            distanceBetweenObjects = -distanceBetweenObjects;
        }
        playerSpeed = player.GetComponentInChildren<ProjectileTest>().speed; //beräkna med att spelaren rör sig snabbare över tiden?
        projectileSpeed = projectile.GetComponent<ProjectileTest>().speed;
        //jetPack = GetComponent<JetPack>();
        //playerSpeed = GetComponentInParent<PlayerMovement>().movementSpeed;
        timeForPlayerToReachEnemy = distanceBetweenObjects/playerSpeed;
        timeForEnemyToReachPlayer = distanceBetweenObjects/projectileSpeed;
        meetingTime =  distanceBetweenObjects /(playerSpeed + projectileSpeed);
        print(meetingTime);
        Vector3 playerPositionAfterTime = new Vector3();
        float playerSpeedInDeltaTime = playerSpeed * Time.deltaTime; 
        float addedMovementToPlayer = (playerPosition.z * playerSpeedInDeltaTime) * meetingTime;
        playerPositionAfterTime.z +=  playerPosition.z + addedMovementToPlayer;
        //Vector3 obj2PositionAfterTime = obj2Position * meetingTime * obj2Speed;
        print(playerPositionAfterTime);
        //print(obj2PositionAfterTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
