using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
 public class InstallPoolData
 {
     public String name;
     public List<GameObject> pooledObjects = new List<GameObject>();
 }


public class FurniturePooling : MultipleObjectPoolingNo
{
    private void Start()
    {
        for (int i = 0; i < ItemData.Instance.ItemType[7].ItemInfos.Count; i++)
        {
            FurnitureDatas.Add(new InstallPoolData());
            FurnitureDatas[i].name = ItemData.Instance.ItemType[7].ItemInfos[i].Name;
        }
        furnitureInstallMode.InstallWhenStart();
    }
    public FurnitureInstallMode furnitureInstallMode;

    //[HideInInspector] 
    public int indexToChange;

    private String selectedFurnitureName;

    public int SelectedFurnitureIndex;

    private static FurniturePooling instance;

    public List<InstallPoolData> FurnitureDatas = new List<InstallPoolData>();
    
    public InstallPoolData FindInstallPoolData(String value)
    {
        int index = FurnitureDatas.FindIndex(data => data.name == value);
        //Debug.Log(index); 
        return FurnitureDatas[index];
    }

    

    public static FurniturePooling Instance
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
    protected override void Awake()
    {
        base.Awake();
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
    }
}
