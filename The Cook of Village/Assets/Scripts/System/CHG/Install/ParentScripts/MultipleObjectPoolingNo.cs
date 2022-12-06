using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트없는 오브젝트 풀링
/// </summary>
public class MultipleObjectPoolingNo : MonoBehaviour
{
    [Serializable]
    public class PoolObjectData
    {
        public String name;
        public GameObject poolObject;
        public Queue<GameObject> objectQueue = new Queue<GameObject>();
        public GameObject objectContainer;
        public int initCount;
        public List<GameObject> pooledObjects = new List<GameObject>();
    }

    public List<PoolObjectData> poolObjectData;
    public PoolObjectData FindPooledObject(GameObject value)
    {
        for (int i = 0; i < poolObjectData.Count; i++)
        {
            for (int j = 0; j < poolObjectData[i].pooledObjects.Count; j++)
            {
                if (value == poolObjectData[i].pooledObjects[j])
                {
                    return poolObjectData[i];
                }
            }
        }
        return null;
    }
    public PoolObjectData FindPoolObjectData(String value)
    {
        int index = poolObjectData.FindIndex(data => data.name == value);
        //Debug.Log(index); 
        return poolObjectData[index];
    }

    private PoolObjectData _poolObjectData;
    
    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual GameObject CreateNewObject(string value)
    {
        _poolObjectData = FindPoolObjectData(value);
        var newObj = Instantiate(_poolObjectData.poolObject, _poolObjectData.objectContainer.transform);
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

    public GameObject GetObject(string name)
    {
        _poolObjectData = FindPoolObjectData(name);
        if (_poolObjectData.objectQueue.Count > 0)
        {
            var obj = _poolObjectData.objectQueue.Dequeue();
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
            newObj.transform.SetParent(_poolObjectData.objectContainer.transform);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    /// <summary>
    /// return 시 초기화 해줘야 하는 값들...
    /// </summary>
    /// <param name="returnObject"></param>
    /// <param name="name"></param>
    public void ReturnObject(GameObject returnObject, String name)
    {
        _poolObjectData = FindPoolObjectData(name);
        //returnObject.transform.SetParent(_poolObjectData.objectContainer.transform);
        _poolObjectData.objectQueue.Enqueue(returnObject);
        returnObject.gameObject.SetActive(false);
    }
}
