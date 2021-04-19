using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{

    public static Events.PositionEvent onCheckPointTriggered;
    Vector3 position;
    Collider collider;
    // Start is called before the first frame update
    private void Awake() {
        position = transform.position;
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            if(onCheckPointTriggered != null)
            {
                onCheckPointTriggered(position);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
