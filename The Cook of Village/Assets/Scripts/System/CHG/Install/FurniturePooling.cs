using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FurniturePooling : MultipleObjectPoolingNo
{
    private int index;
    private void Start()
    {
        furnitureInstallMode._installData = InstallData.Instance;
        furnitureInstallMode._furniturePooling = Instance;
        furnitureInstallMode.InstallWhenStart();
        
    }
    public FurnitureInstallMode furnitureInstallMode;


    private String selectedFurnitureName;


    private static FurniturePooling instance;


    /*public InstalledData FindInstallPoolData(String value)
    {
        int index = InstalledData.FindIndex(data => data.name == value);
        //Debug.Log(index); 
        return InstalledData[index];
    }*/
    
    

    

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
