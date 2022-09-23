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
        public int initCount;
    }
    public List<PoolObjectData> poolObjectData;
    public PoolObjectData FindPoolObjectData(String value)
    {
        int index = poolObjectData.FindIndex(data => data.name == value);
        return poolObjectData[index];
    }
    
    //protected static GameObject[] ObjectContatiner;

    
    protected virtual void Awake()
    {
        Initialize();
    }
    

    protected virtual T CreateNewObject(string value)
    {
        var newObj = Instantiate(FindPoolObjectData(value).poolObject, FindPoolObjectData(value).objectContatiner.transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    protected virtual void Initialize()
    {
        for (int i = 0; i < poolObjectData.Count; i++)
        {
            for (int j = 0; j < poolObjectData[i].initCount; j++)
            {
                poolObjectData[i].objectQueue.Enqueue(CreateNewObject(poolObjectData[i].name));
            }
        }
    }
    public T GetObject(string name)
    {
        if (FindPoolObjectData(name).objectQueue.Count > 0)
        {
            var obj = FindPoolObjectData(name).objectQueue.Dequeue();
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
            var newObj = CreateNewObject(name);
            newObj.transform.SetParent(FindPoolObjectData(name).objectContatiner.transform);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public void ReturnObject(T returnObject, String name)
    {
        
        returnObject.transform.SetParent(FindPoolObjectData(name).objectContatiner.transform);
        FindPoolObjectData(name).objectQueue.Enqueue(returnObject);
        returnObject.gameObject.SetActive(false);
    }
}
