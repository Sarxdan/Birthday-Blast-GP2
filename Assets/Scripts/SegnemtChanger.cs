using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegnemtChanger : MonoBehaviour
{

    [SerializeField] float heightBoost = 1;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        Player player = other.GetComponent<Player>();
        JetPack jetPack = other.GetComponentInChildren<JetPack>();
        if(player != null && player.OnIsland)
        {
             player.StartFlight(heightBoost);
             gameObject.SetActive(false);
        }
        else if(jetPack != null)
        {
            jetPack.StopJetpacking();
            player.StopFlight();
            gameObject.SetActive(false);
        }
    }
}
