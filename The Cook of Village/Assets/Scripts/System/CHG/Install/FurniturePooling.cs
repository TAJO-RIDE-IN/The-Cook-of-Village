using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
 public class InstalledData
 {
     public String name;
     public List<GameObject> pooledObjects = new List<GameObject>();
 }
public class FurniturePooling : MultipleObjectPoolingNo
{
    private int index;
    private void Start()
    {
        furnitureInstallMode._installData = InstallData.Instance;
        furnitureInstallMode._furniturePooling = Instance;
        for (int i = 0; i < ItemData.Instance.ItemType[7].ItemInfos.Count; i++)
        {
            InstalledData.Add(new InstalledData());
            InstalledData[i].name = ItemData.Instance.ItemType[7].ItemInfos[i].Name;
            
        }
        furnitureInstallMode.InstallWhenStart();
        
    }
    public FurnitureInstallMode furnitureInstallMode;

    //[HideInInspector] 
    public int indexToChange;

    private String selectedFurnitureName;

    public int SelectedFurnitureIndex;

    private static FurniturePooling instance;

    public List<InstalledData> InstalledData = new List<InstalledData>();

    public InstalledData FindInstallPoolData(String value)
    {
        int index = InstalledData.FindIndex(data => data.name == value);
        //Debug.Log(index); 
        return InstalledData[index];
    }
    
    public InstalledData FindInstallName(GameObject value)
    {
        for (int i = 0; i < InstalledData.Count; i++)
        {
            for (int j = 0; j < InstalledData[i].pooledObjects.Count; j++)
            {
                if (value == InstalledData[i].pooledObjects[j])
                {
                    Debug.Log(j);
                    return InstalledData[i];
                }
            }
        }
        return null;
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
