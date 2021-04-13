using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //When a pooled object has been used, remember to inactivate it in order to "repool" it
    GameObject[] pool;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountOfObjects;

    private void Awake() {
        pool = new GameObject[amountOfObjects];
        fillPool();
    }

    void fillPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(objectToPool, transform);
            pool[i].SetActive(false);

        }
    }

    public GameObject usePooledObject()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(!pool[i].activeSelf)
            {
                return pool[i];
            }
        }
        return null; //all objects are in use
    }
}
