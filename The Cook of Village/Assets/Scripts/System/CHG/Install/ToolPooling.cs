using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ToolPooling : MultipleObjectPooling<CookingTool>
{
    //private Dictionary<String, List<PoolObjectData>> pooledObjects = new Dictionary<String, List<PoolObjectData>>();
    public Transform[] toolPosition;
    public InstallMode installMode;
    

    //[HideInInspector] 
    public int indexToChange;

    private String selectedToolName;
    public String SelectedToolName
    {
        get
        {
            return selectedToolName;
        }
        set
        {
            selectedToolName = value;
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

    private static ToolPooling instance;
    public static ToolPooling Instance
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

    private void Start()
    {
        
    }
    
    public void FalseDirect()
    {
        installMode.isDirectChange = false;
    }


    


    
    
    

}
