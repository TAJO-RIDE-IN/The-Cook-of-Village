using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MultipleObjectPooling<T> : MonoBehaviour where T : MonoBehaviour
{
    [Serializable]
    public class PoolObjectData
    {
        public String name;
        public T poolObject;
        public Queue<T> objectQueue = new Queue<T>();
        public GameObject objectContatiner;
    }
    public List<PoolObjectData> poolObjectData;
    public PoolObjectData FindePoolObjectData(String value)
    {
        int index = poolObjectData.FindIndex(data => data.name == value);
        return poolObjectData[index];
    }
    
    //protected static GameObject[] ObjectContatiner;
    public int initObjectCount = 3;

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
        /*ObjectContatiner = new GameObject[poolObjectData.Count];
        for (int i = 0; i < poolObjectData.Count; i++)
        {
            ObjectContatiner[i] = transform.GetChild(i).gameObject;
        }*/
        Debug.Log(poolObjectData[0].name);
        Initialize(initObjectCount);
        
    }
    

    protected virtual T CreateNewObject(string value)
    {
        var newObj = Instantiate(FindePoolObjectData(value).poolObject, FindePoolObjectData(value).objectContatiner.transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    protected virtual void Initialize(int count)
    {
        for (int i = 0; i < poolObjectData.Count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                poolObjectData[i].objectQueue.Enqueue(CreateNewObject(poolObjectData[i].name));
            }
        }
    }
    public T GetObject(string name)
    {
        if (FindePoolObjectData(name).objectQueue.Count > 0)
        {
            var obj = FindePoolObjectData(name).objectQueue.Dequeue();
            if (obj.gameObject.activeSelf == false)
            {
                //obj.transform.SetParent(ObjectContatiner.transform);이건 처음부터 저 트랜스폼 자식으로 생성하니까 필요없지않나?
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                return GetObject(name);
            }
        }
        else
        {
            var newObj = Instance.CreateNewObject(name);
            newObj.transform.SetParent(FindePoolObjectData(name).objectContatiner.transform);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public void ReturnObject(T returnObject, String name)
    {
        
        returnObject.transform.SetParent(FindePoolObjectData(name).objectContatiner.transform);
        FindePoolObjectData(name).objectQueue.Enqueue(returnObject);
        returnObject.gameObject.SetActive(false);
    }
}
