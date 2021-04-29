using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreasureChest : MonoBehaviour
{
    public PickUp[] items;
    private List<PickUp> spawnedItems = new List<PickUp>();

    public bool opened;

    private void Start()
    {
        foreach (var pickUp in items)
        {
            var newItem = Instantiate(pickUp, transform.position, Quaternion.identity);
            spawnedItems.Add(newItem);
            newItem.gameObject.SetActive(false);
        }
    }

    public void OpenChest()
    {
        if (opened) return;
        
        
        SpawnItems();
        opened = true;

        GetComponent<Interactable>().enabled = false;
    }


    private void SpawnItems()
    {
        foreach (var item in spawnedItems)
        {
            item.gameObject.SetActive(true);
            var newSpawn = new Vector3(Random.Range(-0.5f,0.5f), 0, Random.Range(-0.5f,0.5f));
            item.transform.position = transform.position + transform.forward + newSpawn;
            item.gameObject.SetActive(true);
        }
    }
}
