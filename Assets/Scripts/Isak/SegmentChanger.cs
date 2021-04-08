using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentChanger : MonoBehaviour
{
    public static Events.EmptyEvent OnsegmentEvent;
    [SerializeField] float heightBoost = 1;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        Player player = other.GetComponent<Player>();
        //JetPack jetPack = other.GetComponentInChildren<JetPack>();
        if(player != null)
        {
             if(OnsegmentEvent != null)
             {
                 OnsegmentEvent();
             }
        }
        //else if(jetPack != null)
        //{
            //jetPack.StopJetpacking();
            //player.StopFlight();
            //gameObject.SetActive(false);
        //}
    }
}
