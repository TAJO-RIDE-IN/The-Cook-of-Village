using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class ToolPooling : MultipleObjectPooling<CookingTool>
{
    //private Dictionary<String, List<PoolObjectData>> pooledObjects = new Dictionary<String, List<PoolObjectData>>();
    public Transform[] toolPosition;
    

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
        
    }

    private void Start()
    {
        selectedPositionIndex = -1;
    }

    public void ReceiveToolName(String name)//조리도구 사용한다에 할당
    {
        SelectedToolName = name;
        //StartInstall();
    }

    


    
    
    

}
