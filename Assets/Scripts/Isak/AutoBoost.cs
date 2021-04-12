using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBoost : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        JetPack jetPack = other.GetComponentInChildren<JetPack>();
        if(jetPack != null)
        {
            jetPack.AutoBoost();
        }
    }
}
