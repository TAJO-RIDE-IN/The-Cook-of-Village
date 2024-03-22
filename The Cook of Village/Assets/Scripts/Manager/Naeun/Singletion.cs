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
                return null;
            }
            return instance;
        }
    }
    private static void AddInstace()
    {
        var component = FindObjectOfType<T>(); //이게 사용하려면 아예 싱글톤오브젝트 자체를 안만들어놨다가 자동으로 만들게 하려고 넣은건가보네
        if (component == null)
        {
            GameObject obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
        }
        else
        {
            instance = component.GetComponent<T>();
        }
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            AddInstace();
        }
        else
        {
            Destroy(this.gameObject);
        }
        Init();
    }
    protected virtual void Init() {  }
}
