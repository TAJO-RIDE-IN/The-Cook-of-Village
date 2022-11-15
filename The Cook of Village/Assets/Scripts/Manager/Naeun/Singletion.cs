using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletion<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                AddInstace();
            }
            return instance;
        }
    }
    private static void AddInstace()
    {
        GameObject obj = GameObject.Find(typeof(T).Name);
        if (obj == null)
        {
            obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
        }
        else
        {
            instance = obj.GetComponent<T>();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            AddInstace();
        }
        Init();
    }
    protected virtual void Init() {  }
}
