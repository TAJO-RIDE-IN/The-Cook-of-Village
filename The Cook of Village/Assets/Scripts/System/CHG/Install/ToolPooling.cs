using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ToolPooling : MultipleObjectPooling<CookingTool>
{
    //private Dictionary<String, List<PoolObjectData>> pooledObjects = new Dictionary<String, List<PoolObjectData>>();
    public Transform[] toolPosition;
    public ToolInstallMode toolInstallMode;
    

    //[HideInInspector] 
    public int indexToChange;
    
    public ItemInfos selectedItemInfos;

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

    /// <summary>
    /// 나은이가 저장한 아이템 데이터에서 값을 빼주고 더해주기 위함
    /// </summary>
    public int SelectedToolID;

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
        toolInstallMode.isDirectChange = false;
    }
    public void ChangeToolAmount(int count, FoodTool.Type type)
    {
        ItemData item = ItemData.Instance;
        ItemInfos infos = item.ToolIdToItem((int)type);
        int amount = infos.Amount + count;
        item.ChangeAmount(infos.ID, amount);
    }


    


    
    
    

}
