using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    Rigidbody rigidbody;
    public float speed = 5;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Vector3.forward * speed;
        transform.Translate(movement * Time.deltaTime);
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            print(other.GetComponent<ProjectileTest>().timer);
            print(other.GetComponent<ProjectileTest>().transform.position);
        }        
        Destroy(gameObject);
    }
}
