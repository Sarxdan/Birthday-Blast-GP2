using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{

    public static T instance;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if(instance != null) Destroy(gameObject);

        instance = (T)this;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy() {
        if(instance == this) instance = null;
    }
}
