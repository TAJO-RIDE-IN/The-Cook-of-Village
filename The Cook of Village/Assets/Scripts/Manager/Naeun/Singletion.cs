using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletion<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private void Awake()
    {
        if (null == instance)
        {
            instance = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(instance.gameObject);
            Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                return default(T);
            }
            return instance;
        }
    }

    protected virtual void Init()
    {

    }
}
