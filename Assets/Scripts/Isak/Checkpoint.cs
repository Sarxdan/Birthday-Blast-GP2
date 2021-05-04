using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{

    public static Events.PositionEvent onCheckPointTriggered;
    Vector3 position;
    new Collider collider;
    [SerializeField] GameObject playerSpawn;
    // Start is called before the first frame update
    private void Awake() {       
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        if(playerSpawn == null)
        {
            Debug.LogError("No spawnpoint found in " + name + ", please create one now");
        }
        position = playerSpawn.transform.position;
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
