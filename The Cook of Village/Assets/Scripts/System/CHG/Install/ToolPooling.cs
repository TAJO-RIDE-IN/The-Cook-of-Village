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
    

    /// <summary>
    /// 쓰는 이유: 다이렉트 체인지에서 사용. CookingTool의 DirectSetUp에서 바꿀 자리 위치 인덱스를 넘겨줄 수 있는데, 이걸 사용하는 함수인 Use는 나은이 스크립트에서 사용하기 때문이다.
    /// 또한 cookingCharacter.cookingTool.index와 같은 값이다.
    /// </summary>
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
        item.ChangeAmount(infos.ID, count);
    }


    


    
    
    

}
