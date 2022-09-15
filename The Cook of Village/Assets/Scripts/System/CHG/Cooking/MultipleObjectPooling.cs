using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectPooling<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    public T PoolObject;
    protected static GameObject ObjectContatiner;
    protected Queue<T> poolQueue = new Queue<T>();
    public int objectpoolCount = 8;

    private static MultipleObjectPooling<T> instance;
    public static MultipleObjectPooling<T> Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    protected virtual void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        ObjectContatiner = this.gameObject;
        Initialize(objectpoolCount);
    }
    

    protected virtual T CreateNewObject()
    {
        var newObj = Instantiate(PoolObject, transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    protected virtual void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            poolQueue.Enqueue(CreateNewObject());
        }
    }
    public static T GetObject()
    {
        if (Instance.poolQueue.Count > 0)
        {
            var obj = Instance.poolQueue.Dequeue();
            if (obj.gameObject.activeSelf == false)
            {
                obj.transform.SetParent(ObjectContatiner.transform);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                return GetObject();
            }
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.transform.SetParent(ObjectContatiner.transform);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnObject(T obejct)
    {
        obejct.transform.SetParent(Instance.transform);
        Instance.poolQueue.Enqueue(obejct);
        obejct.gameObject.SetActive(false);
    }
}
