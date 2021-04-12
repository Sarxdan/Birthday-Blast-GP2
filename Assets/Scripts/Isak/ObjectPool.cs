using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //When a pooled object has been used, remember to inactivate it in order to "repool" it
    GameObject[] pool;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountOfObjects;
    // Start is called before the first frame update
    private void Awake() {
        pool = new GameObject[amountOfObjects];
        fillPool();
    }

    void fillPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            print("test");
            pool[i] = Instantiate(objectToPool, transform.position, Quaternion.identity);
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

    // Update is called once per frame
    void Update()
    {
    }
}
