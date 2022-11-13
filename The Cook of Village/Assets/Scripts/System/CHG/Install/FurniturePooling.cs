using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FurniturePooling : MultipleObjectPoolingNo
{
    public Transform[] toolPosition;
    public Transform[] chairPosition;
    public Transform[] decoPosition;
    public ToolInstallMode toolInstallMode;
    

    //[HideInInspector] 
    public int indexToChange;

    private String selectedFurnitureName;
    public String SelectedFurnitureName
    {
        get
        {
            return selectedFurnitureName;
        }
        set
        {
            selectedFurnitureName = value;
        }
    }
    
    private int selectedPositionIndex;
    public int SelectedPositionIndex
    {
        get
        {
            return selectedPositionIndex;
        }
        set
        {
            selectedPositionIndex = value;
            
        }
    }

    public int SelectedToolIndex;

    private static FurniturePooling instance;
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
        selectedPositionIndex = -1;
        
    }
}
